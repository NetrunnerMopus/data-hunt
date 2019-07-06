using model.play;
using model.timing;
using System.Collections.Generic;
using System.Linq;

namespace model.player
{
    public class AutoPaidWindowPilot : DelegatingPilot, IPaidWindowObserver
    {
        private HashSet<Ability> paidAbilities = new HashSet<Ability>();

        public AutoPaidWindowPilot(IPilot basic) : base(basic) { }

        override public void Play(Game game)
        {
            base.Play(game);
            var window = game.runner.paidWindow;
            window.ObserveWindow(this);
        }

        void IPaidWindowObserver.NotifyPaidWindowOpened(PaidWindow window)
        {
            if (window.ListAbilities().All(ability => !ability.Usable))
            {
                window.Pass();
            }
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed(PaidWindow window)
        {
        }
    }
}