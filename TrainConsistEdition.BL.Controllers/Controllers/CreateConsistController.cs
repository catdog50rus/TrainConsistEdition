using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    /// <summary>
    /// Класс основного контроллера.
    /// Формирует состав поезда на основе классов моделей данных
    /// </summary>
    public class CreateConsistController
    {
        /// <summary>
        /// Объявляем модель свойств и характеристик состава
        /// </summary>
        private readonly TrainConsistInfoModel consistModel;

        /// <summary>
        /// Объявляем список единиц состава поезда
        /// </summary>
        private readonly List<TrainVehicleModel> _listVehicles;

        /// <summary>
        /// Объявляем модель итогового состава. Эти данные будут сериализированы в итоговый XML файл
        /// </summary>
        private readonly ConsistModel serializeModel;

        /// <summary>
        /// Конструктор инициализирует объявленные модели
        /// </summary>
        public CreateConsistController()
        {
            
            consistModel = new TrainConsistInfoModel();
          
            _listVehicles = new List<TrainVehicleModel>();
            serializeModel = new ConsistModel();

        }

        /// <summary>
        /// Метод добавляет в модель свойств и характеристик состава данные введенные пользователем
        /// </summary>
        /// <param name="title">Наименование поезда, отображается в клиенте игры</param>
        /// <param name="description">Описание поезда, отображается в клиенте игры</param>
        /// <param name="couplingModule">Модуль сцепки</param>
        /// <param name="cabinInVehicle">Кабина управления</param>
        /// <param name="chargingPressure">Зарядное давление</param>
        /// <param name="intMainResPressure">Начальное давление в главном резервуаре</param>
        /// <param name="noAir">Флаг, поезд без воздуха</param>
        public void AddConsistOptions(string title, 
                                      string description,
                                      string couplingModule,
                                      int cabinInVehicle, 
                                      double chargingPressure,
                                      double intMainResPressure, 
                                      bool noAir)
        {
            //Добавляем данные поезда в модель
            consistModel.Title = title;
            consistModel.Description = description;
            consistModel.CabineInVehicle = cabinInVehicle;
            consistModel.CouplingModule = couplingModule;
            consistModel.ChargingPressure = chargingPressure;
            consistModel.InitMainResPressure = intMainResPressure;
            consistModel.NoAir = noAir;

            //Передаем в итоговый состав модель свойств и характеристик
            serializeModel.Common = consistModel;
        }

        /// <summary>
        /// Метод добавляет единицу состава (вагон или локомотив)
        /// </summary>
        /// <param name="module">Модуль вагона/локомотива</param>
        /// <param name="moduleConfig">Конфигурационный файл вагона/локомотива</param>
        /// <param name="count">Количество прицепленных подряд вагонов/локомотивов</param>
        /// <param name="payloadCoeff">Коэффициент загруженности вагона</param>
        public void AddTrainVehicle(string module, 
                                    string moduleConfig,
                                    int count,
                                    double payloadCoeff)
        {

            //Объявляем модель единицы подвижного состава и инициализируем ее данными пользователя
            var vehicle = new TrainVehicleModel()
            {
                Module = module,
                ModuleConfig = moduleConfig,
                Count = count,
                PayloadCoeff = payloadCoeff
            };
            
            //Вносим единицу состава в список подвижного состава поезда
            _listVehicles.Add(vehicle);
        }

        /// <summary>
        /// Метод удаления единицы подвижного состава из поезда
        /// </summary>
        /// <param name="index">Индекс удаляемой единицы</param>
        public void RemoveTrainVehicle(int index)
        {
            _listVehicles.RemoveAt(index);
        }

        /// <summary>
        /// Метод добавляет в итоговый состав поезда все выбранные пользователем единицы подвижного состава
        /// </summary>
        /// <returns>Метод возвращает модель состава полностью готового к сериализации в XML файл</returns>
        public ConsistModel GetConsistModel()
        {
            serializeModel.Vehicle = _listVehicles;
            return serializeModel;
        }
    }
}
