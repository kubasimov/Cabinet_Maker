using System;
using System.IO;
using System.Windows;
using Core;
using FluentAssertions;
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
            Assert.Equal(720, cabinet.Height());
            Assert.Equal(600,cabinet.Width());
            Assert.Equal(510,cabinet.Depth());
            Assert.Equal(18,cabinet.SizeElement());
            Assert.Equal("Default",cabinet.Name());
        }

        /// <summary>
        /// Utworzenie szafki o wymiarach (w x s x g) 300 x 600 x 250, nazwa Def   
        /// </summary>
        [Theory]
        [InlineData(800, 500, 200, "lll")]
        [InlineData(1200, 150, 235, "zzz")]
        public void Create_cabinet_for_the_dimensions(int height, int width, int depth, string name)
        {
            var cabinet = new Cabinet().Height(height).Width(width).Depth(depth).Name(name); ;

            Assert.NotNull(cabinet);
            Assert.Equal(height, cabinet.Height());
            Assert.Equal(width, cabinet.Width());
            Assert.Equal(depth, cabinet.Depth());
            Assert.Equal(18, cabinet.SizeElement());
            Assert.Equal(name, cabinet.Name());
            cabinet.Name().Should().Be(name);
        }

        /// <summary>
        /// utworzenie wzorcowej szafki i zmiana parametrow
        /// </summary>
        [Fact]
        public void Create_default_cabinet_and_change_dimensions()
        {
            var cabinet=new Cabinet();

            Assert.Equal(720, cabinet.Height());
            Assert.Equal(600, cabinet.Width());
            Assert.Equal(510, cabinet.Depth());
            Assert.Equal(18, cabinet.SizeElement());
            Assert.Equal("Default", cabinet.Name());
            
            cabinet.Height(500);
            cabinet.Width(550);
            cabinet.Depth(550);

            Assert.Equal(500,cabinet.Height());
            Assert.Equal(550,cabinet.Width());
            Assert.Equal(550, cabinet.Depth());
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

        [Fact]
        public void Serialize_Deserialize()
        {
            var cabinet = new Cabinet();
            cabinet.Serialize();

            cabinet.Deserialize();

            Assert.NotNull(cabinet);
            Assert.Equal(720, cabinet.Height());
            Assert.Equal(600, cabinet.Width());
            Assert.Equal(510, cabinet.Depth());
            Assert.Equal(18, cabinet.SizeElement());
            Assert.Equal("Default", cabinet.Name());
        }

        //[Theory]
        //[InlineData(800,500,200,"lll")]
        //[InlineData(1200,150,235,"zzz")]
        //public void Serialize_Deserialize_of_a_modified_cabinet(int height,int width,int depth,string name)
        //{
        //    var cabinet = new Cabinet().Height(height).Width(width).Depth(depth).Name(name);
        //    cabinet.Serialize();

        //    cabinet.Deserialize();

        //    Assert.NotNull(cabinet);
        //    Assert.Equal(height, cabinet.Height());
        //    Assert.Equal(width, cabinet.Width());
        //    Assert.Equal(depth, cabinet.Depth());
        //    Assert.Equal(18, cabinet.SizeElement());
        //    Assert.Equal(name, cabinet.Name());
        //}

    }
}
