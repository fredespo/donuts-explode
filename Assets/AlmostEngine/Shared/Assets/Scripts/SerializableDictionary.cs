using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;



namespace AlmostEngine {

	[XmlRoot("dictionary")]
	[System.Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable, ISerializationCallbackReceiver
	{

	    public SerializableDictionary() { }
	    public SerializableDictionary(SerializableDictionary<TKey, TValue> dic) : base(dic) {  }

	    #region IXmlSerializable Members

	    public System.Xml.Schema.XmlSchema GetSchema()
	    {
	        return null;
	    }

	    public void ReadXml(System.Xml.XmlReader reader)
	    {
	        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
	        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

	        bool wasEmpty = reader.IsEmptyElement;

	        reader.Read();

	        if (wasEmpty)
	            return;

	        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
	        {
	            reader.ReadStartElement("item");
	            reader.ReadStartElement("key");
	            TKey key = (TKey)keySerializer.Deserialize(reader);
	            reader.ReadEndElement();
	            reader.ReadStartElement("value");
	            TValue value = (TValue)valueSerializer.Deserialize(reader);
	            reader.ReadEndElement();
	            this.Add(key, value);
	            reader.ReadEndElement();
	            reader.MoveToContent();
	        }

	        reader.ReadEndElement();
	    }


	    public void WriteXml(System.Xml.XmlWriter writer)
	    {
	        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
	        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

	        foreach (TKey key in this.Keys)
	        {
	            writer.WriteStartElement("item");
	            writer.WriteStartElement("key");
	            keySerializer.Serialize(writer, key);
	            writer.WriteEndElement();
	            writer.WriteStartElement("value");
	            TValue value = this[key];
	            valueSerializer.Serialize(writer, value);
	            writer.WriteEndElement();
	            writer.WriteEndElement();
	        }
	    }

	    #endregion

	    #region Unity serializaiton

	    [XmlIgnore]
	    [SerializeField]
	    public List<TKey> keys = new List<TKey>();

	    [XmlIgnore]
	    [SerializeField]
	    public List<TValue> values = new List<TValue>();

	    [XmlIgnore]
	    [SerializeField]
	    public List<TValue> nulls = new List<TValue>();

	    // save the dictionary to lists
	    public void OnBeforeSerialize()
	    {
	        // Debug.Log("before serialize");

	        keys.Clear();
	        values.Clear();
	        foreach (KeyValuePair<TKey, TValue> pair in this)
	        {
	            keys.Add(pair.Key);
	            values.Add(pair.Value);
	        }
	    }

	    // load dictionary from lists
	    public void OnAfterDeserialize()
	    {
	        // Debug.Log("after serialize");

	        this.Clear();
	        nulls.Clear();

	        if (keys.Count != values.Count)
	            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

	        for (int i = 0; i < keys.Count; i++)
	        {
	            if (keys[i] == null)
	            {
	                nulls.Add(values[i]);
	            }
	            else
	            {
	                this.Add(keys[i], values[i]);
	            }
	        }
	    }

	    #endregion
	}

}


