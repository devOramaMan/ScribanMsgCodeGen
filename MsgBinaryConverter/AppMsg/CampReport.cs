


using AppMsg;
using System;
using System.Collections.Generic;

namespace AppMsg.OnCampingResort
{
    public class CampReport : BaseMsg
    {
        public const string MsgId = "200";

        private List<int> _bits = [  23,  1,  13,  1,  51  ];

        public CampReport(byte[] data, object? msgHeader) : base(data, msgHeader)
        {
        }
        

        public DTG? Dtg { get; set; }

        public bool HasElevation { get; set; }

        public int? Elevation { get; set; }

        public bool HasLocation { get; set; }

        public Location? Position { get; set; }


        public override byte[] Encode()
        {
            return [];
        }

        public override void Decode(byte[] data)
        {

        }
    }
}

