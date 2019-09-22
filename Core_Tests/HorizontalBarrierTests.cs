﻿using System.Collections.Generic;
using Core;
using Core.Model;
using Xunit;

namespace Core_Tests
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
            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        [Fact]
        public void Dodanie_2_przegrod_poziomych()
        {
            var cabinet = new Cabinet().Name("test_2");

            var barrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(234, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(468, cabinet.HorizontalBarrier[1].Ey);
        }

        [Fact]
        public void Dodanie_4_przegrod_poziomych()
        {
            var cabinet = new Cabinet().Name("test_3");

            var barrierParameter = new BarrierParameter { Number = 4 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(4, cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(140, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(280, cabinet.HorizontalBarrier[1].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[2].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[2].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[2].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[2].Ex);
            Assert.Equal(420, cabinet.HorizontalBarrier[2].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[3].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[3].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[3].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[3].Ex);
            Assert.Equal(560, cabinet.HorizontalBarrier[3].Ey);
        }

        [Fact]
        public void Dodanie_1_przegrody_potem_dodanie_nastepnej()
        {
            var cabinet = new Cabinet().Name("test_4");

            var barrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewHorizontalBarrier(barrierParameter);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);

            barrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(234, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(468, cabinet.HorizontalBarrier[1].Ey);
        }

        [Fact]
        public void Dodanie_1_polki()
        {
            var cabinet = new Cabinet().Name("test_5");

            var barrierParameter = new BarrierParameter { Number = 1,Back=5 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Single(cabinet.HorizontalBarrier);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(505, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

       [Fact]
        public void Dodanie_2_polek()
        {
            var cabinet = new Cabinet().Name("test_6");

            var barrierParameter = new BarrierParameter { Number = 2,Back=10 };

            cabinet.NewHorizontalBarrier(barrierParameter);
            cabinet.Serialize();

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(500, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(234, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(500, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(468, cabinet.HorizontalBarrier[1].Ey);
        }

        //[Fact]
        //public void Dodanie_1_polki_według_ustalonej_wysokosci()
        //{
        //    var cabinet = new Cabinet();

        //    var barrierParameter = new BarrierParameter { Number = 1 };
        //    barrierParameter.Height.Add(250);

        //    cabinet.AddHorizontalBarrier(barrierParameter);
            
        //    Assert.Single(cabinet.HorizontalBarrier);

        //    Assert.Equal(564, cabinet.HorizontalBarrier[0].EWidth);
        //    Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
        //    Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
        //    Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
        //    Assert.Equal(268, cabinet.HorizontalBarrier[0].Ey);
        //}

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
            Assert.Equal(273, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(273, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(309,cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[1].Ey);
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
            Assert.Equal(273, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(309, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
            Assert.Equal(176, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
            
            Assert.Equal(176, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(212, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[1].Ey);
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
            Assert.Equal(176, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(406, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
            Assert.Equal(176, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(212, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
            Assert.Equal(127, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
            Assert.Equal(129, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(453, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
            Assert.Equal(127, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(163, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
            Assert.Equal(127, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(308, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
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
    }
}
