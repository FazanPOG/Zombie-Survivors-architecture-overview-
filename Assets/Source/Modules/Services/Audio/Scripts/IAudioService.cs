using UnityEngine;

namespace Modules.Services.Scripts
{
    public interface IAudioService
    {
        ClipReferences ClipReferences { get; }
        void PlaySound(AudioClip clip);
    }
}
