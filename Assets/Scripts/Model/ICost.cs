using System;
using System.Threading.Tasks;

namespace model
{
    public interface ICost
    {
        Task Pay(Game game);
        event Action<ICost, bool> PayabilityChanged;
        bool Payable(Game game);
    }
}
