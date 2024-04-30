namespace Modules.Unit.Enemy.Units
{
    internal class SkeletonView : EnemyUnitView
    {
        private const string Dead = nameof(Dead);
        private const string MoveSpeed = nameof(MoveSpeed);

        private void Update()
        {
            Animator.SetFloat(MoveSpeed, UnitSpeed.CurrentSpeed / UnitSpeed.MaxSpeed);
        }

        public override void UpdateView() { }

        protected override void Health_OnDied() => Animator.SetTrigger(Dead);

        protected override void Attacker_OnAttackStarted()
        {
        }

        protected override void Attacker_OnAttackEnded()
        {
        }
    }
}
