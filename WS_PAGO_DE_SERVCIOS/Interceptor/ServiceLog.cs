using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS_PAGO_DE_SERVCIOS.Interceptor
{
    public class ServiceLog
    {
        private readonly ILog log;
        public ServiceLog()
        {
            //Load log4net Configuration
            XmlConfigurator.Configure();
            //Get logger
            log = LogManager.GetLogger(typeof(ServiceLog));
            //Start logging

        }
    }
}
