using Xunit;
using CoreS;
using CoreS.Config;

namespace XUnitTestCore
{
    public class ConfigTests
    {
        [Fact]
        public void GetSlots()
        {
            var t = Settings.Instance.GetSlots();
        }
    }
}
