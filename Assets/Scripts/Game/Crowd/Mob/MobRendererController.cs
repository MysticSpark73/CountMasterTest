using UnityEngine;

namespace CountMasters.Game.Crowd.Mob
{
    public class MobRendererController
    {
        public CrowdType CrowdType => _crowdType;
        
        private Renderer _renderer;
        private CrowdType _crowdType;

        public MobRendererController(Renderer renderer)
        {
            _renderer = renderer;
        }

        public void SetCrowdType(CrowdType type)
        {
            _crowdType = type;
            SetColor();
        }

        public CrowdType GetCrowdType() => _crowdType;

        private void SetColor()
        {
            var color = _renderer.materials[0].color = Parameters.GetColorByCrowdType(_crowdType);
            if (color == Color.clear)
            {
                Debug.LogError($"[MobRendererController][SetColor] There is no corresponding color to the given CrowdType {_crowdType}");
                return;
            }
            _renderer.materials[0].color = color;
        }
    }
}