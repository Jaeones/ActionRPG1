using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPGGame
{
    public class PlayerStateManager : MonoBehaviour
    {
        public enum AttackCombo
        {
            None = -1,
            Combo1,
            Combo2,
            Combo3,
            Combo4,
            Length,
        }

        public enum State
        {
            None = -1,
            Idle,
            Move,
            Jump,
            Attack,
            Dead,
            Length,
        }

        [SerializeField] private State state = State.None;

        [SerializeField] private PlayerStateBase[] states;

        [SerializeField] private UnityEvent<State> OnStateChanged;

        private int level = 1;

        // 플레이어가 사용할 데이터 변수(ScriptableObject를 연결)
        [SerializeField] private PlayerData data;

        private CharacterController characterController;

        public bool IsGrounded { get; private set; }

        [SerializeField] private float jumpBufferTime = 0.12f;

        private float jumpBufferCounter;

        private PlayerAnimationController animationController;
        public AttackCombo NextAttackCombo { get;private set; } = AttackCombo.None;

        private WeaponController weaponController;

        public PlayerData.LevelData currentLevelData { get; private set; }

        public bool isPlayerDead { get { return state == State.Dead; } }

        private void Awake()
        {
            //data = Resources.Load("Data/PlayerData") as PlayerData;
            data = DataManager.Instance.PlayerData;


            //플레이어의 현재 레벨 데이터 설정
            currentLevelData = data.levels[level - 1];


            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

            if (animationController == null)
            {
                animationController = GetComponentInChildren<PlayerAnimationController>();
            }

            PlayerAttackState attackState = GetComponent<PlayerAttackState>();

            if (attackState != null)
            {
                attackState.SubscribeOnAttackEnd(OnAttackEnd);
            }

            if (weaponController == null)
            {
                weaponController = GetComponentInChildren<WeaponController>();
            }

            // 플레이어 스테이트 컴포넌트 초기화(데이터 설정)
            for(int ix = 0; ix < states.Length; ix++)
            {
                if(data != null)
                {
                    states[ix].SetData(data);
                }
            }

            HpController hpController = GetComponentInChildren<HpController>();
            if (hpController != null)
            {
                hpController.SubscribeOnDead(OnPlayerDead);
                hpController.SetMaxHp(currentLevelData.maxHp);
            }
        }

        

        private void OnEnable()
        {
            SetState(State.Idle);
        }

        private void Update()
        {
            if (isPlayerDead)
            {
                return;
            }
            IsGrounded = characterController.isGrounded;

            if (InputManager.IsJump)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else if (jumpBufferCounter > 0f)
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (state == State.Jump) return;

            // 공격 입력 확인
            if (InputManager.IsAttack && weaponController.IsWeaponAttached)
            {
                if (state != State.Attack)
                {
                    //첫 번째 공격 콤보 설정
                    NextAttackCombo = AttackCombo.Combo1;

                    // 공격 상태로 전환
                    SetState(State.Attack);
                    // 애니메이터의 AttackCombo 값을 Combo로 설정
                    animationController.SetAttackComboState((int)NextAttackCombo);
                    return;
                }

                // 이미 공격 상태인 경우, 콤보를 진행
                AnimatorStateInfo currentAnimationInfo = animationController.GetCurrentStateInfo();
                if (currentAnimationInfo.IsName("AttackCombo1"))
                {
                    NextAttackCombo = AttackCombo.Combo2;
                }
                else if (currentAnimationInfo.IsName("AttackCombo2"))
                {
                    NextAttackCombo = AttackCombo.Combo3;
                }
                else if (currentAnimationInfo.IsName("AttackCombo3"))
                {
                    NextAttackCombo = AttackCombo.Combo4;
                }
                else
                {
                    NextAttackCombo = AttackCombo.None;
                }
                return;
            }

            if (state == State.Attack) return;

            if (InputManager.Movement == Vector2.zero)
            {
                SetState(State.Idle);
            }
            else
            {
                SetState(State.Move);
            }

           

            if (IsGrounded && state == State.Move && jumpBufferCounter > 0f)
            {
                jumpBufferCounter = 0f;
                IsGrounded = false;

                SetState(State.Jump);
                return;
            }
        }

        public void SetState(State newState)
        {
            if (state == newState || isPlayerDead) return;

            if (state != State.None)
            {
                states[(int)state].enabled = false;
            }

            states[(int)newState].enabled = true;

            state = newState;

            OnStateChanged?.Invoke(state);
        }


        // 애니메이터를 통해 위치가 이동할때 실행
        private void OnAnimatorMove()
        {
            IsGrounded = characterController.isGrounded;
        }

        private void OnAttackEnd()
        {
            NextAttackCombo = AttackCombo.None;

            SetState(State.Idle);

            animationController.SetAttackComboState((int)NextAttackCombo);
        }

        private void OnPlayerDead()
        {
            Util.LogRed("플레이어 사망");
            // 플레이어가 죽었을 때 처리할 로직을 여기에 작성
            SetState(State.Dead);

        }
    }
}

