using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainConsistEdition.BL.Models.Trains
{
    [Serializable]
    public class TrainVehicleModel
    {
        public string Module { get; set; }
        public string ModuleConfig { get; set; }
        public int Count { get; set; }
        public double PayloadCoeff { get; set; }


    }
}
