using Modules.Configs.Scripts;
using Modules.Player.Scripts;
using Modules.Unit.Enemy.Scripts;
using Modules.Unit.Enemy.Units;
using Modules.Unit.Pools;
using UnityEngine;

namespace Modules.Unit.Scripts.Factories
{
    internal class MummyFactory : EnemyUnitFactory
    {
        private PlayerAttacker _playerAttacker;
        private EnemyUnitConfig _mummyConfig;
        private EnemyUnit _prefab;
        private Transform _playerTransform;
        private EnemyUnitPool _mummyPool;

        public override EnemyUnit EnemyUnitPrefab => _prefab;

        public MummyFactory(EnemyUnitConfig zombieConfig, EnemyUnit zombiePrefab, Transform playerTransform, PlayerAttacker playerAttacker)
        {
            _mummyConfig = zombieConfig;
            _prefab = zombiePrefab;
            _playerTransform = playerTransform;
            _playerAttacker = playerAttacker;

            _mummyPool = new EnemyUnitPool();

            _mummyPool.Init(EnemyUnitPrefab);
        }

        public override EnemyUnit Get()
        {
            Mummy unit = (Mummy)_mummyPool.Get();

            RegularEnemyHealth health = new RegularEnemyHealth(_mummyConfig.Health);
            RegularEnemyAttacker attacker = new RegularEnemyAttacker(_mummyConfig, unit.transform, _playerTransform);
            RegularEnemyMovement movement = new RegularEnemyMovement(_mummyConfig, attacker, health, unit.transform, _playerTransform);

            unit.Init(health, movement, attacker, _playerAttacker);

            unit.OnNeedRelease += Release;
            return unit;
        }

        public override void Release(EnemyUnit enemyUnit)
        {
            enemyUnit.OnNeedRelease -= Release;
            _mummyPool.Release(enemyUnit);
        }
    }
}
