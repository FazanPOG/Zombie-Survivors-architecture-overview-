using Modules.Configs.Scripts;
using Modules.Player.Scripts;
using Modules.Unit.Enemy.Scripts;
using Modules.Unit.Enemy.Units;
using Modules.Unit.Pools;
using UnityEngine;

namespace Modules.Unit.Scripts.Factories
{
    internal class SkeletonFactory : EnemyUnitFactory
    {
        private PlayerAttacker _playerAttacker;
        private EnemyUnitConfig _skeletonConfig;
        private EnemyUnitPool _skeletonPool;
        private EnemyUnit _prefab;
        private Transform _playerTransform;

        public override EnemyUnit EnemyUnitPrefab => _prefab;

        public SkeletonFactory(EnemyUnitConfig zombieConfig, EnemyUnit zombiePrefab, Transform playerTransform, PlayerAttacker playerAttacker)
        {
            _skeletonConfig = zombieConfig;
            _prefab = zombiePrefab;
            _playerTransform = playerTransform;
            _playerAttacker = playerAttacker;

            _skeletonPool = new EnemyUnitPool();

            _skeletonPool.Init(EnemyUnitPrefab);
        }

        public override EnemyUnit Get()
        {
            Skeleton unit = (Skeleton)_skeletonPool.Get();

            RegularEnemyHealth health = new RegularEnemyHealth(_skeletonConfig.Health);
            RegularEnemyAttacker attacker = new RegularEnemyAttacker(_skeletonConfig, unit.transform, _playerTransform);
            RegularEnemyMovement movement = new RegularEnemyMovement(_skeletonConfig, attacker, health, unit.transform, _playerTransform);

            unit.Init(health, movement, attacker, _playerAttacker);

            unit.OnNeedRelease += Release;
            return unit;
        }

        public override void Release(EnemyUnit enemyUnit)
        {
            enemyUnit.OnNeedRelease -= Release;
            _skeletonPool.Release(enemyUnit);
        }
    }
}
