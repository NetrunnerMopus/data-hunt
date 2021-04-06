using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.timing.corp;
using model.timing.runner;

namespace model.timing
{
    public class Timing
    {
        private Game game;
        private Checkpoint checkpoint;
        public event Action<CorpTurn> CorpTurnDefined = delegate { };
        public event Action<ITurn> CurrentTurnQueued = delegate { };
        public event Action<ITurn> NextTurnPredicted = delegate { };
        public event Action<GameFinish> Finished = delegate { };
        private bool gameEnded = false;
        private Queue<ITimingStructure> turns = new Queue<ITimingStructure>();
        private IPilot priority;

        public Timing(Game game)
        {
            this.game = game;
            game.corp.zones.rd.Decked += DeckCorp;
            game.runner.zones.score.StolenEnough += StealEnough;
        }

        async public Task StartTurns()
        {
            try
            {
                while (!gameEnded)
                {
                    CorpTurn corpTurn = new CorpTurn(game.corp, this);
                    CorpTurnDefined(corpTurn);
                    corpTurn.DefinePhases();
                    turns.Enqueue(corpTurn);
                    turns.Enqueue(new RunnerTurn(game.runner, this));
                    await StartNextTurn();
                    await StartNextTurn();
                }
            }
            catch (Exception e)
            {
                if (gameEnded)
                {
                    UnityEngine.Debug.Log("The game is over! " + e.Message);
                }
                else
                {
                    throw new Exception("Failed a turn", e);
                }
            }
        }

        private async Task StartNextTurn()
        {
            var currentTurn = turns.Dequeue();
            CurrentTurnQueued(currentTurn);
            NextTurnPredicted(turns.Peek());
            await currentTurn.Open();
        }

        async public Task OpenActionWindow()
        {
            var window = new ActionWindow();
            await window.Open();
        }

        public PaidWindow DefinePaidWindow(bool rezzing, bool scoring)
        {
           return new PaidWindow(
                rezzing,
                scoring,
                acting: game.corp,
                reacting: game.runner
             );
        }

        async public Task OpenPaidWindow(PaidWindow acting, PaidWindow reacting)
        {
            var bothPlayersCouldAct = false;
            while (true)
            {
                var actingDeclined = await acting.AwaitPass();
                if (actingDeclined && bothPlayersCouldAct)
                {
                    break;
                }
                var reactingDeclined = await reacting.AwaitPass();
                bothPlayersCouldAct = true;
                if (reactingDeclined && bothPlayersCouldAct)
                {
                    break;
                }
            }
        }

        private void DeckCorp(Corp corp)
        {
            Finish(new GameFinish(
                winner: "The Runner",
                loser: "The Corp",
                reason: "Corp R&D is empty"
            ));
        }

        private void StealEnough()
        {
            Finish(new GameFinish(
                winner: "The Runner",
                loser: "The Corp",
                reason: "Runner has stolen enough"
            ));
        }

        private void Finish(GameFinish finish)
        {
            gameEnded = true;
            Finished(finish);
            throw new Exception("Game over, " + finish.reason);
        }

        async public Task Checkpoint()
        {
            await checkpoint.Check();
        }
    }

    public class GameFinish
    {
        public string winner;
        public string loser;
        public string reason;

        public GameFinish(string winner, string loser, string reason)
        {
            this.winner = winner;
            this.loser = loser;
            this.reason = reason;
        }
    }
}
