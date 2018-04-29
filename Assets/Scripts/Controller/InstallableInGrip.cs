using model.cards;

namespace controller
{
    public class InstallableInGrip : Droppable
    {
        public ICard Card;

        void Start()
        {
            zone = FindObjectOfType<RigZone>();
        }

        protected override bool IsDroppable()
        {
            return true;
        }

        protected override void Drop()
        {
            if (GameConfig.game.runner.Install(Card))
            {
                Destroy(gameObject);
            }
        }
    }
}