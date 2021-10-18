using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiAnnotations.Anotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutomationProfile : Attribute
    {
        public AutomationProfile()
        {

        }

        public Type Profile { set; get; }

    }
}
