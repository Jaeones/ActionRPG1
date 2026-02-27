using System.Collections.Generic;
using UnityEngine;


namespace RPGGame
{

    [CreateAssetMenu]
    public class NPCData : ScriptableObject
    {

        [System.Serializable]
        public class NPC
        {
            public int id = 0;
            public string name = "";
            public float interactionSight = 0f;
        }

        public List<NPC> npcs = new List<NPC>();

    }

}
