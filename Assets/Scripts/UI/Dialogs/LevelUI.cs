using CountMasters.Game;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CountMasters.UI.Dialogs
{
    public class LevelUI : Dialog
    {
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _tapToStartLabel;
        [SerializeField] private Button _tapDetector;
        [SerializeField] private Transform _tapToStartTransform;

        private float _flashingTime = 5f;

        public void ShowTapToContinue()
        {
            _tapToStartTransform.gameObject.SetActive(true);
        }

        protected override void OnShow()
        {
            Parameters.CoinsAmountChanged += OnCoinsAmountChanged;
            _coinsLabel.text = Parameters.Coins.ToString();
            _tapToStartLabel.DOFade(0, .5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            _tapDetector.onClick.RemoveAllListeners();
            _tapDetector.onClick.AddListener(OnTap);
            ShowTapToContinue();
        }

        protected override void OnHide()
        {
            Parameters.CoinsAmountChanged += OnCoinsAmountChanged;
        }

        protected override void ResetDialog()
        {
            
        }

        private void OnCoinsAmountChanged(int value)
        {
            _coinsLabel.text = value.ToString();
        }

        private void OnTap()
        {
            _tapToStartTransform.gameObject.SetActive(false);
            GameStateManager.SetGameState(GameState.Playing);
        }
    }
}