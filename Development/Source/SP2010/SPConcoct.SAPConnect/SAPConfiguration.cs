using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;

namespace SPConcoct.SAPConnect
{
    public class SAPConfiguration : IDestinationConfiguration
    {
        public RfcConfigParameters GetParameters(String destinationName)
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.AppServerHost, System.Configuration.ConfigurationManager.AppSettings["AppServerHost"]);
            parms.Add(RfcConfigParameters.SystemNumber, System.Configuration.ConfigurationManager.AppSettings["SystemNumber"]);
            parms.Add(RfcConfigParameters.SystemID, System.Configuration.ConfigurationManager.AppSettings["SystemID"]);
            parms.Add(RfcConfigParameters.SAPRouter, System.Configuration.ConfigurationManager.AppSettings["SAPRouter"]);
            parms.Add(RfcConfigParameters.User, System.Configuration.ConfigurationManager.AppSettings["User"]);
            parms.Add(RfcConfigParameters.Password, System.Configuration.ConfigurationManager.AppSettings["Password"]);
            parms.Add(RfcConfigParameters.Client, System.Configuration.ConfigurationManager.AppSettings["Client"]);
            parms.Add(RfcConfigParameters.Language, System.Configuration.ConfigurationManager.AppSettings["Language"]);
            parms.Add(RfcConfigParameters.PoolSize, System.Configuration.ConfigurationManager.AppSettings["PoolSize"]);
            parms.Add(RfcConfigParameters.IdleTimeout, System.Configuration.ConfigurationManager.AppSettings["IdleTimeout"]);

            return parms;
        }

        // The following two are not used in this example:
        public bool ChangeEventsSupported()
        {
            return false;
        }
        public event RfcDestinationManager.ConfigurationChangeHandler
            ConfigurationChanged;

    }

}

