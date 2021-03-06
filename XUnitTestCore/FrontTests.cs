﻿using Config;
using CoreS;
using CoreS.Enum;
using CoreS.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestCore
{
    public class FrontTests
    {
        private IConfig _config;
        public FrontTests()
        {
            _config = new Config.Config();
        }
        
        [Fact]
        public void Dodanie_1_forntu()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            var front = cabinet.GetAllFront().First();

            Assert.Single(cabinet.GetAllFront());
            Assert.Equal(594,front.Width);
            Assert.Equal(714,front.Height);
            Assert.Equal(18,front.Depth);
            Assert.Equal(3,front.X);
            Assert.Equal(3,front.Y);
            Assert.Equal(512,front.Z);
        }

        [Fact]
        public void Dodanie_2_pionowych_frontow()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(2,EnumFront.Pionowo);

            var front = cabinet.GetAllFront().First();
            var front1 = cabinet.GetAllFront().ElementAt(1);

            Assert.Equal(2,cabinet.GetAllFront().Count);

            Assert.Equal(295, front.Width);
            Assert.Equal(714, front.Height);
            Assert.Equal(18, front.Depth);
            Assert.Equal(3, front.X);
            Assert.Equal(3, front.Y);
            Assert.Equal(512, front.Z);

            Assert.Equal(295, front1.Width);
            Assert.Equal(714, front1.Height);
            Assert.Equal(18, front1.Depth);
            Assert.Equal(301, front1.X);
            Assert.Equal(3, front1.Y);
            Assert.Equal(512, front1.Z);
        }

        [Fact]
        public void Dodanie_3_pionowych_frontow()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var front = cabinet.GetAllFront().First();
            var front1 = cabinet.GetAllFront().ElementAt(1);
            var front2 = cabinet.GetAllFront().ElementAt(2);

            Assert.Equal(3, cabinet.GetAllFront().Count);

            Assert.Equal(196, front.Width);
            Assert.Equal(714, front.Height);
            Assert.Equal(18, front.Depth);
            Assert.Equal(3, front.X);
            Assert.Equal(3, front.Y);
            Assert.Equal(512, front.Z);

            Assert.Equal(196, front1.Width);
            Assert.Equal(714, front1.Height);
            Assert.Equal(18, front1.Depth);
            Assert.Equal(202, front1.X);
            Assert.Equal(3, front1.Y);
            Assert.Equal(512, front1.Z);

            Assert.Equal(196, front2.Width);
            Assert.Equal(714, front2.Height);
            Assert.Equal(18, front2.Depth);
            Assert.Equal(401, front2.X);
            Assert.Equal(3, front2.Y);
            Assert.Equal(512, front2.Z);
        }

        [Fact]
        public void Dodanie_1_forntu_z_informacja_o_szczelinach()
        {
            var cabinet = new Cabinet();

            var slots = new SlotsModel(_config)
            {
                Top = 3,
                Bottom = 3,
                Left = 3,
                Right = 3,
                BetweenVertically = 3,
                BetweenCabinet = 2
            };

            cabinet.AddFront(slots,1);

            var front = cabinet.GetAllFront().First();

            Assert.Single(cabinet.GetAllFront());
            Assert.Equal(594, front.Width);
            Assert.Equal(714, front.Height);
            Assert.Equal(18, front.Depth);
            Assert.Equal(3, front.X);
            Assert.Equal(3, front.Y);
            Assert.Equal(512, front.Z);
        }

        [Fact]
        public void Dodanie_3_forntow_pionowych_z_informacja_o_szczelinach()
        {
            var cabinet = new Cabinet();

            var slots = new SlotsModel(_config)
            {
                Top = 3,
                Bottom = 3,
                Left = 3,
                Right = 3,
                BetweenVertically = 3,
                BetweenCabinet = 2
            };

            cabinet.AddFront(slots,3);

            var front = cabinet.GetAllFront().First();
            var front1 = cabinet.GetAllFront().ElementAt(1);
            var front2 = cabinet.GetAllFront().ElementAt(2);

            Assert.Equal(3, cabinet.GetAllFront().Count);

            Assert.Equal(196, front.Width);
            Assert.Equal(714, front.Height);
            Assert.Equal(18, front.Depth);
            Assert.Equal(3, front.X);
            Assert.Equal(3, front.Y);
            Assert.Equal(512, front.Z);

            Assert.Equal(196, front1.Width);
            Assert.Equal(714, front1.Height);
            Assert.Equal(18, front1.Depth);
            Assert.Equal(202, front1.X);
            Assert.Equal(3, front1.Y);
            Assert.Equal(512, front1.Z);

            Assert.Equal(196, front2.Width);
            Assert.Equal(714, front2.Height);
            Assert.Equal(18, front2.Depth);
            Assert.Equal(401, front2.X);
            Assert.Equal(3, front2.Y);
            Assert.Equal(512, front2.Z);
        }

        [Fact]
        public void Dodanie_2_poziomych_frontow()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(2,EnumFront.Poziomo);

            var front = cabinet.GetAllFront().First();
            var front1 = cabinet.GetAllFront().ElementAt(1);

            Assert.Equal(2,cabinet.GetAllFront().Count);
            
            Assert.Equal(594, front.Width);
            Assert.Equal(355, front.Height);
            Assert.Equal(18, front.Depth);
            Assert.Equal(3, front.X);
            Assert.Equal(3, front.Y);
            Assert.Equal(512, front.Z);

            Assert.Equal(594, front1.Width);
            Assert.Equal(355, front1.Height);
            Assert.Equal(18, front1.Depth);
            Assert.Equal(3, front1.X);
            Assert.Equal(361, front1.Y);
            Assert.Equal(512, front1.Z);
        }

        [Fact]
        public void Dodanie_3_forntow_poziomych_z_informacja_o_szczelinach()
        {
            var cabinet = new Cabinet();

            var slots = new SlotsModel(_config)
            {
                Top = 3,
                Bottom = 3,
                Left = 3,
                Right = 3,
                BetweenVertically = 3,
                BetweenCabinet = 2,
                BetweenHorizontally = 3
            };

            cabinet.AddFront(slots, 3, EnumFront.Poziomo);

            var front = cabinet.GetAllFront().First();
            var front1 = cabinet.GetAllFront().ElementAt(1);
            var front2 = cabinet.GetAllFront().ElementAt(2);

            Assert.Equal(3, cabinet.GetAllFront().Count);

            Assert.Equal(594, front.Width);
            Assert.Equal(236, front.Height);
            Assert.Equal(18, front.Depth);
            Assert.Equal(3, front.X);
            Assert.Equal(3, front.Y);
            Assert.Equal(512, front.Z);

            Assert.Equal(594, front1.Width);
            Assert.Equal(236, front1.Height);
            Assert.Equal(18, front1.Depth);
            Assert.Equal(3, front1.X);
            Assert.Equal(242, front1.Y);
            Assert.Equal(512, front1.Z);

            Assert.Equal(594, front2.Width);
            Assert.Equal(236, front2.Height);
            Assert.Equal(18, front2.Depth);
            Assert.Equal(3, front2.X);
            Assert.Equal(481, front2.Y);
            Assert.Equal(512, front2.Z);
        }

        [Fact]
        public void Dodanie_1_forntu_o_zadanych_wymiarach()
        {
            var cabinet = new Cabinet();

            var frontList = new List<ElementModel>();

            var front = new ElementModel(
                description: "Front",
                height: 600,
                width: 300,
                depth: 22,
                x: 12,
                y: 12,
                z: 513,
                enumCabinet: EnumCabinetElement.Front,
                horizontal: false
                );
            
            frontList.Add(front);

            Assert.Equal(600, front.Height);
            Assert.Equal(22, front.Depth);
            Assert.Equal(12, front.X);
            Assert.Equal(12, front.Y);
            Assert.Equal(513, front.Z);
        }

        [Fact]
        public void Dodanie_2_frontow_o_zadanych_wymiarach()
        {
            var cabinet = new Cabinet();

            var frontList = new List<ElementModelDTO>();

            var frontT = new ElementModelDTO(
                description: "",
                height: 300,
                width: 300,
                depth: 22,
                x: 12,
                y: 12,
                z: 513,
                enumCabinet: EnumCabinetElement.Front,
                horizontal: false);
            
            var front1T = new ElementModelDTO(
                description: "",
                height: 300,
                width: 300,
                depth: 22,
                x: 12,
                y: 315,
                z: 513,
                enumCabinet: EnumCabinetElement.Front,
                horizontal: false);
            
            frontList.Add(frontT);
            frontList.Add(front1T);

            cabinet.AddFront(frontList);

            var front = cabinet.GetAllFront().First();
            var front1 = cabinet.GetAllFront().ElementAt(1);

            Assert.Equal(2,cabinet.GetAllFront().Count);

            Assert.Equal(300, front.Width);
            Assert.Equal(300, front.Height);
            Assert.Equal(22, front.Depth);
            Assert.Equal(12, front.X);
            Assert.Equal(12, front.Y);
            Assert.Equal(513, front.Z);


            Assert.Equal(300, front1.Width);
            Assert.Equal(300, front1.Height);
            Assert.Equal(22, front1.Depth);
            Assert.Equal(12, front1.X);
            Assert.Equal(315, front1.Y);
            Assert.Equal(513, front1.Z);
        }

        //[Fact]
        //public void Dodanie_1_frontu_i_jego_modyfikacja()
        //{
        //    var cabinet = new Cabinet();
            
        //    cabinet.AddFront(1);

        //    var front = cabinet.GetAllFront().First();

        //    Assert.Single(cabinet.GetAllFront());
        //    Assert.Equal(594, front.Width);
        //    Assert.Equal(714, front.Height);
        //    Assert.Equal(18, front.Depth);
        //    Assert.Equal(3, front.X);
        //    Assert.Equal(3, front.Y);
        //    Assert.Equal(512, front.Z);

        //    var frontT = cabinet.GetAllFront().First();

        //    frontT.SetWidth(500);
        //    frontT.SetHeight(620);

        //    cabinet.UpdateFront(  frontT);

        //    front = cabinet.GetAllFront().First();

        //    Assert.Single(cabinet.GetAllFront());
        //    Assert.Equal(500, front.Width);
        //    Assert.Equal(620, front.Height);
        //    Assert.Equal(18, front.Depth);
        //    Assert.Equal(3, front.X);
        //    Assert.Equal(3, front.Y);
        //    Assert.Equal(512, front.Z);
        //}

        //[Fact]
        //public void Dodanie_2_frontow_i_modyfikacja_1()
        //{
            
        //    var cabinet = new Cabinet();

        //    cabinet.AddFront(2, EnumFront.Pionowo);

        //    var front = cabinet.GetAllFront().First();
        //    var front1 = cabinet.GetAllFront().ElementAt(1);

        //    Assert.Equal(2, cabinet.GetAllFront().Count);

        //    Assert.Equal(295, front.Width);
        //    Assert.Equal(714, front.Height);
        //    Assert.Equal(18, front.Depth);
        //    Assert.Equal(3, front.X);
        //    Assert.Equal(3, front.Y);
        //    Assert.Equal(512, front.Z);

        //    Assert.Equal(295, front1.Width);
        //    Assert.Equal(714, front1.Height);
        //    Assert.Equal(18, front1.Depth);
        //    Assert.Equal(301, front1.X);
        //    Assert.Equal(3, front1.Y);
        //    Assert.Equal(512, front1.Z);

        //    var frontT = cabinet.GetAllFront().First();

        //    frontT.SetWidth(100);
        //    frontT.SetHeight(620);

        //    cabinet.UpdateFront(frontT);

        //    front = cabinet.GetAllFront().First();
        //    front1 = cabinet.GetAllFront().ElementAt(1);

        //    Assert.Equal(2, cabinet.GetAllFront().Count);

        //    Assert.Equal(100, front.Width);
        //    Assert.Equal(620, front.Height);
        //    Assert.Equal(18, front.Depth);
        //    Assert.Equal(3, front.X);
        //    Assert.Equal(3, front.Y);
        //    Assert.Equal(512, front.Z);

        //    Assert.Equal(295, front1.Width);
        //    Assert.Equal(714, front1.Height);
        //    Assert.Equal(18, front1.Depth);
        //    Assert.Equal(301, front1.X);
        //    Assert.Equal(3, front1.Y);
        //    Assert.Equal(512, front1.Z);
        //}

        //[Fact]
        //public void Dodanie_2_frontow_i_modyfikacja_2()
        //{
        //    var cabinet = new Cabinet();

        //    cabinet.AddFront(2, EnumFront.Pionowo);

        //    var front = cabinet.GetAllFront().First();
        //    var front1 = cabinet.GetAllFront().ElementAt(1);

        //    Assert.Equal(2, cabinet.GetAllFront().Count);

        //    Assert.Equal(295, front.Width);
        //    Assert.Equal(714, front.Height);
        //    Assert.Equal(18, front.Depth);
        //    Assert.Equal(3, front.X);
        //    Assert.Equal(3, front.Y);
        //    Assert.Equal(512, front.Z);

        //    Assert.Equal(295, front1.Width);
        //    Assert.Equal(714, front1.Height);
        //    Assert.Equal(18, front1.Depth);
        //    Assert.Equal(301, front1.X);
        //    Assert.Equal(3, front1.Y);
        //    Assert.Equal(512, front1.Z);

        //    front1.SetWidth(100);
        //    front1.SetHeight(620);

        //    cabinet.UpdateFront(front1);

        //    front = cabinet.GetAllFront().First();
        //    front1 = cabinet.GetAllFront().ElementAt(1);
        //    Assert.Equal(2, cabinet.GetAllFront().Count);

        //    Assert.Equal(295, front.Width);
        //    Assert.Equal(714, front.Height);
        //    Assert.Equal(18, front.Depth);
        //    Assert.Equal(3, front.X);
        //    Assert.Equal(3, front.Y);
        //    Assert.Equal(512, front.Z);

        //    Assert.Equal(100, front1.Width);
        //    Assert.Equal(620, front1.Height);
        //    Assert.Equal(18, front1.Depth);
        //    Assert.Equal(301, front1.X);
        //    Assert.Equal(3, front1.Y);
        //    Assert.Equal(512, front1.Z);
        //}

        [Fact]
        public void Dodanie_1_frontu_i_jego_usuniecie()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            Assert.Single(cabinet.GetAllFront());

            var front = cabinet.GetAllFront().First();

            cabinet.DeleteFront(front);
            Assert.Empty(cabinet.GetAllFront());
        }

        [Fact]
        public void Dodanie_3_frontow_i_usuniecie_1()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var frontList = cabinet.GetAllFront().ToList();

            Assert.Equal(3,frontList.Count);


            var front = cabinet.GetAllFront().First();

            cabinet.DeleteFront(front);

            Assert.Equal(2,cabinet.GetAllFront().Count);

            Assert.NotEqual(cabinet.GetAllFront(),frontList);

        }

        [Fact]
        public void Dodanie_3_frontow_i_usuniecie_2()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var frontList = cabinet.GetAllFront().ToList();

            Assert.Equal(3, frontList.Count);


            var front = cabinet.GetAllFront().ElementAt(1);

            cabinet.DeleteFront(front);

            
            Assert.Equal(2, cabinet.GetAllFront().Count);

            Assert.NotEqual(cabinet.GetAllFront(), frontList);
        }

        [Fact]
        public void Dodanie_3_frontow_i_usuniecie_3()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var frontList = cabinet.GetAllFront().ToList();

            Assert.Equal(3, frontList.Count);


            var front = cabinet.GetAllFront().ElementAt(2);

            cabinet.DeleteFront(front);


            Assert.Equal(2, cabinet.GetAllFront().Count);

            Assert.NotEqual(cabinet.GetAllFront(), frontList);
        }

        //[Fact]
        //public void Dodanie_1_frontu_potem_jego_modyfikacja_i_dodanie_kolejnego()
        //{
        //    var cabinet = new Cabinet();

        //    cabinet.AddFront(1);

        //    var frontlist = cabinet.GetAllFront();

        //    Assert.Single(cabinet.GetAllFront());
        //    Assert.Equal(594, frontlist[0].Width);
        //    Assert.Equal(714, frontlist[0].Height);
        //    Assert.Equal(18, frontlist[0].Depth);
        //    Assert.Equal(3, frontlist[0].X);
        //    Assert.Equal(3, frontlist[0].Y);
        //    Assert.Equal(512, frontlist[0].Z);

        //    var front = cabinet.GetAllFront().First();

        //    front.SetWidth(200);
        //    cabinet.UpdateFront(front);

        //    cabinet.AddFront(1);

        //    Assert.Equal(2, cabinet.GetFrontCount());
        //    Assert.Equal(200, cabinet.GetAllFront()[0].Width);
        //    Assert.Equal(714, cabinet.GetAllFront()[0].Height);
        //    Assert.Equal(18, cabinet.GetAllFront()[0].Depth);
        //    Assert.Equal(3, cabinet.GetAllFront()[0].X);
        //    Assert.Equal(3, cabinet.GetAllFront()[0].Y);
        //    Assert.Equal(512, cabinet.GetAllFront()[0].Z);
        //}
    }
}