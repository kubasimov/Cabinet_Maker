using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Config;
using CoreS.Enum;
using CoreS.Export;
using CoreS.Factory;
using CoreS.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoreS
{
    public class Cabinet : CabinetModel
    {
        private ElementModel _leftSide;
        private ElementModel _rightSide;
        private ElementModel _bottom;
        private ElementModel _top;
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;

        private IConfig _config;

        public Cabinet() : this(720, 600, 510, 18, 3, "Default", new Config.Config()) { }

        public Cabinet(int height, int width, int depth, int sizeElement, int backSize, string name,IConfig config)
        {
            Logger.Info("Start cabinet constructor");
            Logger.Trace("Start Automapper created");
            _mapperConfiguration = new MapperConfiguration(cfg =>
             {
                 cfg.AddCollectionMappers();
                 cfg.ShouldMapProperty = pi => pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);
                 cfg.CreateMap<ElementModelDTO, ElementModel>().EqualityComparison((dto, o) => dto.GetGuid() == o.GetGuid());
                 cfg.CreateMap<ElementModel, ElementModelDTO>().EqualityComparison((dto, o) => dto.GetGuid() == o.GetGuid());
                 cfg.CreateMap<CabinetModel, CabinetModelDTO>();
             });
            _mapperConfiguration.AssertConfigurationIsValid();
            _mapper = _mapperConfiguration.CreateMapper();
            Logger.Trace("Stop AutoMapper created");

            _config = config;

            _height = height;
            _width = width;
            _depth = depth;
            _sizeElement = sizeElement;
            BackSize = backSize;
            Back = EnumBack.Brak;
            _name = name;
            CabinetType = EnumCabinetType.Standard;

            Logger.Trace("Start GlobalCabinetElemen in cabinet constructor");
            GlobalCabinetElement();

            HorizontalBarrierFactory = new HorizontalBarrierFactory(this);
            VerticalBarrierFactory = new VerticalBarrierFactory(this);
            FrontFactory = new FrontFactory(this,_config);
        }

        
        public Cabinet Height(int h)
        {
            Logger.Debug("Change cabinet height from: {0} to: {1}", _height, h);
            _height = h;
            Redraw();
            return this;
        }

        public int Height() => _height;

        public Cabinet Width(int w)
        {
            Logger.Debug("Change cabinet width from: {0} to: {1}", _width, w);
            _width = w;
            Redraw();
            return this;
        }

        public async void ExcelExport()
        {
            var export = new ExcelExport(_config);
            await export.ExportAsync(_mapper.Map<CabinetModelDTO>(this));
        }

        public int Width() => _width;

        public Cabinet Depth(int d)
        {
            Logger.Debug("Change cabinet Depth from: {0} to: {1}", _depth, d);
            _depth = d;
            Redraw();
            return this;
        }

        public int Depth() => _depth;

        public Cabinet SizeElement(int s)
        {
            Logger.Debug("Change cabinet SizeElement from: {0} to: {1}", _sizeElement, s);
            _sizeElement = s;
            Redraw();
            return this;
        }

        public int SizeElement() => _sizeElement;

        public Cabinet Name(string s)
        {
            Logger.Debug("Change cabinet Name from: {0} to: {1}", _name, s);
            _name = s;
            Redraw();
            return this;
        }

        public string Name() => _name;

        private void GlobalCabinetElement()
        {
            Logger.Trace("Start GlobalCabinetElements");
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
            Logger.Trace("Start ChangeCabinetElement in GlobalCabinetElements");

            ChangeCabinetElement(EnumCabinetElement.Leftside, _leftSide);
            ChangeCabinetElement(EnumCabinetElement.Rightside, _rightSide);
            ChangeCabinetElement(EnumCabinetElement.Bottom, _bottom);
            ChangeCabinetElement(EnumCabinetElement.Top, _top);

            Logger.Trace("Stop ChangeCabinetElement in GlobalCabinetElements");
            Logger.Trace("Start GlobalCabinetElements");
        }
        // TODO: Logger
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

                ChangeCabinetElement(EnumCabinetElement.Back, elementBack);
            }
        }
        // TODO: Logger
        public void AddBack(EnumBack back = EnumBack.Nakladane)
        {
            Back = back;
            ChangeBack();
        }
        // TODO: Logger
        private void ChangeCabinetElement(EnumCabinetElement enumCabinetElement, ElementModel element)
        {
            Logger.Trace("Start ChangeCabinetElement");

            if (CabinetElements.Exists(c => c.GetEnumName() == enumCabinetElement))
            {
                Logger.Trace("Change an existing item");
                CabinetElements[CabinetElements.FindIndex(c => c.GetEnumName() == enumCabinetElement)] = element;
            }
            else
            {
                Logger.Trace("Adding a new item: {0}",element.ToString());
                CabinetElements.Add(element);
            }
            Logger.Trace("Stop ChangeCabinetElement");
        }

        #region Vertical Barrier

        public void NewVerticalBarrier(BarrierParameter barrierParameter)
        {
            Logger.Info("New Vertical Barrier in Cabinet");
            Logger.Debug("BarrierParameter Number: {0}, Back: {1}, Height: {2} ", barrierParameter.Number, barrierParameter.Back, string.Join(";", barrierParameter.Height));
            if (barrierParameter == null)
                return;

            VerticalBarrierParameter = barrierParameter;
            try
            {
                VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.NewBarrier(VerticalBarrierParameter));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error New Vertical Barrier");
            }

            RecalculateHorizontalBarrier();
        }

        public void AddVerticalBarrier(int i)
        {
            Logger.Info("Add Vertical Barrier in Cabinet");
            Logger.Debug("Add Vertical Barrier count: {0}", i);
            try
            {
                VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Add(i));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Vertical Barrier");
            }

            RecalculateHorizontalBarrier();
        }

        private void AddVerticalBarrierList(List<ElementModel> verticalBarrier)
        {
            Logger.Info("AddVerticalBarrierList(List<ElementModel> verticalBarrier) in Cabinet");
            Logger.Debug("verticalBarrier: {0}", verticalBarrier);
            try
            {
                VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Add(_mapper.Map<List<ElementModelDTO>>(verticalBarrier)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error AddVerticalBarrierList(List<ElementModel> verticalBarrier)");
            }
        }
        
        public void DeleteVerticalBarrier(int i)
        {
            Logger.Info("Delete Vertical Barrier in Cabinet");
            Logger.Debug("Delete Vertical Barrier count: {0}", i);
            try
            {
                VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Delete(i));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Delete Vertical Barrier");
            }

            RecalculateHorizontalBarrier();
        }

        public void RemoveVerticalBarrier()
        {
            Logger.Info("Remove Vertical Barrier in Cabinet");

            try
            {
                VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Remove());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Remove Delete Vertical Barrier");
            }

            RecalculateHorizontalBarrier();
        }

        public IEnumerable<ElementModel> GetAllVerticalBarrier()
        {
            Logger.Info("Get All Vertical Barrier in Cabinet");
            try
            {
                return _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.GetAll());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Get All Vertical Barrier in Cabinet");
                return new List<ElementModel>();
            }
        }

        public void DeleteElementVerticalBarrier(ElementModel elementModel)
        {
            Logger.Info("Delete Element Vertical Barrier in Cabinet");
            Logger.Debug("Delete Element Vertical Barrier count: {0}", elementModel.ToString());
            try
            {
                VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Delete(_mapper.Map<ElementModelDTO>(elementModel)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Delete Element Vertical Barrier in Cabinet");
            }

            RecalculateHorizontalBarrier();
        }

        private void RecalculateVerticalBarrier()
        {
            Logger.Info("Recalculate Vertical Barrier in Cabinet");
            VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.ReCount());
        }

        #endregion Vertical Barrier

        #region Horizontal Barrier

        public void NewHorizontalBarrier(BarrierParameter barrierParameter)
        {
            Logger.Info("New Horizontal Barrier in Cabinet");
            Logger.Debug("BarrierParameter Number: {0}, Back: {1}, Height: {2} ", barrierParameter.Number, barrierParameter.Back, string.Join(";", barrierParameter.Height));
            if (barrierParameter == null)
                return;

            HorizontalBarrierParameter = barrierParameter;
            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.NewBarrier(HorizontalBarrierParameter));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error New Horizontal Barrier");
            }
        }

        public void AddHorizontalBarrier(int count)
        {
            Logger.Info("AddHorizontalBarrier(int i) in Cabinet");
            Logger.Debug("count: {0}", count);
            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Add(count));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error AddHorizontalBarrier(int i)");
            }
        }

        public void AddHorizontalBarrierByEvery(int size)
        {
            Logger.Info("AddHorizontalBarrierByEvery(int size) in Cabinet");
            Logger.Debug("size: {0}", size);
            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.AddEvery(size));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error AddHorizontalBarrierByEvery(int size)");
            }
        }

        public void AddHorizonatlBarriery(List<ElementModel> elementModels)
        {
            Logger.Info("AddHorizonatlBarriery(List<ElementModel> elementModels)  in Cabinet");
            Logger.Debug("List<ElementModel>: {0}", elementModels);
            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Add(_mapper.Map<List<ElementModelDTO>>(elementModels)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error AddHorizonatlBarriery(List<ElementModel> elementModels)");
            }
        }

        public void DeleteHorizontalBarrier(int delete)
        {
            Logger.Info("Delete Horizontal Barrier in Cabinet");
            Logger.Debug("Delete Horizontal Barrier count: {0}", delete);
            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Delete(delete));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Delete Horizontal Barrier");
            }
        }

        public void RemoveHorizontalBarrier()
        {
            Logger.Info("Remove Horizontal Barrier in Cabinet");

            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Remove());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Remove Delete Horizontal Barrier");
            }
        }

        public List<ElementModel> GetAllHorizontalBarrier()
        {
            Logger.Info("Get All Horizontal Barrier in Cabinet");
            try
            {
                return _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.GetAll());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Get All Horizontal Barrier in Cabinet");
                return new List<ElementModel>();
            }
        }

        public void DeleteElementHorizontalBarrier(ElementModel elementModel)
        {
            Logger.Info("Delete Element Horizontal Barrier in Cabinet");
            Logger.Debug("Delete Element Horizontal Barrier count: {0}", elementModel.ToString());
            try
            {
                HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Delete(_mapper.Map<ElementModelDTO>(elementModel)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Delete Element Horizontal Barrier in Cabinet");
            }
        }

        private void RecalculateHorizontalBarrier()
        {
            Logger.Info("Recalculate Horizontal Barrier in Cabinet");
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.ReCount());
        }

        #endregion Horizontal Barrier

        #region Front command
        // TODO: Logger
        public void FrontRecall()
        {
            FrontFactory.Recal();
        }

        public void AddFront(int number, EnumFront enumFront)
        {
            Logger.Info("Add Front (int number, EnumFront enumFront) in Cabinet");
            Logger.Debug("Number: {0}, EnumFront: {1}", number, enumFront);

            var slots = new SlotsModel(_config);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(number, slots, enumFront));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Front (int number, EnumFront enumFront) in Cabinet");
            }
        }

        public void AddFront(int number)
        {
            Logger.Info("Add Front (int number) in Cabinet");
            Logger.Debug("Number: {0}", number);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Add(number));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Front (int number) in Cabinet");
            }
            
        }

        private void AddFrontList(List<ElementModel> frontList)
        {
            Logger.Info("AddFrontList(List<ElementModel> frontList)  in Cabinet");
            Logger.Debug("frontList: {0}", frontList);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Add(_mapper.Map<List<ElementModelDTO>>(frontList)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error AddFrontList(List<ElementModel> frontList)");
            }
        }

        public void AddFront(SlotsModel slots, int number = 0)
        {
            Logger.Info("Add Front (SlotsModel slots, int number = 0) in Cabinet");
            Logger.Debug("Number: {0}, Slots: {1}", number,slots);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(number, slots, EnumFront.Pionowo));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Front (SlotsModel slots, int number = 0) in Cabinet");
            }
            
        }

        public void AddFront(SlotsModel slots, int number, EnumFront enumFront)
        {
            Logger.Info("Add Front (SlotsModel slots, int number, EnumFront enumFront) in Cabinet");
            Logger.Debug("Number: {0}, Slots: {1}, EnumFront: {2}", number, slots,enumFront);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(number, slots, enumFront));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Front (SlotsModel slots, int number, EnumFront enumFront) in Cabinet");
            }
        }

        public void AddFront(List<ElementModelDTO> frontList)
        {
            Logger.Info("Add Front (List<ElementModelDTO> frontList) in Cabinet");
            Logger.Debug("FrontList: {0}", frontList.ToString());
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.AddFront(frontList));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Front (List<ElementModelDTO> frontList) in Cabinet");
            }
            
        }

        public void AddFront(FrontParameter frontParameter)
        {
            Logger.Info("Add Front (FrontParameter frontParameter) in Cabinet");
            Logger.Debug("FrontParameter: {0}", frontParameter);
            FrontParameter = frontParameter;
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.NewFront(frontParameter.Number, frontParameter.Slots, frontParameter.EnumFront));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Add Front (FrontParameter frontParameter) in Cabinet");
            }
        }

        //public void UpdateFront(ElementModel front)
        //{
        //    Logger.Info("Add Front (ElementModel front) in Cabinet");
        //    Logger.Debug("Front: {0}", front);

        //    try
        //    {
        //        FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Update(_mapper.Map<ElementModelDTO>(front)));
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex, "Error Add Front (ElementModel front) in Cabinet");
        //    }
        //}
                
        public int GetFrontCount()
        {
            Logger.Info("Get Front Count in Cabinet");
            return GetAllFront().Count;
        }
                
        public List<ElementModel> GetAllFront()
        {
            Logger.Info("Get All Front in Cabinet");
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.GetAll());
            return FrontList;
        }

        public void DeleteFront(ElementModel front)
        {
            Logger.Info("Delete Front (ElementModel front) in Cabinet");
            Logger.Debug("Front: {0}", front);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.DeleteElement(_mapper.Map<ElementModelDTO>(front)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Delete Front (ElementModel front) in Cabinet");
            }
            
        }

        public void DeleteLast_x_Front(int x)
        {
            Logger.Info("Delete last x front (int x) in Cabinet");
            Logger.Debug("X: {0}", x);
            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Delete(x));
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error last x front (int x) in Cabinet");
            }
        }

        public void RemoveFront()
        {
            Logger.Info("Remove Front in Cabinet");

            try
            {
                FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Remove());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error Remove Front Barrier");
            }
        }

        private void RecalculateFront()
        {
            Logger.Info("Recalculate Front in Cabinet");
            FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.ReCount());
        }

        #endregion Front command

        // TODO sprawdzanie bledow, dokonczenie implementacji
        public void Serialize()
        {
            Logger.Info("Serialize in Cabinet");
            var serialize = new JsonExport(_config);
            serialize.ExportAsync(_mapper.Map<CabinetModelDTO>(this));
        }

        // TODO sprawdzanie bledow, dokonczenie implementacji
        public void Deserialize(string fileName)
        {
            Logger.Info("Deserialize in Cabinet");
            var deserialize = new JsonImport();
            var cab = deserialize.Import(fileName);
            if (cab == null) return;


            Height(cab.Height()).Width(cab.Width()).Depth(cab.Depth()).SizeElement(cab.SizeElement())
                .Name(cab.Name());

            HorizontalBarrierParameter = cab.HorizontalBarrierParameter;
            AddHorizonatlBarriery(cab.HorizontalBarrier);

            VerticalBarrierParameter = cab.VerticalBarrierParameter;
            AddVerticalBarrierList(cab.VerticalBarrier);

            FrontParameter = cab.FrontParameter;
            AddFrontList(cab.FrontList);

            RedrawCabinetElements();
        }
        
        public void NewCabinet()
        {
            Height(_config.CabinetHeight()).Width(_config.CabinetWidth()).Depth(_config.CabinetDepth()).SizeElement(_config.CabinetSizeElement())
                .Name(_config.CabinetName());
            HorizontalBarrierFactory = new HorizontalBarrierFactory(this);
            VerticalBarrierFactory = new VerticalBarrierFactory(this);
            FrontFactory = new FrontFactory(this, _config);

            Redraw();

        }
        public void Redraw()
        {
            Logger.Info("Redraw in Cabinet");
            RedrawCabinetElements();
            // TODO  dodac wszelkie przeliczenia w klasie cabinet
            VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.ReCount());
            HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.ReCount());
            FrontList= _mapper.Map<List<ElementModel>>(FrontFactory.ReCount());
            
        }

        public void ClipboardExport()
        {
            Logger.Info("ClipboardExport in Cabinet");
            var clip = new Core.Export.ClipboardExport();
            clip.ExportAsync(_mapper.Map<CabinetModelDTO>(this));
        }

        // TODO dodac dodatkowe logowanie i prawidłowe - w fabryce zmiany elementow
        public ElementModel ChangeElemenet(ElementModel element, EnumElementParameter parameter, string text)
        {
            Logger.Info("ChangeElemenet(ElementModel element, EnumElementParameter parameter, string text) in Cabinet");
            
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
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width, false);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Bottom).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width, false);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetX(CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width, false);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Bottom).SetX(CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width, false);
                        }
                        if (element.GetEnumName() == EnumCabinetElement.Rightside && parameter == EnumElementParameter.Width)
                        {
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width, false);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Bottom).SetHeight(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width, false);
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).SetX(_width - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width, false);
                        }
                        if (element.GetEnumName() == EnumCabinetElement.Top && parameter == EnumElementParameter.Width)
                        {
                            CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).SetY(_height - CabinetElements.Find(x => x.GetEnumName() == EnumCabinetElement.Top).Width, false);
                        }

                        HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.ReCount());
                        VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.ReCount());
                        FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.ReCount());
                    }

                    if (find) break;
                }

                foreach (var item in VerticalBarrier)
                {
                    if (item.GetGuid() == element.GetGuid())
                    {
                        find = true;
                        VerticalBarrier = _mapper.Map<List<ElementModel>>(VerticalBarrierFactory.Update(parameter, text, result, _mapper.Map<ElementModelDTO>(item)));
                        element = VerticalBarrier.Find(x => x.GetGuid() == element.GetGuid());
                    }
                }

                foreach (var item in HorizontalBarrier)
                {
                    if (item.GetGuid() == element.GetGuid())
                    {
                        find = true;
                        HorizontalBarrier = _mapper.Map<List<ElementModel>>(HorizontalBarrierFactory.Update(parameter, text, result, _mapper.Map<ElementModelDTO>(item)));
                        element = HorizontalBarrier.Find(x => x.GetGuid() == element.GetGuid());
                    }
                }

                foreach (var item in FrontList)
                {
                    if (item.GetGuid() == element.GetGuid())
                    {
                        find = true;
                        FrontList = _mapper.Map<List<ElementModel>>(FrontFactory.Update(parameter, text, result, _mapper.Map<ElementModelDTO>(item)));
                        element = FrontList.Find(x => x.GetGuid() == element.GetGuid());
                        break;
                    }
                }
            }

            return element;
        }

        // TODO: Przeniesc elementy zmian do odpowiednich fabryk
        // TODO: Update - zmienia tylko elementy glowne szafki
        private static void SwitchChange(EnumElementParameter parameter, string text, int result, ElementModel item)
        {
            Logger.Info("SwitchChange(EnumElementParameter parameter, string text, int result, ElementModel item) in Cabinet");
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

        private void RedrawCabinetElements()
        {
            Logger.Info("RedrawCabinetElements in Cabinet");
            _leftSide.SetDescription(_leftSide.ChangeDescription ? _leftSide.Description : "Bok Lewy", false);
            _leftSide.SetHeight(_leftSide.ChangeHeight ? _leftSide.Height : _height, false);
            _leftSide.SetWidth(_leftSide.ChangeWidth ? _leftSide.Width : _sizeElement, false);
            _leftSide.SetDepth(_leftSide.ChangeDepth ? _leftSide.Depth : _depth, false);
            _leftSide.SetX(_leftSide.ChangeX ? _leftSide.X : 0, false);
            _leftSide.SetY(_leftSide.ChangeY ? _leftSide.Y : 0, false);
            _leftSide.SetZ(_leftSide.ChangeZ ? _leftSide.Z : SwitchBack.ValueAxisZbyBackTypeAndSize(this), false);
            _leftSide.SetEnumName(EnumCabinetElement.Leftside);
            _leftSide.SetHorizontal(false);

            _rightSide.SetDescription(_rightSide.ChangeDescription ? _rightSide.Description : "Bok Prawy", false);
            _rightSide.SetHeight(_rightSide.ChangeHeight ? _rightSide.Height : _height, false);
            _rightSide.SetWidth(_rightSide.ChangeWidth ? _rightSide.Width : _sizeElement, false);
            _rightSide.SetDepth(_rightSide.ChangeDepth ? _rightSide.Depth : _depth, false);
            _rightSide.SetX(_rightSide.ChangeX ? _rightSide.X : _width - _rightSide.Width, false);
            _rightSide.SetY(_rightSide.ChangeY ? _rightSide.Y : 0, false);
            _rightSide.SetZ(_rightSide.ChangeZ ? _rightSide.Z : SwitchBack.ValueAxisZbyBackTypeAndSize(this), false);
            _rightSide.SetEnumName(EnumCabinetElement.Rightside);
            _rightSide.SetHorizontal(false);

            _bottom.SetDescription(_bottom.ChangeDescription ? _bottom.Description : "Spód", false);
            _bottom.SetHeight(_bottom.ChangeHeight ? _bottom.Height : _width - _leftSide.Width - _rightSide.Width, false);
            _bottom.SetWidth(_bottom.ChangeWidth ? _bottom.Width : _sizeElement, false);
            _bottom.SetDepth(_bottom.ChangeDepth ? _bottom.Depth : _depth, false);
            _bottom.SetX(_bottom.ChangeX ? _bottom.X : _leftSide.Width, false);
            _bottom.SetY(_bottom.ChangeY ? _bottom.Y : 0, false);
            _bottom.SetZ(_bottom.ChangeZ ? _bottom.Z : SwitchBack.ValueAxisZbyBackTypeAndSize(this), false);
            _bottom.SetEnumName(EnumCabinetElement.Bottom);
            _bottom.SetHorizontal(true);

            _top.SetDescription(_top.ChangeDescription ? _top.Description : "Góra", false);
            _top.SetHeight(_top.ChangeHeight ? _top.Height : _width - _leftSide.Width - _rightSide.Width, false);
            _top.SetWidth(_top.ChangeWidth ? _top.Width : _sizeElement, false);
            _top.SetDepth(_top.ChangeDepth ? _top.Depth : _depth, false);
            _top.SetX(_top.ChangeX ? _top.X : _leftSide.Width, false);
            _top.SetY(_top.ChangeY ? _top.Y : _height - _top.Width, false);
            _top.SetZ(_top.ChangeZ ? _top.Z : SwitchBack.ValueAxisZbyBackTypeAndSize(this), false);
            _top.SetEnumName(EnumCabinetElement.Top);
            _top.SetHorizontal(true);
        }
    }
}