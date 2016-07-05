using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wic4windows.Model
{
    public class EntryRequest
    {
        private String target;
        private String http_proxy;

        public string Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        public string Http_proxy
        {
            get
            {
                return http_proxy;
            }

            set
            {
                http_proxy = value;
            }
        }
    }
}
