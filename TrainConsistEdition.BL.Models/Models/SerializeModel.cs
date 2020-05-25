using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TrainConsistEdition.BL.Models.Models
{
    /// <summary>
    /// Класс модели настроек сериализации
    /// </summary>
    public class SerializeModel
    {
        /// <summary>
        /// Настройка схемы
        /// </summary>
        public XmlSerializerNamespaces NameSpace { get; }

        /// <summary>
        /// Основные установки XMLWriter
        /// </summary>
        public XmlWriterSettings Settings { get; set; }

        /// <summary>
        /// Конструктор без параметров. Устанавливает настройки сериализации
        /// </summary>
        public SerializeModel()
        {
            //Обнуляем отображение схемы по умолчанию 
            NameSpace = new XmlSerializerNamespaces();
            NameSpace.Add(string.Empty, string.Empty);

            //Основные настройки
            Settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8, //Кодировка
                ConformanceLevel = ConformanceLevel.Document, //Устанавливаем формат документа
                OmitXmlDeclaration = false, //Опускаем объявление заголовка
                Indent = true, //Задаем использование отступов
                IndentChars = "    " //Устанавливаем отступ 4 пробела
            };
        }
    }
}
