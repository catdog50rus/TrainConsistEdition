using System.Collections.Generic;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    /// <summary>
    /// Контроллер проверяет на валидность данные полученные из файла
    /// В случае валидности метод возвращает true и текстовое сообщение "Ok"
    /// В случае выявление ошибок типов данных или не соответствия наименований зарезервированных слов, возвращается false и
    /// наименование параметра в котором возникла ошибка валидации
    /// </summary>
    public class DataCheckController
    {
        // Поля. 
        //Списки возможных значений свойств, определяемые игрой
        private readonly List<string> couplingTypes; // Список типов сцепок
        private readonly List<string> modulesCfg; // Списков конфигураций подвижного состава
        private readonly List<string> modules; //Список модулей подвижного состава
        
        /// <summary>
        /// Конструктор.
        /// Получает данные из установок RRS и заполняет ими списки 
        /// </summary>
        public DataCheckController()
        {
            
            var listData = SettingsController.GetListData(); //Получаем списки необходимых данных из установок приложения

            couplingTypes = listData.Item1; //Заполняем список типов сцепок

            //Список модулей подвижного состава заполняется из списка локомотивов, отбрасывая при необходимости номер локомотива
            //Один и тот же модуль локомотива соответствует нескольким однотипным локомотивам с разными номерами
            //Вагоны вне зависимости от модели используют один модуль, его добавляем отдельно
            modules = new List<string>(); //Создаем список модулей подвижного состава
            foreach (var moduleName in listData.Item2) // Перебирая модули локомотивов, заполняем список, отбрасывая номера
            {
                //TODO в будущем реализовать проверку на задвоенность, при появлении в игре однотипных локомотивов с разными номерами
                int pos = moduleName.LastIndexOf('-');
                if (pos == -1) pos = moduleName.Length;
                modules.Add(moduleName.Substring(0, pos));
            }
            modules.Add("passcar"); //Добавляем модуль вагонов

            modulesCfg = listData.Item2; //Заполняем список конфигураций локомотивами
            modulesCfg.AddRange(listData.Item3);//Заполняем список конфигураций вагонами
        }

        /// <summary>
        /// Метод проверяющий полученную модель из файла
        /// </summary>
        /// <param name="model">Модель состава</param>
        public (bool, string) IsValidModel(ConsistModel model)
        {
            //Разделяем данные на 2 потока
            //Описание свойств всего состава в xml файле соответствует тегу Common
            //и Список единиц подвижного состава (локомотивы / вагоны)

            TrainConsistInfoModel common = model.Common; //Выделяем из общей модели часть относящуюся к описанию свойств
            var resultValidCommon = IsValidCommon(common); //Проверяем на валидацию только свойства, получаем результат и сообщение
            if (!resultValidCommon.Item1) return (false, resultValidCommon.Item2);//Если свойства приходят с ошибкой прекращаем валидацию 
                                                                                  //и возвращаем имя параметра, не прошедшего валидацию

            List<TrainVehicleModel> veheclesList = model.Vehicle; // Выделяем из общей модели список подвижного состава
            var resiltValidVehcle = IsValidVehcle(veheclesList); //Проверяем на валидацию подвижной состав, получаем результат и текст ошибки
            if (!resiltValidVehcle.Item1) return (false, resiltValidVehcle.Item2);//Если в какой-то единице подвижного состава возникает ошибка,  
                                                                                  //прекращаем валидацию и возвращаем имя единицы состав и имя параметра, не прошедшего валидацию

            return (true, "Ok!"); //Если валидация прошла успешно, возвращаем true и Ok!
        }

        /// <summary>
        /// Метод валидации свойства поезда
        /// Наименование состава и описание не проверяем
        /// </summary>
        /// <param name="model">модель свойств поезда</param>
        /// <returns>Возвращает результат валидации и текст ошибки</returns>
        private (bool, string) IsValidCommon(TrainConsistInfoModel model)
        {
            bool result; //Объявляем переменную типа bool

            result = model.CabineInVehicle == 0 || model.CabineInVehicle == 1 ? true : false; //Проверяем значение свойства по возможным значениям
            if (!result) return (false, "CabineInVehicle False");

            result = model.ChargingPressure >= 0 && model.ChargingPressure <= 5.0 ? true : false; //Проверяем значение по вилке возможных значений
            if (!result) return (false, "ChargingPressure False");

            result = couplingTypes.Contains(model.CouplingModule) ? true : false; //Проверяем значение свойства по списку возможных значений
            if (!result) return (false, "CouplingModulee False");


            result = model.InitMainResPressure >= 0 && model.InitMainResPressure <= 5.0 ? true : false; //Проверяем значение по вилке возможных значений
            if (!result) return (false, "InitMainResPressure False");

            result = model.NoAir == 0 || model.NoAir == 1 ? true : false; //Проверяем значение свойства по возможным значениям
            if (!result) return (false, "NoAir False");
            
            return (true, "Ok");

        }

        /// <summary>
        /// Метод валидирует список подвижного состава из поезда
        /// </summary>
        /// <param name="modelsList">Список подвижного состава</param>
        /// <returns>Возвращает результат валидации и текст ошибки</returns>
        private (bool, string) IsValidVehcle(List<TrainVehicleModel> modelsList)
        {
            bool result; //Объявляем переменную типа bool

            foreach (var item in modelsList) //Запускаем перебор всех единиц подвижного состава из списка
            {
                result = item.Count > 0 ? true : false; // Проверяем, чтобы количество было больше 0
                if (!result) return (false, $"{item.ModuleConfig} Count False");

                result = item.PayloadCoeff >= 0 && item.PayloadCoeff <= 1.0 ? true : false; // Проверяем значение свойств по вилке возможных значений
                if (!result) return (false, $"{item.ModuleConfig} PayloadCoeff False");

                result = modulesCfg.Contains(item.ModuleConfig) ? true : false; //Проверяем значение свойства по списку возможных значений
                if (!result) return (false, $"{item.ModuleConfig} ModuleConfig False");


                result = modules.Contains(item.Module) || item.Module == "passcar" ? true : false; //Проверяем значение свойства по списку возможных значений
                if (!result) return (false, $"{item.Module} Module False");

            };

            return (true, "Ok");
        }
    }
}
