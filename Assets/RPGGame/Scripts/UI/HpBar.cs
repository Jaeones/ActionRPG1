using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace RPGGame
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image hpBar;
        [SerializeField] private TextMeshProUGUI hpGaugeText;

        public void OnDamageReceived(float currentHp, float maxHp)
        {
            if (hpBar != null)
            {
                hpBar.fillAmount = currentHp / maxHp;
            }

            if (hpGaugeText != null)
            {
                hpGaugeText.text = $"{currentHp}/{maxHp}";
            }
        }
    }

}
