namespace model.zones.runner
{
    public class Zones
    {
        public readonly Grip grip;
        public readonly Stack stack;
        public readonly Heap heap;
        public readonly Rig rig;
        public readonly Score score;

        public Zones(Grip grip, Stack stack, Heap heap, Rig rig, Score score)
        {
            this.grip = grip;
            this.stack = stack;
            this.heap = heap;
            this.rig = rig;
            this.score = score;
        }
    }
}
