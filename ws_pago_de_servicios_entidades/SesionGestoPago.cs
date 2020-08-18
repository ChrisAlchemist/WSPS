
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ws_pago_de_servicios_entidades.Requests;
using ws_pago_de_servicios_utilidades;

namespace ws_pago_de_servicios_entidades
{
    public static class SesionGestoPago
    {
        private static string codigoDispositivo = "";
        private static string idDistribuidor = "";
        private static string password = "";
        private static string URL = @"";
        public static string getListaProducto = "getListaProducto.do";
        public static string abonar = "abonar.do";

        public static Object PagarServicio(ref RequestPagarServicios requestPagarServicios)
        {
            bool intento = true;
            try
            {
                String result = string.Empty;
                requestPagarServicios.request = ParametrosConfig() + requestPagarServicios.ObtenerParametros();
                byte[] data = UTF8Encoding.UTF8.GetBytes(ParametrosConfig() + requestPagarServicios.ObtenerParametros());
                HttpWebRequest request;
                string url = Path.Combine(SesionGestoPago.URL, SesionGestoPago.abonar);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 60 * 1000; // 60 segundos
                request.Method = "POST";
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader readerResponse = new StreamReader(response.GetResponseStream());

                result = readerResponse.ReadToEnd();
                requestPagarServicios.response = result;
                if (!string.IsNullOrEmpty(result))
                {
                    Console.WriteLine(result.ToString());
                    switch (requestPagarServicios.TipoFront)
                    {
                        case 1:
                        case 2:
                            ResponseAbonar responseAbonar = SerializerManager<ResponseAbonar>.DeseralizarStringToObject(result);
                            return responseAbonar;
                            break;
                        case 4:
                            ResponseConsultaSaldo esponseConsultaSaldo = SerializerManager<ResponseConsultaSaldo>.DeseralizarStringToObject(result);
                            return esponseConsultaSaldo;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    if (intento)
                    {
                        intento = false;
                        //PagarServicio(requestPagarServicios);
                    }
                }else
                    throw e;

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        private static string ParametrosConfig()
        {
            try
            {
                codigoDispositivo = ConfigurationManager.AppSettings["codigoDispositivo"].ToString();
                idDistribuidor = ConfigurationManager.AppSettings["idDistribuidor"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();
                URL = ConfigurationManager.AppSettings["URL"].ToString();
                string urlParameters = "codigoDispositivo=" + SesionGestoPago.codigoDispositivo + "&password=" + SesionGestoPago.password + "&idDistribuidor=" + SesionGestoPago.idDistribuidor;
                return urlParameters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}


