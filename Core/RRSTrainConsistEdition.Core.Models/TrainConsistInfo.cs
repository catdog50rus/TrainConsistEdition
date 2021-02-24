using System;

namespace RRSTrainConsistEdition.Core.Models
{
    /// <summary>
    /// класс модели общего описания и технических характеристик поезда
    /// </summary>
    [Serializable]
    public class TrainConsistInfo
    {
        /// <summary>
        /// Модуль сцепки
        /// </summary>
        public string CouplingModule { get; set; }

        /// <summary>
        /// Выбор кабины управления локомотива
        /// </summary>
        public int CabineInVehicle { get; set; }

        /// <summary>
        /// Зарядное давление
        /// </summary>
        public double ChargingPressure { get; set; }

        /// <summary>
        /// Начальное давление в главном резервуаре
        /// </summary>
        public double InitMainResPressure { get; set; }

        /// <summary>
        /// Флаг наличия / отсутствия воздуха в локомотиве.
        /// </summary>
        public int NoAir { get; set; }

        /// <summary>
        /// Наименование поезда, отображается в игровом клиенте
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание поезда, отображается в игровом клиенте
        /// </summary>
        public string Description { get; set; }

    }
}
