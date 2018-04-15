using model.costs;

namespace model
{
    public class Runner
    {
        private Game game;
        public int tags = 0;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Game game, Stack stack, Heap heap, ClickPool clicks, CreditPool credits)
        {
            this.game = game;
            this.stack = stack;
            this.heap = heap;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
            stack.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                stack.Draw();
            }
            for (int i = 0; i < 4; i++)
            {
                clicks.Gain();
            }
        }

        public bool Draw()
        {
            ICost cost = new RunnerClickCost(1);
            if (cost.CanPay(game))
            {
                cost.Pay(game);
                stack.Draw();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GainCredit()
        {
            ICost cost = new RunnerClickCost(1);
            if (cost.CanPay(game))
            {
                cost.Pay(game);
                credits.Gain(1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Install(ICard card)
        {
            throw new System.Exception("Not implemented yet");
        }

        public bool Play(ICard card)
        {
            ICost totalCost = new Conjunction(new RunnerClickCost(1), card.PlayCost);
            if (totalCost.CanPay(game))
            {
                totalCost.Pay(game);
                card.PlayEffect.Resolve(game);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveTag()
        {
            throw new System.Exception("Not implemented yet");
        }

        public bool Run()
        {
            throw new System.Exception("Not implemented yet");
        }
    }
}