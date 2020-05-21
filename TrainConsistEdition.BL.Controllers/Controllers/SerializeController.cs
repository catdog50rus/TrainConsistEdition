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
            try
            {
                //Создаем экземпляр сериализатора
                var formatter = new XmlSerializer(typeof(ConsistModel));
                //Задаем полное имя итогового XML файла
                //var fileName = path + this.filename + ".xml";
                //Создаем файловый поток
                var fs = new FileStream(filename, FileMode.Create);
                //Создаем экземпляр XMLWriter на основе файлового потока и модели настроек сериализации
                XmlWriter xw = XmlWriter.Create(fs, serializeModel.Settings);
                //Сериализуем итоговый XML файл
                formatter.Serialize(xw, consistModel, serializeModel.NameSpace);
                //Закрываем поток
                fs.Close();
                //Возвращаем флаг успеха сериализации
                return (true, "Состав успешно создан!");
            }
            catch (Exception)
            {
                //Возвращаем флаг неудачи, если что-то пошло не так
                return (false, "Не удалось сформировать состав!");
            }          
        }

        /// <summary>
        /// Метод десериализует данные модели из файла xml
        /// </summary>
        /// <returns>Возвращает кортеж (Модель, результат выполнения, сообщение)</returns>
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
                //Проверяем полученную модель на валидность
                var dataCheck = new DataCheckController();
                var isValidModel = dataCheck.IsValidModel(model);
                //var isValidModel = IsValidModel(model);
                if (isValidModel.Item1)
                {
                    //Возвращаем кортеж данных
                    return (model, true, isValidModel.Item2);
                }
                else
                {
                    return (consistModel, false, isValidModel.Item2);
                }
                
            }
            catch (Exception)
            {

                return (consistModel, false, "Не удалось преобразовать файл! Убедитесь, что открываемый файл соответствует файлу состава.");
            }
        }

        /*

        /// <summary>
        /// Метод прверяющий полученную модель из файла
        /// </summary>
        /// <param name="model">Модель состава</param>
        private (bool, string) IsValidModel(ConsistModel model)
        {
            var resultValidCommon = IsValidCommon(model);
            if (!resultValidCommon.Item1) return (false, resultValidCommon.Item2);

            var resiltValidVehcle = IsValidVehcle(model);
            if (!resiltValidVehcle.Item1) return (false, resiltValidVehcle.Item2);
            return (true, "Ok!");
        }

        private (bool, string) IsValidCommon(ConsistModel model)
        {
            bool result;

            result = model.Common.CabineInVehicle == 0 || model.Common.CabineInVehicle == 0 ? true : false;
            if(!result) return (false, "CabineInVehicle False");

            result = model.Common.ChargingPressure >= 0 || model.Common.ChargingPressure <= 5.0 ? true : false;
            if (!result) return (false, "ChargingPressure False");

            result = couplingTypes.Contains(model.Common.CouplingModule) ? true : false;
            if (!result) return (false, "CouplingModulee False");
            

            result = model.Common.InitMainResPressure >= 0 || model.Common.InitMainResPressure <= 5.0 ? true : false;
            if (!result) return (false, "InitMainResPressure False");

            result = model.Common.NoAir == 0 || model.Common.NoAir == 1 ? true : false;
            if (!result) return (false, "NoAir False");
            return (true, "Ok");
                        
        }
        private (bool, string) IsValidVehcle(ConsistModel model)
        {
            bool result;
            List<TrainVehicleModel> modelsList = model.Vehicle;
            foreach (var item in modelsList)
            {
                result = item.Count > 0 ? true : false;
                if (!result) return (false, $"{item.ModuleConfig} Count False");

                result = item.PayloadCoeff >= 0 || item.PayloadCoeff <= 1.0 ? true : false;
                if (!result) return (false, $"{item.ModuleConfig} PayloadCoeff False");

                result = modulesCfg.Contains(item.ModuleConfig) ? true : false;
                if (!result) return (false, $"{item.ModuleConfig} ModuleConfig False");


                result = modules.Contains(item.Module) || item.Module == "passcar" ? true : false;
                if (!result) return (false, $"{item.Module} Module False");
                
            };
            
            return (true, "Ok");
        }
        */
    }
}
