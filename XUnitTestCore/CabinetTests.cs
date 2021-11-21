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

            cabinet.Should().NotBeNull();
            cabinet.Height().Should().Be(720);
            cabinet.Width().Should().Be(600);
            cabinet.Depth().Should().Be(510);
            cabinet.SizeElement().Should().Be(18);
            cabinet.Name().Should().Be("Default");
        }

        [Theory]
        [InlineData(800, 500, 200, "lll")]
        [InlineData(1200, 150, 235, "zzz")]
        public void Utworzenie_szafki_o_zadanych_wymiarach(int height, int width, int depth, string name)
        {
            var cabinet = new Cabinet(height, width, depth, name);

            cabinet.Should().NotBeNull();
            cabinet.Height().Should().Be(height);
            cabinet.Width().Should().Be(width);
            cabinet.Depth().Should().Be(depth);
            cabinet.SizeElement().Should().Be(18);
            cabinet.Name().Should().Be(name);
        }

        [Theory]
        [InlineData(850, 500, 200)]
        [InlineData(1250, 150, 235)]
        public void Utworzenie_domyslej_szafki_i_zmiana_jej_wymiarow(int height, int width, int depth)
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();
            cabinet.Height().Should().Be(720);
            cabinet.Width().Should().Be(600);
            cabinet.Depth().Should().Be(510);
            cabinet.SizeElement().Should().Be(18);
            cabinet.Name().Should().Be("Default");

            cabinet.Height(height);
            cabinet.Width(width);
            cabinet.Depth(depth);

            cabinet.Height().Should().Be(height);
            cabinet.Width().Should().Be(width);
            cabinet.Depth().Should().Be(depth);
        }

        [Fact]
        public void Utworzenie_szafki_i_dodanie_plecow_nakladanych()
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();

            cabinet.AddBack();

            cabinet.Back.Should().Be(EnumBack.Nakladane);
        }

        [Fact]
        public void Utworzenie_szafki_i_dodanie_plecow_wpuszczanych()
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();

            cabinet.AddBack(EnumBack.Wpuszczane);

            cabinet.Back.Should().Be(EnumBack.Wpuszczane);
        }

        [Fact]
        public void Utworzenie_szafki_gornej_z_domyslnymi_wartosciami()
        {

        }

        [Fact]
        public void Utworzenie_szafki_gornej_o_zadanych_wartosciach()
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

        [Theory]
        [InlineData(EnumCabinetElement.Leftside,EnumElementParameter.Width,18,36 )]
        public void Zmaian_parametrow_elementu_szafki(EnumCabinetElement enumCabinetElement,EnumElementParameter enumElementParameter, int value,int newValue)
        {
            var cabinet =new Cabinet();

            cabinet.Should().NotBeNull();

            var elem = cabinet.GetElements(enumCabinetElement);
            var elementWidth = elem.GetElementParameter(enumElementParameter);

            elementWidth.Should().Be(value);

            elem.SetWidth(newValue);

            elementWidth = elem.GetElementParameter(enumElementParameter);
            elementWidth.Should().Be(newValue);
        }
    }
}
