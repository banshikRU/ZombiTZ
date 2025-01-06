using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SFXSystem
{
    public class SfxPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSourcePrefab;
        [SerializeField]
        private List<UsableSFX> _usableSfx;

        private Queue<AudioSource> _audioSources;
        private SfxEventCatcher _sfxEventCatcher;

        [Inject]
        public void Construct(SfxEventCatcher sfxEventCatcher)
        {
            _sfxEventCatcher = sfxEventCatcher;
            EventInit();
            Initialize();
        }

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

        private void EventInit()
        {
            _sfxEventCatcher.OnsSFXRequested += PlaySfx;
        }

        public void OnDisable()
        {
            _sfxEventCatcher.OnsSFXRequested -= PlaySfx;
        }

        private void PlaySfx(SFXType sfxtype)
        {
            if (_audioSources.Count > 0)
            {
                float randomPitch = Random.Range(0.95f, 0.98f);
                var audioSource = _audioSources.Dequeue();
                audioSource.pitch = randomPitch;
                audioSource.clip = GetSFX(sfxtype).AudioClip;
                audioSource.Play();
                StartCoroutine(ReturnToPool(audioSource));
            }

        }

        private IEnumerator ReturnToPool(AudioSource audioSource)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            _audioSources.Enqueue(audioSource);
        }

        private UsableSFX GetSFX(SFXType sfxType)
        {
            foreach (UsableSFX clip in _usableSfx)
            {
                if (clip.SFXType == sfxType)
                    return clip;
            }
            return null;
        }
    }
}

