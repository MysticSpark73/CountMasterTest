using System;
using CountMasters.Game.Level;
using CountMasters.Pooling;
using UnityEngine;

namespace CountMasters.Game.Crowd.Mob
{
    public class Mob : MonoBehaviour, IPoollable
    {
        public ObjectPool pooledKey { get; set; }
        public CrowdType CrowdType => _rendererController.GetCrowdType();
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;

        private MobAnimator _mobAnimator;
        private MobRendererController _rendererController;

        private readonly float _fallTime = 1f;
        private readonly float _maxDistance = 5f;

        public void Init(params object[] args)
        {
            _mobAnimator = new MobAnimator(_animator);
            _rendererController = new MobRendererController(_renderer);
        }
        
        public void OnSpawnedFromPool()
        {
            GameStateEvents.GameStateChanged += OnGameStateChanged;
        }

        public void OnReturnToPool()
        {
            GameStateEvents.GameStateChanged -= OnGameStateChanged;
            _rigidbody.isKinematic = true;
        }

        public void SetPosition(Vector3 pos, Transform container = null)
        {
            transform.localPosition = pos;
            if (container != null)
            {
                transform.SetParent(container);
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            _mobAnimator.SetActive(value);
        }

        public void SetCrowdType(CrowdType crowdType)
        {
            _rendererController.SetCrowdType(crowdType);
            tag = crowdType.ToString();
        }

        public void SetIsRun()
        {
            _mobAnimator.SetAnimation(GameStateManager.GameState == GameState.Playing && CrowdType == CrowdType.PlayerCrowd
                ? MobAnimator.MobAnimation.Run
                : MobAnimator.MobAnimation.Idle);
        }

        public bool IsBeyondMaxDistance(Vector3 target)
        {
            return Vector3.Distance(transform.position, target) > _maxDistance;
        }

        public async void Fall()
        {
            _rigidbody.isKinematic = false;
            await new WaitForSeconds(_fallTime);
            Kill();
        }

        public void Kill()
        {
            if (gameObject.activeSelf == false) return;
            SetActive(false);
            LevelEvents.MobDied?.Invoke(this);
        }

        private void OnGameStateChanged(GameState state)
        {
            _mobAnimator.SetAnimation(state == GameState.Playing && CrowdType == CrowdType.PlayerCrowd
                ? MobAnimator.MobAnimation.Run
                : MobAnimator.MobAnimation.Idle);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_rendererController.CrowdType.ToString()))
            {
                Mob opponent = other.GetComponent<Mob>();
                if (opponent == null) return;
                opponent.Kill();
                Kill();
            }
        }
    }
}