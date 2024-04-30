namespace Modules.Unit.Enemy.Units
{
    public class MummyView : EnemyUnitView
    {
        private const string Dead = nameof(Dead);

        public override void UpdateView()
        {
        }

        protected override void Attacker_OnAttackEnded()
        {

        }
        

        protected override void Attacker_OnAttackStarted()
        {

        }

        protected override void Health_OnDied() => Animator.SetTrigger(Dead);
    }
}
