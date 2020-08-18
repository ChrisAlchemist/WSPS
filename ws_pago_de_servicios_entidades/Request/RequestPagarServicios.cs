using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_entidades.Requests
{
    [DataContract]
    public class RequestPagarServicios
    {     

        [DataMember (IsRequired =true)]
        [JsonProperty("idProducto")]
        public int IdProducto { get; set; }

        [DataMember (IsRequired =true)]
        [JsonProperty("idServicio")]
        public int IdServicio { get; set; }

        [DataMember (IsRequired =true)]
        [JsonProperty("telefono")]
        public String Telefono { get; set; }

        [DataMember (IsRequired =true)]
        [JsonProperty("referencia")]
        public String NumeroReferencia { get; set; }

        /// <summary>
        /// Debe ser la suma de las propiedades de Precio + MontoComision
        /// </summary>
        [DataMember (IsRequired =true)]
        [JsonProperty("montoPago")]
        public Decimal Monto { get; set; }
        [DataMember (IsRequired =true)]

        [JsonIgnore]
        public string ClabeCorresponsaliasRetiro { get; set; }

        [DataMember (IsRequired =true)]
        [JsonIgnore]
        public int TipoFront { get; set; }

        [DataMember (IsRequired =true)]
        [JsonIgnore]
        public Decimal Precio { get; set; }

        [DataMember (IsRequired =true)]
        [JsonIgnore]
        public String NumeroSocio { get; set; }

        [DataMember (IsRequired =true)]
        [JsonIgnore]
        public TipoOrigen TipoOrigen { get; set; }

        [DataMember(IsRequired = true)]
        [JsonIgnore]
        public Decimal  MontoComision { get; set; }

        [DataMember(IsRequired = true)]
        [JsonProperty("horaLocal")]
        public String HoraLocal { get; set; }

        [DataMember(IsRequired = true)]
        [JsonProperty("upc")]        
        public Int64 EnvioUpc { get; set; }

        public string ObtenerParametros()
        {
            string param = "&idServicio=" + this.IdServicio + "&idProducto=" + this.IdProducto;
            switch (this.TipoFront)
            {
                case 1:
                    if (String.IsNullOrEmpty(this.Telefono))
                        throw new Exception("Parametro Telefono es necesario para realizar el pago");
                    param += "&telefono=" + this.Telefono;
                    this.Monto = this.Precio; //Cuando es tipo drin 
                    
                    break;
                case 2:
                    if (String.IsNullOrEmpty(this.NumeroReferencia))
                        throw new Exception("Parametro Numero de Referencia es necesario para realizar el pago");
                    if (String.IsNullOrEmpty(this.Monto.ToString()) || this.Monto == 0)
                        throw new Exception("Parametro Monto es necesario para realizar el pago");
                    param += "&referencia=" + this.NumeroReferencia + "&montoPago=" + this.Monto;
                    break;
                case 4:
                    if (String.IsNullOrEmpty(this.NumeroReferencia))
                        throw new Exception("Parametro Numero de  Referencia es necesario para realizar el pago");
                    param += "&referencia=" + this.NumeroReferencia;
                    break;

                default:
                    break;
            }
            return param;

        }

    }
}
