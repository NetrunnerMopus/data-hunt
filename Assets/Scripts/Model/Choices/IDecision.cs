using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.choices
{
    public interface IDecision<SUBJECT, OPTION>
    {
        // this API is not fit for open-ended questions like "which programs do you want to trash in order to free up this X MU? might be zero, might many, might choose more than necessary"
        Task<OPTION> Declare(SUBJECT subject, IEnumerable<OPTION> options, Game game);
    }
}