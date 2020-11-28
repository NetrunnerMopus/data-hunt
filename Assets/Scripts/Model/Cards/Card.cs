using model.choices.trash;
using model.player;
using model.zones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.cards
{
    public abstract class Card
    {
        private HashSet<NotifyActivity> activityObservers = new HashSet<NotifyActivity>();
        private HashSet<NotifyMoved> moveObservers = new HashSet<NotifyMoved>();
        private HashSet<NotifyInfo> infoObservers = new HashSet<NotifyInfo>();
        public abstract string Name { get; }
        public abstract IType Type { get; }
        public Zone Zone { get; private set; }
        /// <summary>
        /// Card-specific cost like play cost, rez cost, install cost
        /// </summary>
        public abstract ICost PlayCost { get; }
        public abstract IEffect Activation { get; }
        public abstract Faction Faction { get; }
        public abstract int InfluenceCost { get; }
        public abstract string FaceupArt { get; }
        public bool Faceup { get; private set; } = false;
        public Information Information { get; private set; } = Information.HIDDEN_FROM_ALL;
        public bool Active { get; private set; } = false;

        public virtual IEnumerable<ITrashOption> TrashOptions(Game game) => new[] { new Leave() };

        public Card()
        {
            this.Zone = new Zone("Outside of the game");
            this.Zone.Add(this);
        }

        async public Task Activate(Game game)
        {
            Active = true;
            await Activation.Resolve(game); // TODO either keep this or `public Activation`, because it's risking double resolution
            NotifyActivity();
        }

        public void Deactivate()
        {
            Active = false;
            NotifyActivity();
        }

        private void NotifyActivity()
        {
            foreach (var observer in activityObservers)
            {
                observer();
            }
        }

        public void ObserveActivity(NotifyActivity observer)
        {
            activityObservers.Add(observer);
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
            foreach (var observer in moveObservers)
            {
                observer(this, source, target);
            }
        }

        internal void FlipPreInstall()
        {
            switch (Faction.Side)
            {
                case Side.CORP: FlipFaceDown(); break;
                case Side.RUNNER: FlipFaceUp(); break;
            }
        }

        public IEnumerable<IInstallDestination> FindInstallDestinations(Game game)
        {
            return Type.FindInstallDestinations(game);
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
            NotifyInfoObservers();
        }

        private void NotifyInfoObservers()
        {
            foreach (var observer in infoObservers)
            {
                observer(Information);
            }
        }

        public void ObserveMoves(NotifyMoved observer)
        {
            moveObservers.Add(observer);
        }

        internal void ObserveInformation(NotifyInfo observer)
        {
            infoObservers.Add(observer);
        }

        internal void UnobserveInformation(NotifyInfo observer)
        {
            infoObservers.Remove(observer);
        }


        public override string ToString()
        {
            return Name + " [" + GetHashCode() + "]";
        }
    }

    public delegate void NotifyActivity();
    public delegate void NotifyMoved(Card card, Zone source, Zone target);
    public delegate void NotifyInfo(Information information);
}
