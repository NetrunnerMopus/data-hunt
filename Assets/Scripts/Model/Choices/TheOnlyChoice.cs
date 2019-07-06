using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.choices
{
    public class TheOnlyChoice<T> : IChoice<T>
    {
        private IChoice<T> fallback;

        public TheOnlyChoice(IChoice<T> fallback)
        {
            this.fallback = fallback;
        }

        Task<T> IChoice<T>.Declare(IEnumerable<T> items)
        {
            if (items.Count() == 1)
            {
                return Task.FromResult(items.Single());
            }
            else
            {
                return fallback.Declare(items);
            }
        }
    }
}