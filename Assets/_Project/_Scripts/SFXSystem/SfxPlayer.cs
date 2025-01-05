using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SfxPlayer : IDisposable
{
    private readonly List<UsableSFX> _usableSfx;
    private readonly AudioSource _backGroundSound;
    private readonly AudioSource _sfxSource_1;
    private readonly AudioSource _sfxSource_2;
    private readonly SfxEventCatcher _sfxEventCatcher;

    public SfxPlayer (AudioSource backGroundSound, AudioSource sfxSource_1, AudioSource sfxSource_2,List<UsableSFX> usableSfx,SfxEventCatcher sfxEventCatcher)
    {
        _sfxEventCatcher = sfxEventCatcher;
        _usableSfx = usableSfx;
        _backGroundSound = backGroundSound;
        _sfxSource_1 = sfxSource_1;
        _sfxSource_2 = sfxSource_2;
        EventInit();
    }

    private void EventInit()
    {
        _sfxEventCatcher.OnsSFXRequested += ChoseFreeSource;
    }

    public void Dispose()
    {
        _sfxEventCatcher.OnsSFXRequested -= ChoseFreeSource;
    }

    private void ChoseFreeSource(SFXType sfxtype)
    {
        if (_sfxSource_1.isPlaying)
        {
            PlaySfx(_sfxSource_2,sfxtype);
        }
        else
        {
            PlaySfx(_sfxSource_1, sfxtype);
        }
    }

    private void PlaySfx(AudioSource audioSource,SFXType sfxtype)
    {
        float randomPitch = Random.Range(0.95f, 0.98f);
        audioSource.pitch = randomPitch;
        audioSource.clip = GetSFX(sfxtype).AudioClip;
        audioSource.Play();
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
