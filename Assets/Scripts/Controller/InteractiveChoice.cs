using System.Threading.Tasks;
using UnityEngine;

namespace controller
{
    public class InteractiveChoice<T> : IInteractive
    {
        private T value;
        private bool legal;
        public GameObject GameObject { get; private set; }
        private TaskCompletionSource<T> chosen = new TaskCompletionSource<T>();

        public InteractiveChoice(T value, bool legal, GameObject gameObject)
        {
            this.value = value;
            this.legal = legal;
            this.GameObject = gameObject;
        }

        void IInteractive.Observe(Toggle toggle)
        {
            toggle(legal);
        }

        async Task IInteractive.Interact()
        {
            chosen.SetResult(value);
            await Task.CompletedTask;
        }

        void IInteractive.UnobserveAll()
        {
        }

        public Task<T> AwaitChoice()
        {
            return chosen.Task;
        }
    }
}
