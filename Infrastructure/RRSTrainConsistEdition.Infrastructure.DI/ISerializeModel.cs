using System.Xml;
using System.Xml.Serialization;

namespace RRSTrainConsistEdition.Infrastructure.DI
{
    /// <summary>
    /// Класс модели настроек сериализации
    /// </summary>
    public interface ISerializeModel
    {
        /// <summary>
        /// Настройка схемы
        /// </summary>
        public XmlSerializerNamespaces NameSpace { get; }

        /// <summary>
        /// Основные установки XMLWriter
        /// </summary>
        public XmlWriterSettings Settings { get; set; }
    }

}
