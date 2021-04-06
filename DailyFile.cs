using System;
using System.Collections.Generic;

namespace DailyLogsTool
{
    public class DailyFile
    {
        public string Name;
        public List<DayOfWeek> DaysToGenerate;
        public List<string> Content;

        public string FileName => string.IsNullOrEmpty(Name) ? "" : $"_{Name}";
    }
}