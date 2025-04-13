


using Xunit;
using AppMsg;
using AppMsg.OnCampingResort;

namespace AppMsgUnitTest
{
    public class CampReportTest
    {
        [Fact]
        public void classTest()
        {
            byte[] decode = new byte[] { 1, 3, 4, 5, 6 };
            var header = new MsgHeader(decode);
            var msg = new CampReport(decode, header);
            byte[] encode = msg.Encode();

            Assert.Equal(decode, encode);
        }

        [Fact]
        public void baseClassTest()
        {
            byte[] decode = new byte[]{ 1, 3, 4, 5, 6 };
            var msg = BaseMsg.CreateInstance(decode);
            Assert.Null(msg);
            //Cast msg to the msg type Assert wrong msg type
        }
    }
}


