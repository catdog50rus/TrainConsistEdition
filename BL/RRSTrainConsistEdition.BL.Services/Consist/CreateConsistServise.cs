using RRSTrainConsistEdition.Core.Models;
using RRSTrainConsistEdition.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RRSTrainConsistEdition.BL.Services.Consist
{
    public class CreateConsistServise : ICreateConsistService
    {
        /// <summary>
        /// Объявляем модель свойств и характеристик состава
        /// </summary>
        private TrainConsistInfo _consistInfo;

        /// <summary>
        /// Объявляем список единиц состава поезда
        /// </summary>
        private List<TrainVehicle> _listVehicles;

        /// <summary>
        /// Объявляем модель итогового состава. Эти данные будут сериализованы в итоговый XML файл
        /// </summary>
        private IConsist _consist;

        private readonly ISerializeService _serialize;

        /// <summary>
        /// Конструктор принимает модель состава и разделяют данные по моделям описание состава и список единиц подвижного состава
        /// </summary>
        /// <param name="model"></param>
        public CreateConsistServise(IConsist model, ISerializeService serialize)
        {
            _listVehicles = new List<TrainVehicle>();
            _consistInfo = model.Common;
            if(model.Vehicle != null)
                _listVehicles = model.Vehicle.ToList();
            _consist = model;
            _serialize = serialize;
        }

        public void AddConsistOptions(TrainConsistInfo consistInfo)
        {
            _consistInfo = consistInfo;

        }

        /// <summary>
        /// Метод добавляет единицу состава (вагон или локомотив)
        /// </summary>
        /// <param name="module">Модуль вагона/локомотива</param>
        /// <param name="moduleConfig">Конфигурационный файл вагона/локомотива</param>
        /// <param name="count">Количество прицепленных подряд вагонов/локомотивов</param>
        /// <param name="payloadCoeff">Коэффициент загруженности вагона</param>
        public void AddTrainVehicle(TrainVehicle vehicle)
        {
            _listVehicles.Add(vehicle);
            
        }

        /// <summary>
        /// Метод удаления единицы подвижного состава из поезда
        /// </summary>
        /// <param name="index">Индекс удаляемой единицы</param>
        public void RemoveTrainVehicle(int index)
        {
            if (index < _listVehicles.Count && index >= 0)
            { 
                _listVehicles.RemoveAt(index);
            }
            else
            {
                throw new ArgumentException("Не удалось отредактировать состав!");
            }
        }

        /// <summary>
        /// Метод редактирования количества единицы подвижного состава
        /// </summary>
        /// <param name="index">Индекс редактируемой единицы</param>
        /// <param name="value">Количество</param>
        public void EditTrainVehicle(int index, int value)
        {
            if (index < _listVehicles.Count && index >= 0)
            {
                _listVehicles[index].Count = value;
            }
            else
            {
                throw new ArgumentException("Не удалось отредактировать состав!");
            }

        }

        /// <summary>
        /// Метод редактирования загрузки единицы подвижного состава
        /// </summary>
        /// <param name="index">Индекс редактируемой единицы</param>
        /// <param name="value">Коэффициент загрузки вагона</param>
        public void EditTrainVehicle(int index, double value)
        {
            if (index < _listVehicles.Count && index >= 0) //Проверяем, принадлежит ли index диапазону списка
            {
                // Присваиваем элементу новое значение
                _listVehicles[index].PayloadCoeff = value;
            }
            else
            {
                throw new ArgumentException("Не удалось отредактировать состав!");
            }
        }

        /// <summary>
        /// Метод замены подвижного состава
        /// </summary>
        /// <param name="index">Индекс редактируемой единицы</param>
        /// <param name="module">Имя модуля</param>
        /// <param name="moduleCfg">имя конфигурации единицы подвижного состава</param>
        public void EditTrainVehicle(int index, string module, string moduleCfg)
        {
            if (index < _listVehicles.Count && index >= 0)//Проверяем, принадлежит ли index диапазону списка
            {
                _listVehicles[index].Module = module;
                _listVehicles[index].ModuleConfig = moduleCfg;
            }
            else
            {
                throw new ArgumentException("Не удалось отредактировать состав!");
            }
        }

        /// <summary>
        /// Метод добавляет в итоговый состав поезда все выбранные пользователем единицы подвижного состава
        /// </summary>
        public IConsist GetConsistModel()
        {
            _consist.Common = _consistInfo;
            _consist.Vehicle = _listVehicles;
            return _consist;
        }

        public void SaveConsist(string filename)
        {
            var consist = GetConsistModel();
            _serialize.SerializeConsist(filename, consist);
        }

        public IConsist LoadConsist(string filename)
        {
            _consist = _serialize.OpenConsist(filename);
            _consistInfo = _consist.Common;
            _listVehicles = _consist.Vehicle;
            return _consist;
        }
    }
}
