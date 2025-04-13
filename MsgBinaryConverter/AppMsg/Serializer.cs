namespace AppMsg
{
    interface Serializer
    {
        byte[] Encode();

        abstract void Decode(byte[] data);
    }
}