using UnityEngine;


namespace RPGGame
{
    public class UIGameStart : MonoBehaviour
    {
        // 로딩바 실행할것
        [SerializeField] private GameObject loadingUi;

        public void StartButton()
        {
            loadingUi.SetActive(true);
        }

        private GameManager gameManager;


        private void Awake()
        {
            if (gameManager == null)
            {
                gameManager = FindFirstObjectByType<GameManager>();
            }
        }

        public void ExitGame()
        {
            gameManager.ExitGame();
        }

    }

}
