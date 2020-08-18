using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_utilidades
{
    public class ConexionAPI
    {
        public static string codigoDispositivo = "";
        public static string idDistribuidor = "";
        public static string password = "";
        public static string secretKey = "";
        public static string IV = "";
        public static string URL = @"";
        public static string numeroSerie = @"";
        private static string LlaveBusCrypto = string.Empty;

        //public static string Directorio = WebConfigurationManager.AppSettings["Request"].ToString();
        public static string ObtenerConexion()
        {
            try
            {
                if (ConfigurationManager.AppSettings["serviciosProductivo"].ToString().Equals("0")  || System.Diagnostics.Debugger.IsAttached)
                {
                    codigoDispositivo = ConfigurationManager.AppSettings["codigoDispositivo"].ToString();
                    idDistribuidor = ConfigurationManager.AppSettings["idDistribuidor"].ToString();
                    password = ConfigurationManager.AppSettings["password"].ToString();
                    secretKey = ConfigurationManager.AppSettings["secretKey"].ToString();
                    IV = ConfigurationManager.AppSettings["IV"].ToString();
                    URL = ConfigurationManager.AppSettings["url"].ToString();
                    numeroSerie = ConfigurationManager.AppSettings["numeroSerie"].ToString();
                }
                else
                {

                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\APIGestoPago");
                    if (key != null)
                    {
                        codigoDispositivo = key.GetValue("codigoDispositivo").ToString();
                        idDistribuidor = key.GetValue("idDistribuidor").ToString();
                        password = key.GetValue("password").ToString();
                        secretKey = key.GetValue("secretKey").ToString();
                        IV = key.GetValue("IV").ToString();
                        URL = key.GetValue("url").ToString();
                        numeroSerie = key.GetValue("numeroSerie").ToString();
                        key.Close();
                    }
                    else
                    {
                        throw new Exception("CMV : No existen los parametros de conexion");
                    }
                }
                return @"&codigoDispositivo=" + ConexionAPI.codigoDispositivo + "&password=" + ConexionAPI.password + "&idDistribuidor=" + ConexionAPI.idDistribuidor + "&numeroSerie=" + ConexionAPI.numeroSerie;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtenerLlaveBusCrypto()
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM");
                LlaveBusCrypto = key.GetValue("5").ToString();
                key.Close();
                return LlaveBusCrypto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
