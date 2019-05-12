using model.player;
using model.timing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model
{
    public class Game
    {
        public readonly Corp corp;
        public readonly Runner runner;
        private bool ended = false;
        private HashSet<IGameFinishObserver> finishObservers = new HashSet<IGameFinishObserver>();

        public Game(Player corpPlayer, Player runnerPlayer)
        {
            corp = CreateCorp(corpPlayer);
            runner = CreateRunner(runnerPlayer);
        }

        private Corp CreateCorp(Player player)
        {
            var turn = new timing.corp.Turn(this);
            var paidWindow = new PaidWindow("corp");
            var zones = new zones.corp.Zones(
                new zones.corp.Headquarters(),
                new zones.corp.ResearchAndDevelopment(player.deck),
                new zones.corp.Archives()
            );
            var actionCard = new play.corp.ActionCard(zones);
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Corp(player.pilot, turn, paidWindow, actionCard, zones, clicks, credits, player.deck.identity);
        }

        private Runner CreateRunner(Player player)
        {
            var turn = new timing.runner.Turn(this);
            var paidWindow = new PaidWindow("runner");
            var actionCard = new play.runner.ActionCard();
            var zones = new zones.runner.Zones(
                new zones.runner.Grip(),
                new zones.runner.Stack(player.deck),
                new zones.runner.Heap(),
                new zones.runner.Rig()
            );
            var clicks = new ClickPool();
            var credits = new CreditPool();
            return new Runner(player.pilot, turn, paidWindow, actionCard, 0, zones, clicks, credits, player.deck.identity);
        }

        async public void Start()
        {
            corp.Start(this);
            runner.Start(this);
            await StartTurns();
        }

        async private Task StartTurns()
        {
            try
            {
                while (!ended)
                {
                    await corp.turn.Start();
                    await runner.turn.Start();
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
                    "The Runner",
                    "The Corp",
                    "Corp R&D is empty"
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