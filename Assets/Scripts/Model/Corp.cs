namespace model
{
    public class Corp
    {
        public readonly CreditPool credits;

        public Corp(CreditPool credits)
        {
            this.credits = credits;
        }

        public void StartGame()
        {
            credits.Gain(5);
        }
    }
}