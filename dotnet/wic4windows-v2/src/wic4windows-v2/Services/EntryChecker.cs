using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using wic4windows.Model;
using wic4windows_v2.Controllers;

namespace wic4windows_v2.Services
{
    public class EntryChecker
    {
        private readonly ILogger<WillitConnectController> _logger;

        public EntryChecker(ILogger<WillitConnectController> logger)
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
            e.LastChecked = DateTime.Now;
            return e;
        }

        public void checkUrl(CheckedEntry e)
        {
            
            HttpWebRequest req = HttpWebRequest.CreateHttp(e.Entry);
            Task<WebResponse> res = req.GetResponseAsync();

            try
            {
                res.Wait(30000);
                HttpStatusCode code = ((HttpWebResponse)res.Result).StatusCode;
                e.HttpStatus = (int)code;
                e.CanConnect = true;
            } catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                e.CanConnect = false;
            }
        }

        private void checkHostname(CheckedEntry e)
        {
            Task<IPAddress[]> ipsTask = System.Net.Dns.GetHostAddressesAsync(getHostname(e));
            ipsTask.Wait();
            IPAddress[] IPs = ipsTask.Result;
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
