using UnityEngine;

namespace RPGGame
{
    [CreateAssetMenu(fileName = "New Health Item", menuName = "Inventory/Item/HealthItem")]
    public class HealthItem : Item
    {
        public float healAmount; //회복량

        private void Awake()
        {
            itemName = "체력";
        }

        public override void Use()
        {
            base.Use();
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                HpController hpController = player.transform.root.GetComponentInChildren<HpController>();
                if (hpController != null)
                {
                    hpController.OnHealed(healAmount);
                }
            }
        }
    }
}
