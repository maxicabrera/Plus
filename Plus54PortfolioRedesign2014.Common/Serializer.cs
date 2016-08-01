using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;

namespace Plus54PortfolioRedesign2014.Common
{
    public static class Serializer
    {
        public static string SerializeToXml<T>(ref T toSerialize)
        {
            using (StringWriter outStream = new StringWriter(CultureInfo.InvariantCulture))
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(outStream, toSerialize);
                return outStream.ToString();
            }
        }


        public static T DeserializeFromXml<T>(ref string serialized)
        {
            T obj = default(T);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(serialized))
            {
                obj = (T)serializer.Deserialize(reader);
            }
            return obj;
        }
    }
}
