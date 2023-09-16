using CountMasters.Game.Level;
using UnityEngine;
using UnityEngine.UI;

namespace CountMasters.UI.Dialogs
{
    public class WinDialog : Dialog
    {
        [SerializeField] private Button _nextButton;

        protected override void ResetDialog()
        {
            
        }

        protected override void OnShow()
        {
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(OnNextClicked);
        }

        private async void OnNextClicked()
        {
            LevelEvents.NextLevelRequested?.Invoke();
            await Hide();
        }
    }
}