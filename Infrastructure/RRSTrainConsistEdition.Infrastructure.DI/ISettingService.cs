using System.Collections.Generic;

namespace RRSTrainConsistEdition.Infrastructure.DI
{
    public interface ISettingService
    {
        public string GetPathRRSTrains();

        /// <summary>
        /// Метод устанавливает директорию RRS в конфигурационный файл
        /// </summary>
        /// <param name="path">Путь</param>
        public void SetPathRRS(string path);

        /// <summary>
        /// Метод передает списки с данными установленной RRS
        /// </summary>
        public (IEnumerable<string>, IEnumerable<string>, IEnumerable<string>) GetListData();
    }
}
