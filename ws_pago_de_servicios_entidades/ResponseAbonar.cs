
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace ws_pago_de_servicios_entidades
{
    public class ResponseGeneral
    {
        [JsonProperty("RESPONSE")]
        public ResponseAbonar ResponseAbonar { get; set; }
    }

    [System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "RESPONSE", Namespace = "", IsNullable = false)]
    [DataContract]
    public class ResponseAbonar
    {

        [DataMember]
        public string ID_TX
        { get; set; }

        [DataMember]
        public string NUM_AUTORIZACION
        { get; set; }

        [DataMember]
        public string SALDO
        { get; set; }

        [DataMember]
        public string COMISION
        { get; set; }

        [DataMember]
        public string SALDO_F
        { get; set; }

        [DataMember]
        public string COMISION_F
        { get; set; }

        [DataMember]
        public string FECHA
        { get; set; }

        [DataMember]
        public string MONTO
        { get; set; }

        [DataMember]
        public RESPONSEMENSAJEABONAR MENSAJE
        { get; set; }

    }



    //[System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [DataContract]
    public class RESPONSEMENSAJEABONAR
    {


        [DataMember]
        public string CODIGO
        { get; set; }

        [DataMember]
        public string PIN
        { get; set; }

        [DataMember]
        [JsonProperty("legend")]
        public string legend
        { get; set; }

        [DataMember]
        public string TEXTO
        { get; set; }

        [DataMember]
        public string REFERENCIA
        { get; set; }

        [DataMember]
        public string SALDOCLIENTE
        { get; set; }

    }

}
















