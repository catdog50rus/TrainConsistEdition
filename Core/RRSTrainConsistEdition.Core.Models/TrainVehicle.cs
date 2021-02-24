using System;

namespace RRSTrainConsistEdition.Core.Models
{
    /// <summary>
    /// Класс модели единицы поезда
    /// </summary>
    [Serializable]
    public class TrainVehicle
    {
        /// <summary>
        /// Модуль вагона/локомотива
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Конфигурационный файл вагона/локомотива
        /// </summary>
        public string ModuleConfig { get; set; }

        /// <summary>
        /// Количество вагонов/локомотивов данного типа, расположенные подряд в составе 
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Коэффициент загрузки вагона/локомотива. Изменяется в пределах: [0.0, 1.0], где:
        /// 1.0 - вагон полностью загружен,
        /// 0.0 - вагон пустой.
        /// </summary>
        public double PayloadCoeff { get; set; }
    }
}
