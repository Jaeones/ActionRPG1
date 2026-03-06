using System;
using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    [DefaultExecutionOrder(-1)]
    public class GrenadierStateManager : MonoBehaviour  
    {
        public enum State
        {
            None = -1,
            Idle,
            Rotate,
            Attack,
            Dead,
            Length,
        }

        public enum AttackType
        {
            None = -1,
            MeleeAttack,
            RangeAttack,
            Length,
        }

        [SerializeField]
        private State currentState = State.None;

        [SerializeField]
        private GrenadierStateBase[] states;

        [SerializeField]
        private UnityEvent<State> OnStateChanged;

        [SerializeField]
        private UnityEvent<AttackType> OnAttackTypeChanged;

        [SerializeField]
        private int level = 1;

        private PlayerStateManager targetPlayerStateManager { get; set; }
        private Transform refTransform;

        [SerializeField]
        private float attackInterval = 3f;
        [SerializeField]
        private float attackTime = 0f;

        private bool canAttack { get { return Time.time > attackTime + attackInterval; } }

        public AttackType currentAttackType { get; private set; } = AttackType.None;

        public bool isPlayerDead { get { return targetPlayerStateManager.isPlayerDead; } }

        public MonsterData data;
        public MonsterData.LevelData currentLevelData { get; private set; }

        public Transform playerTransform { get; private set; }

        private void Awake()
        {
            data = DataManager.Instance.GrenadierData;
            currentLevelData = data.levels[level - 1];
            
            states = new GrenadierStateBase[(int)State.Length];
            for (int ix = 0; ix < states.Length; ix++)
            {
                string componentName = $"RPGGame.Grenadier{(State)ix}State";
                states[ix] = GetComponent(Type.GetType(componentName)) as GrenadierStateBase;

                states[ix].enabled = false;
            }

            if (refTransform == null)
            {
                refTransform = transform;
            }

            targetPlayerStateManager = FindAnyObjectByType<PlayerStateManager>();
            playerTransform = targetPlayerStateManager.transform;

            HpController hpController = GetComponentInChildren<HpController>();

            if (hpController != null)
            {
                hpController.SetMaxHp(currentLevelData.maxHp);
                hpController.SetDefense(currentLevelData.defense);
                hpController.SubscribeOnDead(OnDead);
            }

            GrenadierMeleeAttack meleeAttack = GetComponentInChildren<GrenadierMeleeAttack>();
            if (meleeAttack != null)
            {
                meleeAttack.SetAttack(currentLevelData.attack);
            }

            GrenadierRangeAttack rangeAttack = GetComponentInChildren<GrenadierRangeAttack>();
            if (rangeAttack != null)
            {
                rangeAttack.SetAttack(currentLevelData.attack);
                rangeAttack.SetAttackRange(data.rangeAttackRange);
            }
        }

        private void OnEnable()
        {
            SetState(State.Idle);
            BackGroundMusicPlayer musicPlayer = FindFirstObjectByType<BackGroundMusicPlayer>();
            if (musicPlayer != null)
            {
                musicPlayer.PlayBattleMusic();
            }
        }

        public void SetState(int state)
        {
            SetState((State)state);
        }

        public void SetState(State newState)
        {
            if (currentState == State.Dead || currentState == newState)
            {
                return;
            }

            if(currentState != State.None)
            {
                states[(int)currentState].enabled = false;
            }

            states[(int)newState].enabled = true;

            currentState = newState;

            OnStateChanged?.Invoke(currentState);
        }

        public void ChangeToAttack()
        {
            if (currentState == State.Attack || isPlayerDead) return;
            if(!canAttack) return;

            float distanceToPlayer = (playerTransform.position - refTransform.position).sqrMagnitude;

            if(distanceToPlayer > data.rangeAttackRange * data.rangeAttackRange)
            {
                currentAttackType = AttackType.None;
                return;
            }
            
            if ((distanceToPlayer > data.attackRange * data.attackRange) && distanceToPlayer <= data.rangeAttackRange * data.rangeAttackRange)
            {
                currentAttackType = AttackType.RangeAttack;
            }
            else if (distanceToPlayer <= data.attackRange * data.attackRange)
            {
                currentAttackType = AttackType.MeleeAttack;
            }

            attackTime = Time.time;

            OnAttackTypeChanged?.Invoke(currentAttackType);

            SetState(State.Attack);
        }

        private void OnDead()
        {
            SetState(State.Dead);
        }

        public void RotateToPlayer()
        {
            // 8방향 회전
            Vector3 direction = (playerTransform.position - refTransform.position);
            direction.y = 0f; // 수평 방향으로만 회전하도록 y축 고정

            refTransform.rotation = Quaternion.LookRotation(direction);
        }

        public void SubscriveOnStateChanged(UnityAction<State> listener)
        {
            OnStateChanged?.AddListener(listener);
        }

        public void SubscriveOnAttackTypeChanged(UnityAction<AttackType> listener)
        {
            OnAttackTypeChanged?.AddListener(listener);
        }

    }
}