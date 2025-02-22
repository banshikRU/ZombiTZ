using System;
using UnityEngine;

namespace FXSystem
{
    public interface IFXEventSender
    {
        public event Action<Vector3,IFXEventSender> OnFXEvent;
    }
}