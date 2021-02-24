using RRSTrainConsistEdition.Core.Models;

namespace RRSTrainConsistEdition.Infrastructure.DI
{
    public interface ISerializeService
    {

        /// <summary>
        /// Сериализовать модель состава в XML файл
        /// </summary>
        /// <returns>Возвращает результат сериализации true или false</returns>
        public bool SerializeConsist(string filename, IConsist consist);

        /// <summary>
        /// Десериализовать данные модели из файла xml
        /// </summary>
        /// <returns>Возвращает кортеж (Модель, результат выполнения, сообщение)</returns>
        public IConsist OpenConsist(string filename);
        
    }
}
