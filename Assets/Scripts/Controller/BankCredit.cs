namespace controller
{
    public class BankCredit : Droppable
    {
        private void Start()
        {
            zone = FindObjectOfType<CreditDropZone>();
        }

        protected override void Drop()
        {
            GameConfig.game.runner.actionCard.credit.Make(GameConfig.game);
        }
    }
}