using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace RPGGame
{

    [Serializable]
    public class ItemSlot
    {
        public Item item;
        public int count;
        public ItemSlot(Item item, int count)
        {
            this.item = item;
            this.count = count;
        }

        public void AddCount(int count)
        {
            this.count += count;
        }

        public void UseItem()
        {
            --count;
        }
    }

    [DefaultExecutionOrder(-50)]

    public class InventoryManager : MonoBehaviour
    {
        private static InventoryManager instance = null;

        //public static InventoryManager Instance => instance;

        public static InventoryManager Instance {  get {  return instance; } }

        // 플레이어가 수집한 아이템 목록
        [SerializeField] private Dictionary<Item, ItemSlot> items = new Dictionary<Item, ItemSlot>();
        [SerializeField] private UnityEvent OnItemListChanged;

        public int ItemCount { get { return items.Count; } }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
        public ItemSlot GetItemSlot(Item item)
        {
            if (items.TryGetValue(item, out ItemSlot itemSlot))
            {
                return itemSlot;
            }
            return null;
        }

        public List<ItemSlot> GetItems()
        {
            List<ItemSlot> itemSlots = new List<ItemSlot>();

            foreach (var item in items)
            {
                itemSlots.Add(item.Value);
            }

            return itemSlots;
        }

        public void AddItem(Item item)
        {
            if (items.ContainsKey(item))
            {
                if(items.TryGetValue(item, out ItemSlot itemSlot))
                {
                    itemSlot.AddCount(1);
                }
            }
            else
            {
                items.Add(item, new ItemSlot(item, 1));
            }
        }

        // 수집한 아이템이 이미 존재하는지 여부
        public bool HasItem(Item item)
        {
            return items.ContainsKey(item);
        }

        public void OnItemCollected(CollectableItem item)
        {
            AddItem(item.Item);

            OnItemListChanged?.Invoke();
        }

        public void OnItemUsed(Item item)
        {
            if (items.ContainsKey(item))
            {
                if (items.TryGetValue(item, out ItemSlot itemSlot))
                {
                    --itemSlot.count;
                    //itemSlot.UseItem();
                    if (itemSlot.count <= 0)
                    {
                        items.Remove(item);
                    }
                }
            }

            OnItemListChanged?.Invoke();
        }

        public void SubscribeOnItemListChanged(UnityAction listener)
        {
            OnItemListChanged.AddListener(listener);
        }

    }

}
