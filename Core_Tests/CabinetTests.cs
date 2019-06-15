using Core;
using Xunit;

namespace Core_Tests
{
    public class CabinetTests
    {
        /// <summary>
        /// Utworzenie wzorcowej szafki o wymiarach (w x s x g) 720 x 600 x 510
        /// </summary>
        [Fact]
        public void Create_default_cabinet()
        {
            var cabinet = new Cabinet();
            
            Assert.NotNull(cabinet);
            Assert.Equal(720, cabinet.Height);
            Assert.Equal(600,cabinet.Width);
            Assert.Equal(510,cabinet.Depth);
            Assert.Equal(18,cabinet.SizeElement);
            Assert.Equal(3,cabinet.BackSize);
        }

        /// <summary>
        /// Utworzenie szafki o wymiarach (w x s x g) 300 x 600 x 250   
        /// </summary>
        [Fact]
        public void Create_cabinet_for_the_dimensions()
        {
            var cabinet = new Cabinet(300,600,250);

            Assert.NotNull(cabinet);
            Assert.Equal(300,cabinet.Height);
            Assert.Equal(600, cabinet.Width);
            Assert.Equal(250, cabinet.Depth);
        }

        /// <summary>
        /// utworzenie wzorcowej szafki i zmiana parametrow
        /// </summary>
        [Fact]
        public void Create_default_cabinet_and_change_dimensions()
        {
            var cabinet=new Cabinet();
            Assert.Equal(720,cabinet.Height);

            cabinet.Height = 500;
            Assert.Equal(500,cabinet.Height);

            cabinet.Width = 550;
            Assert.Equal(550,cabinet.Width);

            cabinet.Depth = 550;
            Assert.Equal(550, cabinet.Depth);
        }
        
        /// <summary>
        /// Dodanie nakladanych plecow w wzorcowej szafce
        /// </summary>
        [Fact]
        public void Add_default_back_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddBack();

            Assert.Equal(EnumBack.Nakladane,cabinet.Back);
        }

        /// <summary>
        /// Dodanie wpuszczanych plecow w wzorcowej szafce
        /// </summary>
        [Fact]
        public void Add_mortice_back_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddBack(EnumBack.Wpuszczane);

            Assert.Equal(EnumBack.Wpuszczane, cabinet.Back);
        }

        //[Fact]
        //public void Serialize()
        //{
        //    var cabinet = new Cabinet
        //    {
        //        Name = "Dolna_60"
        //    };
        //    cabinet.AddHorizontalBarrier(new Core.Model.BarrierParameter {Number = 3});
        //    cabinet.AddVerticalBarrier(new Core.Model.BarrierParameter {Number = 2});
        //    cabinet.Serialize();


        //}
    }
}
