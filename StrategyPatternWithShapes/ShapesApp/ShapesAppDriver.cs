using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace ShapesApp
{
    internal class ShapesAppDriver
    {
        static void Main(string[] args)
        {
            var dataFromFile = ReadInFile();

            foreach (var item in dataFromFile)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }


        static Dictionary<string, object> ReadJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        static Dictionary<string, object> ReadInFile()
        {
            bool validExt = false;
            string fileName = "";
            string fileExt = "";

            Dictionary<string, object> fileData = new Dictionary<string, object>();

            while (!validExt)
            {
                Console.WriteLine("Enter File Name: ");
                fileName = Console.ReadLine();

                fileExt = System.IO.Path.GetExtension(fileName).ToLower();

                if (fileExt == ".json")
                {
                    validExt = true;

                    var json = File.ReadAllText(fileName);
                    fileData = ReadJson(json);
                }
                else if (fileExt == ".xml")
                {
                    validExt = true;

                    var xml = File.ReadAllText(fileName);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);

                    string json = JsonConvert.SerializeXmlNode(doc);
                    fileData = ReadJson(json);
                }
                else
                {
                    Console.WriteLine("Invalid File type. You must enter an XML or JSON file");
                }
            }

            return fileData;
        }
    }
}
