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
    /// <summary>
    /// Класс контроллера настроек
    /// Контроллер получает путь с каталогом игры RRS
    /// И передает в UI пути к папкам игры
    /// </summary>
    public class ApplicationSettingsController
    {
        /// <summary>
        /// Модель настроек
        /// </summary>
        private readonly ApplicationSettingsModel settingsModel;

        /// <summary>
        /// Наименование еонфигурационного файла
        /// </summary>
        private readonly FileInfo configFileName;

        /// <summary>
        /// Конструктор
        /// Создаем экземпляр модели настроек
        /// И объявляем конфигурационный файл
        /// </summary>
        public ApplicationSettingsController()
        {
            settingsModel = new ApplicationSettingsModel();
            configFileName = new FileInfo("consistEdition.xml");
        }

        /// <summary>
        /// Метод передает в модель путь к установленной игре
        /// </summary>
        /// <returns>Возвращает результат операции</returns>
        public bool GetApplicationDirectory()
        {
            if (configFileName.Exists)//Проверяем существует ли конфигурационный файл
            {
                try
                {
                    //Создаем поток
                    XmlDocument xReader = new XmlDocument();
                    //Считываем содержимое XML файла
                    xReader.Load(configFileName.FullName);
                    // получим корневой элемент
                    XmlElement xRoot = xReader.DocumentElement;
                    //Проверяем существует ли путь и ведет ли он к установленной игре
                    if (xRoot.InnerText != null && Directory.Exists(xRoot.InnerText + @"/cfg"))
                    {
                        //Получаем путь к установленнойигре
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

        /// <summary>
        /// Метод возвращает кортеж путей к папкам установленной игры
        /// </summary>
        public (string, string) GetVehecleAndCoupleTypeDirrectores()
        {
            return ($"{settingsModel.AplicationDirectory}{settingsModel.VehecleDirrectory}", $"{settingsModel.AplicationDirectory}{settingsModel.CoupleTypeDirectory}");
        }

        /// <summary>
        /// Метов передает в модель путь с установленной игрой
        /// </summary>
        /// <param name="appDirectory">Путь к игре</param>
        public void SetApplicationDirectory(string appDirectory)
        {
            //Передаем в модлеь путь к игре
            settingsModel.AplicationDirectory = appDirectory;
            //Производи запись в файл конфигурации
            SaveConfigFile(configFileName, settingsModel.AplicationDirectory);
        }

        /// <summary>
        /// Метод записи конфигурации в XML файл
        /// </summary>
        /// <param name="fileName">Название файла конфигурации</param>
        /// <param name="appDirectory">Сохраняемый путь к игре</param>
        private void SaveConfigFile(FileInfo fileName, string appDirectory)
        {
            // Создаем новый Xml документ.
            var xWriter = new XmlDocument();
            // Создаем Xml заголовок.
            var xmlDeclaration = xWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            // Добавляем заголовок перед корневым элементом.
            xWriter.AppendChild(xmlDeclaration);
            // Создаем Корневой элемент
            var root = xWriter.CreateElement("ApplicationDirectory");
            root.InnerText = appDirectory;
            // Добавляем новый корневой элемент в документ.
            xWriter.AppendChild(root);
            // Сохраняем файл.
            xWriter.Save(fileName.FullName);
        }
    }
}
