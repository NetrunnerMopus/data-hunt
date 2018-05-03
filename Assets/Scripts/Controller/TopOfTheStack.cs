using model;

namespace controller
{
    public class TopOfTheStack : Droppable
    {
        public Game Game { private get; set; }

        void Start()
        {
            zone = FindObjectOfType<GripZone>();
        }

        protected override bool IsDroppable()
        {
            return Game.runner.actionCard.draw.cost.CanPay(Game);
        }

        protected override void Drop()
        {
            Game.runner.actionCard.draw.Trigger(Game);
        }
    }
}