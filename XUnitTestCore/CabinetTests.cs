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
        public void Utworzenie_szafki_gornej_z_domyslnymi_wartosciami()
        {
            var cabinet = new Cabinet(EnumCabinetType.gorna);
            cabinet.Should().NotBeNull();
            cabinet.Type().Should().Be(EnumCabinetType.gorna);
            cabinet.GetElements(EnumCabinetElement.Leftside).GetParameter(EnumElementParameter.Depth).Should().Be(510);
            cabinet.GetElements(EnumCabinetElement.Top).GetParameter(EnumElementParameter.Depth).Should().Be(490);
        }

        [Fact]
        public void Utworzenie_szafki_gornej_o_zadanych_wartosciach()
        {
            var cabinet = new Cabinet(1200,450,350,"XXX",EnumCabinetType.gorna);
            cabinet.Should().NotBeNull();
            cabinet.Type().Should().Be(EnumCabinetType.gorna);
            cabinet.GetElements(EnumCabinetElement.Leftside).GetParameter(EnumElementParameter.Depth).Should().Be(350);
            cabinet.GetElements(EnumCabinetElement.Top).GetParameter(EnumElementParameter.Depth).Should().Be(330);
            cabinet.GetElements(EnumCabinetElement.Bottom).GetParameter(EnumElementParameter.Height).Should().Be(414);
        }

        [Fact]
        public void Utworzenie_szafki_gornej_i_dodanie_plecow_wrebowych()
        {

        }

        [Fact]
        public void Utworzenie_szafki_gornej_i_dodanie_plecow_nakladanych()
        {

        }

        [Theory]
        [InlineData(EnumCabinetElement.Leftside,EnumElementParameter.Width,18,36 )]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.Height, 720, 900)]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.Depth, 510, 600)]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.X, 0, 12)]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.Y, 0, 12)]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.Z, 0, 12)]
        [InlineData(EnumCabinetElement.Rightside, EnumElementParameter.Width, 18, 36)]
        [InlineData(EnumCabinetElement.Rightside, EnumElementParameter.Height, 720, 900)]
        [InlineData(EnumCabinetElement.Rightside, EnumElementParameter.Depth, 510, 600)]

        [InlineData(EnumCabinetElement.Top, EnumElementParameter.Width, 18, 36)]
        [InlineData(EnumCabinetElement.Top, EnumElementParameter.Height, 564, 900)]
        [InlineData(EnumCabinetElement.Top, EnumElementParameter.Depth, 510, 600)]

        [InlineData(EnumCabinetElement.Bottom, EnumElementParameter.Width, 18, 36)]
        [InlineData(EnumCabinetElement.Bottom, EnumElementParameter.Height, 564, 900)]
        [InlineData(EnumCabinetElement.Bottom, EnumElementParameter.Depth, 510, 600)]
        public void Zmaian_parametrow_elementu_szafki_int(EnumCabinetElement enumCabinetElement,EnumElementParameter enumElementParameter, int value,int newValue)
        {
            var cabinet =new Cabinet();

            cabinet.Should().NotBeNull();

            var elem = cabinet.GetElements(enumCabinetElement);
            
            elem.GetParameter(enumElementParameter).Should().Be(value);

            elem.SetParameter(enumElementParameter, newValue,true);

            elem.GetParameter(enumElementParameter).Should().Be(newValue);
        }

        [Theory]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.Description, "Bok Lewy", "XXX")]
        public void Zmaian_parametrow_elementu_szafki_Description(EnumCabinetElement enumCabinetElement, EnumElementParameter enumElementParameter, string value, string newValue)
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();

            var elem = cabinet.GetElements(enumCabinetElement);

            elem.GetParameter(enumElementParameter).Should().Be(value);

            elem.SetParameter(enumElementParameter, newValue, true);

            elem.GetParameter(enumElementParameter).Should().Be(newValue);
        }

        [Theory]
        [InlineData(EnumCabinetElement.Leftside, EnumElementParameter.Horizontal, false, true)]
        [InlineData(EnumCabinetElement.Top, EnumElementParameter.Horizontal, true, false)]
        public void Zmaian_parametrow_elementu_szafki_Horizontal(EnumCabinetElement enumCabinetElement, EnumElementParameter enumElementParameter, bool value, bool newValue)
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();

            var elem = cabinet.GetElements(enumCabinetElement);

            elem.GetParameter(enumElementParameter).Should().Be(value);

            elem.SetParameter(enumElementParameter, newValue, true);

            elem.GetParameter(enumElementParameter).Should().Be(newValue);
        }

        [Fact]
        public void Utworzenie_szafki_i_dodanie_plecow_nakladanych()
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();

            cabinet.AddBack();

            cabinet._enumBack.Should().Be(EnumBack.Nakladane);
        }

        [Fact]
        public void Utworzenie_szafki_i_dodanie_plecow_wpuszczanych()
        {
            var cabinet = new Cabinet();

            cabinet.Should().NotBeNull();

            cabinet.AddBack(EnumBack.Wpuszczane);

            cabinet._enumBack.Should().Be(EnumBack.Wpuszczane);
        }


    }

}
