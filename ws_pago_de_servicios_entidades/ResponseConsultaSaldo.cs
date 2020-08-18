using System.Runtime.Serialization;

namespace ws_pago_de_servicios_entidades
{
    /// <remarks/>
    [System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "RESPONSE" , Namespace = "", IsNullable = false)]

    [DataContract]
    public partial class ResponseConsultaSaldo
    {

        private sbyte iD_TXField;

        private byte nUM_AUTORIZACIONField;

        private decimal sALDOField;

        private decimal cOMISIONField;

        private string sALDO_FField;

        private byte cOMISION_FField;

        private string fECHAField;

        private decimal mONTOField;

        private RESPONSEMENSAJECONSULTA mENSAJEField;

        /// <remarks/>
        public sbyte ID_TX
        {
            get
            {
                return this.iD_TXField;
            }
            set
            {
                this.iD_TXField = value;
            }
        }

        /// <remarks/>
        public byte NUM_AUTORIZACION
        {
            get
            {
                return this.nUM_AUTORIZACIONField;
            }
            set
            {
                this.nUM_AUTORIZACIONField = value;
            }
        }

        /// <remarks/>
        public decimal SALDO
        {
            get
            {
                return this.sALDOField;
            }
            set
            {
                this.sALDOField = value;
            }
        }

        /// <remarks/>
        public decimal COMISION
        {
            get
            {
                return this.cOMISIONField;
            }
            set
            {
                this.cOMISIONField = value;
            }
        }

        /// <remarks/>
        public string SALDO_F
        {
            get
            {
                return this.sALDO_FField;
            }
            set
            {
                this.sALDO_FField = value;
            }
        }

        /// <remarks/>
        public byte COMISION_F
        {
            get
            {
                return this.cOMISION_FField;
            }
            set
            {
                this.cOMISION_FField = value;
            }
        }

        /// <remarks/>
        public string FECHA
        {
            get
            {
                return this.fECHAField;
            }
            set
            {
                this.fECHAField = value;
            }
        }

        /// <remarks/>
        public decimal MONTO
        {
            get
            {
                return this.mONTOField;
            }
            set
            {
                this.mONTOField = value;
            }
        }

        /// <remarks/>
        public RESPONSEMENSAJECONSULTA MENSAJE
        {
            get
            {
                return this.mENSAJEField;
            }
            set
            {
                this.mENSAJEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RESPONSEMENSAJECONSULTA
    {

        private byte cODIGOField;

        private byte sALDOCLIENTEField;

        private object tEXTOField;

        private object rEFERENCIAField;

        /// <remarks/>
        public byte CODIGO
        {
            get
            {
                return this.cODIGOField;
            }
            set
            {
                this.cODIGOField = value;
            }
        }

        /// <remarks/>
        public byte SALDOCLIENTE
        {
            get
            {
                return this.sALDOCLIENTEField;
            }
            set
            {
                this.sALDOCLIENTEField = value;
            }
        }

        /// <remarks/>
        public object TEXTO
        {
            get
            {
                return this.tEXTOField;
            }
            set
            {
                this.tEXTOField = value;
            }
        }

        /// <remarks/>
        public object REFERENCIA
        {
            get
            {
                return this.rEFERENCIAField;
            }
            set
            {
                this.rEFERENCIAField = value;
            }
        }
    }
}
