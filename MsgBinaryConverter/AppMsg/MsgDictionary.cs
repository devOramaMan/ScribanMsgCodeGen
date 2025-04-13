



using AppMsg.OnCampingResort;


namespace AppMsg
{
    internal class MsgDictionary 
    {
        static Dictionary<string, Type> MsgDict = new Dictionary<string, Type>
        { 
            { CampReport.MsgId, typeof(CampReport) }, 
            { ToiletStat.MsgId, typeof(ToiletStat) }, 
        };

        public static BaseMsg CreateInstance(string msgId, object? header, byte[] data)
        {
            if (MsgDict.TryGetValue(msgId, out Type? handlerType))
            {
                return (BaseMsg)Activator.CreateInstance(handlerType, header, data);
            }
            else
            {
                throw new KeyNotFoundException($"Message ID '{msgId}' not found.");
            }
        }
    }
}
