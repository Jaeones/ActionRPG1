using UnityEngine;


namespace RPGGame
{
    public class PlayerSpawnSMB : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            PlayerStateManager stateManager = animator.GetComponent<PlayerStateManager>();
            if (stateManager != null)
            {
                stateManager.SetState(PlayerStateManager.State.Idle);
            }

            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameStart();
            }
        }
    }

}
