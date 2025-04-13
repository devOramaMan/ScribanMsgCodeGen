namespace AppMsg
{
    public class MsgHeader : BaseHeader, Serializer
    {
        public MsgHeader(byte[] data) : base()
        {
           Decode(data);
        }

        public byte[] Encode()
        {
            return [];
        }

        public void Decode(byte[] data)
        {

        }


    }
}