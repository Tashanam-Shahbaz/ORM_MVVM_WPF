using ORM_MVVM_WPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ORM_MVVM_WPF.ViewModels
{
    public class Serialization
    {
        public static bool SerializeList<T>(List<T> itemList)
        {
            try
            {
                if (itemList == null || itemList.Count == 0)
                {
                    Console.WriteLine("No data to serialize.");
                    return false;
                }

                Type[] extraTypes = new Type[] { typeof(Admin), typeof(Models.Customer), 
                    typeof(Seller),typeof(ItemElectronic),typeof(ItemCloth) };



                string typeName = typeof(T).Name; 
                string filePath = $"{typeName}s.xml"; 

                XmlSerializer serializer = new XmlSerializer(typeof(List<T>), extraTypes);

                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, itemList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialization Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public static List<T> DeSerializeList<T>()
        {

            string typeName = typeof(T).Name;
            string filePath = typeName + "s.xml";
            List<T> itemList = new List<T>();

            try
            {
                if (File.Exists(filePath))
                {
                    Type[] extraTypes = new Type[] { typeof(Admin), typeof(Models.Customer),
                                             typeof(Seller), typeof(ItemElectronic),
                                             typeof(ItemCloth) };

                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>), extraTypes);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        itemList = (List<T>)serializer.Deserialize(fileStream);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Deserialization Error: " + e.Message);
                itemList = new List<T>();
            }
            return itemList;
        }
    }
}