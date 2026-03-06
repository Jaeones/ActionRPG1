using UnityEngine;


namespace RPGGame
{

    public class GrenadierDeathSMB : StateMachineBehaviour
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameClear(stateInfo.length + 1f);
            }
        }

    }

}
