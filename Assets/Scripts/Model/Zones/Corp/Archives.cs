using model.player;
using System.Threading.Tasks;

namespace model.zones.corp
{
    public class Archives : IServer
    {
        public Zone Zone { get; } = new Zone("Archives");
        public IceColumn Ice { get; }
        private Game game;

        public Archives(Game game)
        {
            this.game = game;
            Ice = new IceColumn(game);
        }

        Task IServer.Access(int accessCount, IPilot pilot, Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}
