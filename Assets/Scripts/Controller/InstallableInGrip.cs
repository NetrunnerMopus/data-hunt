using model.cards;

namespace controller
{
    public class InstallableInGrip : Droppable
    {
        public ICard Card;

        private void Start()
        {
            zone = FindObjectOfType<RigZone>();
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