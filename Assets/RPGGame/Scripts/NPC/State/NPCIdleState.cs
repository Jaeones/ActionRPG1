using UnityEngine;


namespace RPGGame
{
    public class NPCIdleState : NPCStateBase
    {
        protected override void Update()
        {
            base.Update();
            if (CanTalk())
            {
                manager.SetState(NPCStateManager.State.Talk);
            }
        }
    }

}
