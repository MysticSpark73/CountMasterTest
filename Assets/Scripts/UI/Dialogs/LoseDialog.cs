using CountMasters.Game.Level;
using UnityEngine;
using UnityEngine.UI;

namespace CountMasters.UI.Dialogs
{
    public class LoseDialog : Dialog
    {
        [SerializeField] private Button _retryButton;
        

        protected override void OnShow()
        {
            _retryButton.onClick.RemoveAllListeners();
            _retryButton.onClick.AddListener(Retry);
        }

        protected override void ResetDialog()
        {
            
        }

        private async void Retry()
        {
            LevelEvents.RestartRequested?.Invoke();
            await Hide();
        }
    }
}