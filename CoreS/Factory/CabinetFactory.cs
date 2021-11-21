using Config;
using CoreS.Enum;
using CoreS.Model;
using System.Collections.Generic;

namespace CoreS.Factory
{
    public class CabinetFactory
    {
        private ElementModel _leftSide;
        private ElementModel _rightSide;
        private ElementModel _top;
        private ElementModel _bottom;

        protected readonly Back SwitchBack = new Back();

        public List<ElementModel> Create(int _height, int _width, int _depth, int _sizeElement, int _backSize, IConfig config, EnumCabinetType enumCabinetType, EnumBack enumBack)
        {
            switch (enumCabinetType)
            {
                case EnumCabinetType.Standard:

                    _leftSide = new ElementModel(
                        description: "Bok Lewy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: 0,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack,_backSize),
                        enumCabinet: EnumCabinetElement.Leftside,
                        horizontal: false);

                    _rightSide = new ElementModel(
                        description: "Bok Prawy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: _width - _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Rightside,
                        horizontal: false);

                    _bottom = new ElementModel(
                        description: "Spód",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Bottom,
                        horizontal: true);

                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: true);

                    break;
                case EnumCabinetType.odwrotna:
                    break;
                case EnumCabinetType.duzy_spod:
                    break;
                case EnumCabinetType.duza_gora:
                    break;
                case EnumCabinetType.gorna:
                    _leftSide = new ElementModel(
                        description: "Bok Lewy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: 0,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Leftside,
                        horizontal: false);

                    _rightSide = new ElementModel(
                        description: "Bok Prawy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: _width - _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Rightside,
                        horizontal: false);

                    _bottom = new ElementModel(
                        description: "Spód",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth - 20,
                        x: _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize) + 20,
                        enumCabinet: EnumCabinetElement.Bottom,
                        horizontal: true);

                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth - 20,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize) + 20,
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: true);
                    break;
                case EnumCabinetType.gorna_do_spodu:
                    break;
                default:
                    _leftSide = new ElementModel(
                        description: "Bok Lewy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: 0,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Leftside,
                        horizontal: false);

                    _rightSide = new ElementModel(
                        description: "Bok Prawy",
                        height: _height,
                        width: _sizeElement,
                        depth: _depth,
                        x: _width - _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Rightside,
                        horizontal: false);

                    _bottom = new ElementModel(
                        description: "Spód",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: 0,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Bottom,
                        horizontal: true);

                    _top = new ElementModel(
                        description: "Góra",
                        height: _width - 2 * _sizeElement,
                        width: _sizeElement,
                        depth: _depth,
                        x: _sizeElement,
                        y: _height - _sizeElement,
                        z: SwitchBack.ValueAxisZbyBackTypeAndSize(enumBack, _backSize),
                        enumCabinet: EnumCabinetElement.Top,
                        horizontal: true);
                    break;



            }


            return new List<ElementModel>
            {
                _leftSide,_rightSide,_top, _bottom
            };
        }

        
    }
}