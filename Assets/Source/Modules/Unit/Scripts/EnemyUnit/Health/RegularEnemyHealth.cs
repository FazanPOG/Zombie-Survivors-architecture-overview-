using Modules.Unit.Interfaces;
using System;

namespace Modules.Unit.Enemy.Scripts
{
    public class RegularEnemyHealth : IDamagable
    {
        private int _health;

        public bool CanTakeDamage => _health > 0;

        public event Action<int> OnHealthChanged;
        public event Action OnDied;

        public RegularEnemyHealth(int health)
        {
            _health = health;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage must be more than 0");

            _health -= damage;
            OnHealthChanged?.Invoke(_health);

            if (_health <= 0)
                OnDied?.Invoke();
        }
    }
}
