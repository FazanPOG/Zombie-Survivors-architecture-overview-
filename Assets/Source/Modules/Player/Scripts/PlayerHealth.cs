using Modules.Configs.Scripts;
using Modules.Unit.Interfaces;
using System;
using UnityEngine;

namespace Modules.Player.Scripts
{
    public class PlayerHealth : MonoBehaviour, IDamagable
    {
        private int _maxHealth;
        private int _currentHealth;

        public bool CanTakeDamage => _currentHealth > 0;

        public event Action<int> OnHealthChanged;
        public event Action OnDied;

        internal void Init(PlayerConfig playerConfig)
        {
            _maxHealth = playerConfig.MaxHealth;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage must be more than 0");

            _currentHealth -= damage;
            OnHealthChanged?.Invoke(_currentHealth);

            if (_currentHealth <= 0)
                OnDied?.Invoke();
        }
    }
}
