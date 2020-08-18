
// NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
using System.Runtime.Serialization;
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(ElementName = "RESPONSE", Namespace = "", IsNullable = false)]
[DataContract]
public  class ResponseConsultaProductos
{

   [DataMember]
    public RESPONSEMENSAJE MENSAJE
    {
        get;
        set;
        
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("producto", IsNullable = false)]
     [DataMember]
    public RESPONSEProducto[] PRODUCTOS
    {
        get;
        set;
       
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RESPONSEMENSAJE
{

    private byte cODIGOField;

    private string tEXTOField;

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
    public string TEXTO
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[DataContract(Name ="producto")]
public partial class RESPONSEProducto
{
    private string legendField;

    private string servicioField;

    private string productoField;

    private ushort idServicioField;

    private ushort idProductoField;

    private byte idCatTipoServicioField;

    private byte tipoFrontField;

    private bool hasDigitoVerificadorField;

    private decimal precioField;

    private bool showAyudaField;

    private string tipoReferenciaField;

    /// <remarks/>    /// 
    [DataMember]
    public string legend
    {
        get
        {
            return this.legendField;
        }
        set
        {
            this.legendField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public string servicio
    {
        get
        {
            return this.servicioField;
        }
        set
        {
            this.servicioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public string producto
    {
        get
        {
            return this.productoField;
        }
        set
        {
            this.productoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public ushort idServicio
    {
        get
        {
            return this.idServicioField;
        }
        set
        {
            this.idServicioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public ushort idProducto
    {
        get
        {
            return this.idProductoField;
        }
        set
        {
            this.idProductoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public byte idCatTipoServicio
    {
        get
        {
            return this.idCatTipoServicioField;
        }
        set
        {
            this.idCatTipoServicioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public byte tipoFront
    {
        get
        {
            return this.tipoFrontField;
        }
        set
        {
            this.tipoFrontField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public bool hasDigitoVerificador
    {
        get
        {
            return this.hasDigitoVerificadorField;
        }
        set
        {
            this.hasDigitoVerificadorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public decimal precio
    {
        get
        {
            return this.precioField;
        }
        set
        {
            this.precioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public bool showAyuda
    {
        get
        {
            return this.showAyudaField;
        }
        set
        {
            this.showAyudaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [DataMember]
    public string tipoReferencia
    {
        get
        {
            return this.tipoReferenciaField;
        }
        set
        {
            this.tipoReferenciaField = value;
        }
    }
}
