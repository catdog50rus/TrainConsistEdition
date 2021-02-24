﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace RRSTrainConsistEdition.Core.Models
{
    /// <summary>
    /// Модель итогового состава. Содержит описание состава и список единиц локомотивов/вагонов
    /// </summary>
    [XmlRoot("Config")]
    public interface IConsist
    {
        /// <summary>
        /// Шапка состава поезда (Свойства состава)
        /// </summary>
        public TrainConsistInfo Common { get; set; }

        /// <summary>
        /// Список с элементами поезда (Локомотив + вагоны)
        /// </summary>
        [XmlElement]
        public List<TrainVehicle> Vehicle { get; set; }
    }
}
