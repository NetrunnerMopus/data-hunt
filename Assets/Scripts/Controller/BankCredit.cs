using model;

namespace controller
{
    public class BankCredit : Droppable
    {
        public Game Game { private get; set; }

        void Start()
        {
            zone = FindObjectOfType<CreditDropZone>();
        }

        protected override bool IsDroppable()
        {
            return Game.runner.actionCard.credit.cost.CanPay(Game);
        }

        protected override void Drop()
        {
            Game.runner.actionCard.credit.Trigger(Game);
        }
    }
}