using AutoMapper;
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
    public static class ExecuteProfileClass
    {
        public static void ExecuteProfile(this IServiceCollection services)
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
                        where type.IsDefined(typeof(AutomationProfile), true)
                        select type).ToList();

                types.AddRange(type1Types);
            }

            

                var mappingConfig = new MapperConfiguration(mapper =>
                {
                    //add Profile
                    foreach (var item in types)
                    {

                        AutomationProfile di =
                            (AutomationProfile)Attribute.GetCustomAttribute(item, typeof(AutomationProfile));

                        mapper.AddProfile(di.Profile);

                    }

                });

                IMapper mapper = mappingConfig.CreateMapper();

                //It should be only one Mapper through the application 
                services.AddSingleton(mapper);


            
        }
    }
}
