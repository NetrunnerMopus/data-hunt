using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class Nothing : ICost
    {
        public event Action<ICost, bool> PayabilityChanged;

        async Task ICost.Pay(Game game)
        {
            await Task.CompletedTask;
        }

        bool ICost.Payable(Game game) => true;
    }
}
