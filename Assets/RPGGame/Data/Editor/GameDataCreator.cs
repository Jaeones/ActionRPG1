using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Collections.Generic; // 추가: System.IO 네임스페이스를 임포트하여 File.ReadAllLines 사용 가능


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
    }
}

