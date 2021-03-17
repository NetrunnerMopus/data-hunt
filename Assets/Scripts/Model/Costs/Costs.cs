
using model.cards;
using model.zones.corp;

namespace model.costs
{
    public class Costs
    {
        private CreditPool corpCredits;
        private ClickPool corpClicks;
        private CreditPool runnerCredits;
        private ClickPool runnerClicks;

        public Costs(CreditPool corpCredits, ClickPool corpClicks, CreditPool runnerCredits, ClickPool runnerClicks)
        {
            this.corpCredits = corpCredits;
            this.corpClicks = corpClicks;
            this.runnerCredits = runnerCredits;
            this.runnerClicks = runnerClicks;
        }

        internal ICost ClickCorp(int clicks) => new ClickCost(corpClicks, clicks);
        internal ICost ClickRunner(int clicks) => new ClickCost(runnerClicks, clicks);
        internal ICost PlayOperation(Card operation, int playCost) => new CreditCost(corpCredits, playCost);
        internal ICost PlayEvent(Card @event, int playCost) => new CreditCost(runnerCredits, playCost);
        internal ICost InstallProgram(Card program, int installCost) => new CreditCost(runnerCredits, installCost);
        internal ICost InstallHardware(Card hardware, int installCost) => new CreditCost(runnerCredits, installCost);
        internal ICost InstallResource(Card resource, int installCost) => new CreditCost(runnerCredits, installCost);
        internal ICost InstallIce(IceColumn ice) => new CreditCost(corpCredits, ice.Height);
        internal ICost Rez(Card card, int rezCost) => new CreditCost(corpCredits, rezCost);
        internal ICost Trash(Card trashable, int trashCost) => new CreditCost(runnerCredits, trashCost);
        internal ICost Steal(Card stealable, int stealCost) => new CreditCost(runnerCredits, stealCost);
    }
}
