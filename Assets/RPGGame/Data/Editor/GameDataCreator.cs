using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Collections.Generic;
using System; // 추가: System.IO 네임스페이스를 임포트하여 File.ReadAllLines 사용 가능


namespace RPGGame
{
    public class GameDataCreator
    {
        // 모든 데이터 에셋을 저장할 폴더
        private static readonly string DataFolderPath = "Assets/RPGGame/Resources/Data";

        // PlayerLevelData.csv 데이터 경로
        private static readonly string PlayerLevelDataPath = "Assets/RPGGame/Data/Editor/PlayerLevelData.csv";

        // PlayerLevelData 에셋 경로
        private static readonly string PlayerLevelDataAssetPath = $"{DataFolderPath}/PlayerLevelData.asset";

        // MonsterLevelData.csv 데이터 경로
        private static readonly string MonsterLevelDataPath = "Assets/RPGGame/Data/Editor/MonsterLevelData.csv";

        // MonsterLevelData 에셋 경로
        private static readonly string MonsterLevelDataAssetPath = $"{DataFolderPath}/MonsterLevelData.asset";

        // QuestData.csv 데이터 경로
        private static readonly string QuestDataPath = "Assets/RPGGame/Data/Editor/QuestData.csv";

        // QuestData 에셋 경로
        private static readonly string QuestDataAssetPath = $"{DataFolderPath}/QuestData.asset";

        // NPCData.csv 데이터 경로
        private static readonly string NPCDataPath = "Assets/RPGGame/Data/Editor/NPCData.csv";

        // NPCData 에셋 경로
        private static readonly string NPCDataAssetPath = $"{DataFolderPath}/NPCData.asset";

        // GrenadierLevelData.csv 데이터 경로
        private static readonly string grenadierLevelDataPath = "Assets/RPGGame/Data/Editor/GrenadierLevelData.csv";
        
        // GrenadierLevelData 에셋 경로
        private static readonly string grenadierLevelDataAssetPath = $"{DataFolderPath}/GrenadierLevelData.asset";

        //데이터 에셋을 저장할 폴더가 있는지 확인하고, 없으면 생성하는 함수
        private static void CheckAndCreateDataFolder()
        {
            if (!System.IO.Directory.Exists(DataFolderPath))
            {
                System.IO.Directory.CreateDirectory(DataFolderPath);
                Debug.Log($"Created data folder at: {DataFolderPath}");
            }
        }

        // 유니티 에디터 상단에 메뉴를 생성해주는 구문
        [MenuItem("RPGGame/Create PlayerData")]
        private static void CreatePlayerData()
        {
            CheckAndCreateDataFolder();

            PlayerData playerDataSo = AssetDatabase.LoadAssetAtPath(PlayerLevelDataAssetPath, typeof(PlayerData)) as PlayerData;

            if (playerDataSo == null)
            {
                //PlayerData 스크립터블 오브젝트 인스턴스 생성
                playerDataSo = ScriptableObject.CreateInstance<PlayerData>();

                // 생성한 인스턴스를 에셋으로 저장
                AssetDatabase.CreateAsset(playerDataSo, PlayerLevelDataAssetPath);
            }

            // CSV 파일을 줄 별로 읽어서 리스트에 저장
            string[] lines = System.IO.File.ReadAllLines(PlayerLevelDataPath);

            // 레벨 데이터 초기화
            playerDataSo.levels = new List<PlayerData.LevelData>();

            for (int ix = 1; ix < lines.Length; ix++) // 첫 줄은 헤더이므로 1부터 시작
            {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                // 레벨 데이터 생성
                PlayerData.LevelData levelData = new PlayerData.LevelData
                {
                    level = int.Parse(data[0]),
                    maxHp = float.Parse(data[1]),
                    requiredExp = float.Parse(data[2])
                };

                playerDataSo.levels.Add(levelData);
            }

            // 변경된 내용을 에셋에 저장
            EditorUtility.SetDirty(playerDataSo);
            // 에셋 데이터베이스에 변경 사항을 반영
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create MonsterData")]
        private static void CreateMonsterData()
        {
            CheckAndCreateDataFolder();
            MonsterData monsterDataSo = AssetDatabase.LoadAssetAtPath(MonsterLevelDataAssetPath, typeof(MonsterData)) as MonsterData;
            if (monsterDataSo == null)
            {
                //MonsterData 스크립터블 오브젝트 인스턴스 생성
                monsterDataSo = ScriptableObject.CreateInstance<MonsterData>();
                // 생성한 인스턴스를 에셋으로 저장
                AssetDatabase.CreateAsset(monsterDataSo, MonsterLevelDataAssetPath);
            }
            // CSV 파일을 줄 별로 읽어서 리스트에 저장
            string[] lines = System.IO.File.ReadAllLines(MonsterLevelDataPath);
            // 레벨 데이터 초기화
            monsterDataSo.levels = new List<MonsterData.LevelData>();
            for (int ix = 1; ix < lines.Length; ix++) // 첫 줄은 헤더이므로 1부터 시작
            {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                // 레벨 데이터 생성
                MonsterData.LevelData levelData = new MonsterData.LevelData
                {
                    level = int.Parse(data[0]),
                    maxHp = float.Parse(data[1]),
                    attack = float.Parse(data[2]),
                    defense = float.Parse(data[3]),
                    gainExp = float.Parse(data[4])
                };
                monsterDataSo.levels.Add(levelData);
            }
            // 변경된 내용을 에셋에 저장
            EditorUtility.SetDirty(monsterDataSo);
            // 에셋 데이터베이스에 변경 사항을 반영
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create QuestData")]
        private static void CreateQuestData()
        {
            CheckAndCreateDataFolder();
            QuestData questDataSo = AssetDatabase.LoadAssetAtPath(QuestDataAssetPath, typeof(QuestData)) as QuestData;
            if (questDataSo == null)
            {
                //QuestData 스크립터블 오브젝트 인스턴스 생성
                questDataSo = ScriptableObject.CreateInstance<QuestData>();
                // 생성한 인스턴스를 에셋으로 저장
                AssetDatabase.CreateAsset(questDataSo, QuestDataAssetPath);
            }
            // CSV 파일을 줄 별로 읽어서 리스트에 저장
            string[] lines = System.IO.File.ReadAllLines(QuestDataPath);


            // 퀘스트 데이터 초기화
            questDataSo.quests = new List<QuestData.Quest>();
            for (int ix = 1; ix < lines.Length; ix++) // 첫 줄은 헤더이므로 1부터 시작
            {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);

                // 레벨 데이터 생성
                QuestData.Quest quest = new QuestData.Quest();
                quest.questID = int.Parse(data[0]);
                quest.questTitle = data[1];
                quest.type = (QuestData.Type)Enum.Parse(typeof(QuestData.Type), data[2]);
                quest.targetType = (QuestData.TargetType)Enum.Parse(typeof(QuestData.TargetType), data[3]);
                quest.countToComplete = int.Parse(data[4]);
                quest.exp = float.Parse(data[5]);
                quest.questBeginDialogue = data[6];
                quest.questProgressDialogue = data[7];
                quest.smallTalkDialogue = data[8];
                quest.startCondition = int.Parse(data[9]);
                quest.nextQuestID = int.Parse(data[10]);
                quest.npcID = int.Parse(data[11]);
                quest.monsterLevel = int.Parse(data[12]);

                questDataSo.quests.Add(quest);
            }
            // 변경된 내용을 에셋에 저장
            EditorUtility.SetDirty(questDataSo);
            // 에셋 데이터베이스에 변경 사항을 반영
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create NPCData")]
        private static void CreateNPCData()
        {
            CheckAndCreateDataFolder();
            NPCData npcDataSo = AssetDatabase.LoadAssetAtPath(NPCDataAssetPath, typeof(NPCData)) as NPCData;
            if (npcDataSo == null)
            {
                //NPCData 스크립터블 오브젝트 인스턴스 생성
                npcDataSo = ScriptableObject.CreateInstance<NPCData>();
                // 생성한 인스턴스를 에셋으로 저장
                AssetDatabase.CreateAsset(npcDataSo, NPCDataAssetPath);
            }
            // CSV 파일을 줄 별로 읽어서 리스트에 저장
            string[] lines = System.IO.File.ReadAllLines(NPCDataPath);
            // NPC 데이터 초기화
            npcDataSo.npcs = new List<NPCData.NPC>();
            for (int ix = 1; ix < lines.Length; ix++) // 첫 줄은 헤더이므로 1부터 시작
            {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                // NPC 데이터 생성
                NPCData.NPC npc = new NPCData.NPC
                {
                    id = int.Parse(data[0]),
                    name = data[1],
                    interactionSight = float.Parse(data[2])
                };
                npcDataSo.npcs.Add(npc);
            }
            // 변경된 내용을 에셋에 저장
            EditorUtility.SetDirty(npcDataSo);
            // 에셋 데이터베이스에 변경 사항을 반영
            AssetDatabase.SaveAssets();
        }

        [MenuItem("RPGGame/Create GrenadierLevelData")]
        private static void CreateGrenadierLevelData()
        {
            CheckAndCreateDataFolder();
            MonsterData grenadierLevelDataSo = AssetDatabase.LoadAssetAtPath(grenadierLevelDataAssetPath, typeof(MonsterData)) as MonsterData;
            if (grenadierLevelDataSo == null)
            {
                //MonsterData 스크립터블 오브젝트 인스턴스 생성
                grenadierLevelDataSo = ScriptableObject.CreateInstance<MonsterData>();
                // 생성한 인스턴스를 에셋으로 저장
                AssetDatabase.CreateAsset(grenadierLevelDataSo, grenadierLevelDataAssetPath);
            }
            // CSV 파일을 줄 별로 읽어서 리스트에 저장
            string[] lines = System.IO.File.ReadAllLines(grenadierLevelDataPath);
            // 레벨 데이터 초기화
            grenadierLevelDataSo.levels = new List<MonsterData.LevelData>();
            for (int ix = 1; ix < lines.Length; ix++) // 첫 줄은 헤더이므로 1부터 시작
            {
                string[] data = lines[ix].Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                // 레벨 데이터 생성
                MonsterData.LevelData levelData = new MonsterData.LevelData
                {
                    level = int.Parse(data[0]),
                    maxHp = float.Parse(data[1]),
                    attack = float.Parse(data[2]),
                    rangeAttack = float.Parse(data[3]),
                    defense = float.Parse(data[4]),
                    gainExp = float.Parse(data[5])
                };
                grenadierLevelDataSo.levels.Add(levelData);
            }
            // 변경된 내용을 에셋에 저장
            EditorUtility.SetDirty(grenadierLevelDataSo);
            // 에셋 데이터베이스에 변경 사항을 반영
            AssetDatabase.SaveAssets();
        }
    }
}

