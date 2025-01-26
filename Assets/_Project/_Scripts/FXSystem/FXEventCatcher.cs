using System;
using System.Collections.Generic;
using UnityEngine;
using _Project._Scripts.FXSystem;
using GameStateControl;
using SfxSystem;
using Unity.Mathematics;
using VFXSystem;
using Zenject;

public class FXEventCatcher : IInitializable,IDisposable
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
        
    }
    
    public void Initialize()
    {
        EventInit();
    }
    
    public void Dispose()
    {
        UnsubscribeEvents();
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
