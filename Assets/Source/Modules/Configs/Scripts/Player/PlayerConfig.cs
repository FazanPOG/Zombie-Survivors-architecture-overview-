using UnityEngine;

namespace Modules.Configs.Scripts
{
    [CreateAssetMenu(menuName = "Configs/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField, Range(1, 30)] private float _maxMoveSpeed;
        [SerializeField, Range(0.1f, 5f)] private float _accelerationDuration;
        [Header("Stats")]
        [SerializeField, Range(1, 500)] private int _maxHealth;
        [SerializeField, Range(1, 500)] private int _maxStamina;
        [SerializeField, Range(0.1f, 3f)] private float _statBarFillingTime;
        [SerializeField, Range(0.1f, 5f)] private float _attackRange;
        [SerializeField, Range(0.1f, 5f)] private float _attackDelay;
        [SerializeField, Range(1, 50)] private int _attackDamage;

        public float MaxMoveSpeed => _maxMoveSpeed;
        public float AccelerationDuration => _accelerationDuration;
        public int MaxHealth => _maxHealth;
        public int MaxStamina => _maxStamina;
        public float StatBarFillingTime => _statBarFillingTime;
        public float AttackRange => _attackRange;
        public float AttackDelay => _attackDelay;
        public int AttackDamage => _attackDamage;
    }
}