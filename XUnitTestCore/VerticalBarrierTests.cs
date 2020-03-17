using CoreS;
using CoreS.Model;
using Xunit;

namespace XUnitTestCore
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
            Assert.Equal(18, cabinet.VerticalBarrier[0].Width);
            Assert.Equal(510, cabinet.VerticalBarrier[0].Depth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].Height);
            Assert.Equal(291, cabinet.VerticalBarrier[0].X);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Y);
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

            Assert.Equal(18, cabinet.VerticalBarrier[0].Width);
            Assert.Equal(510, cabinet.VerticalBarrier[0].Depth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].Height);
            Assert.Equal(194, cabinet.VerticalBarrier[0].X);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Y);
            Assert.Equal(18, cabinet.VerticalBarrier[1].Width);
            Assert.Equal(510, cabinet.VerticalBarrier[1].Depth);
            Assert.Equal(684, cabinet.VerticalBarrier[1].Height);
            Assert.Equal(388, cabinet.VerticalBarrier[1].X);
            Assert.Equal(18, cabinet.VerticalBarrier[1].Y);
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
            Assert.Equal(18, cabinet.VerticalBarrier[0].Width);
            Assert.Equal(505, cabinet.VerticalBarrier[0].Depth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].Height);
            Assert.Equal(291, cabinet.VerticalBarrier[0].X);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Y);

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

            Assert.Equal(18, cabinet.VerticalBarrier[0].Width);
            Assert.Equal(505, cabinet.VerticalBarrier[0].Depth);
            Assert.Equal(684, cabinet.VerticalBarrier[0].Height);
            Assert.Equal(194, cabinet.VerticalBarrier[0].X);
            Assert.Equal(18, cabinet.VerticalBarrier[0].Y);
            Assert.Equal(18, cabinet.VerticalBarrier[1].Width);
            Assert.Equal(505, cabinet.VerticalBarrier[1].Depth);
            Assert.Equal(684, cabinet.VerticalBarrier[1].Height);
            Assert.Equal(388, cabinet.VerticalBarrier[1].X);
            Assert.Equal(18, cabinet.VerticalBarrier[1].Y);
        }
    }
}