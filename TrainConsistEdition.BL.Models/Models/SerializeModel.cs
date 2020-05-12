using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Models.Models
{
    public class SerializeModel
    {
        public XmlSerializerNamespaces NameSpace { get; }
        public XmlWriterSettings Settings { get; set; }

        public SerializeModel()
        {
            NameSpace = new XmlSerializerNamespaces();
            NameSpace.Add(string.Empty, string.Empty);


            Settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Document,
                OmitXmlDeclaration = false,
                Indent = true,
                IndentChars = "    "
            };
        }


        
    }
}
