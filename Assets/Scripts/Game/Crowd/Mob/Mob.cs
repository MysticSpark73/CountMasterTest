﻿using CountMasters.Pooling;
using UnityEngine;

namespace CountMasters.Game.Crowd.Mob
{
    public class Mob : MonoBehaviour, IPoollable
    {
        public ObjectPool pooledKey { get; set; }
        public CrowdType CrowdType => _rendererController.GetCrowdType();
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Renderer _renderer;

        private MobAnimator _mobAnimator;
        private MobRendererController _rendererController;

        public void Init()
        {
            _mobAnimator = new MobAnimator(_animator);
            _rendererController = new MobRendererController(_renderer);
        }
        
        public void OnSpawnedFromPooled()
        {
            GameStateEvents.GameStateChanged += OnGameStateChanged;
        }

        public void OnReturnToPool()
        {
            GameStateEvents.GameStateChanged -= OnGameStateChanged;
        }

        public void SetPosition(Vector3 pos, Transform container = null)
        {
            transform.position = pos;
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
        }

        private void OnGameStateChanged(GameState state)
        {
            _mobAnimator.SetAnimation(state == GameState.Playing && CrowdType == CrowdType.Player
                ? MobAnimator.MobAnimation.Run
                : MobAnimator.MobAnimation.Idle);
        }
    }
}