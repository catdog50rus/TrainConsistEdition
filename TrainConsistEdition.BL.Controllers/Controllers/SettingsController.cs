using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Метод возвращает конфигурационный файл приложения
        /// </summary>
        public static FileInfo GetConfigFile()
        {
            return new FileInfo("consistEdition.xml"); 
        }

        /// <summary>
        /// Метод возвращающий путь к папке с составами в игре
        /// </summary>
        public static (bool, string) GetPathRRSTrains()
        {
            LoadConfigFile(); //Загружаем путь к RRS из конфигурационного файла
            string dir = $@"{pathRRS}{trainsDirectory}";
            if (Directory.Exists(dir)) return (true, dir); //Если такая директория существует, возвращаем путь к ней
            else return (false, "Для начала работы приложения, пожалуйста, укажите в меню каталог с установленным RRS");
        }

        /// <summary>
        /// Метод устанавливает директорию RRS в конфигурационный файл
        /// </summary>
        /// <param name="path">Путь</param>
        public static void SetPathRRS(string path)
        {
            SaveConfigFile(path); //Вызываем метод записывающий данные в файл конфигурации
        }

        /// <summary>
        /// Метод передает списки с данными установленной RRS
        /// </summary>
        public static (List<string>, List<string>, List<string>) GetListData()
        {
            //Создаем и заполняем список типов сцепок
            List<string> couplingTypes = new List<string>(); 
            //Циклом проходимся по списку файлов в директории
            //Заполняем список, обрубая расширения файлов
            foreach (var item in new DirectoryInfo($"{pathRRS}{coupleTypeDirectory}").GetFiles())
            {
                couplingTypes.Add(GetListElement(item.Name));
            }
            
            List<string> loco = new List<string>(); //Создаем список локомотивов в игре
            List<string> vagons = new List<string>(); //Создаем список вагонов в игре

            //Отбираем какой подвижной состав относится к вагонам, а какой к локомотивам
            foreach (var item in GetList($@"{pathRRS}{vehecleDirectory}")) //Запускаем цикл по директории с подвижным составом игры
            {
                if (item.Contains("IMR") || (item.Contains("Fr"))) //Заполняем список вагонов
                {
                    vagons.Add(item);
                }
                else //Заполняем список локомотивов
                {
                    loco.Add(item);
                }
            }
            return (couplingTypes, loco, vagons); //Возвращаем кортеж списков
        }

        //Методы privet

        /// <summary>
        /// Метод считывающий данные из файла конфигурации
        /// </summary>
        private static void LoadConfigFile()
        {
            var configFile = GetConfigFile(); //Получаем файл конфигурации
            if (configFile.Exists)//Проверяем существует ли конфигурационный файл
            {
                try
                {
                    XmlDocument xReader = new XmlDocument(); //Создаем поток

                    xReader.Load(configFile.FullName); //Считываем содержимое XML файла

                    XmlElement xRoot = xReader.DocumentElement; // получим корневой элемент

                    //Проверяем существует ли путь и ведет ли он к установленной игре
                    if (Directory.Exists(xRoot.InnerText) && Directory.Exists(xRoot.InnerText + @"/cfg"))
                    {
                        pathRRS = xRoot.InnerText; //Получаем путь к установленной игре
                    }
                }
                catch (Exception)
                {

                }
            }

        }

        /// <summary>
        /// Метод записи конфигурации в XML файл
        /// </summary>
        /// <param name="configFile">Название файла конфигурации</param>
        /// <param name="pathRRS">Сохраняемый путь к игре</param>
        private static void SaveConfigFile(string pathRRS)
        {
            
            var xWriter = new XmlDocument();// Создаем новый Xml документ.
            
            var xmlDeclaration = xWriter.CreateXmlDeclaration("1.0", "UTF-8", null); // Создаем Xml заголовок.
            
            xWriter.AppendChild(xmlDeclaration); // Добавляем заголовок перед корневым элементом.
           
            var root = xWriter.CreateElement("ApplicationDirectory"); // Создаем Корневой элемент
            root.InnerText = pathRRS; // Помещаем путь в созданный элемент
            
            xWriter.AppendChild(root); // Добавляем новый корневой элемент в документ.
            
            xWriter.Save(GetConfigFile().FullName); // Сохраняем файл.
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
            return item.TrimEnd('.', 'x', 'm', 'l');
        }

    }
}
