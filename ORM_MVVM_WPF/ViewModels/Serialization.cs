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

        public static bool SerializeList(List<User> itemList)
        {
            try
            {
                if (itemList == null || itemList.Count == 0)
                {
                    Console.WriteLine("No data to serialize.");
                    return false;
                }

                string filePath = "users.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new[] { typeof(Admin), typeof(Models.Customer), typeof(Seller) });

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



        public static List<User> DeSerializeList()
        {
            string filePath = "users.xml";
            List<User> itemList = new List<User>();

            try
            {
                if (File.Exists(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new[] { typeof(Admin), typeof(Models.Customer), typeof(Seller) });
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        itemList = (List<User>)serializer.Deserialize(fileStream);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Deserialization Error: " + e.Message);
                itemList = new List<User>();
            }

            return itemList;
        }


    }
}
