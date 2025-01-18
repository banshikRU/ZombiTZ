using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using _Project._Scripts.FXSystem;
using GameStateControl;
using SfxSystem;
using Unity.Mathematics;
using VFXSystem;
using WeaponControl;
using ZombieGeneratorBehaviour;

public class FXEventCatcher 
{
    private readonly GameSettings _gameSettings;
    private readonly SfxPlayer _sfxPlayer;
    private readonly VFXGenerator _vfxGenerator;
    
    private readonly List<IFXEventSender> _fxEventSenders;
    
    public FXEventCatcher(List<IFXEventSender> fxEventSenders,VFXGenerator vfxGenerator,SfxPlayer sfxPlayer,GameSettings gameSettings)
    {
        _fxEventSenders = fxEventSenders;
        _vfxGenerator = vfxGenerator;
        _sfxPlayer = sfxPlayer;
        _gameSettings = gameSettings;
        EventInit();
    }

    private void EventInit()
    {
        foreach (IFXEventSender eventSender in _fxEventSenders)
        {
            eventSender.OnFXEvent += CatchEvents;
        }
    }
    
    private void UnsubscribeEvents()
    {
        foreach (IFXEventSender eventSender in _fxEventSenders)
        {
            eventSender.OnFXEvent -= CatchEvents;
        }
    }

    private void CatchEvents(FXType fxType,Vector3 position)
    {

        FXBehaviour currentFXBehaviour  = GetFXBehaviour(fxType);
        _vfxGenerator.PlayVFX(currentFXBehaviour.VfxPrefab, position,quaternion.identity);
        _sfxPlayer.PlaySfx(currentFXBehaviour.SfxSound);
    }

    private FXBehaviour GetFXBehaviour(FXType fxType)
    {
        foreach (FXBehaviour fxBehaviour in _gameSettings.FXPrefab)
        {
            if (fxType == fxBehaviour.FXType)
            {
                return fxBehaviour;
            }
        }

        return null;
    }
}
