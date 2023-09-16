using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CountMasters.UI.Dialogs
{
    public abstract class Dialog : MonoBehaviour
    {
        protected float _animationTime = .5f;
        public bool IsShowing { get; protected set; }

        public virtual async Task Show(bool animate = true, Action callback = null)
        {
            IsShowing = true;
            bool isComplete = false;
            DOTween.Kill(this);
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0,transform.DOScale(Vector3.one, animate ? _animationTime : 0).From(Vector3.zero).SetEase(Ease.OutBack));
            sequence.OnStart(() => { gameObject.SetActive(true); });
            sequence.OnComplete(() => { isComplete = true; });
            sequence.Play();
            await new WaitUntil(() => isComplete);
            callback?.Invoke();
            OnShow();
        }

        public virtual async Task Hide(bool animate = true, Action callback = null)
        {
            OnHide();
            IsShowing = false;
            bool isComplete = false;
            DOTween.Kill(this);
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0,transform.DOScale(Vector3.zero, animate ? _animationTime : 0).SetEase(Ease.InBack));
            sequence.OnComplete(() => { isComplete = true; });
            sequence.Play();
            await new WaitUntil(() => isComplete);
            gameObject.SetActive(false);
            transform.localScale = Vector3.one;
            callback?.Invoke();
        }

        protected virtual void OnShow() { }
        
        protected virtual void OnHide() { }

        public virtual async Task Init(bool animate = true, Action callback = null) 
        {
            await Show(animate, callback);
        }

        protected abstract void ResetDialog();
        
    }
}