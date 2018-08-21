using UnityEngine;
using view.gui;
using model;

namespace controller
{
    public class RunInitiation : IServerBoxObserver
    {
        private GameObject gameObject;
        private Game game;

        public RunInitiation(GameObject gameObject, Game game, ServerRow serverRow)
        {
            this.gameObject = gameObject;
            this.game = game;
            serverRow.Observe(this);
        }

        void IServerBoxObserver.NotifyServerBoxCreated(ServerBox box)
        {
            var boxZone = box.gameObject.AddComponent<DropZone>();
            var initiation = gameObject.AddComponent<DroppableAbility>();
            var runOnServer = game.runner.actionCard.Run(box.server);
            initiation.Represent(runOnServer, game, boxZone);
        }
    }
}