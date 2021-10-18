using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiAnnotations.Anotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Di : Attribute
    {
        public Di()
        {

        }

        public Type Imp { set; get; }
        public Type Int { set; get; }
        public RequestType RequestType { set; get; } = RequestType.Transient;
        public bool IsActive { set; get; } = true;
    }

    public enum RequestType
    {
        Singletone,
        Scoped,
        Transient
    }
}
