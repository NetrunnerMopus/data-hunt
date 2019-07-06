using System.Collections.Generic;
using System.Linq;

namespace model.player
{
    public class TheOnlyChoice<T> : IChoice<T>
    {
        private IChoice<T> fallback;

        public TheOnlyChoice(IChoice<T> fallback)
        {
            this.fallback = fallback;
        }

        T IChoice<T>.Declare(IEnumerable<T> items)
        {
            if (items.Count() == 1)
            {
                return items.Single();
            }
            else
            {
                return fallback.Declare(items);
            }
        }
    }
}