using Modules.Player.Scripts;
using Modules.Unit.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Modules.Unit.Enemy.Units
{
    [RequireComponent(typeof(Collider))]
    public abstract class EnemyUnit : BaseUnit
    {
        protected IDamagable Health { get; private set; }
        protected IUnitMovement Movement { get; private set; }
        protected IUnitAttacker Attacker { get; private set; }
        protected EnemyUnitView View { get; private set; }
        protected PlayerAttacker PlayerAttacker { get; private set; }

        public event Action<EnemyUnit> OnNeedRelease;

        public void Init(IDamagable health, 
            IUnitMovement movement, IUnitAttacker attacker, PlayerAttacker playerAttacker)
        {
            Health = health;
            Movement = movement;
            Attacker = attacker;
            PlayerAttacker = playerAttacker;

            View = (EnemyUnitView)BaseView;

            View.Init(movement, Health, attacker);

            Health.OnDied += DestroySelf;
        }

        private void DestroySelf()
        {
            StartCoroutine(DestroySelfWithDeathAnimationDelay(3f));
        }

        private void Update()
        {
            Movement?.MoveToTarget();

            if (Attacker != null)
            {
                if (Attacker.CanAttack()) 
                {
                    Attacker.Attack();
                    StartCoroutine(Attacker.AttackDelay());
                }
                
            }

            if (Vector3.Distance(transform.position, PlayerAttacker.transform.position) < PlayerAttacker?.GetAttackDistance()) 
                PlayerAttacker?.EnemyUnitOnAttackRange(Health, transform);
        }

        private IEnumerator DestroySelfWithDeathAnimationDelay(float delay) 
        {
            yield return new WaitForSeconds(delay);
            OnNeedRelease?.Invoke(this);
        }
    }
}
