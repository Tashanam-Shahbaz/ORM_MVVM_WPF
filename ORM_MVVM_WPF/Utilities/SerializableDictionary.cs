using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ORM_MVVM_WPF.Utilities
{
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("Item");
                reader.ReadStartElement("Key");
                TKey key = (TKey)new XmlSerializer(typeof(TKey)).Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("Value");
                TValue value = (TValue)new XmlSerializer(typeof(TValue)).Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(key, value);
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartElement("Key");
                new XmlSerializer(typeof(TKey)).Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                TValue value = this[key];
                new XmlSerializer(typeof(TValue)).Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }
}

