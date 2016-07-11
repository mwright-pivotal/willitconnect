using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wic4windows.Model
{
    public class CheckedEntry
    {
        private DateTime lastChecked;
        private String entry;
        private Boolean canConnect;
        private int httpStatus;
        private String httpProxy;

        private const String HOSTCHECK = "[\\w\\.-]+:\\d+";

        public CheckedEntry(String entry)
        {
            this.lastChecked = DateTime.Now;
            this.entry = entry;
            this.canConnect = false;
        }

        public Boolean isValidHostname()
        {
            Regex r = new Regex(HOSTCHECK, RegexOptions.IgnoreCase);
            // Match the regular expression pattern against a text string.

            return r.IsMatch(entry);
        }

        public Boolean isValidUrl()
        {
            Uri uriResult;
            return Uri.TryCreate(entry, UriKind.Absolute, out uriResult);
        }

        public DateTime LastChecked
        {
            get
            {
                return lastChecked;
            }

            set
            {
                lastChecked = value;
            }
        }

        public string Entry
        {
            get
            {
                return entry;
            }

            set
            {
                entry = value;
            }
        }

        public bool CanConnect
        {
            get
            {
                return canConnect;
            }

            set
            {
                canConnect = value;
            }
        }

        public int HttpStatus
        {
            get
            {
                return httpStatus;
            }

            set
            {
                httpStatus = value;
            }
        }

        public string HttpProxy
        {
            get
            {
                return httpProxy;
            }

            set
            {
                httpProxy = value;
            }
        }
    }
}
