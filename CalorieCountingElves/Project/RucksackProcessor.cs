namespace CalorieCountingElves.Project
{
    public class RucksackProcessor
    {
        public string InputString { get; set; }
        
        public static char? GetItemFromSet(string items)
        {
            var compartmentSize = items.Length/2;

            var grouped = items.Select((e, i) => (Value: e, Slot: i)).GroupBy(e => e.Slot < compartmentSize).ToArray();
            if (grouped is not [_, _]) return null;

            var result = grouped[0].FirstOrDefault(e=>grouped[1].Any(a=>char.Equals(a.Value, e.Value)));

            return items[result.Slot];
        }

        public static IEnumerable<char?> AllDuplicates(RucksackProcessor processor)
        {
            var values = processor.InputString;
            var allValues = values.Split('\n').Select(a => a?.Replace(" ", "").Trim());

            var result = allValues.Select(v => GetItemFromSet(v));
            return result;
        }

        public static (IEnumerable<byte> Characters, int Total) TotalGroupItems(RucksackProcessor processor)
        {
            var values = processor.InputString;
            var allValues = values.Split('\n').Select(a => a?.Replace(" ", "").Trim());

            var v = allValues.Select((e,i)=>(Value: e,Index: i)).GroupBy((e)=> (int)(e.Index/3),e=>e.Value);
            
            var agg = v.Sum(a=>CommonCharacter(a));
            
            return (v.Select(e=>CommonCharacter(e)), agg);
        }

        public static byte CommonCharacter(IEnumerable<string> itemGroup)
        {
            var itemMap = new (byte GroupNum, byte Value)?[60];

            foreach((var Value, var Index) in itemGroup.Select((e,i)=>(Value: e, Index: i)))
            {
                foreach(var c in Value)
                {
                    var characterIndex = c.CharCode();

                    var check = itemMap[characterIndex] ??= ((byte) Index, (byte)1);

                    if (check.GroupNum == Index) continue;

                    var result = itemMap[characterIndex] = (((byte)Index, (byte)(check.Value+1)));

                    if (result is { Value: >= 3 })
                    {
                        return characterIndex;
                    }
                }
            }

            return 0;
        }
    }
}
