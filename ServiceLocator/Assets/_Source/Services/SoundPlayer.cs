using UnityEngine;

namespace Services
{
    public class SoundPlayer : ISoundPlayer
    {
        private readonly AudioSource _audioSource;
        private AudioClip _openClip;
        private AudioClip _closeClip;

        public SoundPlayer(AudioSource audioSource, AudioClip openClip, AudioClip closeClip)
        {
            _audioSource = audioSource;
            _openClip = openClip;
            _closeClip = closeClip;
        }

        public void PlayOpenSound()
        {
            if (_openClip != null)
                _audioSource.PlayOneShot(_openClip);
        }

        public void PlayCloseSound()
        {
            if (_closeClip != null)
                _audioSource.PlayOneShot(_closeClip);
        }

        public void SetOpenClip(AudioClip clip)
        {
            _openClip = clip;
        }

        public void SetCloseClip(AudioClip clip)
        {
            _closeClip = clip;
        }
    }
}