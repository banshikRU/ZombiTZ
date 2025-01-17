using System;
using UnityEngine;

namespace _Project._Scripts.FXSystem
{
    [Serializable]
    public class FXBehaviour
    {
        public GameObject VfxPrefab;
        public AudioClip SfxSound;
        public FXType FXType;
    }
}