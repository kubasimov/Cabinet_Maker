using CoreS.Enum;
using Newtonsoft.Json;
using System;

namespace CoreS.Model
{
    public class ElementModel
    {
        [JsonProperty]
        private Guid _guid;
        [JsonProperty]
        public int Height { get; private set; }
        [JsonProperty]
        public int Width { get; private set; }
        [JsonProperty]
        public int Depth { get; private set; }
        [JsonProperty]
        public string Description { get; private set; }
        [JsonProperty]
        public int X { get; private set; }
        [JsonProperty]
        public int Y { get; private set; }
        [JsonProperty]
        public int Z { get; private set; }
        [JsonProperty]
        public bool Horizontal { get; private set; }
        [JsonProperty]
        public EnumCabinetElement _enumCabinet { get; private set; }

        public Guid GetGuid() => _guid;

        public void SetEnumName(EnumCabinetElement enumCabinet)
        {
            _enumCabinet = enumCabinet;
        }
        public EnumCabinetElement GetEnumName() => _enumCabinet;    //Wewnetrzna nazwa elementu
        
        public void SetDescription(string value)
        {
            if (value != Description)
            {
                Description = value;
                ChangeDescription = true;
            }
        }
        
        public void SetHeight(int value)
        {
            if (value >= 0 && value != Height)
            {
                Height = value;
                ChangeHeight = true;
            }
        }
        
        public void SetWidth(int value)
        {
            if (value >= 0 && value != Width)
            {
                Width = value;
                ChangeWidth = true;
            }

        }
        
        public void SetDepth(int value)
        {
            if (value >= 0 && value != Depth)
            {
                Depth = value;
                ChangeDepth = true;
            }
        }
        

        public void SetX(int value)
        {
            if (value != X)
            {
                X = value;
                ChangeX = true;
            }
        }
        
        public void SetY(int value)
        {
            if (value != Y)
            {
                Y = value;
                ChangeY = true;
            }
        }
        
        public void SetZ(int value)
        {
            if (value != Z)
            {
                Z = value;
                ChangeZ = true;
            }
        }
        
        
        public void SetHorizontal(bool value)
        {
            Horizontal = value;
        }
        
        
        public string Material;

        public bool ChangeDescription { get; private set; }
        public bool ChangeHeight { get; private set; }
        public bool ChangeWidth { get; private set; }
        public bool ChangeDepth { get; private set; }
        public bool ChangeX { get; private set; }
        public bool ChangeY { get; private set; }
        public bool ChangeZ { get; private set; }


        public ElementModel()
        {
            _guid = Guid.NewGuid();
            //ChangeHeight = false;

        }

        public ElementModel(string description, int height, int width, int depth, int x, int y, int z, EnumCabinetElement enumCabinet, bool horizontal)
        {
            _guid = Guid.NewGuid();
            Description = description;
            Height = height;
            Width = width;
            Depth = depth;
            X = x;
            Y = y;
            Z = z;
            _enumCabinet = enumCabinet;
            Horizontal = horizontal;
        }

        public ElementModel CopyTo(ElementModel elementModel)
        {
            return new ElementModel
            {
                _guid = elementModel.GetGuid(),
                Description = elementModel.Description,
                Height = elementModel.Height,
                Width=elementModel.Width,
                Depth=elementModel.Depth,
                X=elementModel.X,
                Y=elementModel.Y,
                Z=elementModel.Z,
                _enumCabinet=elementModel._enumCabinet,
                Horizontal=elementModel.Horizontal,
                Material=elementModel.Material,
                ChangeDepth=elementModel.ChangeDepth,
                ChangeDescription=elementModel.ChangeDescription,
                ChangeHeight=elementModel.ChangeHeight,
                ChangeWidth=elementModel.ChangeWidth,
                ChangeX=elementModel.ChangeX,
                ChangeY=elementModel.ChangeY,
                ChangeZ=elementModel.ChangeZ
            };
        }

    }


}

