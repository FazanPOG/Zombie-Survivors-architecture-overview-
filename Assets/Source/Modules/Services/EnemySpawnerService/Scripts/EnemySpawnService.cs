using Modules.Configs.Scripts;
using Modules.Player.Scripts;
using Modules.Unit.Enemy.Units;
using Modules.Unit.Scripts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Modules.Services.Scripts
{
    public class EnemySpawnService
    {
        private CameraMovement _cameraMovement;
        private Dictionary<Type, EnemyUnitFactory> _factories;
        private List<EnemyUnit> _enemies;

        public bool HasEnemies => _enemies.Count != 0;

        [Inject]
        public EnemySpawnService(EnemiesData enemiesData, CameraMovement cameraMovement, PlayerRoot playerRoot)
        {
            _enemies = new List<EnemyUnit>();
            _cameraMovement = cameraMovement;
            PlayerAttacker attacker = playerRoot.GetComponent<PlayerAttacker>();

            EnemyConfigReference zombieReference = enemiesData.EnemiesConfigReferences.First(enemy => enemy.EnemyUnit.GetType() == typeof(Zombie));
            EnemyConfigReference mummyReference = enemiesData.EnemiesConfigReferences.First(enemy => enemy.EnemyUnit.GetType() == typeof(Mummy));
            EnemyConfigReference skeletonReference = enemiesData.EnemiesConfigReferences.First(enemy => enemy.EnemyUnit.GetType() == typeof(Skeleton));

            _factories = new Dictionary<Type, EnemyUnitFactory>()
            {
                [typeof(ZombieFactory)] = new ZombieFactory(zombieReference.EnemyUnitConfig, zombieReference.EnemyUnit, playerRoot.transform, attacker),
                [typeof(MummyFactory)] = new MummyFactory(mummyReference.EnemyUnitConfig, mummyReference.EnemyUnit, playerRoot.transform, attacker),
                [typeof(SkeletonFactory)] = new SkeletonFactory(skeletonReference.EnemyUnitConfig, skeletonReference.EnemyUnit, playerRoot.transform, attacker),
            };

        }

        public void SpawnGroup(EnemyType enemyType, int count)
        {
            EnemyUnitFactory factory = GetFactoryByEnemyType(enemyType);

            for (int i = 0; i < count; i++) 
            {
                EnemyUnit unit = factory.Get();
                SetRandomSpawnPosition(unit);
                _enemies.Add(unit);
                unit.OnNeedRelease += OnEnemyUnitRelease;
            }
                
        }

        public void SpawnGroup(List<EnemyType> enemyTypes)
        {
            if (enemyTypes.Count == 0)
                return;

            Dictionary<EnemyUnitFactory, int> enemyFactoryDictionary = new Dictionary<EnemyUnitFactory, int>();

            foreach (EnemyType enemyType in enemyTypes)
            {
                EnemyUnitFactory factory = GetFactoryByEnemyType(enemyType);

                if (enemyFactoryDictionary.ContainsKey(factory))
                    enemyFactoryDictionary[factory] += 1;
                else 
                    enemyFactoryDictionary.Add(factory, 1);
            }

            foreach (EnemyUnitFactory factory in enemyFactoryDictionary.Keys)
            {
                for (int i = 0; i < enemyFactoryDictionary[factory]; i++) 
                {
                    EnemyUnit unit = factory.Get();
                    SetRandomSpawnPosition(unit);
                    _enemies.Add(unit);
                    unit.OnNeedRelease += OnEnemyUnitRelease;
                }
            }
        }

        public void SpawnSingle(EnemyType enemyType)
        {
            EnemyUnitFactory factory = GetFactoryByEnemyType(enemyType);
            EnemyUnit unit = factory.Get();

            SetRandomSpawnPosition(unit);
            _enemies.Add(unit);
            unit.OnNeedRelease += OnEnemyUnitRelease;
        }

        private EnemyUnitFactory GetFactoryByEnemyType(EnemyType enemyType) 
        {
            switch (enemyType) 
            {
                case EnemyType.Zombie:
                    return _factories.Values.First(factory => factory.EnemyUnitPrefab.GetType() == typeof(Zombie));
                case EnemyType.Mummy:
                    return _factories.Values.First(factory => factory.EnemyUnitPrefab.GetType() == typeof(Mummy));
                case EnemyType.Skeleton:
                    return _factories.Values.First(factory => factory.EnemyUnitPrefab.GetType() == typeof(Skeleton));
            }

            throw new MissingReferenceException($"Factory does not excist with enemy unit type: {enemyType}");
        }

        private void SetRandomSpawnPosition(EnemyUnit unit) 
        {
            Vector3 randomSpawnPosition = _cameraMovement.GetRandomPositionOutsideCamera();
            unit.transform.position = randomSpawnPosition;
        }

        private void OnEnemyUnitRelease(EnemyUnit unit)
        {
            unit.OnNeedRelease -= OnEnemyUnitRelease;

            if (_enemies.Contains(unit) == false)
                throw new MissingReferenceException("Something gone wrong");

            _enemies.Remove(unit);
        }

        public enum EnemyType
        {
            Zombie,
            Mummy,
            Skeleton,
        }
    }
}
