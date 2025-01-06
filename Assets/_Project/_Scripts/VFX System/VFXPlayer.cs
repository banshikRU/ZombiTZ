using System.Collections.Generic;
using UnityEngine;

namespace SFXSystem
{
    public class VFXPlayer
    {
        private VFXTypes _myVfxType;

        public VFXPlayer(VFXTypes myVfxType)
        {
            _myVfxType = myVfxType;
        }

        public void Play(Queue<AudioSource> stayingAudioSources, Queue<AudioSource> playingAudioSources,AudioClip myAudioSource)
        {
            if (stayingAudioSources.Count > 0)
            {
                float randomPitch = Random.Range(0.95f, 0.98f);
                var audioSource = stayingAudioSources.Dequeue();
                playingAudioSources.Enqueue(audioSource);
                audioSource.pitch = randomPitch;
                audioSource.clip = myAudioSource;
                audioSource.Play();
            }
        }
    }
}
