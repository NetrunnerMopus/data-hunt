using model;
using model.cards;
using model.play;
using view;
using view.gui;

namespace controller
{
    public class PlayableInGrip : Droppable, IUsabilityObserver
    {
        public ICard Card { private get; set; }
        public Game Game { private get; set; }
        private Ability ability;
        private AbilityHighlight highlight;
        private bool abilityUsable;

        void Start()
        {
            zone = FindObjectOfType<PlayZone>();
            ability = Game.runner.actionCard.Play(Card);
            ability.Observe(this, Game);
            highlight = new AbilityHighlight(gameObject.AddComponent<Highlight>());
            ability.Observe(highlight, Game);
        }

        protected override bool IsDroppable()
        {
            return abilityUsable;
        }

        protected override void Drop()
        {
            ability.Trigger(Game);
        }

        void OnDestroy()
        {
            ability.Unobserve(this);
            ability.Unobserve(highlight);
        }

        void IUsabilityObserver.NotifyUsable(bool usable)
        {
            abilityUsable = usable;
        }
    }
}