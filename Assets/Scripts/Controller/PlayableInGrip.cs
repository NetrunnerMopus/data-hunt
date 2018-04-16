using model.cards;

namespace controller
{
    public class PlayableInGrip : Droppable
    {
        public ICard Card;

        private void Start()
        {
            zone = FindObjectOfType<PlayZone>();
        }

        protected override void Drop()
        {
            if (Netrunner.game.runner.Play(Card))
            {
                Destroy(gameObject);
            }
        }
    }
}