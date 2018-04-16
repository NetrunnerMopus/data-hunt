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
            Netrunner.game.runner.GainCredit();
        }
    }
}