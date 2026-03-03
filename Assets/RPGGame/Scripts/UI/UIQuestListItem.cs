using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace RPGGame
{
    public class UIQuestListItem : MonoBehaviour
    {
        [SerializeField] private Image questStatusImage;
        [SerializeField] private Color closeColor;
        [SerializeField] private Color progressColor;
        [SerializeField] private Color completeColor;
        [SerializeField] private TextMeshProUGUI questStatusText;

        private readonly string closeText = "´ÝÈû";
        private readonly string progressText = "ÁøÇà Áß";
        private readonly string completeText = "¿Ï·á";

        [SerializeField] private TextMeshProUGUI questTitleText;

        [SerializeField] private TextMeshProUGUI questCountText;

        private int completeCount = 0;

        public void SetClosed()
        {
            questStatusImage.color = closeColor;
            questStatusText.text = closeText;
        }

        public void SetProgress()
        {
            questStatusImage.color = progressColor;
            questStatusText.text = progressText;
        }

        public void SetComplete()
        {
            questStatusImage.color = completeColor;
            questStatusText.text = completeText;

            SetQuestCount(completeCount);
        }

        public void SetQuestTitle(string questTitle)
        {
            questTitleText.text = questTitle;
        }

        public void SetQuestCount(int currentCompleteCount)
        {
            questCountText.text = $"{currentCompleteCount} / {completeCount}";
        }

        public void SetQuestCount(int currentCompleteCount, int countsToCompleteQuest)
        {
            completeCount = countsToCompleteQuest;
            questCountText.text = $"{currentCompleteCount} / {completeCount}";
        }
    }

}
