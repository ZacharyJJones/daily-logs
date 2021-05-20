using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DailyLogsTool
{
    public class Config
    {
        [JsonProperty(Order = 1)] 
        public string OutputPath;

        [JsonProperty(Order = 2)] 
        public string TextEditorPath;

        [JsonProperty(Order = 3)] 
        public string DateFormat;

        [JsonProperty(Order = 4)] 
        public List<DailyFile> Files;
        


        [JsonIgnore]
        public string AsJson => JsonConvert.SerializeObject(this, Formatting.Indented);

        [JsonIgnore]
        public static Config Default => new Config()
        {
            TextEditorPath = "C:\\Program Files\\Notepad++\\notepad++.exe",
            OutputPath = "C:\\DailyLogs",
            DateFormat = "yyyy'-'MM'-'dd",
            Files = new List<DailyFile>
            {
                new DailyFile
                {
                    Name = null,
                    DaysToGenerate = new List<DayOfWeek>
                    {
                        DayOfWeek.Monday,
                        DayOfWeek.Tuesday,
                        DayOfWeek.Wednesday,
                        DayOfWeek.Thursday,
                        DayOfWeek.Friday,
                    },
                    Content = new List<string>
                    {
                        "= Tasks =",
                        "",
                        "[ ] ___",
                        "",
                        "[ ] ___",
                        ""
                    }
                },
                new DailyFile
                {
                    Name = "Standup",
                    DaysToGenerate = new List<DayOfWeek>
                    {
                        DayOfWeek.Monday,
                        DayOfWeek.Tuesday,
                        DayOfWeek.Wednesday,
                        DayOfWeek.Thursday,
                        DayOfWeek.Friday,
                    },
                    Content = new List<string>
                    {
                        "Did",
                        " - ",
                        "",
                        "Doing",
                        " - ",
                        "",
                        "Blocked",
                        " - ",
                        "",
                    }
                }
            }
        };
    }
}
