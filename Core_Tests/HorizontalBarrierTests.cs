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

        }

        /// <summary>
        /// Dodanie 2 polek
        /// </summary>
        [Fact]
        public void Add_2_horizontal_barrier_as_shelf_to_default_cabinet()
        {

        }
    }
}
