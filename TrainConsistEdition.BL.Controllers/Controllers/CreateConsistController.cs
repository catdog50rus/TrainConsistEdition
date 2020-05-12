using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    public class CreateConsistController
    {
        private readonly TrainConsistInfoModel consistModel;
        private readonly ConsistModel serializeModel;
        private readonly List<TrainVehicleModel> _listVehicles;

        public CreateConsistController()
        {
            consistModel = new TrainConsistInfoModel();
          
            _listVehicles = new List<TrainVehicleModel>();
            serializeModel = new ConsistModel();

        }

        public void AddConsistOptions(string title, 
                                      string description,
                                      string couplingModule,
                                      int cabinInVehicle, 
                                      double chargingPressure,
                                      double intMainResPressure, 
                                      bool noAir)
        {
            
            consistModel.Title = title;
            consistModel.Description = description;
            consistModel.CabineInVehicle = cabinInVehicle;
            consistModel.CouplingModule = couplingModule;
            consistModel.ChargingPressure = chargingPressure;
            consistModel.InitMainResPressure = intMainResPressure;
            consistModel.NoAir = noAir;

            serializeModel.Common = consistModel;
        }

        public void AddTrainVehicle(string module, 
                                    string moduleConfig,
                                    int count,
                                    double payloadCoeff)
        {

            var vehicle = new TrainVehicleModel()
            {
                Module = module,
                ModuleConfig = moduleConfig,
                Count = count,
                PayloadCoeff = payloadCoeff
            };
            

            _listVehicles.Add(vehicle);
        }

        public void RemoveTrainVehicle(int index)
        {
            _listVehicles.RemoveAt(index);
        }

        public ConsistModel GetConsistModel()
        {
            serializeModel.Vehicle = _listVehicles;
            return serializeModel;
        }
    }
}
