using model.abilities;

namespace model.cards.text {

    public class YourText {
        public readonly TurnText turn;
        public TurnText nextTurn() {
            return new TurnText();
        }
    }


    public class YouText {
        public GainingText gain(int number) {
            return new GainingText();
        }

        public DrawingText draw(int number) {
            return new DrawingText();
        }
    }

    public class SelfText {
        public PlayedText played() {
            return new PlayedText();
        }

        public TrashedText trashed() {
            return new TrashedText();
        }
    }

    public class PlayedText : TriggerCondition {

    }

    public class TrashedText : TriggerCondition {
        public TrashedText byTaking(params object[] damageType) {
            return new TrashedText();
        }
    }

    public class GainingText {
        public readonly CreditsText credits;
    }

    public class DrawingText : IInstruction {
        public readonly DrawingText cards;
    }

    public class CreditsText : IInstruction {
    }

    public class TurnText {
        public readonly TriggerCondition begins;
    }

    public class ThenText {

        private TriggerCondition condition;

        public ThenText(TriggerCondition condition) {
            this.condition = condition;
        }

        public ConditionalAbility then(IInstruction instruction) {
            return new ConditionalAbility(condition, instruction);
        }
    }
}