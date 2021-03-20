using System;
using System.Threading.Tasks;

namespace model.costs
{
    public class Nothing : ICost
    {
        bool ICost.Payable => true;
        public event Action<ICost, bool> ChangedPayability;
        async Task ICost.Pay() => await Task.CompletedTask;
    }
}
