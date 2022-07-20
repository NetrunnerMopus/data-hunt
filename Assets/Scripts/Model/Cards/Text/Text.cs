using model.abilities;

namespace model.cards.text {

    public class YourText {
        public TurnText Turn { get; private set; }
        public TurnText NextTurn() {
            return new TurnText();
        }
    }


    public class YouText {
        public GainingText Gain(int number) {
            return new GainingText();
        }

        public DrawingText Draw(int number) {
            return new DrawingText();
        }
    }

    public class SelfText {
        public PlayedText Played() {
            return new PlayedText();
        }

        public TrashedText Trashed() {
            return new TrashedText();
        }
    }

    public class PlayedText : TriggerCondition {

    }

    public class TrashedText : TriggerCondition {

        internal object[] damageTypes;

        public TrashedText ByTaking(params object[] damageTypes) {
            return new TrashedText {
                damageTypes = damageTypes
            };
        }
    }

    public class GainingText {
        public CreditsText Credits { get; private set; }
    }

    public class DrawingText : IInstruction {
        public DrawingText Cards { get; private set; }

        internal DrawingText() {
            Cards = this;
        }
    }

    public class CreditsText : IInstruction {
    }

    public class TurnText {
        public TriggerCondition Begins { get; private set; }
    }

    public class ThenText {

        private readonly TriggerCondition condition;

        public ThenText(TriggerCondition condition) {
            this.condition = condition;
        }

        public ConditionalAbility Then(IInstruction instruction) {
            return new ConditionalAbility(condition, instruction);
        }
    }
}