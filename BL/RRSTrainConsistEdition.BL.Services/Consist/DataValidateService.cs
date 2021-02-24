using RRSTrainConsistEdition.Core.Models;
using RRSTrainConsistEdition.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RRSTrainConsistEdition.BL.Services.Consist
{
    public class DataValidateService : IDataValidateService
    {
        // Поля. 
        //Списки возможных значений свойств, определяемые игрой
        private readonly List<string> couplingTypes; // Список типов сцепок
        private readonly List<string> modulesCfg; // Списков конфигураций подвижного состава
        private readonly List<string> modules; //Список модулей подвижного состава
        private readonly ISettingService _setting;

        /// <summary>
        /// Конструктор.
        /// Получает данные из установок RRS и заполняет ими списки 
        /// </summary>
        public DataValidateService(ISettingService settingService)
        {
            _setting = settingService;
            var listData = _setting.GetListData(); //Получаем списки необходимых данных из установок приложения

            couplingTypes = listData.Item1.ToList(); //Заполняем список типов сцепок

            //Список модулей подвижного состава заполняется из списка локомотивов, отбрасывая при необходимости номер локомотива
            //Один и тот же модуль локомотива соответствует нескольким однотипным локомотивам с разными номерами
            //Вагоны вне зависимости от модели используют один модуль, его добавляем отдельно
            modules = new List<string>(); //Создаем список модулей подвижного состава
            foreach (var moduleName in listData.Item2) // Перебирая модули локомотивов, заполняем список, отбрасывая номера
            {
                //TODO в будущем реализовать проверку на задвоенность, при появлении в игре однотипных локомотивов с разными номерами
                int pos = moduleName.LastIndexOf('-');
                if (pos == -1) pos = moduleName.Length;
                modules.Add(moduleName.Substring(0, pos).ToLower());
            }
            modules.Add("passcar"); //Добавляем модуль вагонов

            modulesCfg = listData.Item2.ToList(); //Заполняем список конфигураций локомотивами
            modulesCfg.AddRange(listData.Item3);//Заполняем список конфигураций вагонами
        }

        /// <summary>
        /// Метод проверяющий полученную модель из файла
        /// </summary>
        /// <param name="model">Модель состава</param>
        public void IsValidModel(IConsist model)
        {
            var common = model.Common; 
            IsValidCommon(common);

            IEnumerable<TrainVehicle> veheclesList = model.Vehicle; 
            IsValidVehcle(veheclesList); 
        }

        /// <summary>
        /// Метод валидации свойства поезда
        /// Наименование состава и описание не проверяем
        /// </summary>
        /// <param name="model">модель свойств поезда</param>
        /// <returns>Возвращает результат валидации и текст ошибки</returns>
        private void IsValidCommon(TrainConsistInfo model)
        {
            bool result = model.CabineInVehicle == 0 || model.CabineInVehicle == 1;// ? true : false; //Проверяем значение свойства по возможным значениям
            if (!result) throw new ArgumentException("CabineInVehicle False");

            result = model.ChargingPressure >= 0 && model.ChargingPressure <= 5.0;// ? true : false; //Проверяем значение по вилке возможных значений
            if (!result) throw new ArgumentException("ChargingPressure False");

            //result = couplingTypes.Contains(model.CouplingModule) ? true : false; //Проверяем значение свойства по списку возможных значений
            bool isCouplingType = false;
            foreach (var item in couplingTypes)
            {
                isCouplingType = item.Contains(model.CouplingModule);
                if (isCouplingType) break;
            }
            result = isCouplingType;
            if (!result) throw new ArgumentException("CouplingModule False");


            result = model.InitMainResPressure >= 0 && model.InitMainResPressure <= 5.0;// ? true : false; //Проверяем значение по вилке возможных значений
            if (!result) throw new ArgumentException("InitMainResPressure False");

            result = model.NoAir == 0 || model.NoAir == 1;// ? true : false; //Проверяем значение свойства по возможным значениям
            if (!result) throw new ArgumentException("NoAir False");
        }

        /// <summary>
        /// Метод валидирует список подвижного состава из поезда
        /// </summary>
        /// <param name="modelsList">Список подвижного состава</param>
        /// <returns>Возвращает результат валидации и текст ошибки</returns>
        private void IsValidVehcle(IEnumerable<TrainVehicle> modelsList)
        {
            bool result; //Объявляем переменную типа bool

            foreach (var item in modelsList) //Запускаем перебор всех единиц подвижного состава из списка
            {
                result = item.Count > 0;// ? true : false; // Проверяем, чтобы количество было больше 0
                if (!result) throw new ArgumentException($"{item.ModuleConfig} Count False");

                result = item.PayloadCoeff >= 0 && item.PayloadCoeff <= 1.0;// ? true : false; // Проверяем значение свойств по вилке возможных значений
                if (!result) throw new ArgumentException($"{item.ModuleConfig} PayloadCoeff False");

                result = modulesCfg.Contains(item.ModuleConfig);// ? true : false; //Проверяем значение свойства по списку возможных значений
                if (!result) throw new ArgumentException($"{item.ModuleConfig} ModuleConfig False");


                result = modules.Contains(item.Module) || item.Module == "passcar";// ? true : false; //Проверяем значение свойства по списку возможных значений
                if (!result) throw new ArgumentException($"{item.Module} Module False");

            };
        }
    }
}
