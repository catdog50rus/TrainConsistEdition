using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TrainConsistEdition.BL.Models.Models;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    /// <summary>
    /// Класс контроллера сериализации в XML файл
    /// </summary>
    public class SerializeController
    {
        /// <summary>
        /// Наименование XML файла выбранное пользователем
        /// </summary>
        private readonly string filename;

        /// <summary>
        /// Объявление модели конечного состава пезда
        /// </summary>
        private readonly ConsistModel consistModel;

        /// <summary>
        /// Объявление модели настроек сериализации
        /// </summary>
        private readonly SerializeModel serializeModel;

        /// <summary>
        /// Конструктор принимает 2 параметра и нициализирует модели
        /// </summary>
        /// <param name="controller">Контроллер создания подвижного состава</param>
        /// <param name="fileName">Имя XML файла, выбранное пользователем</param>
        public SerializeController(CreateConsistController controller, string filename)
        {
            this.consistModel = controller.GetConsistModel();//Получаем итоговую модель поезда из контроллера
            
            this.filename = filename;//Получае имя XML файла
            
            this.serializeModel = new SerializeModel();//Инициализируем модель настроек сериализации
        }

        /// <summary>
        /// Конструктор принимает полный путь к файу
        /// </summary>
        /// <param name="filename">путь к файлу</param>
        public SerializeController(string filename)
        {
            consistModel = new ConsistModel();
            this.filename = filename;
        }


        /// <summary>
        /// Метод сериализирует выбранную модель состава в XML файл
        /// </summary>
        /// <param name="path">Путь к папке с составами</param>
        /// <returns>Возвращает результат сериалазации true или false</returns>
        public (bool,string) SerializeConsist()
        {
            FileStream fs = null;
            try
            {
                var formatter = new XmlSerializer(typeof(ConsistModel));//Создаем экземпляр сериализатора
                
                fs = new FileStream(filename, FileMode.Create); //Создаем файловый поток
                
                XmlWriter xw = XmlWriter.Create(fs, serializeModel.Settings);//Создаем экземпляр XMLWriter на основе файлового потока и модели настроек сериализации
                
                formatter.Serialize(xw, consistModel, serializeModel.NameSpace); //Сериализуем модель в XML файл
                
                fs.Close(); //Закрываем поток
                
                return (true, "Состав успешно создан!"); //Возвращаем кортеж данных
            }
            catch (Exception) //Возвращаем кортеж данных в случае ошибки
            {
                return (false, "Не удалось сформировать состав!");
            }
            finally //Если поток открыть, закрываем его
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// Метод десериализует данные модели из файла xml
        /// </summary>
        /// <returns>Возвращает кортеж (Модель, результат выполнения, сообщение)</returns>
        public (ConsistModel, bool, string) OpenConsist()
        {
            FileStream fs = null;
            try
            {
                
                var formatter = new XmlSerializer(typeof(ConsistModel)); //Создаем экземпляр сериализатора
                
                fs = new FileStream(filename, FileMode.Open); //Создаем файловый поток
                
                XmlReader xr = XmlReader.Create(fs);//Создаем экземпляр XmlReader на основе файлового потока
                
                var model = (ConsistModel)formatter.Deserialize(xr); //Получаем модель состава из XML файла
                
                fs.Close();//Закрываем поток

                //Проверяем полученную модель на валидность и возвращаем кортеж данных
                var dataCheck = new DataCheckController(); //Создаем контроллер
                var isValidModel = dataCheck.IsValidModel(model); //Вызываем метод проверки на валидацию модели
                
                if (isValidModel.Item1) //Если модель прошла валидацию
                {
                    return (model, true, isValidModel.Item2); 
                }
                else //Если модель не прошла валидацию
                {
                    return (consistModel, false, isValidModel.Item2);
                }
                
            }
            catch (Exception) //В случае ошибки десериализации возвращаем кортеж данных и текст ошибки, выводимы пользователю
            {
                
                return (consistModel, false, "Не удалось преобразовать файл!\r\nУбедитесь, что открываемый файл соответствует файлу состава или данные в нем несоответствуют типу.");
            }
            finally //Закрываем файл, если он открыт 
            {
                if (fs != null) fs.Close();
            }

        }

    }
}
