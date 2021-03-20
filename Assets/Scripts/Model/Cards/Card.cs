using System.Collections.Generic;
using System.Threading.Tasks;
using model.choices.trash;
using model.player;
using model.steal;
using model.zones;

namespace model.cards
{
    public abstract class Card
    {
        public event NotifyActivity Toggled = delegate { };
        public event NotifyMoved Moved = delegate { };
        public event NotifyInfo ChangedInfo = delegate { };
        public abstract string Name { get; }
        public abstract IType Type { get; }
        public Zone Zone { get; private set; }
        public abstract ICost PlayCost { get; }
        public abstract IEffect Activation { get; }
        public abstract Faction Faction { get; }
        public abstract int InfluenceCost { get; }
        public abstract string FaceupArt { get; }
        public bool Faceup { get; private set; } = false;
        public Information Information { get; private set; } = Information.HIDDEN_FROM_ALL;
        public bool Active { get; private set; } = false;
        public virtual IList<IStealOption> StealOptions() => Type.DefaultStealing(this);
        public virtual IList<ITrashOption> TrashOptions() => new List<ITrashOption>();
        protected Game game;

        public Card(Game game)
        {
            this.game = game;
            this.Zone = new Zone("Outside of the game");
            this.Zone.Add(this);
        }

        async public Task Activate()
        {
            Active = true;
            await Activation.Resolve(); // TODO either keep this or `public Activation`, because it's risking double resolution
            Toggled(this, Active);
        }

        public void Deactivate()
        {
            Active = false;
            Toggled(this, Active);
        }

        public void MoveTo(Zone target)
        {
            var source = Zone;
            if (source == target)
            {
                throw new System.Exception("Tried to move " + Name + " from " + source.Name + " to " + target.Name);
            }
            source.Remove(this);
            target.Add(this);
            Zone = target;
            Moved(this, source, target);
        }

        internal void FlipPreInstall()
        {
            switch (Faction.Side)
            {
                case Side.CORP: FlipFaceDown(); break;
                case Side.RUNNER: FlipFaceUp(); break;
            }
        }

        public IList<IInstallDestination> FindInstallDestinations()
        {
            return Type.FindInstallDestinations();
        }

        public void FlipFaceUp()
        {
            Faceup = true;
            UpdateInfo(Information.OPEN);
        }

        private void FlipFaceDown()
        {
            Faceup = false;
            switch (Faction.Side)
            {
                case Side.CORP: UpdateInfo(Information.HIDDEN_FROM_RUNNER); break;
                case Side.RUNNER: UpdateInfo(Information.HIDDEN_FROM_CORP); break;
            }
        }

        public void UpdateInfo(Information information)
        {
            Information = information;
            ChangedInfo(this, Information);
        }

        public override string ToString()
        {
            return Name + " [" + GetHashCode() + "]";
        }
    }

    public delegate void NotifyActivity(Card card, bool active);
    public delegate void NotifyMoved(Card card, Zone source, Zone target);
    public delegate void NotifyInfo(Card card, Information information);
}
