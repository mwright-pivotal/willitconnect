using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using wic4windows.Controllers;
using wic4windows.Model;

namespace wic4windows.Service
{
    public class EntryChecker
    {
        private readonly ILogger<WillitConnectV2Controller> _logger;

        public EntryChecker(ILogger<WillitConnectV2Controller> logger)
        {
            _logger = logger;
        }
        public CheckedEntry check(CheckedEntry e)
        {
            if (e.isValidUrl())
            {
                checkUrl(e);
            }
            else if (e.isValidHostname())
            {
                checkHostname(e);
            }
            else
            {
                _logger.LogWarning(e.Entry + " is not a valid hostname");
            }
            return e;
        }

        public void checkUrl(CheckedEntry e)
        {
            IPAddress IP;
            IPAddress[] IPs = Dns.GetHostAddresses(getHostname(e));
            if (IPAddress.TryParse("127.0.0.1", out IP))
            {
                Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

                try
                {
                    s.Connect(IPs[0], getPort(e,getHostname(e)));
                }
                catch (Exception ex)
                {
                    e.CanConnect = false;
                    _logger.LogWarning(ex.ToString());
                }
                e.CanConnect = true;
            }
        }

        private void checkHostname(CheckedEntry e)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(getHostname(e));
            Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            try
            {
                s.Connect(IPs[0], getPort(e, getHostname(e)));
                e.CanConnect = true;
            }
            catch (Exception ex)
            {
                e.CanConnect = false;
                _logger.LogWarning(ex.ToString());
            }
        }
        private int getPort(CheckedEntry e, String hostname)
        {
            int Port=0;
            int.TryParse(e.Entry.Substring(hostname.Length+1, e.Entry.Length-(hostname.Length+1)), out Port);
            return Port;
        }

        private String getHostname(CheckedEntry e)
        {
            char[] delimiterChars = { ':' };
            return e.Entry.Split(delimiterChars)[0];
        }
    }
}
