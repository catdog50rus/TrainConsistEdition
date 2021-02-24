using RRSTrainConsistEdition.Infrastructure.DI;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using RRSTrainConsistEdition.Core.Models;

namespace RRSTrainConsistEdition.BL.Services.Serialize
{
    [XmlRoot("Config")]
    public class ConsistModel : IConsist
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
