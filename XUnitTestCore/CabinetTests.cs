using CoreS;
using CoreS.Enum;
using FluentAssertions;
using Xunit;

namespace XUnitTestCore
{
    public class CabinetTests
    {
        [Fact]
        public void Utworzenie_szafki_z_domyslnymi_wartosciami()
        {
            var cabinet = new Cabinet();

            Assert.NotNull(cabinet);
            Assert.Equal(720, cabinet.Height());
            Assert.Equal(600, cabinet.Width());
            Assert.Equal(510, cabinet.Depth());
            Assert.Equal(18, cabinet.SizeElement());
            Assert.Equal("Default", cabinet.Name());
        }

        [Theory]
        [InlineData(800, 500, 200, "lll")]
        [InlineData(1200, 150, 235, "zzz")]
        public void Utworzenie_szafki_0_zadanych_wymiarach(int height, int width, int depth, string name)
        {
            var cabinet = new Cabinet().Height(height).Width(width).Depth(depth).Name(name);

            Assert.NotNull(cabinet);
            Assert.Equal(height, cabinet.Height());
            Assert.Equal(width, cabinet.Width());
            Assert.Equal(depth, cabinet.Depth());
            Assert.Equal(18, cabinet.SizeElement());
            Assert.Equal(name, cabinet.Name());
            cabinet.Name().Should().Be(name);
        }

        [Fact]
        public void Utworzenie_domyslej_szafki_i_zmiana_jej_wymiarow()
        {
            var cabinet = new Cabinet();

            Assert.Equal(720, cabinet.Height());
            Assert.Equal(600, cabinet.Width());
            Assert.Equal(510, cabinet.Depth());
            Assert.Equal(18, cabinet.SizeElement());
            Assert.Equal("Default", cabinet.Name());

            cabinet.Height(500);
            cabinet.Width(550);
            cabinet.Depth(550);

            Assert.Equal(500, cabinet.Height());
            Assert.Equal(550, cabinet.Width());
            Assert.Equal(550, cabinet.Depth());
        }

        [Fact]
        public void Utworzenie_szafki_i_dodanie_plecow_nakladanych()
        {
            var cabinet = new Cabinet();

            cabinet.AddBack();

            Assert.Equal(EnumBack.Nakladane, cabinet.Back);
        }

        [Fact]
        public void Utworzenie_szafki_i_dodanie_plecow_wpuszczanych()
        {
            var cabinet = new Cabinet();

            cabinet.AddBack(EnumBack.Wpuszczane);

            Assert.Equal(EnumBack.Wpuszczane, cabinet.Back);
        }

        [Fact]
        public void Utworzenie_szafki_gornej_z_domyslnymi_wartosciami()
        {

        }

        [Fact]
        public void Utworzenie_szafki_gornej_o_zadanych_wartościach()
        {

        }

        [Fact]
        public void Utworzenie_szafki_gornej_i_dodanie_plecow_wrebowych()
        {

        }

        [Fact]
        public void Utworzenie_szafki_gornej_i_dodanie_plecow_nakladanych()
        {

        }

        [Fact]
        public void Zmiana_grubosci_lewego_bok()
        {
            var cabinet = new Cabinet();

            var width = cabinet.CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width;
            
            cabinet.ChangeElemenet(cabinet.CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside), EnumElementParameter.Width, "36");

            var width1 = cabinet.CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width;

            Assert.Equal(18, width);
            Assert.Equal(36, width1);


        }
    }
}
