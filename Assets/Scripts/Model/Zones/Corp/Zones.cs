namespace model.zones.corp
{
    public class Zones
    {
        public readonly Headquarters hq;
        public readonly ResearchAndDevelopment rd;

        public Zones(Headquarters hq, ResearchAndDevelopment rd)
        {
            this.hq = hq;
            this.rd = rd;
        }
    }
}
