using model;
using model.cards;
using model.play;

namespace controller
{
    public class InstallableInGrip : Droppable, IUsabilityObserver, IPayabilityObserver
    {
        public ICard Card;
        private Game game;
        private Ability ability;
        private bool abilityUsable;
        private bool costPayable;

        void Start()
        {
            zone = FindObjectOfType<RigZone>();
            game = GameConfig.game;
            ability = game.runner.actionCard.Install(Card);
            ability.Observe(this, game);
            Card.PlayCost.Observe(this, game);
        }

        protected override bool IsDroppable()
        {
            return abilityUsable && costPayable;
        }

        protected override void Drop()
        {
            ability.Trigger(game);
            Destroy(gameObject);
        }

        void IUsabilityObserver.NotifyUsable(bool usable)
        {
            abilityUsable = usable;
        }

        void IPayabilityObserver.NotifyPayable(bool payable)
        {
            costPayable = payable;
        }
    }
}