using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class HpController : MonoBehaviour
    {
        [SerializeField] private float maxHp = 0f; // �ִ� ü��
        [SerializeField] private float currentHp = 0f; // ���� ü��
        [SerializeField] private float defense = 0f; // ����
        

        // ������ �޾��� �� ȣ��Ǵ� �̺�Ʈ
        [SerializeField] private UnityEvent<float, float> OnHpChanged;

        // �׾��� �� ����Ǵ� �̺�Ʈ
        [SerializeField] private UnityEvent OnDead;

        public void SetMaxHp(float maxHp)
        {
            this.maxHp = maxHp;
            currentHp = maxHp; // �ִ� ü���� �����ϸ� ���� ü�µ� �ִ� ü������ �ʱ�ȭ

            OnHpChanged?.Invoke(currentHp, maxHp); // ü�� ���� �̺�Ʈ ȣ��
        }

        public void SetDefense(float defense)
        {
            this.defense = defense;
        }


        // Health ������ ȹ���� Hp�� ȸ���� �� �����ϴ� �Լ�
        public virtual void OnHealed(float healAmount)
        {
            currentHp = Mathf.Min(currentHp + healAmount, maxHp); // ���� ü���� �ִ� ü������ �����Ͽ� ȸ��
            OnHpChanged?.Invoke(currentHp, maxHp); // ü�� ���� �̺�Ʈ ȣ��
        }

        public virtual void OnDamaged(float damage)
        {
            float finalDamage = Mathf.Max(0f, damage - defense); // ������ ������ ���� ������ ���
            currentHp = Mathf.Max(0f, currentHp - finalDamage); // ���� ü���� 0 �̻����� �����ϸ鼭 ������ ����
            OnHpChanged?.Invoke(currentHp, maxHp); // ü�� ���� �̺�Ʈ ȣ��
            if (currentHp <= 0f)
            {
                OnDead?.Invoke(); // ���� �̺�Ʈ ȣ��
            }
        }

        public virtual void Die()
        {
            currentHp = 0f; // ü���� 0���� ����
            OnHpChanged?.Invoke(currentHp, maxHp); // ü�� ���� �̺�Ʈ ȣ��
            OnDead?.Invoke(); // ���� �̺�Ʈ ȣ��
        }

        public void SubscribeOnDead(UnityAction onDeadAction)
        {
            OnDead?.AddListener(onDeadAction);
        }
    }

}
