using System;
using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class MonsterStateManager : MonoBehaviour
    {
        public enum State
        {
            None = -1,
            Idle,
            Patrol,
            Chase,
            Attack,
            Dead,
            Length,
        }

        [SerializeField] private State state = State.None;


        // ������ �� ������Ʈ�� ����� ������Ʈ ��ũ��Ʈ �迭
        [SerializeField] private MonsterStateBase[] states;

        [SerializeField] private UnityEvent<State> OnStateChanged;

        [SerializeField] private MonsterData monsterData;

        private Transform refTransform;

        [SerializeField] private int level = 1;

        private PlayerStateManager targetPlayerStateManager;

        public MonsterData.LevelData currentLevelData { get; private set; }

        public Transform playerTransform { get; private set; }

        public bool IsPlayerDead { get { return targetPlayerStateManager.isPlayerDead; } }


        private void Awake()
        {
            MonsterAttackController monsterAttackController = GetComponentInChildren<MonsterAttackController>();

            monsterData = DataManager.Instance.MonsterData;

            currentLevelData = monsterData.levels[level - 1];
            states = new MonsterStateBase[(int)State.Length];

            if (monsterAttackController != null)
            {
                monsterAttackController.SetAttack(currentLevelData.attack);
            }

            // �÷��̾� ������Ʈ ������Ʈ �ʱ�ȭ(������ ����)
            for (int ix = 0; ix < states.Length; ix++)
            {
                string componentName = $"RPGGame.Monster{(State)ix}State";

                states[ix] = GetComponent(Type.GetType(componentName)) as MonsterStateBase;

                states[ix].enabled = false;

                if(monsterData != null)
                {
                    states[ix].SetData(monsterData);
                }
            }

            if (refTransform == null)
            {
                refTransform = transform;
            }

            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform.root;
            }

            targetPlayerStateManager = playerTransform.GetComponent<PlayerStateManager>();

            HpController hpController = GetComponentInChildren<HpController>();

            if (hpController != null)
            {
                hpController.SetMaxHp(currentLevelData.maxHp);
                hpController.SetDefense(currentLevelData.defense);

                hpController.SubscribeOnDead(OnMonsterDead);
            }

            

        }


        private void OnEnable()
        {
            SetState(State.Idle);
        }

        private void Update()
        {

            if (state == State.Dead)
            {
                return;
            }

            if (IsPlayerDead)
            {
                if (state == State.Chase || state == State.Attack)
                {
                    SetState(State.Idle);
                }
                return;
            }

            if (state == State.Idle || state == State.Patrol)
            {
                if (Util.IsInSight(refTransform, playerTransform, monsterData.singtAngle, monsterData.sightRange))
                {
                    SetState(State.Chase);
                    Util.LogRed("플레이어가 시야에 들어왔습니다.");
                    return;
                }
            }

            if (state == State.Chase || state == State.Attack)
            {
                if (!Util.IsInSight(refTransform, playerTransform, monsterData.singtAngle, monsterData.sightRange))
                {
                    SetState(State.Idle);
                    Util.LogGreen("플레이어가 시야에서 벗어났습니다.");
                    return;
                }
            }

            
        }

        public void SetState(State newState)
        {
            if(state == newState || state == State.Dead) return;
            if (state != State.None)
            {
                states[(int)state].enabled = false;
            }

            states[(int)newState].enabled = true;

            state = newState;

            OnStateChanged?.Invoke(state);
        }

        // ������Ʈ ���� �̺�Ʈ�� ������ �� ����� �Լ�
        public void SubscribeOnstateChanged(UnityAction<State> listener)
        {
            OnStateChanged?.AddListener(listener);
        }

        // ������ ������ �����ϴ� �Լ�
        public void SetLevel(int level)
        {
            this.level = level;

            currentLevelData = monsterData.levels[level - 1];
        }

        private void OnMonsterDead()
        {
            Util.LogRed($"{transform.root.name}��(��) �׾����ϴ�.");

            // ���Ͱ� �׾��� �� �߰����� ó���� ���⿡ �ۼ��� �� �ֽ��ϴ�.
            SetState(State.Dead);
        }
    }
}
