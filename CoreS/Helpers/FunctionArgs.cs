using System;

namespace CoreS.Helpers
{
    class FunctionArgs :EventArgs
    {
        private object _result;
        public object Result
        {
            get { return _result; }
            set
            {
                _result = value;
                HasResult = true;
            }
        }

        public bool HasResult { get; set; }

        public FunctionArgs()
        {

        }
    }
}
