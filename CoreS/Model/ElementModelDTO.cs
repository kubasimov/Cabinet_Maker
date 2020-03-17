using CoreS.Enum;
using Newtonsoft.Json;
using System;

namespace CoreS.Model
{
    public class ElementModelDTO
    {
        [JsonProperty]
        private Guid _guid { get; set; }
        [JsonProperty]
        private int _height { get; set; }
        [JsonProperty]
        private int _width { get; set; }
        [JsonProperty]
        private int _depth { get; set; }
        [JsonProperty]
        private string _description { get; set; }
        [JsonProperty]
        private int _x { get; set; }
        [JsonProperty]
        private int _y { get; set; }
        [JsonProperty]
        private int _z { get; set; }
        [JsonProperty]
        private bool _horizontal { get; set; }
        [JsonProperty]
        private EnumCabinetElement _enumCabinet { get; set; }

        public Guid GetGuid() => _guid;

        public void SetEnumName(EnumCabinetElement enumCabinet)
        {
            _enumCabinet = enumCabinet;
        }
        public EnumCabinetElement GetEnumName() => _enumCabinet;    //Wewnetrzna nazwa elementu


        public void SetDescription(string value)
        {
            if (value != _description)
            {
                _description = value;
                ChangeDescription = true;
            }
        }
        public string GetDescription() => _description;

        public void SetHeight(int value)
        {
            if (value >= 0 && value != _height)
            {
                _height = value;
                ChangeHeight = true;
            }
        }
        public int GetHeight() => _height;

        public void SetWidth(int value)
        {
            if (value >= 0 && value != _width)
            {
                _width = value;
                ChangeWidth = true;
            }

        }
        public int GetWidth() => _width;

        public void SetDepth(int value)
        {
            if (value >= 0 && value != _depth)
            {
                _depth = value;
                ChangeDepth = true;
            }
        }
        public int GetDepth() => _depth;



        public void SetX(int value)
        {
            if (value >= 0 && value != _x)
            {
                _x = value;
                ChangeX = true;
            }
        }
        public int GetX() => _x;

        public void SetY(int value)
        {
            if (value >= 0 && value != _y)
            {
                _y = value;
                ChangeY = true;
            }
        }
        public int GetY() => _y;

        public void SetZ(int value)
        {
            if (value >= 0 && value != _z)
            {
                _z = value;
                ChangeZ = true;
            }
        }
        public int GetZ() => _z;

        
        public void SetHorizontal(bool value)
        {
            _horizontal = value;
        }
        public bool GetHorizontal() => _horizontal;
        
        public string Material;

        public bool ChangeDescription { get; private set; }
        public bool ChangeHeight { get; private set; }
        public bool ChangeWidth { get; private set; }
        public bool ChangeDepth { get; private set; }
        public bool ChangeX { get; private set; }
        public bool ChangeY { get; private set; }
        public bool ChangeZ { get; private set; }


        public ElementModelDTO()
        {
            _guid = Guid.NewGuid();
            //ChangeHeight = false;

        }

        public ElementModelDTO(string description, int height, int width, int depth, int x, int y, int z, EnumCabinetElement enumCabinet, bool horizontal)
        {
            _guid = Guid.NewGuid();
            _description = description;
            _height = height;
            _width = width;
            _depth = depth;
            _x = x;
            _y = y;
            _z = z;
            _enumCabinet = enumCabinet;
            _horizontal = horizontal;
        }

    }


}

