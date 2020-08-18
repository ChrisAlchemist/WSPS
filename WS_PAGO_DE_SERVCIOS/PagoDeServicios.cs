using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;
using WS_PAGO_DE_SERVCIOS.Interceptor;
using ws_pago_de_servicios_entidades;
using ws_pago_de_servicios_entidades.Exceptions;
using ws_pago_de_servicios_entidades.Requests;
using ws_pago_de_servicios_entidades.Response;
using ws_pago_de_servicios_entidades.Responses;
using ws_pago_de_servicios_utilidades;
using ws_pago_de_servicios_utilidades.Logg;

namespace WS_PAGO_DE_SERVCIOS
{

    [WS_PAGO_DE_SERVCIOS.Interceptor.Interceptor]
    public class PagoDeServicios : IPagoDeServicios
    {
        public static string getListaProducto = "getListaProducto.do";
        public static string abonar = "abonar.do";
        public static string confirmaTransaccion = "confirmaTransaccion.do";
        public static string sendTx = "sendTx.do";
        public static string confirmTx = "confirmTx.do";
        public static string getSaldo = "getSaldo.do";    

        public _ResponsePagarServicios PagarServicio(RequestPagarServicios requestPagarServicios)
        {

            _ResponsePagarServicios responsePagarServicios = null;
            try
            {

                if (!(requestPagarServicios.TipoFront == 1 || requestPagarServicios.TipoFront == 2))
                {
                    _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                    exceptionPagarServicios.Codigo = -1;
                    exceptionPagarServicios.Mensaje = "El tipo front no corresponde a este servicio";
                    throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "Error personalizado por CMV", new FaultCode("-1"));
                }

                String result = string.Empty;
                responsePagarServicios = new _ResponsePagarServicios();

               // throw new WebException();

                result = PagarServicioJWT(requestPagarServicios,sendTx);
               

                /*  Para consumir a GestoPago antes de implementar JWT
                    responsePagarServicios.request = ParametrosConfig() + requestPagarServicios.ObtenerParametros();
                    byte[] data = UTF8Encoding.UTF8.GetBytes(ParametrosConfig() + requestPagarServicios.ObtenerParametros());
                    HttpWebRequest request = initRequest(data, abonar);
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(data, 0, data.Length);
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    StreamReader readerResponse = new StreamReader(response.GetResponseStream());
                    result = readerResponse.ReadToEnd();
                */

                responsePagarServicios.response = result;
                if (!string.IsNullOrEmpty(result))
                {
                    new Logg().Warning("xml respuesta :" + result.ToString());
                    ResponseAbonar responseAbonar = SerializerManager<ResponseGeneral>.DeseralizarStringXMLToObject(result).ResponseAbonar;
                    responsePagarServicios.request = "Metodo: " + sendTx  + ConexionAPI.ObtenerConexion() + " | " + JsonConvert.SerializeObject(requestPagarServicios);
                    responsePagarServicios.signed = Cifrado.CifradoTexto(Uri.EscapeDataString(JsonConvert.SerializeObject(requestPagarServicios)));
                    responsePagarServicios.ResponseAbonar = responseAbonar;
                    responsePagarServicios.EstatusCmv = EstatusCMV.Ok;
                    responsePagarServicios.MensajeCmv = "OK";                                     

                    return responsePagarServicios;
                }
            }
            catch (FaultException<_ExceptionPagarServicios> ex)
            {
                throw ex;
            }
            catch (WebException e)
            {
                new Logg().Error("Error :" + e.ToString());
                responsePagarServicios = new _ResponsePagarServicios();
                responsePagarServicios.request = "Metodo: " + sendTx  + ConexionAPI.ObtenerConexion() + " | " + JsonConvert.SerializeObject(requestPagarServicios);
                responsePagarServicios.signed = Cifrado.CifradoTexto(Uri.EscapeDataString(JsonConvert.SerializeObject(requestPagarServicios)));
                responsePagarServicios.EstatusCmv = EstatusCMV.Error;
                responsePagarServicios.MensajeCmv = e.Message + "|" + e.StackTrace;
                //return responsePagarServicios;

                _ResponseConfirmaTransaccion responseConfirmaTransaccion = ConfirmaTransaccion(requestPagarServicios);
                responseConfirmaTransaccion.ResponseAbonar.MENSAJE.CODIGO = responseConfirmaTransaccion.ResponseAbonar.MENSAJE.CODIGO == "06" ? "01" : responseConfirmaTransaccion.ResponseAbonar.MENSAJE.CODIGO;
                Mapper.Initialize(x => { x.CreateMap<_ResponseConfirmaTransaccion, _ResponsePagarServicios>(); });
                responsePagarServicios = Mapper.Map<_ResponsePagarServicios>(responseConfirmaTransaccion);
                return responsePagarServicios;

            }
            catch (Exception ex)
            {
                _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                exceptionPagarServicios.Codigo = 1000;//Error no definido p
                exceptionPagarServicios.Mensaje = ex.Message;
                throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "Error personalizado por Exception", new FaultCode("-1"));
            }
            return null;

        }
        public _ResponseConsultaSaldo ConsultarSaldoSerivicio(RequestPagarServicios requestPagarServicios)
        {

            _ResponseConsultaSaldo responseConsultaSaldo = null;
            try
            {

                if (requestPagarServicios.TipoFront != 4)
                {

                    _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                    exceptionPagarServicios.Codigo = -1;
                    exceptionPagarServicios.Mensaje = "El tipo front no corresponde a este servicio";
                    throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "Error personalizado por CMV", new FaultCode("-1"));
                }

                String result = string.Empty;
                responseConsultaSaldo = new _ResponseConsultaSaldo();
                result = PagarServicioJWT(requestPagarServicios, sendTx);
                
                /*
                responseConsultaSaldo.request = ParametrosConfig() + requestPagarServicios.ObtenerParametros();
                byte[] data = UTF8Encoding.UTF8.GetBytes(ParametrosConfig() + requestPagarServicios.ObtenerParametros());
                HttpWebRequest request = initRequest(data, abonar);

                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader readerResponse = new StreamReader(response.GetResponseStream());
                result = readerResponse.ReadToEnd();
                */
                responseConsultaSaldo.response = result;
                if (!string.IsNullOrEmpty(result))
                {                    
                    new Logg().Info("xml respuesta :" +result.ToString());
                    ResponseAbonar responseAbonar = SerializerManager<ResponseGeneral>.DeseralizarStringXMLToObject(result).ResponseAbonar;
                    responseConsultaSaldo.request = "Metodo: " + sendTx + ConexionAPI.ObtenerConexion() + " | " + JsonConvert.SerializeObject(requestPagarServicios);
                    
                    responseConsultaSaldo.ResponseAbonar = responseAbonar;
                    responseConsultaSaldo.EstatusCmv = EstatusCMV.Ok;
                    responseConsultaSaldo.MensajeCmv = "Ejecución exitosa del servicio de consultar saldo";
                    responseConsultaSaldo.Signed = Cifrado.CifradoTexto(Uri.EscapeDataString(JsonConvert.SerializeObject(requestPagarServicios)));
                    return responseConsultaSaldo;
                }
            }
            catch (FaultException<_ExceptionPagarServicios> ex)
            {
                throw ex;
            }
            catch (WebException e)
            { 
                responseConsultaSaldo.request = "Metodo: " + sendTx + ConexionAPI.ObtenerConexion() + " | " + JsonConvert.SerializeObject(requestPagarServicios);
                responseConsultaSaldo.EstatusCmv = EstatusCMV.Error;
                responseConsultaSaldo.MensajeCmv = e.Message + " | " + e.StackTrace;
                return responseConsultaSaldo;
            }

            catch (Exception ex)
            {
                _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                exceptionPagarServicios.Codigo = -1;
                exceptionPagarServicios.Mensaje = ex.Message;
                throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "Error personalizado por Exception | " + ex.Message, new FaultCode("-1"));
            }
            return null;

        }
        public _ResponseConfirmaTransaccion ConfirmaTransaccion(RequestPagarServicios requestPagarServicios)
        {
            bool intento = true;
            _ResponseConfirmaTransaccion responseConfirmaTransaccion = null;
            try
            {
                if (!(requestPagarServicios.TipoFront == 1 || requestPagarServicios.TipoFront == 2))
                {

                    _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                    exceptionPagarServicios.Codigo = -1;
                    exceptionPagarServicios.Mensaje = "El tipo front no corresponde a este servicio";
                    throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "Error personalizado por CMV", new FaultCode("-1"));
                }

                String result = string.Empty;
                responseConfirmaTransaccion = new _ResponseConfirmaTransaccion();

         
                result = PagarServicioJWT(requestPagarServicios, confirmTx);
                /*
                responseConfirmaTransaccion.request = ParametrosConfig() + requestPagarServicios.ObtenerParametros();
                byte[] data = UTF8Encoding.UTF8.GetBytes(ParametrosConfig() + requestPagarServicios.ObtenerParametros());

                HttpWebRequest request = initRequest(data,confirmaTransaccion);
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader readerResponse = new StreamReader(response.GetResponseStream());

                result = readerResponse.ReadToEnd();
                */
                responseConfirmaTransaccion.response = result;
                if (!string.IsNullOrEmpty(result))
                {
                    
                    responseConfirmaTransaccion.request = "Metodo: " + confirmTx  + ConexionAPI.ObtenerConexion() + " | " + JsonConvert.SerializeObject(requestPagarServicios);
                    responseConfirmaTransaccion.signed = Cifrado.CifradoTexto(Uri.EscapeDataString(JsonConvert.SerializeObject(requestPagarServicios)));
                    ResponseAbonar responseAbonar = SerializerManager<ResponseAbonar>.DeseralizarStringToObject(result);
                    responseConfirmaTransaccion.EstatusCmv = EstatusCMV.Ok;
                    responseConfirmaTransaccion.MensajeCmv = "Pago realizado Exitosamente";
                    responseConfirmaTransaccion.ResponseAbonar = responseAbonar;
                    return responseConfirmaTransaccion;
                }
            }
            catch (WebException e)
            {
                responseConfirmaTransaccion.request = "Metodo: " + confirmTx  + ConexionAPI.ObtenerConexion() + " | " + JsonConvert.SerializeObject(requestPagarServicios);
                responseConfirmaTransaccion.signed = Cifrado.CifradoTexto(Uri.EscapeDataString(JsonConvert.SerializeObject(requestPagarServicios)));
                responseConfirmaTransaccion.EstatusCmv = EstatusCMV.Error;
                responseConfirmaTransaccion.MensajeCmv = e.Message + " | " + e.StackTrace;
                return responseConfirmaTransaccion;
            }
            catch (FaultException<_ExceptionPagarServicios> ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                exceptionPagarServicios.Codigo = -1;
                exceptionPagarServicios.Mensaje = ex.Message;
                throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "CMV: Error personalizado por Exception", new FaultCode("-1"));
            }
            return null;
        }
        public _ResponseConsultaProductos ConsultaProductos()
        {
            _ResponseConsultaProductos responseConsultaProductos= null;
            try
            {
                responseConsultaProductos = new _ResponseConsultaProductos();
                String result = string.Empty;
                byte[] data = UTF8Encoding.UTF8.GetBytes(ConexionAPI.ObtenerConexion()+ "&showLegend=true");
                string token = new Authenticate().GeneraToken();
                HttpWebRequest request = initRequest(data, getListaProducto);
                request.Headers.Add("Authorization", "Bearer " + token);
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader readerResponse = new StreamReader(response.GetResponseStream());

                result = readerResponse.ReadToEnd();

                //responsePagarServicios.response = result;
                if (!string.IsNullOrEmpty(result))
                {
                    ResponseConsultaProductos responseAbonar = SerializerManager<ResponseConsultaProductos>.DeseralizarStringToObject(result);
                    responseConsultaProductos.Productos = responseAbonar;
                    return responseConsultaProductos;
                }
            }
            catch (Exception ex)
            {
                _ExceptionPagarServicios exceptionPagarServicios = new _ExceptionPagarServicios();
                exceptionPagarServicios.Codigo = -1;
                exceptionPagarServicios.Mensaje = ex.Message;
                throw new FaultException<_ExceptionPagarServicios>(exceptionPagarServicios, "Error personalizado por Exception", new FaultCode("-1"));
            }
            return null;
        }
        private HttpWebRequest initRequest(byte[] data , string operacion)
        {
            string url = Path.Combine(ConexionAPI.URL, operacion);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 62 * 1000; // 10 segundos
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded;";
            return request;

        }
               
        private string PagarServicioJWT(RequestPagarServicios requestPagarServicios , string metodoApiGestoPago )
        {
            String result = string.Empty;
            _ResponsePagarServicios responsePagarServicios = new _ResponsePagarServicios();
            responsePagarServicios.request = ConexionAPI.ObtenerConexion() + requestPagarServicios.ObtenerParametros();

            string dataPost = JsonConvert.SerializeObject(requestPagarServicios);

            String cadenaNormal = Cifrado.CifradoTexto(dataPost);
            String cadenaEscape = Uri.EscapeDataString(Cifrado.CifradoTexto(dataPost));

            byte[] data = UTF8Encoding.UTF8.GetBytes("signed="+ cadenaEscape);
            new Logg().Info("strKey : " + ConexionAPI.secretKey + " | IV: " + ConexionAPI.IV);
            new Logg().Info("signed cadenaEscape =" + cadenaEscape);
            new Logg().Info("signed cadenaNormal =" + cadenaNormal);
            new Logg().Info("Descrypt: " + Cifrado.DescifradoTexto(cadenaNormal));
            string token = new Authenticate().GeneraToken();

            

            HttpWebRequest request = initRequest(data, metodoApiGestoPago);
            request.Headers.Add("Authorization", "Bearer "+token);
            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader readerResponse = new StreamReader(response.GetResponseStream());

            result = readerResponse.ReadToEnd();
            Debug.WriteLine("Result ws: " + result);
            Debug.WriteLine("Json Params: " + JsonConvert.SerializeObject(requestPagarServicios));
            Debug.WriteLine("Data Cifrada: " + cadenaNormal);
            Debug.WriteLine("Data Cifrada Escape: " + cadenaEscape);
            Debug.WriteLine("token: " + token);
            return result;
        }
        public string GenerarToken()
        {
            return new Authenticate().GeneraToken();
        }
    }
}
