using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrainConsistEdition.BL.Models.Trains
{
    [XmlRoot("Config")]
    public class ConsistModel
    {
        
        /// <summary>
        /// Шапка состава поезда (Свойства состава)
        /// </summary>
        public TrainConsistInfoModel Common { get; set; }


        /// <summary>
        /// Массив с элементами поезда (Локомотив + вагоны)
        /// </summary>
        [XmlElement]
        public List<TrainVehicleModel> Vehicle { get; set; }
    }
}
