﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// Este código fuente fue generado automáticamente por xsd, Versión=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class CONFIGURACION {
    
    private CONFIGURACIONCONEXION_SAP cONEXION_SAPField;
    
    private CONFIGURACIONVENTANA vENTANAField;
    
    /// <remarks/>
    public CONFIGURACIONCONEXION_SAP CONEXION_SAP {
        get {
            return this.cONEXION_SAPField;
        }
        set {
            this.cONEXION_SAPField = value;
        }
    }
    
    /// <remarks/>
    public CONFIGURACIONVENTANA VENTANA {
        get {
            return this.vENTANAField;
        }
        set {
            this.vENTANAField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class CONFIGURACIONCONEXION_SAP {
    
    private string serverNameSAPField;
    
    private string tipoBaseSAPField;
    
    private string servidorLicenciasSAPField;
    
    private string baseSAPField;
    
    private string userSAPField;
    
    private string passSAPField;
    
    private string userSQLField;
    
    private string passSQLField;

    private string ejecucionAutomaticaField;

    private string rutaArchivosSAPField;

    private string rutaArchivoWEBField;

    private string rutaArchivoLocalField;

    private string wsConsultarSolicitudesField;

    private string wsActualizarSolicitudesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string WebServiceConsultarSolicitudes {
        get {
            return this.wsConsultarSolicitudesField;
        }
        set {
            this.wsConsultarSolicitudesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string WebServiceActualizarSolicitudes {
        get {
            return this.wsActualizarSolicitudesField;
        }
        set {
            this.wsActualizarSolicitudesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string RutaArchivosSAP {
        get {
            return this.rutaArchivosSAPField;
        }
        set {
            this.rutaArchivosSAPField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string RutaArchivoWEB {
        get {
            return this.rutaArchivoWEBField;
        }
        set {
            this.rutaArchivoWEBField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string RutaArchivoLocal {
        get {
            return this.rutaArchivoLocalField;
        }
        set {
            this.rutaArchivoLocalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string EjecucionAutomatica {
        get {
            return this.ejecucionAutomaticaField;
        }
        set {
            this.ejecucionAutomaticaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ServerNameSAP {
        get {
            return this.serverNameSAPField;
        }
        set {
            this.serverNameSAPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string TipoBaseSAP {
        get {
            return this.tipoBaseSAPField;
        }
        set {
            this.tipoBaseSAPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ServidorLicenciasSAP {
        get {
            return this.servidorLicenciasSAPField;
        }
        set {
            this.servidorLicenciasSAPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string BaseSAP {
        get {
            return this.baseSAPField;
        }
        set {
            this.baseSAPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UserSAP {
        get {
            return this.userSAPField;
        }
        set {
            this.userSAPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PassSAP {
        get {
            return this.passSAPField;
        }
        set {
            this.passSAPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UserSQL {
        get {
            return this.userSQLField;
        }
        set {
            this.userSQLField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PassSQL {
        get {
            return this.passSQLField;
        }
        set {
            this.passSQLField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class CONFIGURACIONVENTANA {
    
    private string colorField;
    
    private string sucursalField;
    
    private string indexID_CUVVField;
    
    private string cuentaLeadField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Color {
        get {
            return this.colorField;
        }
        set {
            this.colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Sucursal {
        get {
            return this.sucursalField;
        }
        set {
            this.sucursalField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string IndexID_CUVV {
        get {
            return this.indexID_CUVVField;
        }
        set {
            this.indexID_CUVVField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string CuentaLead {
        get {
            return this.cuentaLeadField;
        }
        set {
            this.cuentaLeadField = value;
        }
    }
}