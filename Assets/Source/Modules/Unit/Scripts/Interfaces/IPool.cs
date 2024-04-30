using Modules.Unit;

namespace Modules.Unit.Interfaces
{
    internal interface IPool<T> where T : BaseUnit
    {
        T Get();
        void Release(T obj);
    }
}
