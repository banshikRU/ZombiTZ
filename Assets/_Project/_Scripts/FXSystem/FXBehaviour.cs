using System;
using UnityEngine;

namespace FXSystem
{
    [Serializable]
    public class FXBehaviour
    {
        public GameObject VfxPrefab;
        public AudioClip SfxSound;
        public FXType FXType;
    }
}