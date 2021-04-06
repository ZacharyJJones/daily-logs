using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace DailyLogsTool
{
    class Program
    {
        public static string ProgramDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public static string ConfigFilePath => $"{ProgramDirectory}config.json";
        public static bool ConfigFileIsMissing => !File.Exists(ConfigFilePath);
        
        public static void Main(string[] args)
        {
            if (ConfigFileIsMissing)
            {
                MakeConfigFile();
                return;
            }

            MakeFilesFromConfig();
        }

        public static void MakeConfigFile()
        {
            // 1. Create config file
            File.WriteAllText(ConfigFilePath, Config.Default.AsJson);

            // 2. Show config file (if on windows)
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Process.Start("explorer.exe", $"/select,\"{ConfigFilePath}\"");
            }

            // 3. tell user to edit config file
            Console.WriteLine($"Config File Generated. ({ConfigFilePath})");
            Console.WriteLine("Opening File Explorer window to file location.");
            Console.WriteLine("Please ensure settings are correct to your preference.");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }

        public static Config LoadConfigFile()
        {
            var file = File.ReadAllText(ConfigFilePath);
            return JsonConvert.DeserializeObject<Config>(file);
        }


        public static void MakeFilesFromConfig()
        {
            // Load config file
            var config = LoadConfigFile();
            Console.WriteLine(config.AsJson);
            
            // Grab date variable
            var todayDate = DateTime.Now.Date;

            // Current spec is weekdays only.
            if (todayDate.DayOfWeek == DayOfWeek.Saturday
            ||  todayDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return;
            }
            
            // Grab relevant date strings.
            var today = todayDate.ToString(config.DateFormat);
            var monday = _goBackToDay(todayDate, DayOfWeek.Monday).ToString(config.DateFormat);
            var friday = _goForwardToDay(todayDate, DayOfWeek.Friday).ToString(config.DateFormat);
            
            // Set up path + folder
            var pathToThisWeekLogsFolder = $"{config.OutputPath}\\{monday}_{friday}";
            Directory.CreateDirectory(pathToThisWeekLogsFolder);
            
            // create each specified file
            foreach (var file in config.Files)
            {
                // Only if today is one of it's specified days to generate.
                if (!file.DaysToGenerate.Contains(todayDate.DayOfWeek))
                {
                    continue;
                }

                // get final path for file.
                var path = $"{pathToThisWeekLogsFolder}\\{today}{file.FileName}.txt";

                // Don't overwrite an existing file
                if (!File.Exists(path))
                {
                    using (var sw = new StreamWriter(path, false))
                    {
                        sw.Write(string.Join("\r\n", file.Content));
                    }
                }

                // Open file in the desired text editor.
                var process = new Process {StartInfo = {FileName = config.TextEditorPath, Arguments = path}};
                process.Start();
            }
        }
        
        private static DateTime _goForwardToDay(DateTime startDate, DayOfWeek dayToGet) =>
            _iterateDay(startDate, dayToGet, (x) => x.AddDays(1));

        private static DateTime _goBackToDay(DateTime startDate, DayOfWeek dayToGet) =>
            _iterateDay(startDate, dayToGet, (x) => x.Subtract(TimeSpan.FromDays(1)));

        /// <summary> Does something with a <see cref="DateTime"/> value until it's 'DayOfWeek' value matches the given parameter. </summary>
        /// <param name="day"> The starting <see cref="DateTime"/> value to iterate upon. </param>
        /// <param name="targetDay"> The target <see cref="DayOfWeek"/> value to get as a result of the iteration. </param>
        /// <param name="iterateFunc"> The iterative function to run until the target <see cref="DayOfWeek"/> value is acquired. </param>
        private static DateTime _iterateDay(DateTime day, DayOfWeek targetDay, Func<DateTime, DateTime> iterateFunc)
        {
            while (day.DayOfWeek != targetDay)
            {
                day = iterateFunc(day);
            }

            return day.Date;
        }
    }
}