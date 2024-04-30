using Modules.Unit.Interfaces;
using UnityEngine;

namespace Modules.Unit.Enemy.Units
{
    public abstract class EnemyUnitView : BaseView, IUnitView
    {
        protected Animator Animator {  get; private set; }
        protected IUnitSpeed UnitSpeed {  get; private set; }
        protected IDamagable Health {  get; private set; }
        protected IUnitAttacker Attacker {  get; private set; }

        internal void Init(IUnitSpeed unitSpeed, IDamagable health, IUnitAttacker attacker)
        {
            UnitSpeed = unitSpeed;
            Health = health;
            Attacker = attacker;
            Animator = GetAnimator();

            Health.OnDied += Health_OnDied;
            Attacker.OnAttackStarted += Attacker_OnAttackStarted;
            Attacker.OnAttackEnded += Attacker_OnAttackEnded;
        }
        
        public abstract void UpdateView();
        protected abstract void Attacker_OnAttackStarted();
        protected abstract void Attacker_OnAttackEnded();
        protected abstract void Health_OnDied();

        private void OnDisable()
        {
            Health.OnDied -= Health_OnDied;
            Attacker.OnAttackStarted -= Attacker_OnAttackStarted;
        }
    }
}
