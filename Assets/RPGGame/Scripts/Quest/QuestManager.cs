using System;
using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{

    [DefaultExecutionOrder(-50)]
    public class QuestManager : MonoBehaviour
    {
        public enum QuestState
        {
            None = -1,
            Start,
            Processing,
            Complete,
            Length,
        }

        public static QuestManager Instance { get; private set; } = null;

        [SerializeField] private int currentQuestID = 0;
        [SerializeField] private QuestState currentQuestState = QuestState.None;
        [SerializeField] private int completeQuestCount = 0;
        [SerializeField] private bool isQuestCompleted = false;

        [SerializeField] private UnityEvent<int> OnQuestStarted;
        [SerializeField] private UnityEvent<int> OnQuestCompleted;
        [SerializeField] private UnityEvent OnAllQuestsCompleted;
        [SerializeField] private UnityEvent<int, int> OnQuestCompleteCountChanged;


        public QuestData.Quest CurrentQuest
        {
            get { return DataManager.Instance.QuestData.quests[currentQuestID]; }

        }

        public QuestData.Quest NextQuest
        {
            get
            {
                int nextQuestID = currentQuestID >= DataManager.Instance.QuestData.quests.Count - 1 ? currentQuestID : currentQuestID + 1;
                return DataManager.Instance.QuestData.quests[nextQuestID];
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            currentQuestID = 0;
            currentQuestState = QuestState.Complete; // 0번 인트로 대화 후 NPC 대화로 1번 시작
            completeQuestCount = 0;

            // 게임 시작 시 첫 퀘스트의 시작 대화 보여주기
            OnQuestCompleted?.Invoke(currentQuestID);
            OnQuestCompleteCountChanged?.Invoke(currentQuestID, completeQuestCount);

            Dialogue.Instance.ShowDialogueTextTemporarily(CurrentQuest.questBeginDialogue);
        }


        private void OnEnable()
        {
            //CheckState();

            OnQuestStarted?.AddListener(SpawnMonsters);

            
        }


        public void SetState(QuestState state)
        {
            this.currentQuestState = state;
        }

        private QuestState CheckState()
        {
            if (CurrentQuest.countToComplete <= 0)
                return currentQuestState;

            if (completeQuestCount >= CurrentQuest.countToComplete)
            {
                currentQuestState = QuestState.Complete;
            }
            /*
            else if (currentQuestID > 0)
            {
                currentQuestState = QuestState.Processing;
            }
            */
            else
                currentQuestState = QuestState.Processing;

            return currentQuestState;
        }

        public void ProcessQuest(QuestData.Type type, QuestData.TargetType targetType)
        {
            if (isQuestCompleted) return;
            if (currentQuestState == QuestState.Complete) return;

            if (CurrentQuest.type != type || CurrentQuest.targetType != targetType)
            {
                string massage = $"현재 진행 중인 퀘스트가 아닙니다. {CurrentQuest.type} != {type} && {CurrentQuest.targetType} != {targetType}";
                Dialogue.Instance.ShowDialogueTextTemporarily(massage);

                return;
            }

            ++completeQuestCount;
            OnQuestCompleteCountChanged?.Invoke(currentQuestID, completeQuestCount);

            if (CheckState() == QuestState.Complete)
            {
                if (currentQuestID < DataManager.Instance.QuestData.quests.Count - 1)
                {
                    Dialogue.Instance.ShowDialogueTextTemporarily($"{CurrentQuest.questTitle} 완료! \n다음 퀘스트를 진행할 수 있습니다. 담당 NPC와 대화하세요.");
                }
                else
                {
                    isQuestCompleted = true;
                    OnAllQuestsCompleted?.Invoke();
                }
                PlayerLevelController playerLevelController = FindFirstObjectByType<PlayerLevelController>();
                if (playerLevelController != null)
                {
                    playerLevelController.GainExp(CurrentQuest.exp);
                }

                OnQuestCompleted?.Invoke(currentQuestID);
            }
        }

        public QuestState CheckNPCState(int npcID)
        {
            if (currentQuestID < DataManager.Instance.QuestData.quests.Count - 1 &&
                    currentQuestState == QuestState.Complete &&
                    NextQuest.startCondition == currentQuestID &&
                    NextQuest.npcID == npcID)
            {
                return QuestState.Start;
            }

            if (currentQuestState == QuestState.Processing &&
                    CurrentQuest.npcID == npcID)
            {
                return QuestState.Processing;
            }

            return QuestState.None;
        }


        public void MoveToNextQuest()
        {
            if (currentQuestID >= DataManager.Instance.QuestData.quests.Count - 1) return;

            ++currentQuestID;
            completeQuestCount = 0;
            OnQuestStarted?.Invoke(currentQuestID);
        }


        private void SpawnMonsters(int questID)
        {
            if (CurrentQuest.type == QuestData.Type.Kill && CurrentQuest.targetType == QuestData.TargetType.Chomper)
            {
                MonsterSpawner.SpawnMonsters(CurrentQuest.countToComplete, CurrentQuest.monsterLevel);
            }

            else if (CurrentQuest.type == QuestData.Type.Wave && CurrentQuest.targetType == QuestData.TargetType.Chomper)
            {
                MonsterSpawner.StartMonsterWave();
            }

            // 보스 몬스터 퀘스트의 경우, 보스 몬스터 스폰은 BossMonsterSpawner에서 처리하도록 확장할 수 있습니다.

        }

        public void SubscribeOnQuestStarted(UnityAction<int> listener)
        {
            OnQuestStarted?.AddListener(listener);
        }

        public void SubscribeOnQuestCompleted(UnityAction<int> listener)
        {
            OnQuestCompleted?.AddListener(listener);
        }

        public void SubscribeOnQuestCompleteCountChanged(UnityAction<int, int> listener)
        {
            OnQuestCompleteCountChanged?.AddListener(listener);
        }

    }

}
