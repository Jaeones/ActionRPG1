using UnityEngine;
using System;
using System.Collections;


namespace RPGGame
{

    public class MonsterSpawner : MonoBehaviour
    {
        [Serializable]
        public class MonsterWave
        {
            public int count;
            public int monsterLevel;

            [TextArea(2, 10)]
            public string spawnMessage;
        }

        private static MonsterSpawner instance = null;

        [SerializeField] private GameObject chomperMonsterPrefab;
        [SerializeField] private Transform[] spawnPoints;

        [SerializeField] private MonsterWave[] monsterWaves;

        [SerializeField] private bool isWaveStarted = false;

        [SerializeField] private int currentWaveIndex = 0;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            var transforms = GetComponentsInChildren<Transform>();

            spawnPoints = new Transform[transforms.Length - 1];

            Array.Copy(transforms, 1, spawnPoints, 0, spawnPoints.Length);
        }

        public static void SpawnMonsters(int count, int monsterLevel)
        {
            for (int ix = 0; ix < count; ++ix)
            {
                SpawnAMonster(monsterLevel);
            }
        }

        private static void SpawnAMonster(int monsterLevel)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, instance.spawnPoints.Length);

            Vector3 spawnLocation = instance.spawnPoints[spawnPointIndex].position;

            Quaternion spawnRotation = instance.spawnPoints[spawnPointIndex].rotation;

            GameObject newMonster = Instantiate(instance.chomperMonsterPrefab, spawnLocation, spawnRotation);

            MonsterStateManager monsterStateManager = newMonster.GetComponent<MonsterStateManager>();
            monsterStateManager.SetLevel(monsterLevel);

            if (instance.isWaveStarted)
            {
                monsterStateManager.SetForceToChase();
            }
        }

        private static IEnumerator SpawnMonstersCoroutine(int count, int monsterLevel)
        {
            for (int ix = 0; ix < count; ++ix)
            {
                yield return instance.StartCoroutine(SpawnOneMonster(0.2f, monsterLevel));
            }
        }

        private static IEnumerator SpawnOneMonster(float time, int monsterLevel)
        {
            SpawnAMonster(monsterLevel);
            yield return new WaitForSeconds(time);
        }

        private static IEnumerator SpawnMonstersWithDelay(float time, int count, int monsterLevel)
        {
            yield return new WaitForSeconds(time);
            instance.StartCoroutine(SpawnMonstersCoroutine(count, monsterLevel));
        }

        public static void StartMonsterWave()
        {
            if (instance.isWaveStarted)
            {
                MoveToNextWave();
                return;
            }
            instance.isWaveStarted = true;

            instance.currentWaveIndex = 0;
            int count = instance.monsterWaves[instance.currentWaveIndex].count;
            int monsterLevel = instance.monsterWaves[instance.currentWaveIndex].monsterLevel;

            instance.StartCoroutine(SpawnMonstersWithDelay(2.0f, count, monsterLevel));

            Dialogue.Instance.ShowDialogueTextTemporarily(instance.monsterWaves[instance.currentWaveIndex].spawnMessage);
        }

        public static void MoveToNextWave()
        {
            if (instance.currentWaveIndex == instance.monsterWaves.Length - 1)
            {
                return;
            }

            ++instance.currentWaveIndex;
            int count = instance.monsterWaves[instance.currentWaveIndex].count;
            int monsterLevel = instance.monsterWaves[instance.currentWaveIndex].monsterLevel;

            instance.StartCoroutine(SpawnMonstersWithDelay(3.0f, count, monsterLevel));

            Dialogue.Instance.ShowDialogueTextTemporarily(instance.monsterWaves[instance.currentWaveIndex].spawnMessage);
        }
    }

}
