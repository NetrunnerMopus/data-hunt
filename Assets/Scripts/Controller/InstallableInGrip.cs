using model;
using model.cards;
using model.play;
using view;
using view.gui;

namespace controller
{
    public class InstallableInGrip : Droppable, IUsabilityObserver
    {
        public ICard Card;
        private Game game;
        private Ability ability;
        private AbilityHighlight highlight;
        private bool abilityUsable;

        void Start()
        {
            zone = FindObjectOfType<RigZone>();
            game = GameConfig.game;
            ability = game.runner.actionCard.Install(Card);
            ability.Observe(this, game);
            highlight = new AbilityHighlight(gameObject.AddComponent<Highlight>());
            ability.Observe(highlight, game);
        }

        protected override bool IsDroppable()
        {
            return abilityUsable;
        }

        protected override void Drop()
        {
            ability.Trigger(game);
            ability.Unobserve(this);
            ability.Unobserve(highlight);
            Destroy(gameObject);
        }

        void IUsabilityObserver.NotifyUsable(bool usable)
        {
            abilityUsable = usable;
        }
    }
}