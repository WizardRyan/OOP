using BicycleRaceSoftware.Shared;
using Microsoft.VisualBasic.FileIO;

namespace BicycleRaceSoftware.Server
{
    public static class CSVProcessor
    {

        public static Dictionary<int, int> GroupNumToStartTimeDict = new Dictionary<int, int>();
        public static Dictionary<int, string> GroupNumToGroupNameDict = new Dictionary<int, string>();
        public static List<Racer> Racers = new List<Racer>();

        public static Racer GetRacerByBib(int bibNum)
        {
            return Racers.Find(racer => racer.Bib == bibNum);
        }

        public static void ReadFiles()
        {
            var path = "./CSV/Groups.csv"; 
            TextFieldParser csvParser = new TextFieldParser(path);
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = false;

            while (!csvParser.EndOfData)
            {
                string[] fields = csvParser.ReadFields();
                GroupNumToStartTimeDict.Add(int.Parse(fields[0]), int.Parse(fields[2]));
                GroupNumToGroupNameDict.Add(int.Parse(fields[0]), fields[1]);
            }

            csvParser.Close();

            csvParser = new TextFieldParser("./CSV/Racers.csv");
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = false;

            while (!csvParser.EndOfData)
            {
                string[] fields = csvParser.ReadFields();
                string firstName = fields[0];
                string lastName = fields[1];
                int bib = int.Parse(fields[2]);
                int groupNum = int.Parse(fields[3]);

                Racers.Add(new Racer{
                    Bib = bib,
                    Name = lastName + ", " + firstName,
                    GroupNum = groupNum,
                });
            }

            csvParser.Close();
        }

    }
}
