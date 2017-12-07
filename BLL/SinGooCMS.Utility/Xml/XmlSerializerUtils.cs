using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SinGooCMS.Utility
{
    public class XmlSerializerUtils
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString)
        {
            T local = default(T);
            if (!string.IsNullOrEmpty(xmlString))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    TextReader textReader = new StringReader(xmlString);
                    local = (T)serializer.Deserialize(textReader);
                    textReader.Close();
                    return local;
                }
                catch (InvalidOperationException)
                {
                    return default(T);
                }
            }
            return default(T);
        }

        public static T DeserializeFromFile<T>(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("文件不存在");
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StreamReader(filepath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static IList<T> DeserializeList<T>(string xmlString)
        {
            IList<T> list = new List<T>();
            if (string.IsNullOrEmpty(xmlString))
            {
                return list;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextReader reader = new StringReader(xmlString))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }

        public static IList<T> DeserializeListFromFile<T>(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("文件不存在");
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            IList<T> list = new List<T>();
            using (TextReader reader = new StreamReader(filepath))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize<T>(T value)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringUTF8Writer writer = new StringUTF8Writer(new StringBuilder()))
            {
                serializer.Serialize(writer, value, namespaces);
                return writer.ToString();
            }
        }

        public static string SerializeList<T>(IList<T> entryList)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (StringUTF8Writer writer = new StringUTF8Writer(new StringBuilder()))
            {
                serializer.Serialize(writer, entryList, namespaces);
                return writer.ToString();
            }
        }

        public static void SerializeListToFile<T>(IList<T> entryList, string filepath)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextWriter writer = new StreamWriter(filepath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, entryList, namespaces);
            }
        }

        public static void SerializeToFile<T>(T value, string filepath)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(filepath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, value, namespaces);
            }
        }
        #endregion
    }

    public class StringUTF8Writer : System.IO.StringWriter
    {
        public StringUTF8Writer(StringBuilder sb)
            : base(sb)
        {
            //调用基类
        }
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}