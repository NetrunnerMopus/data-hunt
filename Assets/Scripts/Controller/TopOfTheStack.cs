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
            Netrunner.game.runner.Draw();
        }
    }
}