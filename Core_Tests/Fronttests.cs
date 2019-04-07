using System.Collections.Generic;
using Core;
using Xunit;

namespace Core_Tests
{
    public class FrontTests
    {
        /// <summary>
        /// Dodanie frontu/frontow
        /// </summary>
        [Fact]
        public void Add_1_front_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            Assert.Single(cabinet.FrontList);
            Assert.Equal(600,cabinet.FrontList[0].EWidth);
            Assert.Equal(720,cabinet.FrontList[0].EHeight);
            Assert.Equal(18,cabinet.FrontList[0].EDepth);
            Assert.Equal(0,cabinet.FrontList[0].Ex);
            Assert.Equal(0,cabinet.FrontList[0].Ey);
            Assert.Equal(510,cabinet.FrontList[0].Ez);
        }

        [Fact]
        public void Add_2_vertically_front_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(2,EnumFront.Pionowo);

            Assert.Equal(2,cabinet.FrontList.Count);

            Assert.Equal(298, cabinet.FrontList[0].EWidth);
            Assert.Equal(720, cabinet.FrontList[0].EHeight);
            Assert.Equal(18, cabinet.FrontList[0].EDepth);
            Assert.Equal(0, cabinet.FrontList[0].Ex);
            Assert.Equal(0, cabinet.FrontList[0].Ey);
            Assert.Equal(510, cabinet.FrontList[0].Ez);

            Assert.Equal(298, cabinet.FrontList[1].EWidth);
            Assert.Equal(720, cabinet.FrontList[1].EHeight);
            Assert.Equal(18, cabinet.FrontList[1].EDepth);
            Assert.Equal(301, cabinet.FrontList[1].Ex);
            Assert.Equal(0, cabinet.FrontList[1].Ey);
            Assert.Equal(510, cabinet.FrontList[1].Ez);
        }

        [Fact]
        public void Add_3_vertically_front_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(3);

            Assert.Equal(3, cabinet.FrontList.Count);

            Assert.Equal(198, cabinet.FrontList[0].EWidth);
            Assert.Equal(720, cabinet.FrontList[0].EHeight);
            Assert.Equal(18, cabinet.FrontList[0].EDepth);
            Assert.Equal(0, cabinet.FrontList[0].Ex);
            Assert.Equal(0, cabinet.FrontList[0].Ey);
            Assert.Equal(510, cabinet.FrontList[0].Ez);

            Assert.Equal(198, cabinet.FrontList[1].EWidth);
            Assert.Equal(720, cabinet.FrontList[1].EHeight);
            Assert.Equal(18, cabinet.FrontList[1].EDepth);
            Assert.Equal(201, cabinet.FrontList[1].Ex);
            Assert.Equal(0, cabinet.FrontList[1].Ey);
            Assert.Equal(510, cabinet.FrontList[1].Ez);

            Assert.Equal(198, cabinet.FrontList[2].EWidth);
            Assert.Equal(720, cabinet.FrontList[2].EHeight);
            Assert.Equal(18, cabinet.FrontList[2].EDepth);
            Assert.Equal(402, cabinet.FrontList[2].Ex);
            Assert.Equal(0, cabinet.FrontList[2].Ey);
            Assert.Equal(510, cabinet.FrontList[2].Ez);
        }

        [Fact]
        public void Add_1_front_to_default_cabinet_with_list_of_slots()
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

            Assert.Single(cabinet.FrontList);
            Assert.Equal(594, cabinet.FrontList[0].EWidth);
            Assert.Equal(714, cabinet.FrontList[0].EHeight);
            Assert.Equal(18, cabinet.FrontList[0].EDepth);
            Assert.Equal(3, cabinet.FrontList[0].Ex);
            Assert.Equal(3, cabinet.FrontList[0].Ey);
            Assert.Equal(512, cabinet.FrontList[0].Ez);
        }

        [Fact]
        public void Add_3_vartically_front_to_default_cabinet_with_list_of_slots()
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

            Assert.Equal(3, cabinet.FrontList.Count);

            Assert.Equal(196, cabinet.FrontList[0].EWidth);
            Assert.Equal(714, cabinet.FrontList[0].EHeight);
            Assert.Equal(18, cabinet.FrontList[0].EDepth);
            Assert.Equal(3, cabinet.FrontList[0].Ex);
            Assert.Equal(3, cabinet.FrontList[0].Ey);
            Assert.Equal(512, cabinet.FrontList[0].Ez);

            Assert.Equal(196, cabinet.FrontList[1].EWidth);
            Assert.Equal(714, cabinet.FrontList[1].EHeight);
            Assert.Equal(18, cabinet.FrontList[1].EDepth);
            Assert.Equal(202, cabinet.FrontList[1].Ex);
            Assert.Equal(3, cabinet.FrontList[1].Ey);
            Assert.Equal(512, cabinet.FrontList[1].Ez);

            Assert.Equal(196, cabinet.FrontList[2].EWidth);
            Assert.Equal(714, cabinet.FrontList[2].EHeight);
            Assert.Equal(18, cabinet.FrontList[2].EDepth);
            Assert.Equal(401, cabinet.FrontList[2].Ex);
            Assert.Equal(3, cabinet.FrontList[2].Ey);
            Assert.Equal(512, cabinet.FrontList[2].Ez);
        }

        [Fact]
        public void Add_2_horizontally_front_to_default_cabinet()
        {
            var cabinet = new Cabinet();

            cabinet.AddFront(1);

            Assert.Single(cabinet.FrontList);
            Assert.Equal(600, cabinet.FrontList[0].EWidth);
            Assert.Equal(720, cabinet.FrontList[0].EHeight);
            Assert.Equal(18, cabinet.FrontList[0].EDepth);
            Assert.Equal(0, cabinet.FrontList[0].Ex);
            Assert.Equal(0, cabinet.FrontList[0].Ey);
            Assert.Equal(510, cabinet.FrontList[0].Ez);
        }


    }
}