using UnityEngine;

namespace Modules.Configs.Scripts
{
    [CreateAssetMenu(menuName = "Configs/EnemyUnitConfig")]
    public class EnemyUnitConfig : ScriptableObject
    {
        [Header("Health")]
        [SerializeField, Range(1, 1000)] private int _maxHealth;
        [Header("Attacker")]
        [SerializeField, Range(1, 200)] private int _defaultDamage;
        [SerializeField, Range(0.5f, 5f)] private float _attackRange;
        [SerializeField, Range(0.5f, 5f)] private float _attackDelay;
        [Header("Movement")]
        [SerializeField, Range(1, 30)] private float _moveSpeed;
        [SerializeField, Range(0.1f, 5f)] private float _acceleretionDuration;

        public int Health => _maxHealth;
        public int DefaultDamage => _defaultDamage;
        public float AttackRange => _attackRange;
        public float AttackDelay => _attackDelay;
        public float MoveSpeed => _moveSpeed;
        public float AcceleretionDuration => _acceleretionDuration;
    }
}
