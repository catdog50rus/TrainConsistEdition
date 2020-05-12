using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrainConsistEdition.BL.Models.Trains
{
    [Serializable]
    public class TrainConsistInfoModel
    {
        public string CouplingModule { get; set; }
        public int CabineInVehicle { get; set; }
        public double ChargingPressure { get; set; }
        public double InitMainResPressure { get; set; }
        public bool NoAir { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; } 

        public TrainConsistInfoModel() { }
        

    }
}
