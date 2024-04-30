using Modules.Configs.Scripts;
using Modules.Services.Scripts;
using Modules.Unit.Interfaces;
using System.Collections;
using UnityEngine;

namespace Modules.Player.Scripts
{
    public class PlayerAttacker : MonoBehaviour
    {
        private PlayerConfig _config;
        private IAudioService _audioService;
        private int _damage;
        private bool _canAttack = true;

        public Transform EnemyTransform { get; private set; }

        internal void Init(PlayerConfig config, IAudioService audioService) 
        {
            _config = config;
            _audioService = audioService;

            _damage = _config.AttackDamage;
        }

        public void EnemyUnitOnAttackRange(IDamagable damagable, Transform enemyTransform)
        {
            if (_canAttack && damagable.CanTakeDamage)
            {
                EnemyTransform = enemyTransform;
                damagable.TakeDamage(_damage);
                _audioService.PlaySound(_audioService.ClipReferences.PlayerShootSound);
                _canAttack = false;
                StartCoroutine(AttackDelay());
            }
        }

        public float GetAttackDistance() 
        {
            return _config.AttackRange;
        }

        private IEnumerator AttackDelay() 
        {
            yield return new WaitForSeconds(_config.AttackDelay);
            _canAttack = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 3f);
        }
    }
}
