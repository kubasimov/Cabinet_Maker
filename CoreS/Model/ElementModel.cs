using CoreS.Enum;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CoreS.Model
{
    public class ElementModel
    {
        [JsonProperty]
        public Guid _guid { get; private set; }
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
        
        public void SetDescription(string value,bool change=true)
        {
            if (value != Description)
            {
                Description = value;
                if(change) ChangeDescription = true;
            }
        }
        
        public void SetHeight(int value, bool change = true)
        {
            if (value >= 0 && value != Height)
            {
                Height = value;
                if (change) ChangeHeight = true;
            }
        }
        
        public void SetWidth(int value, bool change = true)
        {
            if (value >= 0 && value != Width)
            {
                Width = value;
                if (change) ChangeWidth = true;
            }

        }
        
        public void SetDepth(int value, bool change = true)
        {
            if (value >= 0 && value != Depth)
            {
                Depth = value;
                if (change) ChangeDepth = true;
            }
        }
        

        public void SetX(int value, bool change = true)
        {
            if (value != X)
            {
                X = value;
                if (change) ChangeX = true;
            }
        }
        
        public void SetY(int value, bool change = true)
        {
            if (value != Y)
            {
                Y = value;
                if (change) ChangeY = true;
            }
        }
        
        public void SetZ(int value, bool change = true)
        {
            if (value != Z)
            {
                Z = value;
                if (change) ChangeZ = true;
            }
        }
        
        
        public void SetHorizontal(bool value, bool change = true)
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Description: {0}{1}", Description, Environment.NewLine);
            
            stringBuilder.AppendFormat("Height: {0}, Width: {1}, Depth: {2}{3}", Height, Width, Depth, Environment.NewLine);
            
            stringBuilder.AppendFormat("x: {0},y: {1}, z: {2}{3}", X, Y, Z, Environment.NewLine);
            
            stringBuilder.AppendFormat("EnumCabinet: {0}{1}", _enumCabinet.ToString(), Environment.NewLine);

            stringBuilder.AppendFormat("Horizontal: {0}", Horizontal);

            return stringBuilder.ToString();
        }

    }


}

