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
        private IDictionary<ServerBox, DropZone> dropZones = new Dictionary<ServerBox, DropZone>();
        private  IDictionary<ServerBox, Droppable> abilities = new Dictionary<ServerBox, Droppable>();

        public RunInitiation(GameObject gameObject, Game game, ServerRow serverRow)
        {
            this.gameObject = gameObject;
            this.game = game;
            serverRow.Observe(this);
        }

        void IServerBoxObserver.NotifyServerBox(ServerBox box)
        {
            var boxZone = box.gameObject.AddComponent<DropZone>();
            var ability = gameObject.AddComponent<Droppable>();
            dropZones[box] = boxZone;
            abilities[box] = ability;
            var runOnServer = game.runner.actionCard.Run(box.server);
            ability.Represent(new InteractiveAbility(runOnServer, game), boxZone);
        }

        void IServerBoxObserver.NotifyServerBoxDisappeared(ServerBox box)
        {
           Object.Destroy(dropZones[box]);
           Object.Destroy(abilities[box]);
        }
    }
}
