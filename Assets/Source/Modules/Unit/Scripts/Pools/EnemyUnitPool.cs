using Modules.Unit.Enemy.Units;

namespace Modules.Unit.Pools
{
    internal class EnemyUnitPool : BasePool<EnemyUnit>
    {
        public override EnemyUnit Get()
        {
            return _objectPool.Get();
        }

        public override void Release(EnemyUnit obj)
        {
            _objectPool.Release(obj);
        }
    }
}
