using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace BicycleRaceSoftware.Shared
{
    [DataContract]
    public class RacerStatus
    {
        [DataMember]
        public int SensorId { get; set; }
        [DataMember]
        public int RacerBibNumber { get; set; }
        [DataMember]
        public int Timestamp { get; set; }

        public byte[] Encode()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RacerStatus));

            MemoryStream mstream = new MemoryStream();
            serializer.WriteObject(mstream, this);

            return mstream.ToArray();
        }

        public static RacerStatus Decode(byte[] bytes)
        {

            MemoryStream mstream = new MemoryStream(bytes);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RacerStatus));
            RacerStatus result = (RacerStatus)serializer.ReadObject(mstream);

            return result;
        }

        public override string ToString()
        {
            return $"Bib: ${RacerBibNumber} SensorID: ${SensorId} Timestamp: {Timestamp}";
        }
    }
}
