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
            if (GameConfig.game.runner.Play(Card))
            {
                Destroy(gameObject);
            }
        }
    }
}