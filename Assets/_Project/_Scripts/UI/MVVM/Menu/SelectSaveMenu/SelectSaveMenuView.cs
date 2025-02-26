using System;
using System.Globalization;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIControl.MVVM
{
    public class SelectSaveMenuView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _localSaveLastSaveTime;
        [SerializeField] 
        private TextMeshProUGUI _cloudSaveLastSaveTime;
        [SerializeField] 
        private Button _localSaveButton;
        [SerializeField] 
        private Button _cloudSaveButton;

        private SelectSaveMenuViewModel _selectSaveMenuViewModel;

        [Inject]
        public void Construct(SelectSaveMenuViewModel selectSaveMenuViewModel)
        {
            _selectSaveMenuViewModel = selectSaveMenuViewModel;
        }

        private void Awake()
        {
            SubscribeEvents();
            AddListenerForButtons();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _selectSaveMenuViewModel.LastCloudSaveTime.Subscribe(OnCloudSaveLastSaveTimeChanged);
            _selectSaveMenuViewModel.LastLocalSaveTime.Subscribe(OnLocalSaveLastSaveTimeChanged);
            _selectSaveMenuViewModel.IsCloudSaveButtonActive.Subscribe(OnCloudSaveButtonStateChanged);
            _selectSaveMenuViewModel.IsLocalSaveButtonActive.Subscribe(OnLocalSaveButtonStateChanged);
        }

        private void UnsubscribeEvents()
        {
            _selectSaveMenuViewModel.LastCloudSaveTime.Dispose();
            _selectSaveMenuViewModel.LastLocalSaveTime.Dispose();
            _selectSaveMenuViewModel.IsCloudSaveButtonActive.Dispose();
            _selectSaveMenuViewModel.IsLocalSaveButtonActive.Dispose();
        }

        private void OnLocalSaveLastSaveTimeChanged(DateTime value)
        {
            _localSaveLastSaveTime.text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void OnCloudSaveLastSaveTimeChanged(DateTime value)
        {
            _cloudSaveLastSaveTime.text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void OnLocalSaveButtonStateChanged(bool value)
        {
            _localSaveButton.interactable = value;
        }

        private void OnCloudSaveButtonStateChanged(bool value)
        {
            _cloudSaveButton.interactable = value;
        }

        private void AddListenerForButtons()
        {
            _localSaveButton.onClick.AddListener(_selectSaveMenuViewModel.LoadLocalSave);
            _cloudSaveButton.onClick.AddListener(_selectSaveMenuViewModel.LoadCloudSave);
        }
    }
}