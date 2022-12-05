namespace CalorieCountingElves.Project
{
    public class CalorieProcessor
    {
        public string InputString { get; set; }
        public record struct ElfEntry (int Id, long? Calories);

        public static ElfEntry? GetElfWithTheMostCalories(CalorieProcessor processor)
        {
            return GetElvesSortedByCalories(processor).FirstOrDefault();
        }

        public static IEnumerable<ElfEntry> GetElvesSortedByCalories(CalorieProcessor processor)
        {
            if (processor.InputString is not { Length: > 0 }) return default;

            var inputData = CalorieProcessor.StringGroups(processor.InputString).Select((e, i) => new ElfEntry(i, e));
            var orderedElves = inputData.OrderByDescending(e => e.Calories);
            return orderedElves;
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
