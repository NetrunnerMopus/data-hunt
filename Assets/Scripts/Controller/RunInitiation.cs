using UnityEngine;
using view.gui;
using model;
using System.Collections.Generic;

namespace controller
{
    public class RunInitiation : IServerBoxObserver
    {
        private GameObject gameObject;
        private Game game;
        private Droppable droppable;
        private IDictionary<ServerBox, DropZone> dropZones = new Dictionary<ServerBox, DropZone>();
        private IDictionary<ServerBox, IInteractive> abilities = new Dictionary<ServerBox, IInteractive>();

        public RunInitiation(GameObject gameObject, Game game, ServerRow serverRow)
        {
            this.gameObject = gameObject;
            this.game = game;
            droppable = gameObject.AddComponent<Droppable>();
            serverRow.Observe(this);
        }

        void IServerBoxObserver.NotifyServerBox(ServerBox box)
        {
            var boxZone = box.box.AddComponent<DropZone>();
            dropZones[box] = boxZone;
            var runOnServer = game.runner.actionCard.Run(box.server);
            abilities[box] = new InteractiveAbility(runOnServer, boxZone, game);
            droppable.Represent(abilities[box]);
        }

        void IServerBoxObserver.NotifyServerBoxDisappeared(ServerBox box)
        {
            Object.Destroy(dropZones[box]);
            droppable.Unlink(abilities[box]);
            abilities.Remove(box);
        }
    }
}
