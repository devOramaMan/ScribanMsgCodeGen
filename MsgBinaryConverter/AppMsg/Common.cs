using AppMsg;
using System.Collections.Generic;

namespace AppMsg
{


    public class DTG : Serializer
    {
        public DTG(byte[] data)
        {
            Decode(data);
        }


        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public bool HasSeconds { get; set; }

        public bool Seconds { get; set; }


        public byte[] Encode()
        {
            return [];
        }

        public void Decode(byte[] data)
        {

        }
    }



    public class Location : Serializer
    {
        public Location(byte[] data)
        {
            Decode(data);
        }


        public float Latitude { get; set; }

        public float Longitude { get; set; }


        public byte[] Encode()
        {
            return [];
        }

        public void Decode(byte[] data)
        {

        }
    }



    public class Elevation : Serializer
    {
        public Elevation(byte[] data)
        {
            Decode(data);
        }


        public int Value { get; set; }


        public byte[] Encode()
        {
            return [];
        }

        public void Decode(byte[] data)
        {

        }
    }


}

