using RRSTrainConsistEdition.Core.Models;

namespace RRSTrainConsistEdition.Infrastructure.DI
{
    /// <summary>
    /// Сформировать состав поезда на основе классов моделей данных
    /// </summary
    public interface ICreateConsistService
    {
        /// <summary>
        /// Добавить в модель свойства и характеристики состава
        /// </summary>
        public void AddConsistOptions(TrainConsistInfo consistInfo);

        /// <summary>
        /// Добавить единицу состава (вагон или локомотив)
        /// </summary>
        public void AddTrainVehicle(TrainVehicle vehicle);

        /// <summary>
        /// Удалить единицу подвижного состава из поезда
        /// </summary>
        /// <param name="index">Индекс удаляемой единицы</param>
        public void RemoveTrainVehicle(int index);

        /// <summary>
        /// Редактировать количество единиц подвижного состава
        /// </summary>
        /// <param name="index">Индекс редактируемой единицы</param>
        /// <param name="value">Количество</param>
        public void EditTrainVehicle(int index, int value);

        /// <summary>
        /// Редактировать коэффициент загрузки
        /// </summary>
        /// <param name="index">Индекс редактируемой единицы</param>
        /// <param name="value">Коэффициент загрузки вагона</param>
        public void EditTrainVehicle(int index, double value);

        /// <summary>
        /// Редактировать единицы подвижного состава
        /// </summary>
        /// <param name="index">Индекс редактируемой единицы</param>
        /// <param name="module">Имя модуля</param>
        /// <param name="moduleCfg">имя конфигурации единицы подвижного состава</param>
        public void EditTrainVehicle(int index, string module, string moduleCfg);

        /// <summary>
        /// Получить состав
        /// </summary>
        /// <returns>Метод возвращает модель состава полностью готового к сериализации в XML файл</returns>
        public IConsist GetConsistModel();

        public void SaveConsist(string filename);

        public IConsist LoadConsist(string filename);
    }
}
