using RRSTrainConsistEdition.BL.Services.Consist;
using RRSTrainConsistEdition.BL.Services.Settings;
using RRSTrainConsistEdition.Core.Models;
using RRSTrainConsistEdition.Infrastructure.DI;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RRSTrainConsistEdition.BL.Services.Serialize
{
    public class SerializeService : ISerializeService
    {
        private readonly ISerializeModel _serializeModel;

        public SerializeService(ISerializeModel serializeModel)
        {
            _serializeModel = serializeModel;

        }

        /// <summary>
        /// Сериализовать выбранную модель состава в XML файл
        /// </summary>
        /// <param name="path">Путь к папке с составами</param>
        /// <returns>Возвращает результат сериализации true или false</returns>
        public bool SerializeConsist(string filename, IConsist consist)
        {
            ValidateConsistModel(consist);
            
            var model = new ConsistModel()
            {
                Common = consist.Common,
                Vehicle = consist.Vehicle
            };
            
            using FileStream fs = new FileStream(filename, FileMode.Create);
            try
            {
                var xw = XmlWriter.Create(fs, _serializeModel.Settings);
                XmlSerializer formatter = new XmlSerializer(typeof(ConsistModel));   
                formatter.Serialize(xw, model, _serializeModel.NameSpace);
            }
            catch (Exception ex) 
            {
                throw new Exception("Не удалось сформировать состав!", ex);
            }
            finally 
            {
                if (fs != null) fs.Close();
            }
            return true;
        }

        /// <summary>
        /// Метод десериализует данные модели из файла xml
        /// </summary>
        /// <returns>Возвращает кортеж (Модель, результат выполнения, сообщение)</returns>
        public IConsist OpenConsist(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"Файл {filename} не найден!");
            var model = new ConsistModel();
           
            using FileStream fs = new FileStream(filename, FileMode.Open);
            try
            {
                var xr = XmlReader.Create(fs);
                var formatter = new XmlSerializer(typeof(ConsistModel));
                model = (ConsistModel)formatter.Deserialize(xr);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось преобразовать файл!\r\nУбедитесь, что открываемый файл соответствует файлу состава или данные в нем не соответствуют типу.", ex);
            }
            finally 
            {
                if (fs != null) fs.Close();
            }

            ValidateConsistModel((IConsist)model); 

            return (IConsist)model;
        }

        private void ValidateConsistModel(IConsist consist)
        {
            var setting = new SettingService();
            var validater = new DataValidateService(setting);
            validater.IsValidModel(consist);
            
        }
    }
}
