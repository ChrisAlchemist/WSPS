using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_entidades.Response
{
    [DataContract]
    public class _ResponseConsultaProductos
    {
        [DataMember(Name = "RESPONSE")]
        public ResponseConsultaProductos Productos { get; set; }
    }
}
