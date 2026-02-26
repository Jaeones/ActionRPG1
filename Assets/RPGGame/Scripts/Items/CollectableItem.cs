using System;
using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    [RequireComponent(typeof(Collider))]
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] protected Item item; //획득할 아이템 정보

        [SerializeField] private bool shouldDeleteAfterCollected = true;

        [SerializeField] protected UnityEvent OnItemCollected;

        protected Transform refTransform;


        // 아이템이 수집됐는지를 알려주는 프로퍼티
        public bool HasCollected { get; protected set; } = false;

        public Item Item { get { return item; } }

        protected virtual void Awake()
        {
            if (refTransform == null)
            {
                refTransform = transform;
            } 
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnCollect(other);
            }
        }

        protected virtual void OnCollect(Collider other)
        {
            if (item == null)
            {
                Debug.Log("item 변수가 설정되지 않았습니다");
                return;
            }

            if (HasCollected)
            {
                Debug.Log("이미 수집된 아이템입니다");
                return;
            }

            // 인벤토리 매니저에 아이템 추가
            InventoryManager.Instance.OnItemCollected(this);

            if (shouldDeleteAfterCollected)
            {
                Destroy(gameObject);  // 아이템 오브젝트 삭제
            }

            OnItemCollected?.Invoke();  // 아이템이 수집됐을 때 실행할 이벤트

            // 다이얼로그에 아이템 수집 메시지 전달

            Dialogue.Instance.ShowDialogueTextTemporarily(item.messageWhenCollected);


        }
    }

}
