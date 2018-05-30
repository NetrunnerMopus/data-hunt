using System;
using System.Threading.Tasks;

namespace model.timing
{
    public class GameFlow
    {
        public readonly PaidWindow paidWindow = new PaidWindow();
        public readonly corp.Turn corpTurn;
        public readonly runner.Turn runnerTurn;
        private bool ended;

        public GameFlow(corp.Turn corpTurn, runner.Turn runnerTurn)
        {
            this.corpTurn = corpTurn;
            this.runnerTurn = runnerTurn;
        }

        async public Task Start()
        {
            try
            {
                while (!ended)
                {
                    await corpTurn.Start();
                    await runnerTurn.Start();
                }
            }
            catch (Exception e)
            {
                if (ended)
                {
                    UnityEngine.Debug.Log("The game is over! " + e.Message);
                }
            }
        }

        public void DeckCorp()
        {
            ended = true;
            throw new System.Exception("Corp is decked, the Runner wins!");
        }
    }
}