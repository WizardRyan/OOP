using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            Console.WriteLine("Press any key to close the program...");
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
            string[] valsToCheck = { "Total for All Shapes", "Ellipses", "Non-CircleEllipses", "Circles", };
            Console.WriteLine("\n{0,-40} {1,5:N2}\n", "Total area for all shapes: ", areaTotals["Total for All Shapes"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "    Ellipses: ", areaTotals["Ellipses"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "        Non-circle Ellipses: ", areaTotals["Non-CircleEllipses"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "        Circles: ", areaTotals["Circles"]);
            Console.WriteLine("\n{0,-40} {1,5:N2}", "    Convex Polygons: ", areaTotals["ConvexPolygons"]);
            Console.WriteLine("\n{0,-40} {1,5:N2}", "        Rectangles: ", areaTotals["Rectangles"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "            Non-square Rectangles: ", areaTotals["Non-SquareRectangles"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "            Squares: ", areaTotals["Squares"]);
            Console.WriteLine("\n{0,-40} {1,5:N2}", "        Triangles: ", areaTotals["Triangles"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "            Isosceles Triangles: ", areaTotals["IsoscelesTriangles"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "            Scalene Triangles: ", areaTotals["ScaleneTriangles"]);
            Console.WriteLine("{0,-40} {1,5:N2}", "            Equilateral Triangles: ", areaTotals["EquilateralTriangles"]);
            Console.WriteLine("\n{0,-40} {1,5:N2}", "        Regular Polygons: ", areaTotals["RegularPolygons"]);
        }

        static Dictionary<string, double> CalculateAreaTotals(dynamic dataFromFile)
        {
            double totalAreaAllShapes = 0;

            double circleAreaTotals = 0;
            double nonCircleEllipseAreaTotals = 0;

            double nonSquareRectangleAreaTotals = 0;
            double squareAreaTotals = 0;

            double isoscelesAreaTotals = 0;
            double scaleneAreaTotals = 0;
            double equilateralAreaTotals = 0;


            foreach (dynamic item in dataFromFile)
            {
                string shapeName = Convert.ToString(item.shapeName).ToLower();
                double area = 0;

                switch(shapeName)
                {
                    case "ellipse":
                        var ellipse = new Ellipse(Convert.ToDouble(item.majorAxis), Convert.ToDouble(item.minorAxis));
                        area = ellipse.GetArea();
                        nonCircleEllipseAreaTotals += area;
                        break;
                    case "circle":
                        var circle = new Circle(Convert.ToDouble(item.radius));
                        area = circle.GetArea();
                        circleAreaTotals += area;
                        break;
                    case "rectangle":
                        var rectangle = new Rectangle(Convert.ToDouble(item.length), Convert.ToDouble(item.width));
                        area = rectangle.GetArea();
                        nonSquareRectangleAreaTotals+= area;
                        break;
                    case "square":
                        var square = new Square(Convert.ToDouble(item.sideLength));
                        area = square.GetArea();
                        squareAreaTotals+= area;
                        break;
                    case "equilateraltriangle":
                        var equilTriangle = new EquilateralTriangle(Convert.ToDouble(item.sideLength));
                        area = equilTriangle.GetArea();
                        equilateralAreaTotals+= area;
                        break;
                    case "isoscelestriangle":
                        var isoscelesTriangle = new IsoscelesTriangle(Convert.ToDouble(item.legsLength), Convert.ToDouble(item.baseLength));
                        area= isoscelesTriangle.GetArea();
                        isoscelesAreaTotals+= area;
                        break;
                    case "scalenetriangle":
                        var scaleneTriangle = new ScaleneTriangle(Convert.ToDouble(item.sideA), Convert.ToDouble(item.sideB), Convert.ToDouble(item.sideC));
                        area = scaleneTriangle.GetArea();
                        scaleneAreaTotals += area;
                        break;
                }

                totalAreaAllShapes+= area;
            }

            var triangleArea = isoscelesAreaTotals + equilateralAreaTotals+ scaleneAreaTotals;
            var rectangleArea = squareAreaTotals + nonSquareRectangleAreaTotals;

            return new Dictionary<string, double>() {
                {"Ellipses", circleAreaTotals + nonCircleEllipseAreaTotals },
                {"Circles", circleAreaTotals},
                {"Non-CircleEllipses", nonCircleEllipseAreaTotals},
                {"Total for All Shapes", totalAreaAllShapes},
                {"Rectangles", rectangleArea},
                {"Squares", squareAreaTotals },
                {"Non-SquareRectangles", nonSquareRectangleAreaTotals },
                {"Triangles", triangleArea},
                {"ScaleneTriangles", scaleneAreaTotals },
                {"IsoscelesTriangles", isoscelesAreaTotals },
                {"EquilateralTriangles", equilateralAreaTotals },
                {"ConvexPolygons", triangleArea + rectangleArea },
                {"RegularPolygons", equilateralAreaTotals + squareAreaTotals + nonSquareRectangleAreaTotals},
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
