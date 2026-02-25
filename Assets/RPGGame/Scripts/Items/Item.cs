using UnityEngine;

namespace RPGGame
{
    public abstract class Item : ScriptableObject
    {
        public string itemName; //인벤토리에서 보여줄 아이템 이름
        public Sprite itemIcon; //인벤토리에서 보여줄 아이템 아이콘

        [TextArea(2, 15)]
        public string messageWhenCollected;

        [TextArea(2, 15)]
        public string messageWhenUsed;

        public virtual void Use()
        {
            //아이템 사용 시의 효과를 구현하는 메서드
        }
    }

}