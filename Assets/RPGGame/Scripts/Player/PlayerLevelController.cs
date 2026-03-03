using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

namespace RPGGame
{
    public class PlayerLevelController : MonoBehaviour
    {
        [SerializeField] private int level = 1;
        [SerializeField] private float currentExp = 0;

        [SerializeField] private UnityEvent<int> OnLevelUp;
        [SerializeField] private TextMeshProUGUI levelGauseText;

        [SerializeField] private Image expBar;

        [SerializeField] private TextMeshProUGUI expBarText;

        private int maxLevel;
        private bool isMaxLevel { get { return level >= maxLevel; } }


        private void Awake()
        {
            maxLevel = DataManager.Instance.PlayerData.levels.Count;
            UpdateExpBar();
            UpdateLevelText();
        }

        private void UpdateLevelText()
        {
            if (levelGauseText != null)
            {
                if (isMaxLevel)
                {
                    levelGauseText.text = $"{maxLevel}/{maxLevel}";
                    return;
                }
                levelGauseText.text = $"{level}/{maxLevel}";
            }
        }

        private void UpdateExpBar()
        {
            if (isMaxLevel)
            {
                if (expBar != null)
                {
                    expBar.fillAmount = 1f;
                }
                if (expBarText != null)
                {
                    expBarText.text = "MAX";
                }
                return;
            }

            int nextLevelIndex = level;

            float requiredExpForNextLevel = DataManager.Instance.PlayerData.levels[nextLevelIndex].requiredExp;
            float requiredExpForCurrentLevel = DataManager.Instance.PlayerData.levels[level - 1].requiredExp;
            
            if (expBar != null)
            {
                float expAmount = (currentExp - requiredExpForCurrentLevel) / (requiredExpForNextLevel - requiredExpForCurrentLevel);
                expBar.fillAmount = expAmount;
            }

            if (expBarText != null)
            {
                expBarText.text = $"{currentExp - requiredExpForCurrentLevel}/{requiredExpForNextLevel - requiredExpForCurrentLevel}";
            }
        }
        public void GainExp(float exp)
        {
            currentExp += exp;
            UpdateExpBar();

            if(isMaxLevel) return;

            int oldLevelIndex = level - 1;
            int targetLevelIndex = 0;
            for (int ix = targetLevelIndex; ix < maxLevel; ix++)
            {
                if (currentExp < DataManager.Instance.PlayerData.levels[ix].requiredExp)
                {
                    targetLevelIndex = ix - 1;
                    break;
                }
            }

            if (level > 1 && currentExp > 0f && targetLevelIndex == 0)
            {
                targetLevelIndex = maxLevel - 1;
            }
            if (oldLevelIndex != targetLevelIndex)
            {
                level = targetLevelIndex + 1;
                UpdateLevelText();
                OnLevelUp?.Invoke(level);
                UpdateExpBar();
            }
        }

        public void SubscriveOnLevelUp(UnityAction<int> listener)
        {
            OnLevelUp?.AddListener(listener);
        }

    }

}
