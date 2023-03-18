using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using BicycleRaceSoftware.Shared;
using System.Diagnostics;

namespace BicycleRaceSoftware.Server
{

    public class DataReceiver
    {
        private UdpClient udpClient;
        private bool keepGoing;
        private Thread myRunThread;

        //Subject, holds a list of observers
        public event EventHandler<RacerStatusReceivedEventArgs>? DataReceived;

        public void Start()
        {
            udpClient = new UdpClient(14000);
            keepGoing = true;
            myRunThread = new Thread(new ThreadStart(Run));
            myRunThread.Start();
        }

        public void Stop()
        {
            keepGoing = false;
        }

        private void Run()
        {
            Debug.WriteLine("----------- Starting Listener ---------------");
            while (keepGoing)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                udpClient.Client.ReceiveTimeout = 1000;
                byte[] messageByes;
                try
                {
                    messageByes = udpClient.Receive(ref ep);
                    if (messageByes != null)
                    {
                        RacerStatus statusMessage = RacerStatus.Decode(messageByes);
                        if (statusMessage != null)
                        {
                            //ProgramLogger.myLog?.LogInformation("Race Bib #={0}, Sensor={1}, Time={2}",
                            //            statusMessage.RacerBibNumber,
                            //            statusMessage.SensorId,
                            //            statusMessage.Timestamp);

                            //This is when the subject decides to udpate it's observers
                            OnDataReceived(statusMessage);
                        }
                    }
                }
                catch (SocketException err)
                {
                    if (err.SocketErrorCode != SocketError.Interrupted && err.SocketErrorCode != SocketError.TimedOut)
                        ProgramLogger.myLog?.LogInformation(err.ToString());
                }
            }
        }

        protected virtual void OnDataReceived(RacerStatus status)
        {
            //This is where the subject is actually updating each of it's observers
            DataReceived?.Invoke(this, new RacerStatusReceivedEventArgs { Status = status});
        }
    }
}
