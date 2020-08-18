using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ws_pago_de_servicios_utilidades
{
    public static class SerializerManager<T> where T : class
    {

        public static T DeseralizarXML(string arhivo)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(arhivo))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeseralizarStringToObject(string value)
        {
            object result;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(value))
                {
                    result = serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (T)result;
        }

        public static T DeseralizarStringXMLToObject(string value)
        {
            object result;
            try
            {
                var doc = XElement.Parse(value);
 
                var cdata = doc.DescendantNodes().OfType<XCData>().ToList();
                foreach (var cd in cdata)
                {
                    cd.Parent.Add(cd.Value);
                    cd.Remove();
                }
                string jsonText = JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.Indented);
                result = JsonConvert.DeserializeObject<T>(jsonText);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (T)result;
        }

        public static void Serealizar(string nombreArchivo, T value)
        {
            try
            {
                int x = nombreArchivo.LastIndexOf(@"\");
                string pathDirectorio = nombreArchivo.Substring(0, x);
                if (!Directory.Exists(pathDirectorio))
                {
                    Directory.CreateDirectory(pathDirectorio);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(nombreArchivo, Encoding.UTF8);
                xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(xmlTextWriter, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SerealizarObjtecToString(T value)
        {
            String result = string.Empty;
            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, value);

                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }

}
