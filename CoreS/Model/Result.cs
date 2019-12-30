using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class Result<T>
    {
        [DataMember]
        public bool IsValid;

        [DataMember]
        public List<Error> Errors;
        
        [DataMember]
        public T Value { get; set; }
        
        [DataMember]
        public List<string> InfoMessages { get; set; }
        
        public Result()
        {
            IsValid = false;
            Errors = new List<Error>();
        }
    }

    
    [DataContract]
    public class Error
    {
        [DataMember]
        public string ErrorCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

    }
}