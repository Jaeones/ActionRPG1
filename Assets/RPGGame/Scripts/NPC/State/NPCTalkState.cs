using UnityEngine;


namespace RPGGame
{
    public class NPCTalkState : NPCStateBase
    {
        private QuestManager questManager;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (questManager == null)
            {
                questManager = QuestManager.Instance;
            }

            switch (questManager.CheckNPCState(manager.NPCID))
            {
                case QuestManager.QuestState.Start:
                    questManager.MoveToNextQuest();
                    Dialogue.Instance.ShowDialogueText(questManager.CurrentQuest.questBeginDialogue);
                    questManager.SetState(QuestManager.QuestState.Processing);
                    break;

                case QuestManager.QuestState.Processing:
                    Dialogue.Instance.ShowDialogueText(questManager.CurrentQuest.questProgressDialogue);
                    break;

                default:
                    Dialogue.Instance.ShowDialogueText(questManager.CurrentQuest.smallTalkDialogue);
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();
            if (!CanTalk())
            {
                Dialogue.Instance.CloseDialogueAfterTime(3f);

                manager.SetState(NPCStateManager.State.Idle);
            }

            Vector3 direction = manager.PlayerTransform.position - refTransform.position;
            direction.y = 0f;
            refTransform.rotation = Quaternion.LookRotation(direction);
        }
    }

}
