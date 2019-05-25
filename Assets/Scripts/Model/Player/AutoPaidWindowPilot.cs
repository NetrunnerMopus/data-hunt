using model.cards;
using model.play;
using model.timing;
using model.zones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.player
{
    public class AutoPaidWindowPilot : IPilot, IPaidWindowObserver
    {
        private IPilot basic;
        private HashSet<Ability> paidAbilities = new HashSet<Ability>();
        private Game game;

        public AutoPaidWindowPilot(IPilot basic)
        {
            this.basic = basic;
        }

        void IPilot.Play(Game game)
        {
            basic.Play(game);
            this.game = game;
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

        Task<IEffect> IPilot.TriggerFromSimultaneous(List<IEffect> effects)
        {
            return basic.TriggerFromSimultaneous(effects);
        }

        IChoice<Card> IPilot.ChooseACard()
        {
            return basic.ChooseACard();
        }

        IChoice<IInstallDestination> IPilot.ChooseAnInstallDestination()
        {
            return basic.ChooseAnInstallDestination();
        }
    }
}