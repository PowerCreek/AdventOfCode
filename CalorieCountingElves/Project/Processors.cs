using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCountingElves.Project
{

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
