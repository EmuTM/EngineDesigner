using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.ComponentModel;

namespace EngineDesigner.Common.Serialization
{
    /// <summary>
    /// Provides the base class for easy creating serializable classes using [DataMember] attributes.
    /// </summary>
    /// <typeparam name="T">The type of object to be serialized.</typeparam>
    [DataContract]
    [Serializable]
    public abstract class Serializable<T> : IExtensibleDataObject where T : Serializable<T>
    {
        static Serializable()
        {
            xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
        }



        protected static readonly XmlWriterSettings xmlWriterSettings;



        [NonSerialized]
        protected ExtensionDataObject extensionData;
        /// <summary>
        /// Stores data from a versioned data contract that has been extended by adding new members.
        /// </summary>
        [Browsable(false)]
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return extensionData;
            }
            set
            {
                extensionData = value;
            }
        }



        /// <summary>
        /// Deserializes into object of type T the content of the file.
        /// </summary>
        /// <param name="_fileName">The full name of the file to deserialize from.</param>
        public static T From(string _fileName)
        {
            using (FileStream _fileStream = new FileStream(_fileName, FileMode.Open))
            {
                return From(_fileStream);
            }
        }
        /// <summary>
        /// Deserializes into object of type T the content of the stream.
        /// </summary>
        /// <param name="_stream">The stream to deserialize from.</param>
        public static T From(Stream _stream)
        {
            T _object = default(T);

            using (XmlReader _xmlReader = XmlReader.Create(_stream))
            {
                //naredimo inštanco, da lahko dobimo derivan klas
                T _t = (T)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(T));
                DataContractSerializer _dataContractSerializer = new DataContractSerializer(typeof(T), _t.GetKnownTypes());
                _object = (T)_dataContractSerializer.ReadObject(_xmlReader, true);

                _object.AfterLoad(_object);
            }

            return _object;
        }
        /// <summary>
        /// Serializes the object into a file.
        /// </summary>
        /// <param name="_fileName">The full name of the file to serialize to.</param>
        public void Save(string _fileName)
        {
            FileInfo _fileInfo = new FileInfo(_fileName);
            if (!_fileInfo.Directory.Exists)
            {
                _fileInfo.Directory.Create();
            }

            using (FileStream _fileStream = new FileStream(
                _fileName,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None,
                Int16.MaxValue,
                FileOptions.WriteThrough))
            {
                this.Save(_fileStream);
            }
        }
        /// <summary>
        /// Serializes the object into a stream.
        /// </summary>
        /// <param name="_stream">The stream to serialize to.</param>
        public void Save(Stream _stream)
        {
            using (MemoryStream _memoryStream = new MemoryStream())
            {
                _memoryStream.Seek(0, SeekOrigin.Begin);
                _memoryStream.SetLength(0);

                using (XmlWriter _xmlWriter = XmlWriter.Create(_memoryStream, Serializable<T>.xmlWriterSettings))
                {
                    this.BeforeSave((T)this);

                    DataContractSerializer _dataContractSerializer = new DataContractSerializer(typeof(T), this.GetKnownTypes());
                    _dataContractSerializer.WriteObject(_xmlWriter, this);
                }

                long _position = _memoryStream.Position;
                _memoryStream.Seek(0, SeekOrigin.Begin);
                _stream.Position = 0;
                _memoryStream.WriteTo(_stream);
                _stream.SetLength(_position);
                _stream.Flush();
            }
        }



        protected virtual void BeforeSave(T _object)
        {
        }
        protected virtual void AfterLoad(T _object)
        {
        }
        protected virtual Type[] GetKnownTypes()
        {
            return null;
        }

    }

}
