using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TrainConsistEdition.BL.Models.Trains;

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
                //Кодировка
                Encoding = Encoding.UTF8,
                //Устанавливаем формат документа
                ConformanceLevel = ConformanceLevel.Document,
                //Опускаем объявление заголовка
                OmitXmlDeclaration = false,
                //Задаем использование отступов
                Indent = true,
                //Устанавлиаем отступ 4 пробела
                IndentChars = "    "
            };
        }


        
    }
}
