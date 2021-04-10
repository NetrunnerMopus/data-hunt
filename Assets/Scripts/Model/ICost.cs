using System;
using System.Threading.Tasks;
using model.player;

namespace model
{
    public interface ICost
    {
        bool Payable { get; }
        Task Pay(IPilot controller);
        event Action<ICost, bool> ChangedPayability;
    }
}
