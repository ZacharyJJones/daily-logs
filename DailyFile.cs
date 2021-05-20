using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace DailyLogsTool
{
    public class DailyFile
    {
        [JsonProperty(Order = 1)]
        public string Name;

        [JsonProperty(Order = 2, ItemConverterType = typeof(StringEnumConverter))]
        public List<DayOfWeek> DaysToGenerate;

        [JsonProperty(Order = 3)]
        public List<string> Content;



        [JsonIgnore]
        public string FileName => string.IsNullOrEmpty(Name) ? "" : $"_{Name}";
    }
}