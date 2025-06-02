using UnityEngine;

namespace Scripts.Utils
{
    public static class SoundsPlayer
    {
        public static void PlayOneShot(this AudioSource audioSource, AudioClip sound) => audioSource.PlayOneShot(sound);
    }
}
