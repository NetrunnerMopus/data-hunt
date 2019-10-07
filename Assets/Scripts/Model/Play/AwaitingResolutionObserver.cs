
using System.Threading.Tasks;

namespace model.play
{
    public class AwaitingResolutionObserver : IResolutionObserver
    {

        private TaskCompletionSource<bool> resolving = new TaskCompletionSource<bool>();

        void IResolutionObserver.NotifyResolved()
        {
            resolving.SetResult(true);
        }

        async public Task AwaitResolution()
        {
            await resolving.Task;
        }
    }
}