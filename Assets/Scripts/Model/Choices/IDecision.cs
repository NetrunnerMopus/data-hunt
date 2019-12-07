using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.choices
{
    public interface IDecision<SUBJECT, OPTION>
    {
        Task<OPTION> Declare(SUBJECT subject, IEnumerable<OPTION> options, Game game);
    }
}