using model;
using model.cards;
using model.play;

namespace controller
{
    public class PlayableInGrip : Droppable, IUsabilityObserver, IPayabilityObserver
    {
        public ICard Card;
        private Game game;
        private Ability ability;
        private bool abilityUsable;
        private bool costPayable;

        void Start()
        {
            zone = FindObjectOfType<PlayZone>();
            game = GameConfig.game;
            ability = game.runner.actionCard.Play(Card);
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