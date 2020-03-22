using CoreS.Enum;
using CoreS.Interface;
using CoreS.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreS.Factory
{
    public class VerticalBarrierFactory:BarrierFactory, IElementRepository
    {
        public VerticalBarrierFactory(Cabinet cabinet)
        {
            Logger.Info("VerticalBarrierFactory Constructor"); 
            _cabinet = cabinet;
            elements = new List<ElementModelDTO>();
        }

        public List<ElementModelDTO> NewBarrier(BarrierParameter barrierParameter)
        {
            Logger.Info("NewBarrier(BarrierParameter barrierParameter) in VerticalBarrierFactory");
            Logger.Debug("barrierParameter: {0} ", barrierParameter);
            Number = barrierParameter.Number;
            
            back = barrierParameter.Back;

            return Recalculate();
        }

        public List<ElementModelDTO> ReCount()
        {
            Logger.Info("ReCount() in VerticalBarrierFactory"); 
            return Recalculate();
        }

        public List<ElementModelDTO> Add(int element)
        {
            Logger.Info("Add(int element) in VerticalBarrierFactory");
            Logger.Debug("element: {0} ", element);
            Number = elements.Count + element;

            return Recalculate();
        }

        public List<ElementModelDTO> Add(List<ElementModelDTO> list)
        {
            Logger.Info("Add(List<ElementModelDTO> list) in VerticalBarrierFactory");
            Logger.Debug("list: {0} ", list);
            elements = new List<ElementModelDTO>();
            foreach (var item in list)
            {
                elements.Add(item);
            }
            return elements;
        }

        public List<ElementModelDTO> Delete(int delete)
        {
            Logger.Info("Delete(int delete) in VerticalBarrierFactory");
            Logger.Debug("delete: {0} ", delete);
            Number = elements.Count - delete;
            if (Number < 0)
                Number = 0;
            return Recalculate();
        }

        public List<ElementModelDTO> Delete(ElementModelDTO element)
        {
            Logger.Info("Delete(ElementModelDTO element) in VerticalBarrierFactory");
            Logger.Debug("element: {0} ", element);
            var findElement = elements.Find(x => x.GetGuid() == element.GetGuid());
            if (findElement != null)
            {
                elements.Remove(findElement);
                Number -= 1;
            }


            return elements;

        }

        public List<ElementModelDTO> Remove()
        {
            Logger.Info("Remove() in VerticalBarrierFactory");
            Number = 0;
            elements = new List<ElementModelDTO>();
            return elements;
        }

        public List<ElementModelDTO> GetAll()
        {
            Logger.Info("GetAll() in VerticalBarrierFactory");
            return elements;
        }

        public List<ElementModelDTO> Update(EnumElementParameter parameter, string text, int result, ElementModelDTO elementModelDTO)
        {
            Logger.Info("Update(EnumElementParameter parameter, string text, int result, ElementModelDTO elementModelDTO) in VerticalBarrierFactory");
            Logger.Debug("Parameter: {0}, Text:, {1}, Result: {2}, ElemenModelDTO: {3}", parameter, text, result, elementModelDTO);
            if (!elements.Exists(x => x.GetGuid() == elementModelDTO.GetGuid())) return elements;

            var item = elements.Find(x => x.GetGuid() == elementModelDTO.GetGuid());

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

            return elements;
        }

        private List<ElementModelDTO> Recalculate()
        {
            Logger.Info("Recalculate() in VerticalBarrierFactory");
            elements = new List<ElementModelDTO>();

            tempHeight = _cabinet.Height() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Top).Width;

            tempWidth = _cabinet.SizeElement();

            tempDepth = _cabinet.Depth() - back;

            tempEy = _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Bottom).Width;

            tempEx = (_cabinet.Width() - _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width -
                      _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Rightside).Width - Number * _cabinet.SizeElement()) / (Number + 1);

            for (var i = 0; i < Number; i++)
            {
                var element = new ElementModelDTO(
                    description: "Pion",
                    height: tempHeight,
                    width: tempWidth,
                    depth: tempDepth,
                    x: _cabinet.CabinetElements.First(x => x.GetEnumName() == EnumCabinetElement.Leftside).Width + tempEx * (i + 1) + _cabinet.SizeElement() * i,
                    y: tempEy,
                    z: 0,
                    enumCabinet: EnumCabinetElement.VerticalBarrier,
                    horizontal: false);
                
                elements.Add(element);
            }

            return elements;
        }

        
    }
}