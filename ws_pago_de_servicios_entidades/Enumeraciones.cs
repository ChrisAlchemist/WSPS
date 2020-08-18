using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_entidades
{
    [DataContract]
    public enum EstatusPagoDeServicio
    {
        [EnumMember]
        Ninguno = 0,
        [EnumMember]
        Pendiente = 1,
        [EnumMember]
        Realizada = 2,
        [EnumMember]
        Rechazada = 3
    }

    [DataContract]
    public enum TipoOrigen
    {
        [EnumMember]
        MOVIL = 1,
        [EnumMember]
        SUCURSAL = 2,
        [EnumMember]
        BANCA_WEB_ = 3,
        [EnumMember]
        DATABANKING = 4,

    }

    [DataContract]
    public enum EstatusCMV
    {
        [EnumMember]
        Ok = 1,
        [EnumMember]
        Error= 2,
        
    }

}
