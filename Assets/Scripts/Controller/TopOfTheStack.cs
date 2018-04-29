using model;

namespace controller
{
    public class TopOfTheStack : Droppable
    {
        private Game game;

        void Start()
        {
            game = GameConfig.game;
            zone = FindObjectOfType<GripZone>();
        }

        protected override bool IsDroppable()
        {
            return game.runner.actionCard.draw.cost.CanPay(game);
        }

        protected override void Drop()
        {
            game.runner.actionCard.draw.Trigger(GameConfig.game);
        }
    }
}