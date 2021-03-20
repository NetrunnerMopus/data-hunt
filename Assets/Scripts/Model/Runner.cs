using System.Threading.Tasks;
using model.install;
using model.play;
using model.player;
using model.run;
using model.steal;
using model.timing;
using model.timing.runner;
using model.zones;
using model.zones.runner;

namespace model
{
    public class Runner
    {
        public readonly IPilot pilot;
        public readonly RunnerTurn turn;
        public readonly PaidWindow paidWindow;
        public int tags = 0;
        public readonly Zones zones;
        public readonly ClickPool clicks;
        public readonly CreditPool credits;
        public RunnerActing Acting { get; }
        public Installing Installing { get; }
        public Running Running { get; }
        public Stealing Stealing { get; }

        public Runner(
            IPilot pilot,
            RunnerTurn turn,
            PaidWindow paidWindow,
            Zone playArea,
            Shuffling shuffling,
            Game game
        )
        {
            this.pilot = pilot;
            this.turn = turn;
            this.paidWindow = paidWindow;
            zones = new Zones(
               new Grip(),
               new Stack(shuffling),
               new Heap(),
               new Rig(this, pilot),
               new Score(),
               playArea
           );
            clicks = new ClickPool(4);
            credits = new CreditPool();
            this.Acting = new RunnerActing(this);
            this.Installing = new Installing(pilot, playArea);
            this.Running = new Running(game);
            this.Stealing = new Stealing(this);
        }

        async public Task Start(Game game, Deck deck)
        {
            zones.stack.AddDeck(deck);
            var identity = deck.identity;
            zones.identity.Add(identity);
            identity.FlipFaceUp();
            await identity.Activate();
            pilot.Play(game);
            credits.Gain(5);
            zones.stack.Draw(5, zones.grip);
        }
    }
}
