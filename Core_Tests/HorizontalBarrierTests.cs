using Core;
using Xunit;

namespace Core_Tests
{
    public class HorizontalBarrierTests
    {
        /// <summary>
        /// Dodanie przegrody poziomej
        /// </summary>
        [Fact]
        public void Add_horizontal_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddHorizontalBarrier(1);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        /// <summary>
        /// Dodanie 2 przegrod poziomych
        /// </summary>
        [Fact]
        public void Add_2_horizontal_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddHorizontalBarrier(2);

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);
            Assert.Equal(564, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(234, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(468, cabinet.HorizontalBarrier[1].Ey);
        }


        /// <summary>
        /// Dodanie 4 przegrod poziomych
        /// </summary>
        [Fact]
        public void Add_4_horizontal_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddHorizontalBarrier(4);

            Assert.Equal(4, cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(140, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(280, cabinet.HorizontalBarrier[1].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[2].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[2].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[2].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[2].Ex);
            Assert.Equal(420, cabinet.HorizontalBarrier[2].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[3].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[3].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[3].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[3].Ex);
            Assert.Equal(560, cabinet.HorizontalBarrier[3].Ey);
        }


        /// <summary>
        /// Dodanie polki
        /// </summary>
        [Fact]
        public void Add_horizontal_barrier_as_shelf_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddHorizontalBarrier(1, back: 5);

            Assert.Single(cabinet.HorizontalBarrier);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(505, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        /// <summary>
        /// Dodanie 2 polek
        /// </summary>
        [Fact]
        public void Add_2_horizontal_barrier_as_shelf_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddHorizontalBarrier(2, back: 10);

            Assert.Equal(2, cabinet.HorizontalBarrier.Count);

            Assert.Equal(564, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(500, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(234, cabinet.HorizontalBarrier[0].Ey);

            Assert.Equal(564, cabinet.HorizontalBarrier[1].EWidth);
            Assert.Equal(500, cabinet.HorizontalBarrier[1].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[1].Ex);
            Assert.Equal(468, cabinet.HorizontalBarrier[1].Ey);
        }

        /// <summary>
        /// Dodanie polki według ustalonej wysokosci
        /// </summary>
        [Fact]
        public void Add_horizonatl_barrier_with_declare_size()
        {
            var cabinet = new Cabinet();

            cabinet.AddHorizontalBarrier(2, back: 10);
        }

        /// <summary>
        /// Dodanie przegrody poziomej gdy jest przegroda pionowa 
        /// </summary>
        [Fact]
        public void Add_vertical_barrier_first_and_a_horizontal_one()
        {
            var cabinet = new Cabinet();

            cabinet.AddVerticalBarrier(1);

            Assert.Single(cabinet.VerticalBarrier);

            cabinet.AddHorizontalBarrier(1);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(273, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        /// <summary>
        /// Dodanie najpierw pionowej bariery a potem poziomej do drugiej kolumny
        /// </summary>
        [Fact]
        public void Add_vertical_barrier_first_and_a_horizontal_to_the_second()
        {
            var cabinet = new Cabinet();

            cabinet.AddVerticalBarrier(1);

            Assert.Single(cabinet.VerticalBarrier);

            cabinet.AddHorizontalBarrier(1, 1);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(273, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(309, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        /// <summary>
        /// dodanie najpierw dwoch pionowych barier a potem poziomej do pierwszej kolumny
        /// </summary>
        [Fact]
        public void Add_two_vertical_barrier_first_and_a_horizontal_to_the_first()
        {
            var cabinet = new Cabinet();

            cabinet.AddVerticalBarrier(2);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);

            cabinet.AddHorizontalBarrier(1);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(176, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        /// <summary>
        /// dodanie najpierw dwoch pionowych barier a potem poziomej do trzeciej - ostatniej - kolumny
        /// </summary>
        [Fact]
        public void Add_two_vertical_barrier_first_and_a_horizontal_to_the_third()
        {
            var cabinet = new Cabinet();

            cabinet.AddVerticalBarrier(2);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);

            cabinet.AddHorizontalBarrier(1,2);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(176, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(406, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }

        /// <summary>
        /// dodanie najpierw dwoch pionowych barier a potem poziomej do drugiej - srodkowej - kolumny
        /// </summary>
        [Fact]
        public void Add_two_vertical_barrier_first_and_a_horizontal_to_the_second()
        {
            var cabinet = new Cabinet();

            cabinet.AddVerticalBarrier(2);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);

            cabinet.AddHorizontalBarrier(1, 1);

            Assert.Single(cabinet.HorizontalBarrier);
            Assert.Equal(176, cabinet.HorizontalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.HorizontalBarrier[0].EDepth);
            Assert.Equal(18, cabinet.HorizontalBarrier[0].EHeight);
            Assert.Equal(212, cabinet.HorizontalBarrier[0].Ex);
            Assert.Equal(351, cabinet.HorizontalBarrier[0].Ey);
        }
    }
}
