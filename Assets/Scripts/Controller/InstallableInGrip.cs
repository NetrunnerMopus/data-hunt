using model;
using model.cards;
using model.play;

namespace controller
{
    public class InstallableInGrip : Droppable, IAvailabilityObserver<Ability>, IAvailabilityObserver<ICost>
    {
        public ICard Card;
        private Game game;
        private Ability ability;
        private bool abilityAvailable;
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
            return abilityAvailable && costPayable;
        }

        protected override void Drop()
        {
            ability.Trigger(game);
            Destroy(gameObject);
        }

        public void Notify(bool available, Ability resource)
        {
            abilityAvailable = available;
        }

        public void Notify(bool available, ICost resource)
        {
            costPayable = available;
        }
    }
}