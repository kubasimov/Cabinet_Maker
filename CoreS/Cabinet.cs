using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CoreS.Enum;
using CoreS.Export;
using CoreS.Factory;
using CoreS.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoreS
{
    public class Cabinet:CabinetModel
    {
        private ElementModel _leftSide;
        private ElementModel _rightSide;
        private ElementModel _bottom;
        private ElementModel _top;
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        public Cabinet(int height = 720, int width = 600, int depth = 510, int sizeElement = 18, int backSize = 3, string name = "Default")
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
             {
                 cfg.AddCollectionMappers();
                 cfg.ShouldMapProperty = pi =>pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);
                 cfg.CreateMap<ElementModelDTO, ElementModel>().EqualityComparison((dto,o)=>dto.GetGuid()==o.GetGuid()) ;
                 cfg.CreateMap<ElementModel, ElementModelDTO>().EqualityComparison((dto, o) => dto.GetGuid() == o.GetGuid());
                 
             }) ;
            _mapperConfiguration.AssertConfigurationIsValid();
            _mapper = _mapperConfiguration.CreateMapper();

            
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
            VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.NewBarrier(VerticalBarrierParameter));
            
            
            if(HorizontalBarrierParameter!=null)
                NewHorizontalBarrier(HorizontalBarrierParameter);

        }
        
        public void AddVerticalBarrier(int i)
        {
            VerticalBarrier = _mapper.Map <List<ElementModel>>(VerticalBarrierFactory.Add(i));
            if (HorizontalBarrierParameter != null)
                NewHorizontalBarrier(HorizontalBarrierParameter);
        }

        public void DeleteVerticalBarrier()
        {
            VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Delete(1));
            if (HorizontalBarrierParameter != null)
                NewHorizontalBarrier(HorizontalBarrierParameter);
            Redraw();
        }

        public void RemoveVerticalBarrier()
        {
            VerticalBarrier = _mapper.Map < List < ElementModel >> (VerticalBarrierFactory.DeleteAll());
            if (HorizontalBarrierParameter != null)
                NewHorizontalBarrier(HorizontalBarrierParameter);
        }

        public IEnumerable<ElementModel> GetAllVerticalBarrier()=>VerticalBarrier;
        
        #endregion
        
        #region Horizontal Barrier

        public void NewHorizontalBarrier(BarrierParameter barrierParameter)
        {
            if (barrierParameter == null)
                return;

            HorizontalBarrierParameter = barrierParameter;
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.NewBarrier(HorizontalBarrierParameter));
        }

        public void AddHorizontalBarrier(int i)
        {
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Add(i));
        }

        public void AddHorizontalBarrierByEvery(int size)
        {
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.AddEvery(size));
        }

        public void DeleteHorizontalBarrier(int delete)
        {
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Delete(delete));
        }

        public void DeleteAllHorizontalBarrier()
        {
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.DeleteAll());
        }

        public List<ElementModel> GetAllHorizontalBarrier() => HorizontalBarrier;
        
        public void DeleteElementHorizontalBarrier(ElementModel elementModel)
        {
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Delete(_mapper.Map<ElementModelDTO>(elementModel)));
        }
        
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
                    
                    _bottom = new ElementModel(
                        description: "Spód",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Bottom,
                        horizontal: true);
                    
                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: true);
                    
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
                        horizontal: true);
                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(this),
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: true);

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
            var slots = new SlotsModel();
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(number, slots, enumFront));
        }

        public void AddFront(int number)
        {
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Add(number));
        }

        public void AddFront(SlotsModel slots,int number=0 )
        {
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(number, slots,EnumFront.Pionowo));
        }

        public void AddFront(SlotsModel slots, int number, EnumFront enumFront)
        {
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(number,slots,enumFront));
        }

        public void AddFront(List<ElementModelDTO> frontList)
        {
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.AddFront(frontList));
        }

        public void AddFront(FrontParameter frontParameter)
        {
            FrontParameter = frontParameter;
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(frontParameter.Number, frontParameter.Slots, frontParameter.EnumFront));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }
        // TODO: Przenieśc do fabryki
        public void UpdateFront(ElementModel front)
        {
            var result = _mapper.Map<List<ElementModel>>(FrontFactory.Update(_mapper.Map<ElementModelDTO>(front)));

            //if (result.IsValid)
            //{
            //    FrontList = result.Value;
            //}
            //else
            //{

            //}

            FrontList = _mapper.Map<List<ElementModel>>(result);
        }

        // TODO: Przenieśc do fabryki
        public ElementModel GetFront(int number)
        {
            if(FrontList.Count >= number)return FrontList[number];
            return new ElementModel();
            
        }

        // TODO: Przenieśc do fabryki
        public int GetFrontCount()
        {
            return FrontList.Count;
        }

        // TODO: Przenieśc do fabryki
        public List<ElementModel> GetFrontList()=> FrontList;
        // TODO: Przenieśc do fabryki
        public void DeleteFront(ElementModel front)
        {
            if (!FrontList.Exists(x => x.GetGuid() == front.GetGuid())) return;
            {
                FrontList.Remove(front);
            }
        }

        public void DeleteFront(int i)
        {
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Delete(i));
        }

        public void DeleteAllFront()
        {
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.DeleteAll());
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
            VerticalBarrier= _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.ReCount());
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.ReCount());
            AddFront(FrontParameter);
        }

        public void ClipboardExport()
        {
            var clip = new Core.Export.ClipboardExport();
            clip.Export(this);
        }

        public void ChangeElemenet(ElementModel element, EnumElementParameter parameter, string text)
        {
            if (int.TryParse(text, out int result) || parameter == EnumElementParameter.Description)
            {
                bool find = false;
                
                foreach (var item in CabinetElements)
                {
                    if (item.GetGuid() == element.GetGuid())
                    {
                        find = true;
                        SwitchChange(parameter, text, result, item);

                        if (element.GetEnumName() == EnumCabinetElement.Leftside && parameter == EnumElementParameter.Width)
                        {
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Bottom).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetX(CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Bottom).SetX(CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width);
                        }
                        if (element.GetEnumName() == EnumCabinetElement.Rightside && parameter == EnumElementParameter.Width)
                        {
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Bottom).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).SetX(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width);
                        }
                        if (element.GetEnumName() == EnumCabinetElement.Top && parameter == EnumElementParameter.Width)
                        {
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetY(_height - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).Width);
                        }

                        HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.ReCount());
                        VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.ReCount());
                        //FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.ReCount());

                    }
                    
                    if (find) return;
                }

                foreach (var item in VerticalBarrier)
                {
                    if (item.GetGuid()==element.GetGuid())
                    {
                        find = true;
                        SwitchChange(parameter, text, result, item);
                    }
                }

                foreach (var item in HorizontalBarrier)
                {
                    if (item.GetGuid() == element.GetGuid())
                    {
                        find = true;
                        SwitchChange(parameter, text, result, item);
                    }
                }

                foreach (var item in FrontList)
                {
                    if (item.GetGuid() == element.GetGuid())
                    {
                        find = true;
                        SwitchChange(parameter, text, result, item);
                    }
                }
            }



            //RedrawChange(element);
        }

        private static void SwitchChange(EnumElementParameter parameter, string text, int result, ElementModel item)
        {
            switch (parameter)
            {
                case EnumElementParameter.Width:
                    item.SetWidth(result);
                    break;
                case EnumElementParameter.Height:
                    item.SetHeight(result);
                    break;
                case EnumElementParameter.Depth:
                    item.SetDepth(result);
                    break;
                case EnumElementParameter.Description:
                    item.SetDescription(text);
                    break;
                case EnumElementParameter.X:
                    item.SetX(result);
                    break;
                case EnumElementParameter.Y:
                    item.SetY(result);
                    break;
                case EnumElementParameter.Z:
                    item.SetZ(result);
                    break;
                default:
                    break;
            }
        }

    }
}
     
