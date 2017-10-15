public class Game
{
    public readonly Corp corp;
    public readonly Runner runner;

    public Game(Corp corp, Runner runner)
    {
        this.corp = corp;
        this.runner = runner;
    }

    public void Start()
    {
        runner.StartGame();
    }
}
