using Modules.Unit.Enemy.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Configs.Scripts
{
    [CreateAssetMenu(menuName = "Configs/EnemiesConfigReferences")]
    public class EnemiesData : ScriptableObject
    {
        [SerializeField] private List<EnemyConfigReference> _enemiesConfigReferences;

        public List<EnemyConfigReference> EnemiesConfigReferences => _enemiesConfigReferences;
    }

    [Serializable]
    public class EnemyConfigReference
    {
        [SerializeField] private EnemyUnit _enemyUnit;
        [SerializeField] private EnemyUnitConfig _enemyUnitConfig;

        public EnemyUnit EnemyUnit => _enemyUnit;
        public EnemyUnitConfig EnemyUnitConfig => _enemyUnitConfig;
    }
}