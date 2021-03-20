using System.Threading.Tasks;
using UnityEngine;

namespace controller
{
    public class InteractiveChoice<T> : IInteractive
    {
        public bool Active { get; }
        public DropZone Activation { get; }
        public GameObject GameObject { get; }
        public event Update Updated;
        private T value;
        private TaskCompletionSource<T> chosen = new TaskCompletionSource<T>();

        public InteractiveChoice(T value, bool legal, DropZone activation, GameObject gameObject)
        {
            this.value = value;
            this.Active = legal;
            this.Activation = activation;
            this.GameObject = gameObject;
        }

        async Task IInteractive.Interact()
        {
            chosen.SetResult(value);
            await Task.CompletedTask;
        }

        public Task<T> AwaitChoice()
        {
            return chosen.Task;
        }
    }
}
