using model.zones;
using System.Collections.Generic;

namespace model.cards
{
    public abstract class Card
    {
        private HashSet<IFlipObserver> flipObservers = new HashSet<IFlipObserver>();

        public abstract string Name { get; }
        public abstract IType Type { get; }
        public abstract ICost PlayCost { get; }
        public abstract IEffect Activation { get; }
        public abstract Faction Faction { get; }
        public abstract int InfluenceCost { get; }
        public abstract string FaceupArt { get; }
        public bool Faceup { get; private set; } = false;

        public List<IInstallDestination> FindInstallDestinations(Game game)
        {
            return Type.FindInstallDestinations(game);
        }

        public void FlipFaceUp()
        {
            if (Faceup)
            {
                throw new System.Exception("Cannot flip " + Name + " face up, because it already is face up");
            }
            else
            {
                Faceup = true;
                NotifyFlipObservers();
            }
        }

        public void FlipFaceDown()
        {
            if (!Faceup)
            {
                throw new System.Exception("Cannot flip " + Name + " face down, because it already is face down");
            }
            else
            {
                Faceup = false;
                NotifyFlipObservers();
            }
        }

        private void NotifyFlipObservers()
        {
            foreach (var observer in flipObservers)
            {
                observer.NotifyFlipped(Faceup);
            }
        }

        public void ObserveFlips(IFlipObserver observer)
        {
            flipObservers.Add(observer);
        }

        public override string ToString()
        {
            return Name + " [" + GetHashCode() + "]";
        }
    }

    public interface IFlipObserver
    {
        void NotifyFlipped(bool faceup);
    }
}