using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.BL.Controllers.Controllers
{
    public class DataCheckController
    {
        private readonly List<string> couplingTypes;
        private readonly List<string> modulesCfg;
        private readonly List<string> modules;
        
        public DataCheckController()
        {
            
            var listData = SettingsController.GetListData();

            couplingTypes = listData.Item1;

            modules = new List<string>();
            foreach (var moduleName in listData.Item2)
            {
                int pos = moduleName.LastIndexOf('-');
                if (pos == -1) pos = moduleName.Length;
                modules.Add(moduleName.Substring(0, pos));
            }
            modules.Add("passcar");

            modulesCfg = listData.Item2;
            modulesCfg.AddRange(listData.Item3);
        }



        /// <summary>
        /// Метод прверяющий полученную модель из файла
        /// </summary>
        /// <param name="model">Модель состава</param>
        public (bool, string) IsValidModel(ConsistModel model)
        {
            var resultValidCommon = IsValidCommon(model);
            if (!resultValidCommon.Item1) return (false, resultValidCommon.Item2);

            var resiltValidVehcle = IsValidVehcle(model);
            if (!resiltValidVehcle.Item1) return (false, resiltValidVehcle.Item2);
            return (true, "Ok!");
        }

        private (bool, string) IsValidCommon(ConsistModel model)
        {
            bool result;

            result = model.Common.CabineInVehicle == 0 || model.Common.CabineInVehicle == 0 ? true : false;
            if (!result) return (false, "CabineInVehicle False");

            result = model.Common.ChargingPressure >= 0 || model.Common.ChargingPressure <= 5.0 ? true : false;
            if (!result) return (false, "ChargingPressure False");

            result = couplingTypes.Contains(model.Common.CouplingModule) ? true : false;
            if (!result) return (false, "CouplingModulee False");


            result = model.Common.InitMainResPressure >= 0 || model.Common.InitMainResPressure <= 5.0 ? true : false;
            if (!result) return (false, "InitMainResPressure False");

            result = model.Common.NoAir == 0 || model.Common.NoAir == 1 ? true : false;
            if (!result) return (false, "NoAir False");
            return (true, "Ok");

        }
        private (bool, string) IsValidVehcle(ConsistModel model)
        {
            bool result;
            List<TrainVehicleModel> modelsList = model.Vehicle;
            foreach (var item in modelsList)
            {
                result = item.Count > 0 ? true : false;
                if (!result) return (false, $"{item.ModuleConfig} Count False");

                result = item.PayloadCoeff >= 0 || item.PayloadCoeff <= 1.0 ? true : false;
                if (!result) return (false, $"{item.ModuleConfig} PayloadCoeff False");

                result = modulesCfg.Contains(item.ModuleConfig) ? true : false;
                if (!result) return (false, $"{item.ModuleConfig} ModuleConfig False");


                result = modules.Contains(item.Module) || item.Module == "passcar" ? true : false;
                if (!result) return (false, $"{item.Module} Module False");

            };

            return (true, "Ok");
        }
    }
}
