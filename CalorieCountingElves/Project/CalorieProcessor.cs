using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCountingElves.Project
{

    public class CalorieProcessor
    {
        public string InputString { get; set; }
        public record struct ElfEntry (int Id, long? Calories);

        public static ElfEntry? GetElfWithTheMostCalories(CalorieProcessor processor)
        {
            if (processor.InputString is not { Length: > 0 }) return default;

            var inputData = CalorieProcessor.StringGroups(processor.InputString).Select((e,i)=> new ElfEntry(i,e));
            var elfWithMostCalories = inputData.GroupBy(e => e.Calories).OrderByDescending(e=>e.Key).FirstOrDefault()?.FirstOrDefault();
            
            return elfWithMostCalories;
        }

        public static IEnumerable<long?> StringGroups(string s)
        {
            var calories = default(long);
            
            foreach (var v in s.Split('\n').Select(e=>e.Trim()))
            {
                Func<bool> result = v switch
                {
                    { Length: > 0 } add =>
                        () =>
                        {
                            calories += (long.TryParse(add, out var num)?num:0);
                            return false;
                        },
                    _ =>
                        () =>
                        {
                            return true;
                        }
                };

                if (result())
                {
                    yield return calories;
                    calories = 0;
                }
            }

            if(calories is not 0)
            {
                yield return calories;
                calories = 0;
            }
        }
    }
}
