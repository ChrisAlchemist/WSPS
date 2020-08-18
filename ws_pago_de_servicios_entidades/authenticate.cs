using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ws_pago_de_servicios_utilidades;

namespace ws_pago_de_servicios_entidades
{

    public  class Authenticate
    {
        //[JsonIgnore]
        //public  string codigoDispositivo { get; set; }
        //[JsonIgnore]
        //public  string password { get; set; }
        //[JsonIgnore]
        //public  string idDistribuidor { get; set; }
        //[JsonIgnore]
        //public  string numeroSerie { get; set; }
       
        public string param { get; set; }

        public Authenticate()
        {
            Init();
        }
        private  void Init()
        {           
            this.generarParam();
        }
        private void generarParam()
        {
            this.param = ConexionAPI.ObtenerConexion();
        }

        public string GeneraToken()
        {
            try
            {
                string folder = ConfigurationManager.AppSettings["pathToken"];
                string NameFile = "Token" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                string TokenGestoPago = "";

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (File.Exists(Path.Combine(folder, NameFile)))
                {
                    // Open the text file using a stream reader.
                    using (var sr = new StreamReader(Path.Combine(folder, NameFile)))
                    {
                        // Read the stream as a string, and write the string to the console.
                        TokenGestoPago=sr.ReadToEnd();
                    }
                }

                if(!string.IsNullOrEmpty(TokenGestoPago))
                {
                    return TokenGestoPago;
                }         
                else
                {
                    HttpWebRequest request = WebRequest.Create(@"http://gestopago.portalventas.net/sistema/app/jwt-gp/authenticate/") as HttpWebRequest;
                    request.Timeout = 5 * 1000; // 10 segundos
                    request.Method = "POST";
                    Authenticate auth = new Authenticate();
                    Debug.WriteLine("Json Auth: " + JsonConvert.SerializeObject(auth));
                    byte[] data = UTF8Encoding.UTF8.GetBytes(this.param);
                    request.ContentLength = data.Length;
                    request.ContentType = "application/x-www-form-urlencoded;";
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(data, 0, data.Length);
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    StreamReader readerResponse = new StreamReader(response.GetResponseStream());
                    Token t = JsonConvert.DeserializeObject<Token>(readerResponse.ReadToEnd());
                    System.IO.File.WriteAllText(Path.Combine(folder, NameFile), t.token);
                    return t.token;
                }
              
                    



            }
            catch (Exception ex)
            {
                throw new Exception("CMV : Error al generar token ->"+ex.Message, ex);
                Debug.WriteLine(ex.Message);
            }
            return string.Empty;
        }
    }
}
