using UnityEngine;


namespace RPGGame
{
    public class QuestItem : MonoBehaviour
    {
        [SerializeField] private QuestData.Type type = QuestData.Type.None;
        [SerializeField] private QuestData.TargetType targetType = QuestData.TargetType.None;

        public void SetType(QuestData.Type type)
        {
            this.type = type;
        }

        public virtual void OnCompleted()
        {
            if (type == QuestData.Type.None || targetType == QuestData.TargetType.None) return;

            QuestManager.Instance.ProcessQuest(type, targetType);
        }
    }

}