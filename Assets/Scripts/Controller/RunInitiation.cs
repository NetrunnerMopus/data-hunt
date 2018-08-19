using UnityEngine;
using view.gui;
using model.zones.corp;
using model;

namespace controller
{
    public class RunInitiation : MonoBehaviour, IServerCreationObserver
    {
        private Game game;
        private ServerRow serverRow;

        public void Represent(Game game, ServerRow serverRow)
        {
            this.game = game;
            this.serverRow = serverRow;
            game.corp.zones.ObserveServerCreation(this);
        }

        void IServerCreationObserver.NotifyRemoteCreated(Remote remote)
        {
            var initiation = gameObject.AddComponent<DroppableAbility>();
            var box = serverRow.boxes[remote].gameObject.AddComponent<DropZone>();
            var runOnServer = game.runner.actionCard.Run(remote);
            initiation.Represent(runOnServer, game, box);
        }
    }
}