using model;

namespace tests.mocks
{
    public class PassiveCorp
    {
        private Game game;

        public PassiveCorp(Game game)
        {
            this.game = game;
        }

        public void SkipTurn()
        {
            var clickForCredit = game.corp.actionCard.credit;
            for (int i = 0; i < 3; i++)
            {
                clickForCredit.Trigger(game);
            }
            DiscardRandomCards();
        }

        public void DiscardRandomCards()
        {
            var hq = game.corp.zones.hq;
            hq.Discard(hq.Random(), game.corp.zones.archives);
        }
    }
}
