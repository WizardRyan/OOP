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
using Newtonsoft.Json.Linq;
using System.Runtime.ConstrainedExecution;

namespace ShapesApp
{
    internal class ShapesAppDriver
    {
        static void Main(string[] args)
        {
            try
            {
                var dataFromFile = ReadInFile();
                var areaTotals = CalculateAreaTotals(dataFromFile);
                HandleOutput(areaTotals);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            //make the console hang
            Console.ReadKey();
        }

        static void HandleOutput(Dictionary<string, double> areaTotals)
        {
            string response = "";
            do
            {
                Console.WriteLine("Would you like to (1) view output immediately, or  (2) save to a CSV? (enter num): ");
                response = Console.ReadLine();
            } while (!(response == "1" || response == "2"));

            if (response == "1")
            {
                DisplayOutput(areaTotals);
            }
            else if(response == "2")
            {
                WriteToCSV(areaTotals);
                Console.WriteLine("Output was written to \"areaTotals.csv\" using semi colon as a delimeter");
            }
        }

        //https://stackoverflow.com/questions/10538425/c-sharp-dictionary-to-csv Tim Schmelter's solution
        static void WriteToCSV(Dictionary<string, double> areaTotals)
        {
            String csv = String.Join(
            Environment.NewLine,
            areaTotals.Select(d => $"{d.Key};{d.Value};")
);
            File.WriteAllText("areaTotals.csv", csv);
        }

        static void DisplayOutput(Dictionary<string, double> areaTotals)
        {
            Console.WriteLine("\n{0,-40} {1,5:N2}\n", "Total area for all shapes: ", areaTotals["Total for All Shapes"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "    Ellipses: ", areaTotals["Ellipses"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "        Non-circle Ellipses: ", areaTotals["Non-CircleEllipses"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "        Circles: ", areaTotals["Circles"]);
        }

        static Dictionary<string, double> CalculateAreaTotals(dynamic dataFromFile)
        {

            double ellipseAreaTotals = 0;
            double circleAreaTotals = 0;
            double nonCircleEllipseAreaTotals = 0;
            double totalAreaAllShapes = 0;

            double area = 0;

            foreach (dynamic item in dataFromFile)
            {
                string shapeName = Convert.ToString(item.shapeName).ToLower();

                switch(shapeName)
                {
                    case "ellipse":
                        var ellipse = new Ellipse(Convert.ToDouble(item.majorAxis), Convert.ToDouble(item.minorAxis));
                        area = ellipse.GetArea();
                        ellipseAreaTotals += area;
                        nonCircleEllipseAreaTotals += area;
                        totalAreaAllShapes += area;
                        break;

                    case "circle":
                        var circle = new Circle(Convert.ToDouble(item.radius));
                        area = circle.GetArea();
                        ellipseAreaTotals+= area;
                        circleAreaTotals += area;
                        totalAreaAllShapes+= area;
                        break;
                }
            }


            return new Dictionary<string, double>() {
                {"Ellipses", ellipseAreaTotals },
                {"Circles", circleAreaTotals},
                {"Non-CircleEllipses", nonCircleEllipseAreaTotals},
                { "Total for All Shapes", totalAreaAllShapes}
            };
        }

        static dynamic ReadInFile()
        {
            bool validExt = false;
            string fileName = "";
            string fileExt = "";

            dynamic fileData = null;

            while (!validExt)
            {
                Console.WriteLine("Enter file name (must be of type XML or JSON): ");
                fileName = Console.ReadLine();

                fileExt = System.IO.Path.GetExtension(fileName).ToLower();

                if (fileExt == ".json")
                {
                    validExt = true;

                    var json = File.ReadAllText(fileName);
                    fileData = JArray.Parse(json);
                }
                else if (fileExt == ".xml")
                {
                    validExt = true;

                    var xml = File.ReadAllText(fileName);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);

                    //this works under the assumption that data is stored under root, and each element is wrapped in a row tag
                    string json = JsonConvert.SerializeXmlNode(doc);
                    fileData = JsonConvert.DeserializeObject<dynamic>(json);
                    fileData = fileData.root.row;
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
