using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiAnnotations.Execute
{
    public static class ExecuteDi
    {
        public static void ExecuteDI(this IServiceCollection services, Assembly[] assemblies)
        {
            
            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll",
                                         SearchOption.AllDirectories);

            List<Type> types = new List<Type>();

            foreach(string dll in filePaths)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.LoadFrom(dll);
                }
                catch(System.BadImageFormatException ex)
                {
                    continue;
                }

                var type1Types =
                        (from type in assembly.GetTypes()
                        where type.IsDefined(typeof(Di), true)
                        select type).ToList();

                types.AddRange(type1Types);
            }

            foreach (var item in types)
            {

                Di di =
                    (Di)Attribute.GetCustomAttribute(item, typeof(Di));

                if (!di.IsActive)
                    continue;

                switch (di.RequestType)
                {
                    case RequestType.Scoped:
                        services.AddScoped(di.Int, di.Imp);
                        break;
                    case RequestType.Singletone:
                        services.AddSingleton(di.Int, di.Imp);
                        break;
                    case RequestType.Transient:
                    default:
                        services.AddTransient(di.Int, di.Imp);
                        break;
                }
            }
        }
    }
}
