using Modules.Unit.Enemy.Units;

namespace Modules.Unit.Scripts.Factories
{
    internal abstract class EnemyUnitFactory
    {
        public abstract EnemyUnit EnemyUnitPrefab { get; }
        public abstract EnemyUnit Get();
        public abstract void Release(EnemyUnit enemyUnit);
    }
}
