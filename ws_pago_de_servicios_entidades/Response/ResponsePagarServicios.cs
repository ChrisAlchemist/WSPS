﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_entidades.Responses
{
    [DataContract]
  public  class _ResponsePagarServicios
    {
        [DataMember]
        public ResponseAbonar ResponseAbonar { get; set; }

        [DataMember]
        public string request { get; set; }
        [DataMember]
        public string response { get; set; }

        [DataMember]
        public string signed { get; set; }

        /// <summary>
        /// si es true, quiere decir que llego al web service, si es false no llega al web service
        /// </summary>
        [DataMember]
        public EstatusCMV EstatusCmv { get; set; }

        /// <summary>
        /// comentarios de alguna a falla
        /// </summary>
        [DataMember]
        public string MensajeCmv { get; set; }

    }
    
}
