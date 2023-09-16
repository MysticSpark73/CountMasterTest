using System;
using System.Collections.Generic;
using System.Linq;
using CountMasters.Game;
using UnityEngine;

namespace CountMasters.UI.Dialogs
{
    public class DialogsManager : MonoBehaviour
    {
        [SerializeField] private List<Dialog> _dialogs;

        public async void ShowDialog<T>(bool animate = true, Action callback = null) where T : Dialog
        {
            var dialog = _dialogs.FirstOrDefault(d => d is T);
            if (dialog == null) return;
            await dialog.Show(animate, callback);
        }
        
        public async void HideDialog<T>(bool animate = true, Action callback = null) where T : Dialog
        {
            var dialog = _dialogs.FirstOrDefault(d => d is T);
            if (dialog == null) return;
            await dialog.Hide(animate, callback);
        }

        public bool IsDialogShowing<T>()
        {
            return false;
        }

        public T GetDialog<T>() where T : Dialog
        {
            return _dialogs.FirstOrDefault(d => d is T) as T;
        }
            
        private void Awake()
        {
            GameStateEvents.GameStateChanged += OnGameStateChanged;
        }

        private void OnApplicationQuit()
        {
            GameStateEvents.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            
        }
    }
}