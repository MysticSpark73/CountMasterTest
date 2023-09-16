using CountMasters.Core;
using TMPro;
using UnityEngine.UI;

namespace CountMasters.Game.Crowd
{
    public class CrowdUIController : IInitable
    {
        private Image _cloudImage, _arrowImage;
        private TextMeshProUGUI _countLabel;
        
        public CrowdUIController(Image cloudImage, Image arrowImage, TextMeshProUGUI countLabel)
        {
            _cloudImage = cloudImage;
            _arrowImage = arrowImage;
            _countLabel = countLabel;
        }

        public void Init(params object[] args)
        {
            
        }

        public void UpdateCountText(int count)
        {
            _countLabel.text = count.ToString();
        }

        public void SetType(CrowdType crowdType)
        {
            _cloudImage.color = Parameters.GetColorByCrowdType(crowdType);
            _arrowImage.color = Parameters.GetColorByCrowdType(crowdType);
        }
    }
}