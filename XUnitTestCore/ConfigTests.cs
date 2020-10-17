using Config;
using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace XUnitTestCore
{
    public class ConfigTests
    {
        readonly string _appName = "CabinetMaker";

        [Fact]
        public void GetCabinetConfig()
        {
            IConfig _config = new Config.Config();

            _config.CabinetName().Should().Be("Default");
            _config.CabinetHeight().Should().Be(720);
            _config.CabinetWidth().Should().Be(600);
            _config.CabinetDepth().Should().Be(510);
            _config.CabinetSizeElement().Should().Be(18);
            _config.CabinetBack().Should().Be(3);
            _config.SlotsLeft().Should().Be(3);
            _config.SlotsRight().Should().Be(3);
            _config.SlotsBottom().Should().Be(3);
            _config.SlotsTop().Should().Be(3);
            _config.SlotsBetweenCabinet().Should().Be(2);
            _config.SlotsBetweenHorizontally().Should().Be(3);
            _config.SlotsBetweenVertically().Should().Be(3);

        }

        [Fact]
        public void GetDirectoryConfig()
        {
            IConfig _config = new Config.Config();

            _config.CabinetFilesDirectory().Should().Be(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _appName));
        }
    }
}
