using System;
using System.Collections.Generic;
using CoreS;
using CoreS.Model;
using FluentAssertions;
using Xunit;

namespace XUnitTestCore
{
    public class HorizontalBarrierTests
    {
        [Fact]
        public void Dodanie_1_przegrody_poziomej()
        {
            var cabinet = new Cabinet().Name("test_1");
            var barrierParameter = new BarrierParameter {Number = 1};

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_2_przegrod_poziomych()
        {
            var cabinet = new Cabinet().Name("test_2");

            var barrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(234, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(468, cabinet.HorizontalBarrier[1].GetY());
        }

        [Fact]
        public void Dodanie_4_przegrod_poziomych()
        {
            var cabinet = new Cabinet().Name("test_3");

            var barrierParameter = new BarrierParameter { Number = 4 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(4, cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(140, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(280, cabinet.HorizontalBarrier[1].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[2].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[2].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[2].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[2].GetX());
            Assert.Equal(420, cabinet.HorizontalBarrier[2].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[3].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[3].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[3].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[3].GetX());
            Assert.Equal(560, cabinet.HorizontalBarrier[3].GetY());
        }

        [Fact]
        public void Dodanie_1_przegrody_potem_dodanie_nastepnej()
        {
            var cabinet = new Cabinet().Name("test_4");

            var barrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewHorizontalBarrier(barrierParameter);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());

            barrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(234, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(468, cabinet.HorizontalBarrier[1].GetY());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(6)]
        [InlineData(0)]
        //[InlineData(null)] is 0
        public void Szybkie_dodanie_przegrody_poziomej(int i)
        {
            var cabinet = new Cabinet();
            cabinet.AddHorizontalBarrier(i);

            Assert.Equal(cabinet.GetAllHorizontalBarrier().Count,i);

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-6)]
        [InlineData(-1000)]
        public void szybkie_dodanie_blednej_ilosci_przegrod(int i)
        {
            var cabinet = new Cabinet();
            Assert.Throws<ArgumentException>(()=>cabinet.AddHorizontalBarrier(i));
        }

        [Theory]
        [InlineData(2, 1, 1)]
        [InlineData(3, 1, 2)]
        [InlineData(5, 5, 0)]
        [InlineData(4, 0, 4)]
        [InlineData(0, 5, 0)]
        [InlineData(1, 10, 0)]
        [InlineData(10, 20, 0)]
        public void Szybkie_dodanie_przegrod_i_szybkie_ich_usuniecie(int start,int delete,int end)
        {
            var cabinet = new Cabinet();
            cabinet.AddHorizontalBarrier(start);
            cabinet.GetAllHorizontalBarrier().Count.Should().Be(start);

            cabinet.DeleteHorizontalBarrier(delete);
            cabinet.GetAllHorizontalBarrier().Count.Should().Be(end);
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(20, 0)]
        [InlineData(0, 0)]
        public void Szybkie_dodanie_przegrod_i_skasowanie_wszystkich(int start, int end)
        {
            var cabinet = new Cabinet();
            cabinet.AddHorizontalBarrier(start);
            cabinet.GetAllHorizontalBarrier().Count.Should().Be(start);

            cabinet.DeleteAllHorizontalBarrier();
            cabinet.GetAllHorizontalBarrier().Count.Should().Be(end);
        }

        [Fact]
        public void Dodanie_1_polki()
        {
            var cabinet = new Cabinet().Name("test_5");

            var barrierParameter = new BarrierParameter { Number = 1,Back=5 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(505, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

       [Fact]
        public void Dodanie_2_polek()
        {
            var cabinet = new Cabinet().Name("test_6");

            var barrierParameter = new BarrierParameter { Number = 2,Back=10 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(500, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(234, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(500, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(468, cabinet.HorizontalBarrier[1].GetY());
        }

        [Fact]
        public void Dodanie_1_polki_według_ustalonej_wysokosci()
        {
            var cabinet = new Cabinet();

            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.Height.Add(250);
            
            cabinet.NewHorizontalBarrier(barrierParameter);

            Assert.Single(cabinet.HorizontalBarrier);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(268, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_2_polek_według_ustalonej_wysokosci()
        {
            var cabinet = new Cabinet();

            var barrierParameter = new BarrierParameter { Number = 2 };
            barrierParameter.Height.Add(250);
            barrierParameter.Height.Add(300);

            cabinet.NewHorizontalBarrier(barrierParameter);

            Assert.Equal(2,cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(268, cabinet.HorizontalBarrier[0].GetY());
            Assert.Equal(318, cabinet.HorizontalBarrier[1].GetY());
        }

        [Fact]
        public void Dodanie_dwoch_przegrod_w_tym_jednej_o_okreslonej_wysokosci()
        {
            var cabinet = new Cabinet();

            var barrierParameter = new BarrierParameter {Number = 2, Height = new List<int> {250}};
            cabinet.NewHorizontalBarrier(barrierParameter);
            Assert.Equal(2, cabinet.HorizontalBarrier.Count);

            

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(268, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(485, cabinet.HorizontalBarrier[1].GetY());

        }

        [Fact]
        public void Dodanie_trzech_przegrod_w_tym_jednej_o_okreslonej_wysokosci()
        {
            var cabinet = new Cabinet();
            var barrierParameter = new BarrierParameter { Number = 3, Height = new List<int> { 250 } };
            cabinet.NewHorizontalBarrier(barrierParameter);
            Assert.Equal(3, cabinet.HorizontalBarrier.Count);

            

            Assert.Equal(564, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(268, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(412, cabinet.HorizontalBarrier[1].GetY());

            Assert.Equal(564, cabinet.HorizontalBarrier[2].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[2].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[2].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[2].GetX());
            Assert.Equal(556, cabinet.HorizontalBarrier[2].GetY());

        }

        [Fact]
        public void Dodanie_3_przegrod_w_tym_jednej_o_okreslonej_wysokosci_na_samej_gorze_powodujac_blad_miejsca_na_inne_przegrody()
        {
            var cabinet = new Cabinet();
            var barrierParameter = new BarrierParameter { Number = 3, Height = new List<int> { 698 } };
            cabinet.NewHorizontalBarrier(barrierParameter);
            Assert.Equal(3, cabinet.HorizontalBarrier.Count);
        }


        [Fact]
        public void Dodanie_1_przegrody_poziomej_gdy_jest_przegroda_pionowa()
        {
            var cabinet = new Cabinet().Name("test_8");


            var vBarrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Single(cabinet.VerticalBarrier);
            
            var barrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);
            Assert.Equal(273, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());

            Assert.Equal(273, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(309,cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[1].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_1_pionowej_bariery_a_potem_poziomej_do_2_kolumny()
        {
            var cabinet = new Cabinet().Name("test_9");

            var vBarrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Single(cabinet.VerticalBarrier);

            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.AddBarrier(1);

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(273, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(309, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_2_pionowych_barier_a_potem_poziomej_do_1_kolumny()
        {
            var cabinet = new Cabinet().Name("test_10");

            var vBarrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);

            var barrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(3, cabinet.HorizontalBarrier.Count);
            Assert.Equal(176, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
            
            Assert.Equal(176, cabinet.HorizontalBarrier[1].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[1].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[1].GetWidth());
            Assert.Equal(212, cabinet.HorizontalBarrier[1].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[1].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_2_pionowych_barier_a_potem_poziomej_do_3_kolumny()
        {
            var cabinet = new Cabinet().Name("test_11");

            var vBarrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);
            
            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.AddBarrier(2);

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();


            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(176, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(406, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_2_pionowych_barier_a_potem_poziomej_do_2_kolumny()
        {
            var cabinet = new Cabinet().Name("test_12");

            var vBarrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);

            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.AddBarrier(1);

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(176, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(212, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_3_pionowych_barier_a_potem_poziomej_do_1_kolumny()
        {
            var cabinet = new Cabinet().Name("test_13");

            var vBarrierParameter = new BarrierParameter { Number = 3 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(3, cabinet.VerticalBarrier.Count);

            var barrierParameter = new BarrierParameter { Number = 1 };
            
            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(4,cabinet.HorizontalBarrier.Count);
            Assert.Equal(127, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

       [Fact]
        public void Dodanie_najpierw_3_pionowych_barier_a_potem_poziomej_do_4_kolumny()
        {
            var cabinet = new Cabinet().Name("test_14");

            var vBarrierParameter = new BarrierParameter { Number = 3 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(3, cabinet.VerticalBarrier.Count);

            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.AddBarrier(3);

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(129, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(453, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_3_pionowych_barier_a_potem_poziomej_do_2_kolumny()
        {
            var cabinet = new Cabinet().Name("test_15");

            var vBarrierParameter = new BarrierParameter { Number = 3 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(3, cabinet.VerticalBarrier.Count);

            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.AddBarrier(1);

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(127, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(163, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_3_pionowych_barier_a_potem_poziomej_do_3_kolumny()
        {
            var cabinet = new Cabinet().Name("test_16");

            var vBarrierParameter = new BarrierParameter { Number = 3 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(3, cabinet.VerticalBarrier.Count);

            var barrierParameter = new BarrierParameter { Number = 1 };
            barrierParameter.AddBarrier(2);

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(127, cabinet.HorizontalBarrier[0].GetHeight());
            Assert.Equal(510, cabinet.HorizontalBarrier[0].GetDepth());
            Assert.Equal(18, cabinet.HorizontalBarrier[0].GetWidth());
            Assert.Equal(308, cabinet.HorizontalBarrier[0].GetX());
            Assert.Equal(351, cabinet.HorizontalBarrier[0].GetY());
        }

        [Fact]
        public void Dodanie_najpierw_1_pionowej_bariery_nastepnie_poziomej_a_na_koncu_jeszcze_jedna_pionowa()
        {
            var cabinet = new Cabinet().Name("test_17");

            var vBarrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Single(cabinet.VerticalBarrier);

            var barrierParameter = new BarrierParameter { Number = 1 };
            
            cabinet.NewHorizontalBarrier(barrierParameter);
            
            Assert.Equal(2,cabinet.HorizontalBarrier.Count);

            vBarrierParameter.Number = 2;
            cabinet.NewVerticalBarrier(vBarrierParameter);

            Assert.Equal(2,cabinet.VerticalBarrier.Count);

            Assert.Equal(3, cabinet.HorizontalBarrier.Count);
            cabinet.Serialize();

        }

        [Fact]
        public void Dodanie_pionowej_bariery_potem_pozioma_2_razy_szybko()
        {
            var cabinet = new Cabinet().Name("test_18");

            var vBarrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewVerticalBarrier(vBarrierParameter);

            cabinet.AddHorizontalBarrier(1);
            cabinet.AddHorizontalBarrier(1);
        }
    }
}
