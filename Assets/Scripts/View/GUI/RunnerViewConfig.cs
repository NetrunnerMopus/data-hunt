using controller;
using model;
using UnityEngine;

namespace view.gui
{
    public class RunnerViewConfig
    {
        public void Display(Runner runner, GameFlowView flowView, CorpView corpView, BoardParts parts)
        {
            var presentBox = flowView.TimeCross.PresentBox;
            RigGrid rigGrid = new RigGrid(GameObject.Find("Rig"), runner, flowView.PaidChoice, parts);
            HeapPile heapPile = new HeapPile(GameObject.Find("Heap"), runner, parts);
            GripFan gripFan = new GripFan(GameObject.Find("Grip"), runner, presentBox.RunnerActionPhase.AddComponent<DropZone>(), rigGrid.DropZone, heapPile.DropZone, parts);
            new StackPile(GameObject.Find("Stack"), runner, gripFan.DropZone, parts);
            new ZoneBox(GameObject.Find("Runner/Left hand/Score"), parts).Represent(runner.zones.score.zone);
            runner.zones.identity.Added += (zone, identity) => parts.Print(GameObject.Find("Runner/Right hand/Core/Identity")).Print(identity);
            new RunInitiation(
                gameObject: GameObject.Find("Run"),
                serverRow: corpView.serverRow,
                runner: runner
            );
            var credits = GameObject.Find("Runner/Credits");
            presentBox
                .BankCredit
                .AddComponent<Droppable>()
                .Represent(
                    new InteractiveAbility(runner.Acting.credit, credits.AddComponent<DropZone>())
                );
            runner.credits.Changed += credits.AddComponent<CreditSpiral>().UpdateBalance;
            runner.credits.Changed += credits.AddComponent<PileCount>().UpdateBalance;
        }
    }
}
