using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainConsistEdition.BL.Models.Models
{
    public class ApplicationSettingsModel
    {
        public string AplicationDirectory { get; set; }
        public string VehecleDirrectory { get; set; } = @"\cfg\vehicles";
        public string CoupleTypeDirectory { get; set; } = @"\cfg\couplings";
        public string TrainsDirectory { get; set; } = @"\cfg\trains\";
    }
}
