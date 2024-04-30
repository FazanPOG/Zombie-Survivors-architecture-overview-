namespace Modules.Unit.Enemy.Units
{
    public class ZombieView : EnemyUnitView
    {
        private const string Dead = nameof(Dead);
        private const string MoveSpeed = nameof(MoveSpeed);
        private const string IsAttacking = nameof(IsAttacking);

        private void Update()
        {
            Animator.SetFloat(MoveSpeed, UnitSpeed.CurrentSpeed / UnitSpeed.MaxSpeed);
        }

        public override void UpdateView(){}

        protected override void Health_OnDied() => Animator.SetTrigger(Dead);

        protected override void Attacker_OnAttackStarted() => Animator.SetBool(IsAttacking, true);

        protected override void Attacker_OnAttackEnded() => Animator.SetBool(IsAttacking, false);
    }
}
