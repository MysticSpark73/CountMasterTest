using CountMasters.Game.Crowd;
using UnityEngine;

namespace CountMasters.Game
{
    public static class Parameters
    {
        #region Pathes

        public static readonly string path_resources_levels_data = "Data/LevelsData";

        #endregion
        #region Colors

        public static Color color_player = new Color(0.0f, .5f, 1.0f, 1.0f);
        public static Color color_enemy = new Color(.95f, .12f,.05f, 1);

        public static readonly Color color_level_pink = new Color(.8f, .3f, .95f, 1);
        public static readonly Color color_level_violet = new Color(.46f, .32f, .95f, 1);
        public static readonly Color color_level_blue = new Color(.31f, .7f, .95f, 1);
        public static readonly Color color_level_yellow = new Color(.95f, .72f, .02f, 1);

        public static readonly Color[] level_colors =
        {
            color_level_pink,
            color_level_violet,
            //color_level_blue,
            color_level_yellow
        };

        public static Color GetRandomLevelColor()
        {
            return level_colors[Random.Range(0, level_colors.Length)];
        }


        public static Color GetColorByCrowdType(CrowdType crowdType) => crowdType switch
        {
            CrowdType.Player => color_player,
            CrowdType.Enemy => color_enemy,
            _ => Color.clear
        };

        #endregion
    }
}