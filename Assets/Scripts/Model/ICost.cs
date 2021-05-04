using System;
using System.Threading.Tasks;

namespace model
{
    public interface ICost
    {
        bool Payable { get; }
        Task Pay();
        event Action<ICost, bool> ChangedPayability;
    }
}
