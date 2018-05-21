namespace model.zones.corp
{
    public class Zones
    {
        public readonly Headquarters hq;
        public readonly ResearchAndDevelopment rd;
        public readonly Archives archives;

        public Zones(Headquarters hq, ResearchAndDevelopment rd, Archives archives)
        {
            this.hq = hq;
            this.rd = rd;
            this.archives = archives;
        }
    }
}
