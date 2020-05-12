using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrainConsistEdition.BL.Models.Trains
{
    /// <summary>
    /// Класс модели итогового состава. Содержит описание состава и список единиц локомотивов/вагонов
    /// </summary>
    [XmlRoot("Config")]
    public class ConsistModel
    {
        
        /// <summary>
        /// Шапка состава поезда (Свойства состава)
        /// </summary>
        public TrainConsistInfoModel Common { get; set; }


        /// <summary>
        /// Список с элементами поезда (Локомотив + вагоны)
        /// </summary>
        [XmlElement]
        public List<TrainVehicleModel> Vehicle { get; set; }
    }
}
