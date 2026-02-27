using System;
using UnityEngine;


namespace RPGGame
{
    [DefaultExecutionOrder(-100)] // 다른 스크립트보다 먼저 실행되도록 설정
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; } = null;

        public PlayerData PlayerData { get; private set; }

        public MonsterData MonsterData { get; private set; }

        public QuestData QuestData { get; private set; }

        public NPCData NPCData { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 씬이 변경되어도 이 오브젝트가 파괴되지 않도록 설정
                Initialize();
            }
            else
            {
                Destroy(gameObject); // 이미 인스턴스가 존재하면 중복 생성 방지
            }
        }

        private void Initialize()
        {
            if (PlayerData == null)
            {
                PlayerData = Resources.Load<PlayerData>("Data/PlayerLevelData");
                if (PlayerData.levels.Count == 0)
                {
                    Debug.LogError("플레이어 레벨 데이터가 초기화 되지않았습니다");
                }
            }

            if (MonsterData == null)
            {
                MonsterData = Resources.Load<MonsterData>("Data/MonsterLevelData");
                if (MonsterData.levels.Count == 0)
                {
                    Debug.LogError("몬스터 레벨 데이터가 초기화 되지않았습니다");
                }
            }

            if (QuestData == null)
            {
                QuestData = Resources.Load<QuestData>("Data/QuestData");
                if (QuestData.quests.Count == 0)
                {
                    Debug.LogError("퀘스트 데이터가 초기화 되지않았습니다");
                }
            }

            if (NPCData == null)
            {
                NPCData = Resources.Load<NPCData>("Data/NPCData");
                if (NPCData.npcs.Count == 0)
                {
                    Debug.LogError("NPC 데이터가 초기화 되지않았습니다");
                }
            }
        }
    }
}
