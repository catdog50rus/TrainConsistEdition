using RRSTrainConsistEdition.Core.Models;

namespace RRSTrainConsistEdition.Infrastructure.DI
{
    /// <summary>
    /// Валидация данных полученные из файла
    /// </summary>
    public interface IDataValidateService
    {
        /// <summary>
        /// Валидация модели, полученной из файла
        /// </summary>
        public void IsValidModel(IConsist model);
    }
}
