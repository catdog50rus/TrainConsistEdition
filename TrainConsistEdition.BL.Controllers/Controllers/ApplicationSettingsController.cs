using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TrainConsistEdition.BL.Models.Models;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    public class ApplicationSettingsController
    {
        private readonly ApplicationSettingsModel settingsModel;

        private readonly FileInfo configFileName;

        public ApplicationSettingsController()
        {
            settingsModel = new ApplicationSettingsModel();
            configFileName = new FileInfo("consistEdition.xml");


        }

        public bool GetApplicationDirectory()
        {
            if (configFileName.Exists)
            {
                try
                {

                    XmlDocument xReader = new XmlDocument();
                    xReader.Load(configFileName.FullName);
                    // получим корневой элемент
                    XmlElement xRoot = xReader.DocumentElement;
                    if (xRoot.InnerText != null)
                    {
                        settingsModel.AplicationDirectory = xRoot.InnerText;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public (string, string) GetVehecleAndCoupleTypeDirrectores()
        {
            return ($"{settingsModel.AplicationDirectory}{settingsModel.VehecleDirrectory}", $"{settingsModel.AplicationDirectory}{settingsModel.CoupleTypeDirectory}");
        }

        public void SetApplicationDirectory(string appDirectory)
        {
                            settingsModel.AplicationDirectory = appDirectory;

                SetApplicationDirectory(configFileName, settingsModel.AplicationDirectory);
                
            
           
            
        }


        public void SetApplicationDirectory(FileInfo fileName, string appDirectory)
        {
            // Создаем новый Xml документ.
            var doc = new XmlDocument();

            // Создаем Xml заголовок.
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            // Добавляем заголовок перед корневым элементом.
            doc.AppendChild(xmlDeclaration);

            // Создаем Корневой элемент
            var root = doc.CreateElement("ApplicationDirectory");

            root.InnerText = appDirectory;
           
            // Добавляем новый корневой элемент в документ.
            doc.AppendChild(root);

            // Сохраняем документ.
            doc.Save(fileName.FullName);
        }

        
    }
}
