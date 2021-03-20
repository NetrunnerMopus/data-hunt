using System.Collections.Generic;
using model;
using UnityEngine;
using view.gui;

namespace controller
{
    public class RunInitiation
    {
        private GameObject gameObject;
        private Runner runner;
        private Droppable droppable;
        private IDictionary<ServerBox, DropZone> dropZones = new Dictionary<ServerBox, DropZone>();
        private IDictionary<ServerBox, IInteractive> abilities = new Dictionary<ServerBox, IInteractive>();

        public RunInitiation(GameObject gameObject, Runner runner, ServerRow serverRow)
        {
            this.gameObject = gameObject;
            this.runner = runner;
            droppable = gameObject.AddComponent<Droppable>();
            serverRow.BoxAdded += EnableRunning;
            serverRow.BoxRemoved += DisableRunning;
            foreach (var box in serverRow.ListBoxes())
            {
                EnableRunning(box);
            }
        }

        private void EnableRunning(ServerBox box)
        {
            var boxZone = box.gameObject.AddComponent<DropZone>();
            dropZones[box] = boxZone;
            var runOnServer = runner.Acting.Run(box.server);
            abilities[box] = new InteractiveAbility(runOnServer, boxZone);
            droppable.Represent(abilities[box]);
        }

        private void DisableRunning(ServerBox box)
        {
            Object.Destroy(dropZones[box]);
            droppable.Unlink(abilities[box]);
            abilities.Remove(box);
        }
    }
}
