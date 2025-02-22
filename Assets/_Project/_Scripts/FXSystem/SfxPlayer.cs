using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace FXSystem
{
    public class SfxPlayer : MonoBehaviour,IInitializable
    {
        [SerializeField]
        private AudioSource _audioSourcePrefab;
        [SerializeField]
        private float _minimalRandomPitch = 0.96f;
        [SerializeField]
        private float _maximumRandomPitch = 0.99f;

        private Queue<AudioSource> _audioSources;

        public void Initialize()
        {
            _audioSources = new Queue<AudioSource>();

            for (int i = 0; i < 30; i++)
            {
                var audioSource = Instantiate(_audioSourcePrefab);
                audioSource.transform.parent = transform;
                _audioSources.Enqueue(audioSource);
            }
        }

        public void PlaySfx(AudioClip clip)
        {
            if (_audioSources.Count <= 0) 
                return;
            float randomPitch = Random.Range(_minimalRandomPitch,_maximumRandomPitch);
            var audioSource = _audioSources.Dequeue();
            audioSource.pitch = randomPitch;
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(ReturnToPool(audioSource));

        }

        private IEnumerator ReturnToPool(AudioSource audioSource)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            _audioSources.Enqueue(audioSource);
        }
    }
}

