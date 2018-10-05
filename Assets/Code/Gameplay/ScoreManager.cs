
public static class ScoreManager {

    private static int s_currentScore;

    public static void AddScore(int val)
    {
        s_currentScore += val;
    }

    public static int GetScore()
    {
        return s_currentScore;
    }
}
