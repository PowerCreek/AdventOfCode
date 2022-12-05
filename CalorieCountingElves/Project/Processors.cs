using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCountingElves.Project
{

    public class StackProcessor
    {
        public string InputString { get; set; }

        public static string GetTopCrates(StackProcessor processor)
        {
            var map = new Dictionary<int, Stack<char>>();

            var strings = processor.InputString.Split('\n');

            var columns = strings[8].Select((e, i) => 
            {
                return (Value: e, Index: i); 
            }).Where(e =>
            {
                var success =  int.TryParse(e.Value.ToString(), out var item);
                return success;
            }).Select((e,i) => (e,i)).ToDictionary((e) => e.i);

            foreach(var line in strings[..8].Reverse())
            {
                foreach((var Key, var Value) in columns)
                {
                    var val = line[Value.e.Index];
                    if (char.IsLetter(val) is false) continue;

                    map.TryAdd(Key, new Stack<char>()); 
                    var stack = map[Key];
                    stack.Push(val);
                }
            }

            var instructions = strings[9..].Select(e=>e.Split(' ').Select(e=>int.TryParse(e, out var i) && i > 0?i:0).Where(e=>e > 0).ToArray()).Where(e=>e.Count() > 0).Select(e=>e is [var Amount, var From, var To]?(Amount,From,To):default).ToArray();

            foreach((var Amount, var From, var To) in instructions)
            {
                for(int i = 0; i < Amount; i++)
                {
                    map[To-1].Push(map[From-1].Pop());
                }
            }

            IEnumerable<char> GetTopValues()
            {
                foreach(var m in map.ToArray().OrderBy(e => e.Key))
                {
                    yield return m.Value.Peek();
                }
            }

            var topValues = map.ToArray().OrderBy(e => e.Key).Select(e => e.Value.TryPeek(out var c)?c:'_');

            return String.Join("", topValues);
        }
    }

    public class JobProcessor
    {
        public string InputString { get; set; }

        public static int GetTotalEncapsulatedTasks(JobProcessor processor)
        {
            var values = processor.InputString;
            var allValues = values.Split('\n').Select(a => a?.Replace(" ", "").Trim());

            var groupedItems = allValues.Select((e, i) => (Value: e, Index: (int)i / 2)).GroupBy(e => e.Index);

            int count = 0;

            foreach(var gItems in groupedItems.Select(e=>e.Select(e=>e.Value)).ToArray())
            {
                //Todo
                //set up the string check to account for the ..# and #..
                var item = gItems.Select(a=>a.Split(',').Select(e=>e.Split('-').Select(b=>int.Parse(b)).ToArray()).ToArray()).ToArray();

                count += ((item[1][0][0] <= item[0][0][0] && item[1][0][1] >= item[0][0][1]) || (item[0][0][0] <= item[1][0][0] && item[0][0][1] >= item[1][0][1])) ? 1 : 0;
                count += ((item[1][1][0] <= item[0][1][0] && item[1][1][1] >= item[0][1][1]) || (item[0][1][0] <= item[1][1][0] && item[0][1][1] >= item[1][1][1])) ? 1 : 0;
            }

            return count;
        }
    }

    public static class CharCodeExt
    {
        public static byte CharCode(this char ch)
        {
            if (!char.IsLetter(ch)) throw new ArgumentException();

            byte index = (byte)(ch - 'a');
            byte index2 = (byte)(ch - 'A');

            return (byte)(index switch
            { 
                <= 25 => index + 1,    
                _ => index2 + 27
            });
        }
    }
}
