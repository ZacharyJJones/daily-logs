using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DailyLogsTool
{
    public class Config
    {
        public string OutputPath;
        public string TextEditorPath;
        public string DateFormat;
        public List<DailyFile> Files;


        public string AsJson => JsonConvert.SerializeObject(this, Formatting.Indented);

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
