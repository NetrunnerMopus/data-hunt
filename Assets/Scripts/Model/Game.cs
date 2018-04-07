using model.costs;

namespace model
{
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

        public bool Play(ICard card)
        {
            ICost totalCost = new Conjunction(new RunnerClickCost(1), card.PlayCost);
            if (totalCost.CanPay(this))
            {
                totalCost.Pay(this);
                card.PlayEffect.Resolve(this);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}