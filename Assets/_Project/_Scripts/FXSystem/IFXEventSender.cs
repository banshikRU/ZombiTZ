using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project._Scripts.FXSystem
{
    public interface IFXEventSender
    {
        public event Action<FXType,Vector3> OnFXEvent;
    }
}