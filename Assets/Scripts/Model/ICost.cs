using System;
using System.Threading.Tasks;

namespace model
{
    public interface ICost
    {
        Task Pay();
        bool Payable();
        event Action<ICost, bool> PayabilityChanged;
    }
}
