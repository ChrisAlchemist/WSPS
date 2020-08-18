using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml.Serialization;

namespace WS_PAGO_DE_SERVCIOS.Interceptor
{
    public class Header : MessageHeader
    {
        private const string CUSTOM_HEADER_NAME = "";
        private const string CUSTOM_HEADER_NAMESPACE = "";

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Namespace
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //public override string Name => throw new NotImplementedException();
        //public override string Namespace => throw new NotImplementedException();

        protected override void OnWriteHeaderContents(
            System.Xml.XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ModelInterceptor));
            StringWriter textWriter = new StringWriter();
            //serializer.Serialize(textWriter, _customData);
            textWriter.Close();
            string text = textWriter.ToString();
            writer.WriteElementString(CUSTOM_HEADER_NAME, "Key", text.Trim());
        }

        public static ModelInterceptor ReadHeader(Message request)
        {
            ModelInterceptor header = new ModelInterceptor();
            header.usuario = GetHeader(request, "Usuario", "");
            header.contrasena = GetHeader(request, "Contrasena", "");
            return header;
        }

        private static string GetHeader(Message request, string name, string ns)
        {

            if (request.Headers.FindHeader(name, ns) > -1)
                return request.Headers.GetHeader<String>(name, ns);
            else if (request.Headers.FindHeader(name, "http://schemas.xmlsoap.org/soap/envelope/") > -1)
                return request.Headers.GetHeader<String>(name, "http://schemas.xmlsoap.org/soap/envelope/");
            else
                return string.Empty;

            return string.Empty;
        }
    }
}