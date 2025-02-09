using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UIControl.MVVM.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _currentBulletValue;

        private BulletViewModel _bulletViewModel;
        
        [Inject]
        public void Construct(BulletViewModel bulletViewModel)
        {
            _bulletViewModel = bulletViewModel;
        }

        private void Awake()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _bulletViewModel.Bullets.Subscribe(DisplayBullet)
                .AddTo(this);
        }
        
        private void DisplayBullet(int value)
        {
            _currentBulletValue.text = value.ToString();
        }
    }
}