using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


namespace RPGGame
{
    [CreateAssetMenu]
    public class QuestData : ScriptableObject
    {
        public enum Type
        {
            None = -1,
            CollectWeapon,
            Kill,
            Wave,
            Length,
        }

        public enum TargetType
        {
            None = -1,
            Player,
            Chomper,
            Grenadier,
            Length,
        }

        [System.Serializable]
        public class Quest
        {
            public int questID = 0;
            public string questTitle = "";
            public Type type = Type.None;
            public TargetType targetType = TargetType.None;
            public int countToComplete = 0;
            public float exp = 0f;
            public string questBeginDialogue = "";
            public string questProgressDialogue = "";
            public string smallTalkDialogue = "";
            public int startCondition = 0;
            public int nextQuestID = 0;
            public int npcID = 0;
            public int monsterLevel = 0;
        }

        public List<Quest> quests = new List<Quest>();
    }

}
