using model;

namespace controller
{
    public class BankCredit : Droppable
    {
        private Game game;

        void Start()
        {
            zone = FindObjectOfType<CreditDropZone>();
            game = GameConfig.game;
        }

        protected override bool IsDroppable()
        {
            return game.runner.actionCard.credit.cost.CanPay(game);
        }

        protected override void Drop()
        {
            game.runner.actionCard.credit.Trigger(GameConfig.game);
        }
    }
}