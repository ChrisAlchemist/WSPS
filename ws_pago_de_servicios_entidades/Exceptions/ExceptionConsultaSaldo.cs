using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_entidades.Exceptions
{
    [DataContract]
   public class ExceptionConsultaSaldo
    {
        [DataMember]
        public int Codigo { get; set; }
        [DataMember]
        public String Mensaje { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
    }
}
