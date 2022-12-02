namespace AdventOfCode
{
    class RockPaperScissors
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            int score = 0;

            foreach (string line in lines)
            {
                string[] plays = line.Split(" ");

                string opponentPlay = plays[0];
                string yourPlay = plays[1];

                // Score the play itself
                score += ScorePlay(yourPlay);

                score += ScoreOutcome(yourPlay, opponentPlay);
            }

            Console.WriteLine(score);
            Console.ReadLine();
        }

        static int ScorePlay(string play)
        {
            switch(play)
            {
                case "X": return 1;
                case "Y": return 2;
                case "Z": return 3;
                default: return 0;
            }
        }

        static int ScoreOutcome(string yourPlay, string opponentPlay)
        {
            switch (opponentPlay)
            {
                case "A":
                    {
                        switch(yourPlay)
                        {
                            case "Y": return 6;
                            case "X": return 3;
                            default: return 0;
                        }
                    }
                case "B":
                    {
                        switch (yourPlay)
                        {
                            case "Z": return 6;
                            case "Y": return 3;
                            default: return 0;
                        }
                    }
                case "C":
                    {
                        switch (yourPlay)
                        {
                            case "X": return 6;
                            case "Z": return 3;
                            default: return 0;
                        }
                    }
                default: return 0;
            }
        }

        static void Part2(string[] lines)
        {
            int score = 0;

            foreach(string line in lines)
            {
                string[] plays = line.Split(" ");

                string opponentPlay = plays[0];
                string roundEnd = plays[1];

                score += ScoreRoundEnd(roundEnd);

                score += ScorePlayFromRoundEnd(roundEnd, opponentPlay);
            }

            Console.WriteLine(score);
            Console.ReadLine();
        }

        static int ScoreRoundEnd(string roundEnd)
        {
            switch(roundEnd)
            {
                case "Z": return 6;
                case "Y": return 3;
                default: return 0;
            }
        }

        static int ScorePlayFromRoundEnd(string roundEnd, string opponentPlay)
        {
            string play = "";
            switch (opponentPlay)
            {
                case "A":
                    {
                        switch (roundEnd)
                        {
                            case "X": play = "Z"; break;
                            case "Y": play = "X"; break;
                            case "Z": play = "Y"; break;
                            default: break;
                        }
                        break;
                    }
                case "B":
                    {
                        switch (roundEnd)
                        {
                            case "X": play = "X"; break;
                            case "Y": play = "Y"; break;
                            case "Z": play = "Z"; break;
                            default: break;
                        }
                        break;
                    }
                case "C":
                    {
                        switch (roundEnd)
                        {
                            case "X": play = "Y"; break;
                            case "Y": play = "Z"; break;
                            case "Z": play = "X"; break;
                            default: break;
                        }
                        break;
                    }

                default: return 0;
            }

            return ScorePlay(play);
        }
    }
}