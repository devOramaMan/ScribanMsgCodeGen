using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AppMsg
{
    //Factory Method pattern combined with the Template Method pattern
    public abstract class BaseMsg : BaseHeader, Serializer
    {
        MsgHeader? _header;
        
        public static BaseMsg CreateInstance(byte[] data)
        {
            if (data == null || data.Length < 3)
                throw new ArgumentException("Data cannot be null or empty");

            var msgHeader = new MsgHeader(data);
            //Todo Pop bits
            string msgid = getMsgId(data);

            return MsgDictionary.CreateInstance(msgid, msgHeader, data);
        }

        private static string getMsgId(byte[] data)
        {
            return System.Text.Encoding.UTF8.GetString(data, 0, 3);
        }

        protected BaseMsg(byte[] data, object? msgHeader):base() { _header = (MsgHeader?)msgHeader; }


        public abstract byte[] Encode();

        public abstract void Decode(byte[] data);
    }
}
