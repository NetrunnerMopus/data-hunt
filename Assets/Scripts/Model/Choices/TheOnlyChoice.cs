using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.choices
{
    public class TheOnlyChoice<SUBJECT, OPTION> : IChoice<SUBJECT, OPTION>
    {
        private IChoice<SUBJECT, OPTION> fallback;

        public TheOnlyChoice(IChoice<SUBJECT, OPTION> fallback)
        {
            this.fallback = fallback;
        }

        public Task<OPTION> Declare(SUBJECT subject, IEnumerable<OPTION> options)
        {
            if (options.Count() == 1)
            {
                return Task.FromResult(options.Single());
            }
            else
            {
                return fallback.Declare(subject, options);
            }
        }
    }
}