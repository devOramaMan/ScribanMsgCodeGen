

using AppMsg;
using System;
using System.Collections.Generic;

namespace AppMsg.OnCampingResort
{
    public class ToiletStat : BaseMsg
    {
        public const string MsgId = "201";

        private List<int> _bits = [  23,  1,  1,  51  ];

        public ToiletStat(byte[] data, object? msgHeader) : base(data, msgHeader)
        {
        }
        

        public DTG? Dtg { get; set; }

        public bool HasToiletPaper { get; set; }

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

