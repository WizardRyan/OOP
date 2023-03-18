using BicycleRaceSoftware.Shared;

namespace BicycleRaceSoftware.Server
{
    public class CheatingComputer
    {

        private class Sensor
        {
            public List<RacerStatus> History { get; set; }
            public List<(RacerStatus, RacerStatus)> SameTimeCoincidences { get; set; }

            public int ID { get; set; }
        }

        public List<(CheaterStamp, CheaterStamp)> CheatingList { get; set; }

        //Subject, holds a list of observers
        public event EventHandler<CheaterDetectedEventArgs>? CheaterDetected;


        //SensorId, Racerstatus
        private Dictionary<int, Sensor> _sensorDict;

        public CheatingComputer()
        {
            CheatingList = new List<(CheaterStamp, CheaterStamp)>();
            _sensorDict = new Dictionary<int, Sensor>();
        }
        public void OnRacerStatusReceived(object source, RacerStatusReceivedEventArgs arg)
        {

            if(!_sensorDict.ContainsKey(arg.Status.SensorId)) {
                AddSensor(arg);
            }
            else
            {
                DetectCheating(arg);
            }
        }

        public void AddSensor(RacerStatusReceivedEventArgs arg)
        {
            var l = new List<RacerStatus>
                {
                    arg.Status
                };
            var sensor = new Sensor {History  = l, SameTimeCoincidences = new List<(RacerStatus, RacerStatus)>(), ID=arg.Status.SensorId};
            _sensorDict.Add(arg.Status.SensorId, sensor);
        }

        public void DetectCheating(RacerStatusReceivedEventArgs arg)
        {
            var sensor = _sensorDict[arg.Status.SensorId];
            (RacerStatus, RacerStatus)? currentCoincidence = null;
            foreach(var status in sensor.History) { 
                if(Math.Abs(status.Timestamp
                    - arg.Status.Timestamp) < 3000
                    && (CSVProcessor.GetRacerByBib(status.RacerBibNumber).GroupNum != CSVProcessor.GetRacerByBib(arg.Status.RacerBibNumber).GroupNum))
                {
                    //ProgramLogger.myLog.LogInformation($"Time Coincidence {status}");
                    currentCoincidence = (arg.Status, status);
                    sensor.SameTimeCoincidences.Add((arg.Status, status));
                }

            }

            if(sensor.ID != 0)
            {
                var prevSensor = _sensorDict[sensor.ID - 1];
                if(CoincidenceListContainsRacerStatus(prevSensor, arg.Status) && currentCoincidence != null)
                {
                    var a = GetCheaterStamp(arg.Status, sensor.ID);
                    var b = GetCheaterStamp(currentCoincidence?.Item2, sensor.ID);
                    ProgramLogger.myLog.LogInformation($"Cheating Detected: {(a, b)}");
                    CheatingList.Add((a, b));

                    //This is when the subject decides to udpate it's observers
                    OnCheatingDetected();
                }
            }
      

            _sensorDict[arg.Status.SensorId].History.Add(arg.Status);
        }

        protected virtual void OnCheatingDetected()
        {
            //This is where the subject is actually updating each of it's observers
            CheaterDetected?.Invoke(this, new CheaterDetectedEventArgs {Cheaters= CheatingList});
        }

        private bool CoincidenceListContainsRacerStatus(Sensor sensor, RacerStatus status)
        {
            foreach(var c in sensor.SameTimeCoincidences)
            {
                if(c.Item1.RacerBibNumber == status.RacerBibNumber)
                {
                    return true;
                }
            }
            return false;
        }


        private CheaterStamp GetCheaterStamp(RacerStatus status, int sensorNum)
        {
            return new CheaterStamp
            {
                Bib = status.RacerBibNumber,
                Name = CSVProcessor.GetRacerByBib(status.RacerBibNumber).Name,
                Time = status.Timestamp,
                Sensor = sensorNum
            };
        }

    }
}
