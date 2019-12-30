using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoreS.Factory;
using CoreS.Model;
using CoreS.Enum;
using CoreS.Export;

namespace CoreS
{
    public class Cabinet:CabinetModel
    {
        private ElementModel _leftSide;
        private ElementModel _rightSide;
        private ElementModel _bottom;
        private ElementModel _top;
         
        public Cabinet(int height = 720, int width = 600, int depth = 510, int sizeElement = 18, int backSize = 3, string name = "Default")
        {
            HorizontalBarrier = new List<ElementModel>();
            VerticalBarrier = new List<ElementModel>();
            CabinetElements = new List<ElementModel>();
            FrontList = new List<ElementModel>();

            //Default value
            _height =height;
            _width = width;
            _depth = depth;
            _sizeElement = sizeElement;
            BackSize = backSize;
            Back = EnumBack.Brak;
            _name = name;
            CabinetType = EnumCabinetType.Standard;


            GlobalCabinetElement();

            HorizontalBarrierFactory = new HorizontalBarrierFactory(this);
            VerticalBarrierFactory = new VerticalBarrierFactory(this);
            FrontFactory = new FrontFactory(this);
        }
        
        public Cabinet Height(int h)
        {
            _height = h;
            Redraw();
            return this;
        }
        public int Height() => _height;

        public Cabinet Width(int w)
        {
            _width = w;
            Redraw();
            return this;
        }
        public int Width() => _width;

        public Cabinet Depth(int d)
        {
            _depth = d;
            Redraw();
            return this;
        }
        public int Depth() => _depth;

        public Cabinet SizeElement(int s)
        {
            _sizeElement = s;
            Redraw();
            return this;
        }
        public int SizeElement() => _sizeElement;

        public Cabinet Name(string s)
        {
            _name = s;
            Redraw();
            return this;
        }
        public string Name() => _name;


        #region Vertical Barrier

        public void NewVerticalBarrier(BarrierParameter barrierParameter)
        {
            if (barrierParameter == null)
                return;
            
            VerticalBarrierParameter = barrierParameter;
            VerticalBarrier = VerticalBarrierFactory.NewBarrier(VerticalBarrierParameter);
            
            
            if(HorizontalBarrierParameter!=null)
                NewHorizontalBarrier(HorizontalBarrierParameter);

        }
        
        public void AddVerticalBarrier(int i)
        {
            VerticalBarrier = VerticalBarrierFactory.Add(i);
            if (HorizontalBarrierParameter != null)
                NewHorizontalBarrier(HorizontalBarrierParameter);
        }

        public void DeleteVerticalBarrier()
        {
            VerticalBarrier = VerticalBarrierFactory.Delete(1);
            if (HorizontalBarrierParameter != null)
                NewHorizontalBarrier(HorizontalBarrierParameter);
            Redraw();
        }

        public void RemoveVerticalBarrier()
        {
            VerticalBarrier = VerticalBarrierFactory.DeleteAll();
            if (HorizontalBarrierParameter != null)
                NewHorizontalBarrier(HorizontalBarrierParameter);
        }

        public IEnumerable<ElementModel> GetAllVerticalBarrier()
        {
            return VerticalBarrierFactory.GetAll();
        }
        
        #endregion
        
        #region Horizontal Barrier

        public void NewHorizontalBarrier(BarrierParameter barrierParameter)
        {
            if (barrierParameter == null)
                return;

            HorizontalBarrierParameter = barrierParameter;
            HorizontalBarrier = HorizontalBarrierFactory.NewBarrier(HorizontalBarrierParameter);
        }

        public void AddHorizontalBarrier(int i)
        {
            HorizontalBarrier = HorizontalBarrierFactory.Add(i);
        }

        public void DeleteHorizontalBarrier(int delete)
        {
            HorizontalBarrier = HorizontalBarrierFactory.Delete(delete);
        }

        public void DeleteAllHorizontalBarrier()
        {
            HorizontalBarrier = HorizontalBarrierFactory.DeleteAll();
        }

        public List<ElementModel> GetAllHorizontalBarrier() => HorizontalBarrierFactory.GetAll();
        
        
        #endregion

        private void GlobalCabinetElement()
        {
            switch (CabinetType)
            {
                case EnumCabinetType.Standard:

                    _leftSide = new ElementModel(
                        description: "Bok Lewy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: 0,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Leftside,
                        horizontal: false);
                    //{
                    //    GetEnumName() = EnumCabinetElement.Leftside,
                    //    Description = "Bok Lewy",
                    //    EHeight = _height,
                    //    EWidth = _sizeElement,
                    //    EDepth = _depth,
                    //    Ex = 0,
                    //    Ey = 0,
                    //    Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                    //    Horizontal=false
                    //};
                    _rightSide = new ElementModel(
                        description: "Bok Prawy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: _width-_sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Rightside,
                        horizontal: false);
                    //{
                    //    GetEnumName() = EnumCabinetElement.Rightside,
                    //    Description = "Bok Prawy",
                    //    EHeight = _height,
                    //    EWidth = _sizeElement,
                    //    EDepth = _depth,
                    //    Ex = _width - _sizeElement,
                    //    Ey = 0,
                    //    Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                    //    Horizontal = false
                    //};
                    _bottom = new ElementModel(
                        description: "Spód",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Bottom,
                        horizontal: false);
                    //{
                    //    GetEnumName() = EnumCabinetElement.Bottom,
                    //    Description = "Spód",
                    //    EHeight = _width - 2 * _sizeElement,
                    //    EWidth = _sizeElement,
                    //    EDepth = _depth,
                    //    Ex = _sizeElement,
                    //    Ey = 0,
                    //    Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                    //    Horizontal = true

                    //};
                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: false);
                    //{
                    //    GetEnumName() = EnumCabinetElement.Top,
                    //    Description = "Góra",
                    //    EHeight = _width - 2 * _sizeElement,
                    //    EWidth = _sizeElement,
                    //    EDepth = _depth,
                    //    Ex = _sizeElement,
                    //    Ey = _height - _sizeElement,
                    //    Ez = SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                    //    Horizontal=true
                    //};
                    break;

                case EnumCabinetType.odwrotna:
                    break;
                case EnumCabinetType.duzy_spod:
                    break;
                case EnumCabinetType.duza_gora:
                    break;

                default:

                    _leftSide = new ElementModel(
                        description: "Bok Lewy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: 0,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Leftside,
                        horizontal: false);
                    _rightSide = new ElementModel(
                        description: "Bok Prawy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: _width - _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Rightside,
                        horizontal: false);
                    _bottom = new ElementModel(
                        description: "Spód",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Bottom,
                        horizontal: false);
                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: false);

                    break;

            }

            ChangeCabinetElement(EnumCabinetElement.Leftside,_leftSide);
            ChangeCabinetElement(EnumCabinetElement.Rightside,_rightSide);
            ChangeCabinetElement(EnumCabinetElement.Bottom,_bottom);
            ChangeCabinetElement(EnumCabinetElement.Top,_top);
        }

        private void ChangeBack()
        {
            ElementModel elementBack;

            if (Back == EnumBack.Nakladane)
            {
                elementBack = new ElementModel(
                        description: "Plecy",
                        height: _height,
                        width: _width,
                        depth: BackSize,
                        x: 0,
                        y: 0,
                        z: 0,
                        enumCabinet: EnumCabinetElement.Back,
                        horizontal: false);

                

                _depth = SwitchBack.SwitchDepthByBackType(_depth, Back, BackSize);

                ChangeCabinetElement(EnumCabinetElement.Back,elementBack);
                
            }
            
        }

        public void AddBack(EnumBack back=EnumBack.Nakladane)
        {
            Back = back;
            ChangeBack();
        }

        private void ChangeCabinetElement(EnumCabinetElement enumCabinetElement, ElementModel element)
        {
            if (CabinetElements.Exists(c => c.GetEnumName() == enumCabinetElement))
            {
                CabinetElements[CabinetElements.FindIndex(c => c.GetEnumName() == enumCabinetElement)] = element;
                
                //var index = CabinetElements.FindIndex(c => c.GetEnumName() == enumCabinetElement);
                //CabinetElements[index].Description = element.Description;
                //CabinetElements[index].GetEnumName() = element.GetEnumName();
                //CabinetElements[index].EHeight = element.EHeight;
                //CabinetElements[index].EWidth = element.EWidth;
                //CabinetElements[index].EDepth = element.EDepth;
                //CabinetElements[index].Ex = element.Ex;
                //CabinetElements[index].Ey = element.Ey;
                //CabinetElements[index].Ez = element.Ez;
                //CabinetElements[index].Horizontal = element.Horizontal;
                //CabinetElements[index].Material = element.Material;
            }
            else
            {
                CabinetElements.Add(element);
            }
        }
        
        #region Front command

        public void AddFront(int number, EnumFront enumFront)
        {
            var slots = new SlotsModel { BetweenVertically = 3, BetweenHorizontally = 3 };
            FrontList=FrontFactory.NewFront(number, slots,enumFront);
        }

        public void AddFront(int number)
        {
            //var slots = new SlotsModel {BetweenVertically = 3, BetweenHorizontally = 3 };

            //AddFront(slots,numberVertically);
            FrontList = FrontFactory.Add(number);
        }

        public void AddFront(SlotsModel slots,int number=0 )
        {
            FrontList = FrontFactory.NewFront(number, slots,EnumFront.Pionowo);
        }

        public void AddFront(SlotsModel slots, int number, EnumFront enumFront)
        {
            FrontList = FrontFactory.NewFront(number,slots,enumFront);
        }

        public void AddFront(List<ElementModel> frontList)
        {
            FrontList = FrontFactory.AddFront(frontList);
        }

        public void AddFront(FrontParameter frontParameter)
        {
            FrontParameter = frontParameter;
            try
            {
                FrontList = FrontFactory.NewFront(frontParameter.Number, frontParameter.Slots, frontParameter.EnumFront);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
        }

        public void UpdateFront(ElementModel front)
        {
            var result = FrontFactory.Update(front);
             
            if(result.IsValid)
            {
                FrontList = result.Value;
            }
            else
            {
                
            }
        }

        public ElementModel GetFront(int number)
        {
            if(FrontList.Count >= number)return FrontList[number];
            return new ElementModel();
        }

        public int GetFrontCount()
        {
            return FrontList.Count;
        }

        public List<ElementModel> GetFrontList()
        {
            return FrontList;
        }

        public void DeleteFront(ElementModel front)
        {
            if (!FrontList.Exists(x => x.GetGuid() == front.GetGuid())) return;
            {
                FrontList.Remove(front);
            }
        }

        #endregion

        public void Serialize()
        {
            var serialize = new JsonExport();
            serialize.Export(this);
        }

        public void Deserialize()
        {
            var deserialize = new JsonImport();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker", "Default" + ".json");
            var cab = deserialize.Import(path);
            this.Height(cab.Height()).Width(cab.Width()).Depth(cab.Depth()).SizeElement(cab.SizeElement())
                .Name(cab.Name());
            this.HorizontalBarrier = cab.HorizontalBarrier;
            HorizontalBarrierParameter = cab.HorizontalBarrierParameter;
            VerticalBarrier = cab.VerticalBarrier;
            VerticalBarrierParameter = cab.VerticalBarrierParameter;
            FrontList = cab.FrontList;
            FrontParameter = cab.FrontParameter;


        }

        public void Redraw()
        {
            GlobalCabinetElement();
            VerticalBarrier= VerticalBarrierFactory.ReCount();
            HorizontalBarrier = HorizontalBarrierFactory.ReCount();
            AddFront(FrontParameter);
        }

        public void ClipboardExport()
        {
            var clip = new Core.Export.ClipboardExport();
            clip.Export(this);
        }
    }
}
     
