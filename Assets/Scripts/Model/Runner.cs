using model.cards;
using model.costs;
using model.effects.runner;

namespace model
{
    public class Runner
    {
        private Game game;
        public int tags = 0;
        public readonly Grip grip;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly Rig rig;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;

        public Runner(Game game, Grip grip, Stack stack, Heap heap, Rig rig, ClickPool clicks, CreditPool credits)
        {
            this.game = game;
            this.grip = grip;
            this.stack = stack;
            this.heap = heap;
            this.rig = rig;
            this.clicks = clicks;
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
            stack.Shuffle();
            ((IEffect)new Draw(5)).Resolve(game);
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
                ((IEffect)new Draw(1)).Resolve(game);
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
                ((IEffect)new Gain(1)).Resolve(game);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Install(ICard card)
        {
            ICost totalCost = new Conjunction(new RunnerClickCost(1), card.PlayCost);
            if (totalCost.CanPay(game))
            {
                totalCost.Pay(game);
                ((IEffect)new Install(card)).Resolve(game);
                return true;
            }
            else
            {
                return false;
            }
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