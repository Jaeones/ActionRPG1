using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{

    public class MonsterData : ScriptableObject
    {
        [System.Serializable]
        public class LevelData
        {
            public int level = 1;
            public float maxHp = 0f;
            public float attack = 0f;
            public float rangeAttack = 0f;
            public float defense = 0f;
            public float gainExp = 0f;
        }

        [SerializeField]
        public List<LevelData> levels;
        public float patrolWaitTime = 3f;
        public float singtAngle = 60f;
        public float sightRange = 10f;
        public float chaseRotateSpeed = 540f;
        public float patrolRotateSpeed = 360f;
        public float attackRange = 2f;      // 공격 가능거리
        public float rangeAttackRange = 6f; // 원거리 공격 가능거리
    }

}
