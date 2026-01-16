using com.company.spancy.dto;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.binder
{
    public class EstateDtoBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                string json = reader.ReadToEnd();
                JObject jObject = JObject.Parse(json);

                string type = jObject.GetValue("type", StringComparison.OrdinalIgnoreCase)?.ToString();

                Type targetType = null;
                switch (type)
                {
                    case "building":
                        targetType = typeof(BuildingDto);
                        break;
                    case "land":
                        targetType = typeof(LandDto);
                        break;
                }

                return JsonConvert.DeserializeObject(json, targetType);
            }
        }

        public bool CanBind(Type modelType)
        {
            return modelType == typeof(EstateDto);
        }
    }
}
