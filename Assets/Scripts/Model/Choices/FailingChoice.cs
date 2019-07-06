using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.choices
{
    class FailingChoice<T> : IChoice<T>
    {
        Task<T> IChoice<T>.Declare(IEnumerable<T> items)
        {
           throw new System.Exception("Don't know how to choose from " + items);
        }
    }
}