using System.Collections.Generic;
using System.Linq;
using model.play;
using model.timing;

namespace model.player
{
    public class AutoPaidWindowPilot : DelegatingPilot
    {
        private HashSet<Ability> paidAbilities = new HashSet<Ability>();

        public AutoPaidWindowPilot(IPilot basic) : base(basic) { }

        override public void Play(Game game)
        {
            base.Play(game);
            var window = game.runner.paidWindow;
            window.Opened += PassIfNoneAvailable;
        }

        private void PassIfNoneAvailable(PaidWindow window)
        {
            if (window.ListAbilities().All(ability => !ability.Ability.Usable))
            {
                window.Pass();
            }
        }
    }
}
