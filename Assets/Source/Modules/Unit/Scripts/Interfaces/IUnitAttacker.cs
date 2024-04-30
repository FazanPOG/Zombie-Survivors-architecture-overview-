using System;
using System.Collections;

namespace Modules.Unit.Interfaces
{
    public interface IUnitAttacker
    {
        bool CanAttack();
        void Attack();
        IEnumerator AttackDelay();

        event Action OnAttackStarted;
        event Action OnAttackEnded;
    }
}
