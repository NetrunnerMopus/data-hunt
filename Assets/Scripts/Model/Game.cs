using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.cards;
using model.choices.steal;
using model.costs;
using model.effects;
using model.player;
using model.timing;
using model.zones;

namespace model
{
    public class Game
    {
        public readonly Corp corp;
        public readonly Runner runner;
        public readonly Zone PlayArea;
        public Costs Costs { get; } // TODO split into features, not layers
        public readonly Checkpoint checkpoint;
        public event EventHandler<ITurn> CurrentTurn = delegate { };
        public event EventHandler<ITurn> NextTurn = delegate { };
        private bool ended = false;
        private Queue<ITurn> turns = new Queue<ITurn>();
        private HashSet<IGameFinishObserver> finishObservers = new HashSet<IGameFinishObserver>();
        private Shuffling shuffling;

        public Game(Shuffling shuffling)
        {
            this.shuffling = shuffling;
            corp = CreateCorp(corpPlayer);
            runner = CreateRunner(runnerPlayer);
            PlayArea = new Zone("Play area");
            this.checkpoint = new Checkpoint(this);
        }

        private Corp CreateCorp(Player player)
        {
            var turn = new timing.corp.CorpTurn(this);
            var paidWindow = new PaidWindow("corp");
            var zones = new zones.corp.Zones(
                new zones.corp.Headquarters(this, new Random()),
                new zones.corp.ResearchAndDevelopment(this, shuffling),
                new zones.corp.Archives(this),
                this
            );
            var clicks = new ClickPool(3);
            var actionCard = new play.corp.ActionCard(clicks, zones, player.pilot);
            var credits = new CreditPool();
            return new Corp(player.pilot, turn, paidWindow, actionCard, zones, clicks, credits, player.deck.identity);
        }

        private Runner CreateRunner(Player player)
        {
            var turn = new timing.runner.RunnerTurn(this);
            var paidWindow = new PaidWindow("runner");
            var actionCard = new play.runner.ActionCard(player.pilot);
            var zones = new zones.runner.Zones(
                new zones.runner.Grip(),
                new zones.runner.Stack(shuffling),
                new zones.runner.Heap(),
                new zones.runner.Rig(this, player.pilot),
                new zones.runner.Score(this)
            );
            var clicks = new ClickPool(4);
            var credits = new CreditPool();
            return new Runner(player.pilot, turn, paidWindow, actionCard, 0, zones, clicks, credits, player.deck.identity);
        }

        async public void Start(Deck corpDeck, Deck runnerDeck)
        {
            await corp.Start(this, corpDeck);
            await runner.Start(this, runnerDeck);
            await StartTurns();
        }

        async private Task StartTurns()
        {
            turns.Enqueue(corp.turn);
            turns.Enqueue(runner.turn);
            try
            {
                while (!ended)
                {
                    turns.Enqueue(corp.turn);
                    turns.Enqueue(runner.turn);
                    await StartNextTurn();
                    await StartNextTurn();
                }
            }
            catch (Exception e)
            {
                if (ended)
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
            CurrentTurn(this, currentTurn);
            NextTurn(this, turns.Peek());
            await currentTurn.Start();
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

        public void ObserveFinish(IGameFinishObserver observer)
        {
            finishObservers.Add(observer);
        }

        public void DeckCorp()
        {
            Finish(new GameFinish(
                winner: "The Runner",
                loser: "The Corp",
                reason: "Corp R&D is empty"
            ));
        }

        public void StealEnough()
        {
            Finish(new GameFinish(
                winner: "The Runner",
                loser: "The Corp",
                reason: "Runner has stolen enough"
           ));
        }

        private void Finish(GameFinish finish)
        {
            ended = true;
            foreach (var observer in finishObservers)
            {
                observer.NotifyGameFinished(finish);
            }
            throw new Exception("Game over, " + finish.reason);
        }

        async public Task Checkpoint()
        {
            await checkpoint.Check();
        }
    }

    public interface IGameFinishObserver
    {
        void NotifyGameFinished(GameFinish finish);
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
