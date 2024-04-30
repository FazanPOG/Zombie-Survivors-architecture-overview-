using UnityEngine;

namespace Modules.Services.Scripts
{
    public class AudioService : IAudioService
    {
        private ClipReferences _clipreferences;
        private AudioSource _mainAudioSource;

        public ClipReferences ClipReferences => _clipreferences;

        public AudioService(ClipReferences clipreferences, AudioSource mainAudioSource) 
        {
            _clipreferences = clipreferences;
            _mainAudioSource = mainAudioSource;
        }

        public void PlaySound(AudioClip clip)
        {
            _mainAudioSource.PlayOneShot(clip);
        }
    }
}