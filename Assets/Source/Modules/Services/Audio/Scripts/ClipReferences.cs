using UnityEngine;

namespace Modules.Services.Scripts
{
    [CreateAssetMenu(menuName = "Configs/Audio/ClipReferences")]
    public class ClipReferences : ScriptableObject
    {
        [Header("Enemy")]
        [SerializeField] private AudioClip _zombieSound;
        [SerializeField] private AudioClip _skeletonSound;
        [SerializeField] private AudioClip _mummySound;
        [SerializeField] private AudioClip _zombieDeadSound;
        [SerializeField] private AudioClip _skeletonDeadSound;
        [SerializeField] private AudioClip _mummyDeadSound;
        [Header("Player")]
        [SerializeField] private AudioClip _playerShootSound;
        [SerializeField] private AudioClip _playerStepSound;
        [SerializeField] private AudioClip _playerDeadSound;

        public AudioClip ZombieSound => _zombieSound;
        public AudioClip SkeletonSound => _skeletonSound;
        public AudioClip MummySound => _mummySound;
        public AudioClip ZombieDeadSound => _zombieDeadSound;
        public AudioClip SkeletonDeadSound => _skeletonDeadSound;
        public AudioClip MummyDeadSound => _mummyDeadSound;
        public AudioClip PlayerShootSound => _playerShootSound;
        public AudioClip PlayerStepSound => _playerStepSound;
        public AudioClip PlayerDeadSound => _playerDeadSound;
    }
}
