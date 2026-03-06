using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace RPGGame
{
    public class UISceneLoader : MonoBehaviour
    {
        [SerializeField] private string mainSceneName;

        [SerializeField] private Image progressBar;

        [SerializeField] private float loadingTime = 2.5f;

        private void Start()
        {
            StartCoroutine(LoadMainScene());
        }

        // Simulate loading process
        private IEnumerator LoadMainScene()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(mainSceneName);
            asyncOperation.allowSceneActivation = false;
            yield return asyncOperation.isDone;

            float elapsedTime = 0f;

            while (elapsedTime < loadingTime)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / loadingTime;
                progressBar.fillAmount = progress;
                yield return null;

                if(progress >= 0.99f)
                {
                    progressBar.fillAmount = 1f;
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);

            // Load the main scene here
            asyncOperation.allowSceneActivation = true;
        }
    }

}
