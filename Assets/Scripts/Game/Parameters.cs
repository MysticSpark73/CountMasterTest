using CountMasters.Game.Crowd;
using UnityEngine;

namespace CountMasters.Game
{
    public static class Parameters
    {
        #region Colors

        public static Color color_player = new Color(0.0f, .5f, 1.0f, 1.0f);
        public static Color color_enemy = new Color(1, 0,0, 1);

        public static Color GetColorByCrowdType(CrowdType crowdType) => crowdType switch
        {
            CrowdType.Player => color_player,
            CrowdType.Enemy => color_enemy,
            _ => Color.clear
        };

        #endregion
    }
}