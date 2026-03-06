using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private float gameMenuOpenDelay = 0.5f;

        [SerializeField, TextArea(5, 5)] private string gameStartComment;
        [SerializeField, TextArea(5, 5)] private string gameClearComment;

        public void GameStart()
        {
            Invoke("ShowGameStartDialog", 1f);
        }

        public void GameClear(float time = 0f)
        {
            float delay = time == 0f ? 2f : time + 2f;
            StartCoroutine(ProcessGameClear(delay));
        }

        IEnumerator ProcessGameClear(float delay)
        {
            yield return new WaitForSeconds(delay);
            Dialogue.Instance.ShowDialogueTextTemporarily(gameClearComment, 10f);

            BackGroundMusicPlayer musicPlayer = FindFirstObjectByType<BackGroundMusicPlayer>();
            if (musicPlayer != null)
            {
                musicPlayer.PlayNormalMusic();
            }

            yield return new WaitForSeconds(5f);

            GameOver(delay + 5f);
        }

        public void GameOver(float time = 0f)
        {
            float delay = time == 0f ? gameMenuOpenDelay : time;
            Invoke("ActivateGameMenu", delay);
        }

        private void ActivateGameMenu()
        {
            if (gameMenu != null)
            {
                gameMenu.SetActive(true);
            }
        }

        public void RestartGame()
        {
            BackGroundMusicPlayer musicPlayer = FindFirstObjectByType<BackGroundMusicPlayer>();
            if (musicPlayer != null)
            {
                musicPlayer.PlayNormalMusic();
            }
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        //Dialogue에서 게임 시작 대사와 게임 클리어 대사를 보여주는 함수
        private void ShowGameStartDialog()
        {
            if (!string.IsNullOrEmpty(gameStartComment))
            {
                Dialogue.Instance.ShowDialogueTextTemporarily(gameStartComment, 10f);
            }
        }

        /*
        private void ShowGameClearDialog()
        {
            if (!string.IsNullOrEmpty(gameClearComment))
            {
                Dialogue.Instance.ShowDialogueTextTemporarily(gameClearComment, 10f);
            }
        }
        */
    }

}
