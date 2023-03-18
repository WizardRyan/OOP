using Microsoft.VisualBasic.FileIO;
using BicycleRaceSoftware.Shared;
using BicycleRaceSoftware.Client;
using BicycleRaceSoftware.Server;

namespace TestProject1
{
    public class UnitTests
    {

        [Fact]
        public void TestRacer()
        {
            //Do racers compare correctly with their custom comparator?
            Racer a = new Racer { Bib = 1, GroupNum = 1, Name = "Not a real racer" };
            Racer b = new Racer { Bib = 2, GroupNum = 1, Name = "Not a real racer... 2!" };

            Assert.True(!a.Equals(b));
            Assert.True(a < b);
        }

        [Fact]
        public void TestCheatingComputer()
        {
            var c = new CheatingComputer();
            c.OnRacerStatusReceived(c, new RacerStatusReceivedEventArgs { Status= new RacerStatus { RacerBibNumber= 1, SensorId=0, Timestamp=4000 } });
            c.OnRacerStatusReceived(c, new RacerStatusReceivedEventArgs { Status = new RacerStatus { RacerBibNumber = 2, SensorId = 0, Timestamp = 4001 } });

            //cheating list should be empty
            Assert.True(c.CheatingList.Count == 0);
        }

        [Fact]
        public void TestCSVReader()
        {
            //can we read in files?
            CSVProcessor.ReadFiles();

            //did we even read any racers in?
            Assert.True(CSVProcessor.Racers.Count > 0);

            //Do racers have the data we expect?
            Assert.True(CSVProcessor.Racers[0].Bib == 1);
            Assert.True(CSVProcessor.Racers[0].GroupNum == 1);
            Assert.True(CSVProcessor.Racers[0].Name == "RATLIFF, MILFORD");

            //does the helper function work?
            Assert.True(CSVProcessor.GroupNumToGroupNameDict[CSVProcessor.Racers[0].GroupNum] == "Mens Cat 1-2");
        }

        [Fact]
        public void TestRacerStatus()
        {
            RacerStatus r = new RacerStatus { RacerBibNumber = 1, SensorId = 0, Timestamp = 100 };

            var bytes = r.Encode();

            var r2 = RacerStatus.Decode(bytes);

            //Do our racer statuses survive encoding and decoding properly?
            Assert.True(r2.RacerBibNumber == r.RacerBibNumber && r2.SensorId == r.SensorId && r.Timestamp == r2.Timestamp);
        }
    }

}
   