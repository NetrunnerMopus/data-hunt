using System.Collections.Generic;
using System.Threading.Tasks;
using model.abilities;
using model.cards.types;
using model.choices.trash;
using model.costs;
using model.play;
using model.player;
using model.timing.corp;

namespace model.cards.corp {

    public static class costs2 {
        public static string credits(this System.Int32 creds) {
            return creds + " credits";
        }
    }

    public class PadCampaign : Card {


        override public string FaceupArt => "pad-campaign";
        override public string Name => "PAD Campaign";
        override public Faction Faction => Factions.SHADOW;
        override public int InfluenceCost => 0;
        override public ICost PlayCost => game.corp.credits.PayingForPlaying(this, 2);
        override public IType Type => new Asset(game);
        override public IList<ITrashOption> TrashOptions() => new List<ITrashOption> {
            new Leave(),
            new PayToTrash(4, this, game)
        };

        private Ability drip;
        private IList<CorpDrawPhase> phases = new List<CorpDrawPhase>();

        public PadCampaign(Game game) : base(game, game.corp) {
            ///// SYNTAX /////

            // PAD Campaign
            when(your.turn.begins)
                .then(you.gain(1).credits);

            // I've Had Worse
            when(self.played())
                .then(you.draw(3).cards);
            when(self.trashed().byTaking("NET", "MEAT"))
                .then(you.draw(3).cards);

            // Hyoubu Precog Manifold
            statically(self.restrict(play.when(game.Abilities.noLockdown())));
            when(self.played())
                .then(self.deferTrashUntil(your.nextTurn().begins));
            var precogChoice = when(self.played())
                .then(choose().server());
            when(game.runner.runs.successfully().on(precogChoice.server))
                .then(game.psi().miss(game.run.end()))

            //// SEMANTICS //// CR: 9.3.7
            new StaticAbility(restrictions: null, declarations: null, conditions: null)
            new ConditionalAbility(condition: new TriggerCondition(this.trashed().byTaking(NET, MEAT)))

            //// TRY TO COMPILE ////
            game.Abilities.AddConditional(new ConditionalAbility(condition: your.turn.begins, instruction: you.gain(1).credits));
        }

        protected override Task Activate() {


            game.Timing.CorpTurnDefined += DeferDrip;
            return Task.CompletedTask;
        }

        override protected Task Deactivate() {
            game.Timing.CorpTurnDefined -= DeferDrip;
            return Task.CompletedTask;
        }

        private void DeferDrip(CorpTurn turn) {
            turn.Begins.Offer(game.corp.pilot, drip);
        }
    }
}
