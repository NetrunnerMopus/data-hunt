using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.choices
{
    public interface IDecision<SUBJECT>
    {
        Task<IChoice> Declare(SUBJECT subject, IEnumerable<IChoice> options, Game game);
    }
}