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
            //Получаем итоговую модель поезда из контроллера
            this.consistModel = controller.GetConsistModel();
            //Получае имя XML файла
            this.filename = filename;
            //Инициализируем моделль настроек сериализации
            this.serializeModel = new SerializeModel();
        }

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
        public bool SerializeConsist(string path)
        {
            try
            {
                //Создаем экземпляр сериализатора
                var formatter = new XmlSerializer(typeof(ConsistModel));
                //Задаем полное имя итогового XML файла
                var fileName = path + this.filename + ".xml";
                //Создаем файловый поток
                var fs = new FileStream(fileName, FileMode.Create);
                //Создаем экземпляр XMLWriter на основе файлового потока и модели настроек сериализации
                XmlWriter xw = XmlWriter.Create(fs, serializeModel.Settings);
                //Сериализуем итоговый XML файл
                formatter.Serialize(xw, consistModel, serializeModel.NameSpace);
                //Закрываем поток
                fs.Close();
                //Возвращаем флаг успеха сериализации
                return true;
            }
            catch (Exception)
            {
                //Возвращаем флаг неудачи, если что-то пошло не так
                return false;
            }          
        }

        public (ConsistModel, bool, string) OpenConsist()
        {
            try
            {
                //Создаем экземпляр сериализатора
                var formatter = new XmlSerializer(typeof(ConsistModel));
                //Создаем файловый поток
                var fs = new FileStream(filename, FileMode.Open);
                //Создаем экземпляр XmlReader на основе файлового потока и модели настроек сериализации
                XmlReader xr = XmlReader.Create(fs);
                //Сериализуем итоговый XML файл
                var model = (ConsistModel)formatter.Deserialize(xr);
                //Закрываем поток
                fs.Close();
                //Возвращаем флаг успеха сериализации
                return (model, true, "Ok!");
            }
            catch (Exception)
            {

                return (consistModel, false, "Не удалось преобразовать файл! Убедитесь, что открываемый файл соответствует файлу состава.");
            }
        }

    }
}
