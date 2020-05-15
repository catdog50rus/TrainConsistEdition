using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainConsistEdition.BL.Models.Models;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    public class OpenFileController
    {
        public ConsistModel ConsistModel { get; set; }
        public string File { get; }

        public OpenFileController(string filename)
        {
            ConsistModel = new ConsistModel();
            File = filename;

        }
    }
}
