
public class ScoresFunctions
{
    public int ConvertToInt(Scores score)
    {
        switch (score)
        {
            case Scores.TenThousand: return 10000;

            case Scores.FiveThousand: return 5000;

            case Scores.FourThousand: return 4000;

            case Scores.ThreeThousand: return 3000;

            case Scores.TwoThousand: return 2000;
           
            default: return 1000;
        }
    }
}
