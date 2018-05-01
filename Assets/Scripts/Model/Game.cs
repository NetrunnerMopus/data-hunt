namespace model
{
    public class Game
    {
        public Corp corp;
        public Runner runner;

        public void Start()
        {
            runner.StartGame();
        }
    }
}