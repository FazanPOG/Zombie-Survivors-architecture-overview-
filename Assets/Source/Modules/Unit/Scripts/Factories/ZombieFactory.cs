using Modules.Configs.Scripts;
using Modules.Player.Scripts;
using Modules.Unit.Enemy.Scripts;
using Modules.Unit.Enemy.Units;
using Modules.Unit.Pools;
using UnityEngine;

namespace Modules.Unit.Scripts.Factories
{
    internal class ZombieFactory : EnemyUnitFactory
    {
        private PlayerAttacker _playerAttacker;
        private EnemyUnitConfig _zombieConfig;
        private EnemyUnitPool _zombiePool;
        private EnemyUnit _prefab;
        private Transform _playerTransform;

        public override EnemyUnit EnemyUnitPrefab => _prefab;

        public ZombieFactory(EnemyUnitConfig zombieConfig, EnemyUnit zombiePrefab, Transform playerTransform, PlayerAttacker playerAttacker) 
        {
            _zombieConfig = zombieConfig;
            _prefab = zombiePrefab;
            _playerTransform = playerTransform;
            _playerAttacker = playerAttacker;

            _zombiePool = new EnemyUnitPool();
            
            _zombiePool.Init(EnemyUnitPrefab);
        }

        public override EnemyUnit Get()
        {
            Zombie unit = (Zombie)_zombiePool.Get();

            RegularEnemyHealth health = new RegularEnemyHealth(_zombieConfig.Health);
            RegularEnemyAttacker attacker = new RegularEnemyAttacker(_zombieConfig, unit.transform, _playerTransform);
            RegularEnemyMovement movement = new RegularEnemyMovement(_zombieConfig, attacker, health, unit.transform, _playerTransform);

            unit.Init(health, movement, attacker, _playerAttacker);

            unit.OnNeedRelease += Release;
            return unit;
        }

        public override void Release(EnemyUnit enemyUnit)
        {
            enemyUnit.OnNeedRelease -= Release;
            _zombiePool.Release(enemyUnit);
        }
    }
}
