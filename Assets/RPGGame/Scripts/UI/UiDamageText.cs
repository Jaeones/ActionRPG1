using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace RPGGame
{
    public class UiDamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text damageText;

        [SerializeField] private float scaleMax = 1.5f;
        [SerializeField] private float scaleMin = 0.8f;

        [SerializeField] private float scaleAnimationTime = 0.5f;

        private float originalFontSize;

        private float elapsedTime = 0f;

        private void Awake()
        {
            originalFontSize = damageText.fontSize;
        }

        public void OnDamageRecived(float damage)
        {
            damageText.text = $"-{damage}";
            StartCoroutine(PlayScaleAnimation());
        }

        IEnumerator PlayScaleAnimation()
        {
            damageText.fontSize = originalFontSize * scaleMax;
            elapsedTime = 0f;

            while (elapsedTime <= scaleAnimationTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(scaleMax, scaleMin, elapsedTime / scaleAnimationTime);

                damageText.fontSize = originalFontSize * scale;
            }
            SetEmpty();
        }

        // 텍스트를 비워주는 메서드
        private void SetEmpty()
        {
            damageText.text = string.Empty;
            damageText.fontSize = originalFontSize;
        }
    }

}
