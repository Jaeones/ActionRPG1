using System.Collections.Generic;
using UnityEngine;


namespace RPGGame
{
    public class UIQuestWindow : MonoBehaviour
    {
        private static UIQuestWindow instance;

        [SerializeField] private GameObject window;
        [SerializeField] private RectTransform contentTransform;

        [SerializeField] private UIQuestListItem itemPrefab;

        [SerializeField] private float itemHeight = 60f;

        [SerializeField] private List<UIQuestListItem> items = new List<UIQuestListItem>();

        public static bool IsOn { get { return instance != null && instance.window != null && instance.window.activeSelf; } }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            Initialize();
        }

        private void Initialize()
        {
            QuestManager.Instance.SubscribeOnQuestStarted(OnQuestStarted);
            QuestManager.Instance.SubscribeOnQuestCompleted(OnQuestCompleted);
            QuestManager.Instance.SubscribeOnQuestCompleteCountChanged(OnQuestCompleteCountChanged);

            for (int ix = 0; ix < DataManager.Instance.QuestData.quests.Count; ++ix)
            {
                QuestData.Quest quest = DataManager.Instance.QuestData.quests[ix];
                UIQuestListItem newItem = Instantiate(itemPrefab, contentTransform);

                newItem.SetClosed();
                newItem.SetQuestTitle(quest.questTitle);
                newItem.SetQuestCount(0, quest.countToComplete);

                items.Add(newItem);
            }

            Vector2 size = contentTransform.sizeDelta;
            size.y = (DataManager.Instance.QuestData.quests.Count - 1) * itemHeight;
            contentTransform.sizeDelta = size;
        }

        public static void Show()
        {
            if (instance == null || instance.window == null) return;
            instance.window.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public static void Close()
        {
            if (instance == null || instance.window == null) return;
            instance.window.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnQuestCompleteCountChanged(int questID, int completeCount)
        {
            items[questID].SetQuestCount(completeCount);
        }

        private void OnQuestCompleted(int questID)
        {
            items[questID].SetComplete();
        }

        private void OnQuestStarted(int questID)
        {
            items[questID].SetProgress();
        }
    }

}
