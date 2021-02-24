using RRSTrainConsistEdition.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RRSTrainConsistEdition.BL.Services.Settings
{
    public class SettingService : ISettingService
    {
        //Поля
        private const string _vehecleDirectory = @"\cfg\vehicles";
        private const string _coupleTypeDirectory = @"\cfg\couplings";
        private const string _trainsDirectory = @"\cfg\trains\";
        private const string _settingFileName = "consistEdition.xml";
        private static string pathRRS = "";


        //Методы public

        /// <summary>
        /// Метод возвращающий путь к папке с составами в игре
        /// </summary>
        public string GetPathRRSTrains()
        {
            LoadConfigFile(); 
            string dir = $@"{pathRRS}{_trainsDirectory}";
            if (!Directory.Exists(dir)) 
                throw new DirectoryNotFoundException("Для начала работы приложения, пожалуйста, укажите в меню каталог с установленным RRS");
            return dir;
        }

        /// <summary>
        /// Метод устанавливает директорию RRS в конфигурационный файл
        /// </summary>
        /// <param name="path">Путь</param>
        public void SetPathRRS(string path)
        {
            SaveConfigFile(path); 
        }

        /// <summary>
        /// Метод передает списки с данными установленной RRS
        /// </summary>
        public (IEnumerable<string>, IEnumerable<string>, IEnumerable<string>) GetListData()
        {
            //Создаем и заполняем список типов сцепок
            List<string> couplingTypes = new List<string>();

            foreach (var item in Directory.GetFiles($"{pathRRS}{_coupleTypeDirectory}"))
            {
                var name = GetListElement(item);
                couplingTypes.Add(name);
            }

            var vehicleList = GetList($@"{pathRRS}{_vehecleDirectory}");
            //Создаем список локомотивов в игре
            var loco = new List<string>(); 
            //Создаем список вагонов в игре
            var vagons = new List<string>(); 

            
            foreach (var item in vehicleList)
            {
                if (item.Contains("IMR") || (item.Contains("Fr")))
                {
                    vagons.Add(item);
                }
                else
                {
                    loco.Add(item);
                }
            }
            return (couplingTypes, loco, vagons);
        }

        #region Реализация

        /// <summary>
        /// Получить путь к игре из файла конфигурации
        /// </summary>
        private void LoadConfigFile()
        {
            var configfile = GetConfigFilePath();
            if (!configfile.Exists)
                throw new FileNotFoundException("Не найден файл конфигурации");

            try
            {
                var xReader = new XmlDocument();
                xReader.Load(configfile.FullName);
                var xRoot = xReader.DocumentElement;

                //Проверяем существует ли путь и ведет ли он к установленной игре
                if (Directory.Exists(xRoot.InnerText) && Directory.Exists(xRoot.InnerText + @"/cfg"))
                {
                    //Получаем путь к установленной игре
                    pathRRS = xRoot.InnerText;
                }
            }
            catch (Exception)
            {
                throw new FileLoadException("Ошибка чтения файла конфигурации");
            }
        }


        /// <summary>
        /// Записать в XML файл конфигурации
        /// </summary>
        /// <param name="pathRRS">Сохраняемый путь к игре</param>
        private void SaveConfigFile(string pathRRS)
        {
            try
            {
                var configFile = GetConfigFilePath();
                var xWriter = new XmlDocument();
                var xmlDeclaration = xWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
                xWriter.AppendChild(xmlDeclaration);

                var root = xWriter.CreateElement("ApplicationDirectory");
                root.InnerText = pathRRS;

                xWriter.AppendChild(root);

                xWriter.Save(configFile.FullName);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось записать файл конфигурации", ex);
            }

        }

        /// <summary>
        /// Вспомогательный метод создающий список поддиректорий по нужному пути
        /// </summary>
        private static List<string> GetList(string path)
        {
            List<string> result = new List<string>();

            //Проходим циклом по поддиректориям, заполняя список наименованиями поддиректорий
            foreach (var item in new DirectoryInfo(path).GetDirectories())
            {
                result.Add(item.Name);
            }
            return result;


        }

        /// <summary>
        /// Метод убирающий расширение xml из имени файла
        /// </summary>
        /// <param name="item">элемент коллекции имен файлов</param>
        private static string GetListElement(string item)
        {
            return new FileInfo(item).Name.TrimEnd('.', 'x', 'm', 'l');
            //return item.TrimEnd('.', 'x', 'm', 'l');
        }

        private FileInfo GetConfigFilePath()
        {
            return new FileInfo(_settingFileName);
        }

        #endregion


    }
}
