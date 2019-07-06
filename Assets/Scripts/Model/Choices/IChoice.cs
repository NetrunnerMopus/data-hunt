using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.choices
{
    public interface IChoice<T>
    {
        Task<T> Declare(IEnumerable<T> items);
    }
}