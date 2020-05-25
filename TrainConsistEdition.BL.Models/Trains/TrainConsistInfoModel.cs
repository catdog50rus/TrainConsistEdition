using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrainConsistEdition.BL.Models.Trains
{
    
    /// <summary>
    /// класс модели общего описания и технических характеристик поезда
    /// </summary>
    [Serializable]
    public class TrainConsistInfoModel
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
        /// 
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

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public TrainConsistInfoModel() { }
        

    }
}
