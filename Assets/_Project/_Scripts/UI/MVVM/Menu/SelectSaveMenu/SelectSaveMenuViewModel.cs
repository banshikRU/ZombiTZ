using System;
using SaveSystem;
using UniRx;
using Zenject;

namespace UIControl.MVVM
{
    public class SelectSaveMenuViewModel: IInitializable
    {
        private readonly SaveGameController _saveGameController;
        private readonly AllGameMenusViewModel _allGameMenusViewModel;
        
        public readonly ReactiveProperty<DateTime> LastLocalSaveTime = new();
        public readonly ReactiveProperty<DateTime> LastCloudSaveTime = new();
        public readonly ReactiveProperty<bool> IsLocalSaveButtonActive = new(); 
        public readonly ReactiveProperty<bool> IsCloudSaveButtonActive = new();

        public SelectSaveMenuViewModel(SaveGameController saveGameController, AllGameMenusViewModel allGameMenusViewModel)
        {
            _saveGameController = saveGameController;
            _allGameMenusViewModel = allGameMenusViewModel;
        }
        
        public void Initialize()
        {
            InitSelectSaveButtons();
        }

        public void LoadLocalSave()
        {
            _saveGameController.SetUpLocalSave();
            _allGameMenusViewModel.SaveSelected();
        }

        public void LoadCloudSave()
        {
            _saveGameController.SetUpCloudSave();
            _allGameMenusViewModel.SaveSelected();
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