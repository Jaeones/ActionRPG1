using System;
using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class NPCStateManager : MonoBehaviour
    {
        public enum State
        {
            None = -1,
            Idle,
            Talk,
            Length,
        }

        [SerializeField] private State state = State.None;
        [SerializeField] private NPCStateBase[] states;
        [SerializeField] private UnityEvent<State> OnStateChanged;
        [SerializeField] private int npcID = 0;
        public int NPCID { get { return npcID; } }

        public NPCData.NPC data { get; private set; } = null;
        public Transform PlayerTransform { get; private set; } = null;

        private void Awake()
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            data = DataManager.Instance.NPCData.npcs[NPCID - 1];

            states = new NPCStateBase[(int)State.Length];

            for (int ix = 0; ix < (int)states.Length; ix++)
            {
                string componentName = $"RPGGame.NPC{(State)ix}State";
                states[ix] = GetComponent(Type.GetType(componentName)) as NPCStateBase;
                states[ix].enabled = false;
            }
        }

        private void OnEnable()
        {
            SetState(State.Idle);
        }

        public void SetState(State newState)
        {
            if (state == newState) return;

            if (state != State.None)
            {
                states[(int)state].enabled = false;
            }

            states[(int)newState].enabled = true;
            state = newState;
            OnStateChanged?.Invoke(state);
        }

        public void SubscribeOnStateChanged(UnityAction<State> listener)
        {
            OnStateChanged?.AddListener(listener);
        }
    }

}
