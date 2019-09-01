using Core;
using Core.Model;
using Xunit;

namespace Core_Tests
{
    public class VerticalBarrierTests
    {
        /// <summary>
        /// Dodanie przegrody pionowej
        /// </summary>
        [Fact]
        public void Add_vertical_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();
            var barrierParameter = new BarrierParameter { Number = 1 };

            cabinet.NewVerticalBarrier(barrierParameter);

            Assert.Single(cabinet.VerticalBarrier);
            Assert.Equal(18, cabinet.VerticalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.VerticalBarrier[0].EDepth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].EHeight);
            Assert.Equal(291, cabinet.VerticalBarrier[0].Ex);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Ey);
        }

        /// <summary>
        /// Dodanie 2 przegrod pionowych
        /// </summary>
        [Fact]
        public void Add_2_vertical_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            var barrierParameter = new BarrierParameter { Number = 2 };

            cabinet.NewVerticalBarrier(barrierParameter);

            Assert.Equal(2,cabinet.VerticalBarrier.Count);

            Assert.Equal(18, cabinet.VerticalBarrier[0].EWidth);
            Assert.Equal(510, cabinet.VerticalBarrier[0].EDepth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].EHeight);
            Assert.Equal(194, cabinet.VerticalBarrier[0].Ex);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Ey);
            Assert.Equal(18, cabinet.VerticalBarrier[1].EWidth);
            Assert.Equal(510, cabinet.VerticalBarrier[1].EDepth);
            Assert.Equal(684, cabinet.VerticalBarrier[1].EHeight);
            Assert.Equal(388, cabinet.VerticalBarrier[1].Ex);
            Assert.Equal(18, cabinet.VerticalBarrier[1].Ey);
        }

        /// <summary>
        /// Dodanie płytszej przegrody pionowej
        /// </summary>
        [Fact]
        public void Add_shallwo_vertical_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            var barrierParameter = new BarrierParameter { Number = 1,Back=5 };

            cabinet.NewVerticalBarrier(barrierParameter);

            Assert.Single(cabinet.VerticalBarrier);
            Assert.Equal(18, cabinet.VerticalBarrier[0].EWidth);
            Assert.Equal(505, cabinet.VerticalBarrier[0].EDepth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].EHeight);
            Assert.Equal(291, cabinet.VerticalBarrier[0].Ex);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Ey);

        }

        /// <summary>
        /// Dodanie 2 płytszych przegrod pionowych
        /// </summary>
        [Fact]
        public void Add_2_shallow_barrier_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            var barrierParameter = new BarrierParameter { Number = 2,Back=5 };

            cabinet.NewVerticalBarrier(barrierParameter);

            Assert.Equal(2, cabinet.VerticalBarrier.Count);

            Assert.Equal(18, cabinet.VerticalBarrier[0].EWidth);
            Assert.Equal(505, cabinet.VerticalBarrier[0].EDepth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].EHeight);
            Assert.Equal(194, cabinet.VerticalBarrier[0].Ex);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Ey);
            Assert.Equal(18, cabinet.VerticalBarrier[1].EWidth);
            Assert.Equal(505, cabinet.VerticalBarrier[1].EDepth);
            Assert.Equal(684, cabinet.VerticalBarrier[1].EHeight);
            Assert.Equal(388, cabinet.VerticalBarrier[1].Ex);
            Assert.Equal(18, cabinet.VerticalBarrier[1].Ey);
        }
    }
}