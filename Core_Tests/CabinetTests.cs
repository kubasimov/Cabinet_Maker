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
            Assert.Equal(18,cabinet.Size);
            Assert.Equal(3,cabinet.BackSize);
        }

        /// <summary>
        /// Utworzenie szafki o wymiarach (w x s x g) 300 x 600 x 250   
        /// </summary>
        [Fact]
        public void Create_cabinet()
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
        public void Create_default_cabinet_and_change_height()
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

            Assert.Equal(Back.Imposed,cabinet.Back);
        }

        /// <summary>
        /// Dodanie wpuszczanych plecow w wzorcowej szafce
        /// </summary>
        [Fact]
        public void Add_mortice_back_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddBack(Back.Mortice);

            Assert.Equal(Back.Mortice, cabinet.Back);
        }
    }
}
