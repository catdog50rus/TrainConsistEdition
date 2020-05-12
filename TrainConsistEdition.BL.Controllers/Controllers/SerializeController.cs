using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TrainConsistEdition.BL.Models.Models;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    public class SerializeController
    {

        private readonly string fileName;

        private readonly ConsistModel consistModel;
        private readonly SerializeModel serializeModel;

        public SerializeController(CreateConsistController controller, string fileName)
        {
            this.consistModel = controller.GetConsistModel();
            this.fileName = fileName;
            this.serializeModel = new SerializeModel();
        }

        public bool SerializeConsist(string path = @"C:\Users\2334\AppData\Local\RRS\cfg\trains")
        {
            try
            {
                var formatter = new XmlSerializer(typeof(ConsistModel));
                
                var fileName = path + @"\" + this.fileName + ".xml";

                var fs = new FileStream(fileName, FileMode.Create);
                XmlWriter xw = XmlWriter.Create(fs, serializeModel.Settings);

                formatter.Serialize(xw, consistModel, serializeModel.NameSpace);

                fs.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }          
        }

    }
}
