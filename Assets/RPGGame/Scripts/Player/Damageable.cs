using UnityEngine;
using UnityEngine.Events;



namespace RPGGame
{
    public class Damageable : MonoBehaviour
    {
        // 데미지를 입었을 때 발생하는 이벤트
        [SerializeField] private UnityEvent<float> OnDamageRecived;

        // 데미지를 중복으로 받지 않도록 하기 위한 플래그
        [SerializeField] private bool isInvulnerable = false;

        [SerializeField] private float invulnerabilityDuration = 0.2f; // 무적 상태 지속 시간
        [SerializeField] private float time = 0f; // 무적 상태 타이머

        [SerializeField] private float defense = 0f; // 현재 체력

        private void Update()
        {
            if(isInvulnerable && Time.time > time + invulnerabilityDuration)
            {
                isInvulnerable = false;
            }
        }

        // 방어력을 설정하는 메서드
        public void SetDefense(float defense)
        {
            this.defense = defense;
        }

        // 데미지를 입는 메서드
        public void ReceiveDamage(float damageAmount)
        {
            if (isInvulnerable) { return; }

            isInvulnerable = true;

            time = Time.time;

            float finalDamage = Mathf.Max(0f, damageAmount - defense); // 방어력을 고려한 최종 데미지 계산

            OnDamageRecived?.Invoke(finalDamage); // 데미지를 입었을 때 이벤트 호출

            Util.Log($"Received damage: {transform.root.name}, Damage: {finalDamage}");
        }
    }

}