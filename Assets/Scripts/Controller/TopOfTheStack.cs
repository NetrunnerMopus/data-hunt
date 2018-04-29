namespace controller
{
    public class TopOfTheStack : Droppable
    {
        private void Start()
        {
            zone = FindObjectOfType<GripZone>();
        }

        protected override void Drop()
        {
            GameConfig.game.runner.actionCard.draw.Make(GameConfig.game);
        }
    }
}