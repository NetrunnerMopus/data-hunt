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

        public PadCampaign(Game game) : base(game) {
            ///// SYNTAX /////

            // PAD Campaign
            When(your.Turn.Begins)
                .Then(you.Gain(1).Credits);

            // I've Had Worse
            When(self.Played())
                .Then(you.Draw(3).Cards);
            When(self.Trashed().ByTaking("NET", "MEAT"))
                .Then(you.Draw(3).Cards);

            // Hyoubu Precog Manifold
            // self.Play.OnlyIf(game.Abilities.NoLockdown());
            // When(self.Played())
            //     .Then(self.DeferTrashUntil(your.NextTurn().Begins));
            // var precogChoice = When(self.Played())
            //     .Then(Choose().Server());
            // When(game.Runner.Runs.Successfully().On(precogChoice.Server))
            //     .Then(game.Corp.Psi().Miss(game.Run.End()));

            // //// SEMANTICS //// CR: 9.3.7
            // new StaticAbility(restrictions: null, declarations: null, conditions: null);
            // new ConditionalAbility(condition: new TriggerCondition(this.Trashed().ByTaking(NET, MEAT)));

            //// TRY TO COMPILE ////
            game.Abilities.AddConditional(new ConditionalAbility(condition: your.Turn.Begins, instruction: you.Gain(1).Credits));
        }
    }
}
