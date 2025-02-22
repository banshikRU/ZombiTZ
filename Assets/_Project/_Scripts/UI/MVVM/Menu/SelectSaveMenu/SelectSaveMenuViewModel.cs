using System;
using UniRx;
using Zenject;

namespace UIControl.SelectSaveMenu
{
    public class SelectSaveMenuViewModel: IInitializable
    {
        public readonly ReactiveProperty<DateTime> LastLocalSaveTime = new();
        public readonly ReactiveProperty<DateTime> LastCloudSaveTime = new();
        public readonly ReactiveProperty<bool> IsLocalSaveButtonActive = new(); 
        public readonly ReactiveProperty<bool> IsCloudSaveButtonActive = new();
        
        private readonly SaveGameController _saveGameController;
        private readonly UIViewModel _uiViewModel;

        public SelectSaveMenuViewModel(SaveGameController saveGameController, UIViewModel uiViewModel)
        {
            _saveGameController = saveGameController;
            _uiViewModel = uiViewModel;
        }
        
        public void Initialize()
        {
            InitSelectSaveButtons();
        }

        public void LoadLocalSave()
        {
            _saveGameController.SetUpLocalSave();
            _uiViewModel.SaveSelected();
        }

        public void LoadCloudSave()
        {
            _saveGameController.SetUpCloudSave();
            _uiViewModel.SaveSelected();
        }

        private void InitSelectSaveButtons()
        {
            if (_saveGameController.CloudPlayerData!= null)
            { 
                LastCloudSaveTime.Value = _saveGameController.CloudPlayerData.SaveTime;
                IsCloudSaveButtonActive.Value = true;
            }
            if (_saveGameController.LocalPlayerData != null)
            {
                LastLocalSaveTime.Value = _saveGameController.LocalPlayerData.SaveTime; 
                IsLocalSaveButtonActive.Value = true;
            }
        }
        
    }
}