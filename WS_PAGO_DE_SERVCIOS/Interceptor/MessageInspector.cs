using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web;
using System.Xml;
using ws_pago_de_servicios_utilidades;
using ws_pago_de_servicios_utilidades.Logg;

namespace WS_PAGO_DE_SERVCIOS.Interceptor
{
    public class MessageInspector : ServiceLog ,IClientMessageInspector, IDispatchMessageInspector
    {
        private readonly ILog log4netRequest;
        public MessageInspector() : base()
        {
            //log4netResponse = LogManager.GetLogger("LogResponse");
            log4netRequest = LogManager.GetLogger("LogRequest");
        }
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();
            Message messageCopy = buffer.CreateMessage();

            //para obtener el metodo y la peticion del ws 
            var action = OperationContext.Current.IncomingMessageHeaders.Action;
            var operationName = action.Substring(action.LastIndexOf("/") + 1);
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 1;
            xmlTextWriter.IndentChar = '\t';
            messageCopy.WriteMessage(xmlTextWriter);


             // Read the custom context data from the headers
              ModelInterceptor requestHeaders = Header.ReadHeader(request);

            // Add an extension to the current operation context so
            // that our custom context can be easily accessed anywhere.
            //ServerContext customContext = new ServerContext();
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Utilerias.validaUsuario(requestHeaders.usuario, requestHeaders.contrasena);
                new Logg().Info("Mensaje de Entrada: (Request) \n\n" + stringWriter.ToString());
                log4netRequest.Info("Mensaje de Entrada: (Request) \n\n" + stringWriter.ToString());
            }
            OperationContext.Current.IncomingMessageProperties.Add(
                     "CurrentContext", new ModelInterceptor());
            return null;
        }


        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            reply = buffer.CreateMessage();
            Message messageCopy = buffer.CreateMessage();

            if (reply.IsFault)
            {
                XmlElement elm;
                var messageFault = MessageFault.CreateFault(messageCopy, Int32.MaxValue);
                if (messageFault.HasDetail)
                {
                    new Logg().Error("Mensaje de Salida: " + messageFault.GetReaderAtDetailContents().ReadOuterXml());
                    log4netRequest.Info("Mensaje de Salida: " + messageFault.GetReaderAtDetailContents().ReadOuterXml());
                }
                else
                {
                    new Logg().Error(messageFault.Reason.ToString() + " | " + messageFault.Code.Name);
                    log4netRequest.Info("Mensaje de Salida: " + messageFault.GetReaderAtDetailContents().ReadOuterXml());
                }
            }
            else
            {
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.Indentation = 1;
                xmlTextWriter.IndentChar = '\t';


                messageCopy.WriteMessage(xmlTextWriter);
                if (stringWriter.ToString().Contains("ObtenerImagenesAntiphishingResponse") ||
                    stringWriter.ToString().Contains("ValidaNumeroResponse") ||
                    stringWriter.ToString().Contains("ResponseObtenerPublicidadGeneral") ||
                    stringWriter.ToString().Contains("ResponseObtenerPublicidadDirigida"))
                {

                    string cadena = stringWriter.ToString();
                    string toFind1 = "ImagenAntiphishing";
                    int start = cadena.IndexOf(toFind1) + toFind1.Length;
                    int end = cadena.IndexOf(toFind1, start); //Start after the index of 'my' since 'is' appears twice
                    if (start > 0 && end > 0)
                    {
                        StringBuilder sb = new StringBuilder(cadena);
                        sb.Remove(start, end - start);
                        cadena = sb.ToString();
                    }
                    new Logg().Info("Mensaje de Salida (Response): \n\n" + cadena);
                    log4netRequest.Info("Mensaje de Salida (Response): \n\n" + cadena);
                }
                else
                {
                    new Logg().Info("Mensaje de Salida (Response): \n\n" + stringWriter.ToString());
                    log4netRequest.Info("Mensaje de Salida (Response): \n\n" + stringWriter.ToString());
                }
            }
            //OperationContext.Current.Extensions.Remove(ServerContext.Current);
        }




        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            /*MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();

            ServiceHeader customData = new ServiceHeader();

            customData.usuario = ClientContext.usuario;
            customData.contrasena = ClientContext.contrasena;

            CustomHeader header = new CustomHeader(customData);

            // Add the custom header to the request.
            request.Headers.Add(header);
            */
            return null;
        }
    }
}