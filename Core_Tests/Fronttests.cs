using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core;
using Xunit;

namespace Core_Tests
{
    public class FrontTests
    {
        /// <summary>
        /// Dodanie 1 frontu
        /// Add_1_front_to_default_cabinet
        /// </summary>
        [Fact]
        public void Dodanie_1_forntu()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            var front = cabinet.GetFront(0);

            Assert.Single(cabinet.GetFrontList());
            Assert.Equal(600,front.EWidth);
            Assert.Equal(720,front.EHeight);
            Assert.Equal(18,front.EDepth);
            Assert.Equal(0,front.Ex);
            Assert.Equal(0,front.Ey);
            Assert.Equal(510,front.Ez);
        }

        /// <summary>
        /// Dodanie 2 frontow pionowych
        /// Add_2_vertically_front_to_default_cabinet
        /// </summary>
        [Fact]
        public void Dodanie_2_pionowych_frontow()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(2,EnumFront.Pionowo);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);

            Assert.Equal(2,cabinet.GetFrontList().Count);

            Assert.Equal(298, front.EWidth);
            Assert.Equal(720, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(298, front1.EWidth);
            Assert.Equal(720, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(301, front1.Ex);
            Assert.Equal(0, front1.Ey);
            Assert.Equal(510, front1.Ez);
        }

        /// <summary>
        /// Dodanie 3 frontow pionowych
        /// Add_3_vertically_front_to_default_cabinet
        /// </summary>
        [Fact]
        public void Dodanie_3_pionowych_frontow()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);
            var front2 = cabinet.GetFront(2);

            Assert.Equal(3, cabinet.GetFrontList().Count);

            Assert.Equal(198, front.EWidth);
            Assert.Equal(720, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(198, front1.EWidth);
            Assert.Equal(720, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(201, front1.Ex);
            Assert.Equal(0, front1.Ey);
            Assert.Equal(510, front1.Ez);

            Assert.Equal(198, front2.EWidth);
            Assert.Equal(720, front2.EHeight);
            Assert.Equal(18, front2.EDepth);
            Assert.Equal(402, front2.Ex);
            Assert.Equal(0, front2.Ey);
            Assert.Equal(510, front2.Ez);
        }

        /// <summary>
        /// Dodanie 1 frontu z informacja o szczelinach
        /// Add_1_front_to_default_cabinet_with_list_of_slots
        /// </summary>
        [Fact]
        public void Dodanie_1_forntu_z_informacja_o_szczelinach()
        {
            var cabinet = new Cabinet();

            var slots = new Slots
            {
                top = 3,
                bottom = 3,
                left = 3,
                right = 3,
                betweenVertically = 3,
                betweenCabinet = 2
            };

            cabinet.AddFront(slots,1);

            var front = cabinet.GetFront(0);

            Assert.Single(cabinet.GetFrontList());
            Assert.Equal(594, front.EWidth);
            Assert.Equal(714, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(3, front.Ex);
            Assert.Equal(3, front.Ey);
            Assert.Equal(512, front.Ez);
        }

        /// <summary>
        /// dodanie 3 frontow z informacja o szczelinach
        /// Add_3_vartically_front_to_default_cabinet_with_list_of_slots
        /// </summary>
        [Fact]
        public void Dodanie_3_forntow_pionowych_z_informacja_o_szczelinach()
        {
            var cabinet = new Cabinet();

            var slots = new Slots
            {
                top = 3,
                bottom = 3,
                left = 3,
                right = 3,
                betweenVertically = 3,
                betweenCabinet = 2
            };

            cabinet.AddFront(slots,3);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);
            var front2 = cabinet.GetFront(2);

            Assert.Equal(3, cabinet.GetFrontList().Count);

            Assert.Equal(196, front.EWidth);
            Assert.Equal(714, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(3, front.Ex);
            Assert.Equal(3, front.Ey);
            Assert.Equal(512, front.Ez);

            Assert.Equal(196, front1.EWidth);
            Assert.Equal(714, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(202, front1.Ex);
            Assert.Equal(3, front1.Ey);
            Assert.Equal(512, front1.Ez);

            Assert.Equal(196, front2.EWidth);
            Assert.Equal(714, front2.EHeight);
            Assert.Equal(18, front2.EDepth);
            Assert.Equal(401, front2.Ex);
            Assert.Equal(3, front2.Ey);
            Assert.Equal(512, front2.Ez);
        }

        /// <summary>
        /// Dodanie 2 frontow poziomych
        /// Add_2_horizontally_front_to_default_cabinet
        /// </summary>
        [Fact]
        public void Dodanie_2_poziomych_frontow()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(2,EnumFront.Poziomo);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);
            
            Assert.Equal(2,cabinet.GetFrontList().Count);
            Assert.Equal(600, front.EWidth);
            Assert.Equal(358, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(600, front1.EWidth);
            Assert.Equal(358, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(0, front1.Ex);
            Assert.Equal(361, front1.Ey);
            Assert.Equal(510, front1.Ez);
        }

        /// <summary>
        /// dodanie 3 frontow poziomych z informacja o szczelinach
        /// Add_3_horizontally_front_to_Default_cabinet_with_list_of_slots
        /// </summary>
        [Fact]
        public void Dodanie_3_forntow_poziomych_z_informacja_o_szczelinach()
        {
            var cabinet = new Cabinet();

            var slots = new Slots
            {
                top = 3,
                bottom = 3,
                left = 3,
                right = 3,
                betweenVertically = 3,
                betweenCabinet = 2,
                betweenHorizontally = 3
            };

            cabinet.AddFront(slots, 3, EnumFront.Poziomo);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);
            var front2 = cabinet.GetFront(2);

            Assert.Equal(3, cabinet.GetFrontList().Count);

            Assert.Equal(594, front.EWidth);
            Assert.Equal(236, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(3, front.Ex);
            Assert.Equal(3, front.Ey);
            Assert.Equal(512, front.Ez);

            Assert.Equal(594, front1.EWidth);
            Assert.Equal(236, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(3, front1.Ex);
            Assert.Equal(242, front1.Ey);
            Assert.Equal(512, front1.Ez);

            Assert.Equal(594, front2.EWidth);
            Assert.Equal(236, front2.EHeight);
            Assert.Equal(18, front2.EDepth);
            Assert.Equal(3, front2.Ex);
            Assert.Equal(481, front2.Ey);
            Assert.Equal(512, front2.Ez);
        }

        [Fact]
        public void Dodanie_1_forntu_o_zadanych_wymiarach()
        {
            var cabinet = new Cabinet();

            var frontList = new List<Element>();

            var front = new Element
            {
                EName = EnumCabinetElement.Front,
                EDepth = 22,
                EHeight = 600,
                EWidth = 300,
                Ex = 12,
                Ey = 12,
                Ez = 513
            };

            frontList.Add(front);

            cabinet.AddFront(frontList);

            Assert.Single(cabinet.GetFrontList());
            Assert.Equal(300, front.EWidth);
            Assert.Equal(600, front.EHeight);
            Assert.Equal(22, front.EDepth);
            Assert.Equal(12, front.Ex);
            Assert.Equal(12, front.Ey);
            Assert.Equal(513, front.Ez);
        }

        [Fact]
        public void Dodanie_2_frontow_o_zadanych_wymiarach()
        {
            var cabinet = new Cabinet();

            var frontList = new List<Element>();

            var frontT = new Element
            {
                EName = EnumCabinetElement.Front,
                EDepth = 22,
                EHeight = 300,
                EWidth = 300,
                Ex = 12,
                Ey = 12,
                Ez = 513
            };

            var front1T = new Element
            {
                EName = EnumCabinetElement.Front,
                EDepth = 22,
                EHeight = 300,
                EWidth = 300,
                Ex = 12,
                Ey = 315,
                Ez = 513
            };

            frontList.Add(frontT);
            frontList.Add(front1T);

            cabinet.AddFront(frontList);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);
            
            Assert.Equal(2,cabinet.GetFrontList().Count);

            Assert.Equal(300, front.EWidth);
            Assert.Equal(300, front.EHeight);
            Assert.Equal(22, front.EDepth);
            Assert.Equal(12, front.Ex);
            Assert.Equal(12, front.Ey);
            Assert.Equal(513, front.Ez);


            Assert.Equal(300, front1.EWidth);
            Assert.Equal(300, front1.EHeight);
            Assert.Equal(22, front1.EDepth);
            Assert.Equal(12, front1.Ex);
            Assert.Equal(315, front1.Ey);
            Assert.Equal(513, front1.Ez);
        }

        [Fact]
        public void Dodanie_1_frontu_i_jego_modyfikacja()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            var front = cabinet.GetFront(0);
            
            Assert.Single(cabinet.GetFrontList());
            Assert.Equal(600, front.EWidth);
            Assert.Equal(720, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            var frontT = cabinet.GetFront(0);

            frontT.EWidth = 500;
            frontT.EHeight = 620;

            cabinet.UpdateFront(frontT);

            front = cabinet.GetFront(0);

            Assert.Single(cabinet.GetFrontList());
            Assert.Equal(500, front.EWidth);
            Assert.Equal(620, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);
        }

        [Fact]
        public void Dodanie_2_frontow_i_modyfikacja_1()
        {
            
            var cabinet = new Cabinet();

            cabinet.AddFront(2, EnumFront.Pionowo);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);

            Assert.Equal(2, cabinet.GetFrontList().Count);

            Assert.Equal(298, front.EWidth);
            Assert.Equal(720, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(298, front1.EWidth);
            Assert.Equal(720, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(301, front1.Ex);
            Assert.Equal(0, front1.Ey);
            Assert.Equal(510, front1.Ez);

            var frontT = cabinet.GetFront(0);

            frontT.EWidth = 100;
            frontT.EHeight = 620;

            cabinet.UpdateFront(frontT);

            front = cabinet.GetFront(0);
            front1 = cabinet.GetFront(1);

            Assert.Equal(2, cabinet.GetFrontList().Count);

            Assert.Equal(100, front.EWidth);
            Assert.Equal(620, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(298, front1.EWidth);
            Assert.Equal(720, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(301, front1.Ex);
            Assert.Equal(0, front1.Ey);
            Assert.Equal(510, front1.Ez);
        }

        [Fact]
        public void Dodanie_2_frontow_i_modyfikacja_2()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(2, EnumFront.Pionowo);

            var front = cabinet.GetFront(0);
            var front1 = cabinet.GetFront(1);

            Assert.Equal(2, cabinet.GetFrontList().Count);

            Assert.Equal(298, front.EWidth);
            Assert.Equal(720, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(298, front1.EWidth);
            Assert.Equal(720, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(301, front1.Ex);
            Assert.Equal(0, front1.Ey);
            Assert.Equal(510, front1.Ez);

            var frontT = cabinet.GetFront(1);

            frontT.EWidth = 100;
            frontT.EHeight = 620;

            cabinet.UpdateFront(frontT);

            front = cabinet.GetFront(0);
            front1 = cabinet.GetFront(1);
            Assert.Equal(2, cabinet.GetFrontList().Count);

            Assert.Equal(298, front.EWidth);
            Assert.Equal(720, front.EHeight);
            Assert.Equal(18, front.EDepth);
            Assert.Equal(0, front.Ex);
            Assert.Equal(0, front.Ey);
            Assert.Equal(510, front.Ez);

            Assert.Equal(100, front1.EWidth);
            Assert.Equal(620, front1.EHeight);
            Assert.Equal(18, front1.EDepth);
            Assert.Equal(301, front1.Ex);
            Assert.Equal(0, front1.Ey);
            Assert.Equal(510, front1.Ez);
        }

        [Fact]
        public void Dodanie_1_frontu_i_jego_usuniecie()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            Assert.Single(cabinet.GetFrontList());

            var front = cabinet.GetFront(0);

            cabinet.DeleteFront(front);
            Assert.Empty(cabinet.GetFrontList());
        }

        [Fact]
        public void Dodanie_3_frontow_i_usuniecie_1()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var frontList = cabinet.GetFrontList().ToList();

            Assert.Equal(3,frontList.Count);


            var front = cabinet.GetFront(0);

            cabinet.DeleteFront(front);

            Assert.Equal(2,cabinet.GetFrontList().Count);

            Assert.NotEqual(cabinet.GetFrontList(),frontList);

        }

        [Fact]
        public void Dodanie_3_frontow_i_usuniecie_2()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var frontList = cabinet.GetFrontList().ToList();

            Assert.Equal(3, frontList.Count);


            var front = cabinet.GetFront(1);

            cabinet.DeleteFront(front);

            
            Assert.Equal(2, cabinet.GetFrontList().Count);

            Assert.NotEqual(cabinet.GetFrontList(), frontList);
        }

        [Fact]
        public void Dodanie_3_frontow_i_usuniecie_3()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            var frontList = cabinet.GetFrontList().ToList();

            Assert.Equal(3, frontList.Count);


            var front = cabinet.GetFront(2);

            cabinet.DeleteFront(front);


            Assert.Equal(2, cabinet.GetFrontList().Count);

            Assert.NotEqual(cabinet.GetFrontList(), frontList);
        }


    }
}