using view;

namespace model
{
    public class Game
    {
        public Corp corp;
        public Runner runner;

        public void AttachView(RunnerView view)
        {
            runner.AttachView(view);
        }

        public void Start()
        {
            runner.StartGame();
        }
    }
}