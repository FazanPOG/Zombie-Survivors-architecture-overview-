using System;

namespace Modules.Unit.Interfaces
{
    public interface IDamagable
    {
        bool CanTakeDamage { get; }

        void TakeDamage(int damage);

        event Action<int> OnHealthChanged;

        event Action OnDied;
    }
}