using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.choices
{
    class FailingChoice<SUBJECT, OPTION> : IDecision<SUBJECT, OPTION>
    {
        public Task<OPTION> Declare(SUBJECT subject, IEnumerable<OPTION> options)
        {
            throw new System.Exception("Don't know how to choose " + subject + " from " + options);
        }
    }
}
