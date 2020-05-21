using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    public static class SettingsController
    {
        //Поля
        private static readonly string vehecleDirectory = @"\cfg\vehicles";
        private static readonly string coupleTypeDirectory = @"\cfg\couplings";
        private static readonly string trainsDirectory = @"\cfg\trains\";
        private static string pathRRS = "";


        //Методы public
        public static FileInfo GetConfigFile()
        {
            
            return new FileInfo("consistEdition.xml"); 
        }

        public static (bool, string) GetPathRRSTrains()
        {
            LoadConfigFile();
            if (pathRRS.Length > 0) return (true, $@"{pathRRS}{trainsDirectory}");
            else return (false, "Для начала работы приложения, пожалуйста, укажите в меню каталог с установленным RRS");
        }

        public static void SetPathRRS(string path)
        {
            SaveConfigFile(path);
        }

        private static void LoadConfigFile()
        {
            var configFile = GetConfigFile();
            if (configFile.Exists)//Проверяем существует ли конфигурационный файл
            {
                try
                {
                    //Создаем поток
                    XmlDocument xReader = new XmlDocument();
                    //Считываем содержимое XML файла
                    xReader.Load(configFile.FullName);
                    // получим корневой элемент
                    XmlElement xRoot = xReader.DocumentElement;
                    //Проверяем существует ли путь и ведет ли он к установленной игре
                    if (xRoot.InnerText != null && Directory.Exists(xRoot.InnerText + @"/cfg"))
                    {
                        //Получаем путь к установленнойигре
                        pathRRS = xRoot.InnerText;
                        
                    }
                   
                }
                catch (Exception)
                {
                    
                }
            }

        }

        public static (List<string>, List<string>, List<string>) GetListData()
        {
           
            List<string> couplingTypes = new List<string>();
            foreach (var item in new DirectoryInfo($"{pathRRS}{coupleTypeDirectory}").GetFiles())
            {
                couplingTypes.Add(GetListElement(item.ToString()));
            }
            
            List<string> loco = new List<string>();
            List<string> vagons = new List<string>();
            foreach (var item in GetList($@"{pathRRS}{vehecleDirectory}"))
            {
                //Отбираем какой подвижной состав относится к вагонам, а какой к локомотивам
                //Заполняем Vagons
                if (item.Contains("IMR") || (item.Contains("Fr")))
                {
                    vagons.Add(item);
                }
                //Заполняем Loco
                else
                {
                    loco.Add(item);
                }
            }
            return (couplingTypes, loco, vagons);
        }

        //Методы privet

        /// <summary>
        /// Метод записи конфигурации в XML файл
        /// </summary>
        /// <param name="configFile">Название файла конфигурации</param>
        /// <param name="pathRRS">Сохраняемый путь к игре</param>
        private static void SaveConfigFile(string pathRRS)
        {
            // Создаем новый Xml документ.
            var xWriter = new XmlDocument();
            // Создаем Xml заголовок.
            var xmlDeclaration = xWriter.CreateXmlDeclaration("1.0", "UTF-8", null);
            // Добавляем заголовок перед корневым элементом.
            xWriter.AppendChild(xmlDeclaration);
            // Создаем Корневой элемент
            var root = xWriter.CreateElement("ApplicationDirectory");
            root.InnerText = pathRRS;
            // Добавляем новый корневой элемент в документ.
            xWriter.AppendChild(root);
            // Сохраняем файл.
            xWriter.Save(GetConfigFile().FullName);
        }

        private static List<string> GetList(string path)
        {
            List<string> result = new List<string>();
            //Путь к папке
            var folder = new DirectoryInfo(path);
            foreach (var item in folder.GetDirectories())
            {
                result.Add(item.ToString());
            }
            return result;


        }

        /// <summary>
        /// Метод убирающий расширение xml из имени файла
        /// </summary>
        /// <param name="item">элемент коллекции имен файлов</param>
        private static string GetListElement(string item)
        {
            return item.TrimEnd('.', 'x', 'm', 'l');
        }

    }
}
