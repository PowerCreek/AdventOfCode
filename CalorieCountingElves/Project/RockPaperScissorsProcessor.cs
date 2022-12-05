namespace CalorieCountingElves.Project
{
    public class RockPaperScissorsProcessor
    {
        public string InputString { get; set; }

        public static int GetScore(RockPaperScissorsProcessor processor)
        {
            int CalculateRoundResult(string input)
            {
                const char ROCK_OPP = 'A';
                const char PAPER_OPP = 'B';
                const char SCISSORS_OPP = 'C';

                const char ROCK_YOU = 'X';
                const char PAPER_YOU = 'Y';
                const char SCISSORS_YOU = 'Z';

                const int LOSE = 0;
                const int DRAW = 3;
                const int WIN = 6;

                const int ROCK_WIN = WIN + 1;
                const int PAPER_WIN = WIN + 2;
                const int SCISSORS_WIN = WIN + 3;

                const int ROCK_DRAW = DRAW + 1;
                const int PAPER_DRAW = DRAW + 2;
                const int SCISSORS_DRAW = DRAW + 3;

                const int ROCK_LOSE = LOSE + 1;
                const int PAPER_LOSE = LOSE + 2;
                const int SCISSORS_LOSE = LOSE + 3;

                return input switch
                {
                    [SCISSORS_OPP, ROCK_YOU] => ROCK_WIN,
                    [PAPER_OPP, ROCK_YOU] => ROCK_LOSE,
                    [_, ROCK_YOU] => ROCK_DRAW,

                    [ROCK_OPP, PAPER_YOU] => PAPER_WIN,
                    [SCISSORS_OPP, PAPER_YOU] => PAPER_LOSE,
                    [_, PAPER_YOU] => PAPER_DRAW,

                    [PAPER_OPP, SCISSORS_YOU] => SCISSORS_WIN,
                    [ROCK_OPP, SCISSORS_YOU] => SCISSORS_LOSE,
                    [_, SCISSORS_YOU] => SCISSORS_DRAW,
                };
            }

            var values = processor.InputString;// "C X\r\nC X\r\nC X\r\nA Z\r\nC X\r\nC X\r\nA Y\r\nB X\r\nB Y\r\nB Z";

            var allValues = values.Split('\n').GroupBy(a => a?.Replace(" ", "").Trim()).Select(v => (Key: v.Key, Count: v.Count()));

            var calculatedValue = allValues.Select(a => CalculateRoundResult(a.Key[..2]) * a.Count).Sum();
            return calculatedValue;
        }


        public static int GetScore2(RockPaperScissorsProcessor processor)
        {
            int CalculateRoundResult(string input)
            {
                const char ROCK_OPP = 'A';
                const char PAPER_OPP = 'B';
                const char SCISSORS_OPP = 'C';

                const char FORCE_LOSE = 'X';
                const char FORCE_DRAW = 'Y';
                const char FORCE_WIN = 'Z';

                const int LOSE = 0;
                const int DRAW = 3;
                const int WIN = 6;

                const int ROCK_WIN = WIN + 1;
                const int PAPER_WIN = WIN + 2;
                const int SCISSORS_WIN = WIN + 3;

                const int ROCK_DRAW = DRAW + 1;
                const int PAPER_DRAW = DRAW + 2;
                const int SCISSORS_DRAW = DRAW + 3;

                const int ROCK_LOSE = LOSE + 1;
                const int PAPER_LOSE = LOSE + 2;
                const int SCISSORS_LOSE = LOSE + 3;

                return input switch
                {
                    [ROCK_OPP, FORCE_LOSE] => SCISSORS_LOSE,
                    [ROCK_OPP, FORCE_WIN] => PAPER_WIN,
                    [ROCK_OPP, _] => ROCK_DRAW,

                    [PAPER_OPP, FORCE_LOSE] => ROCK_LOSE,
                    [PAPER_OPP, FORCE_WIN] => SCISSORS_WIN,
                    [PAPER_OPP, _] => PAPER_DRAW,

                    [SCISSORS_OPP, FORCE_LOSE] => PAPER_LOSE,
                    [SCISSORS_OPP, FORCE_WIN] => ROCK_WIN,
                    [SCISSORS_OPP, _] => SCISSORS_DRAW,
                };
            }

            var values = processor.InputString;// "C X\r\nC X\r\nC X\r\nA Z\r\nC X\r\nC X\r\nA Y\r\nB X\r\nB Y\r\nB Z";

            var allValues = values.Split('\n').GroupBy(a => a?.Replace(" ", "").Trim()).Select(v => (Key: v.Key, Count: v.Count()));

            var calculatedValue = allValues.Select(a => CalculateRoundResult(a.Key[..2]) * a.Count).Sum();
            return calculatedValue;
        }
    }
}
