using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem
{
    internal class XMLClasses
    {
    }

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class dataBase
    {

        private dataBaseFaculty[] facultyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("faculty")]
        public dataBaseFaculty[] faculty
        {
            get
            {
                return this.facultyField;
            }
            set
            {
                this.facultyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class dataBaseFaculty
    {

        private dataBaseFacultyChair[] chairField;

        private string fACULTYField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("chair")]
        public dataBaseFacultyChair[] chair
        {
            get
            {
                return this.chairField;
            }
            set
            {
                this.chairField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FACULTY
        {
            get
            {
                return this.fACULTYField;
            }
            set
            {
                this.fACULTYField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class dataBaseFacultyChair
    {

        private dataBaseFacultyChairResearcher[] researcherField;

        private string cHAIRField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("researcher")]
        public dataBaseFacultyChairResearcher[] researcher
        {
            get
            {
                return this.researcherField;
            }
            set
            {
                this.researcherField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CHAIR
        {
            get
            {
                return this.cHAIRField;
            }
            set
            {
                this.cHAIRField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class dataBaseFacultyChairResearcher
    {

        private string nameField;

        private string surnameField;

        private string degreeField;

        private string dateofreceivingField;

        private byte iDENTField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string surname
        {
            get
            {
                return this.surnameField;
            }
            set
            {
                this.surnameField = value;
            }
        }

        /// <remarks/>
        public string degree
        {
            get
            {
                return this.degreeField;
            }
            set
            {
                this.degreeField = value;
            }
        }

        /// <remarks/>
        public string dateofreceiving
        {
            get
            {
                return this.dateofreceivingField;
            }
            set
            {
                this.dateofreceivingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte IDENT
        {
            get
            {
                return this.iDENTField;
            }
            set
            {
                this.iDENTField = value;
            }
        }
    }
}