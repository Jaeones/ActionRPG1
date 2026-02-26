using System.Collections;
using TMPro;
using UnityEngine;


namespace RPGGame
{
    [DefaultExecutionOrder(-1)]
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueWindow;

        [SerializeField] private TextMeshProUGUI dialogueText;

        [SerializeField] private float dialogueShowTime = 5f;

        [SerializeField] private float textAnimationInterval = 0.04f;

        private static Dialogue instance = null;

        public static Dialogue Instance => instance;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        public void ShowDialogue()
        {
            StopAllCoroutines();

            dialogueWindow.SetActive(true);
        }

        public void CloseDialogue()
        {
            dialogueWindow.SetActive(false);
        }

        public void CloseDialogueAfterTime(float time)
        {
            StartCoroutine(CloseDialogueWithDelay(time));
        }

        public void ShowDialogueText(string text)
        {
            ShowDialogue();
            StartCoroutine(SetTextWithAnimation(text));
        }

        public void ShowDialogueTextTemporarily(string text, float time = 0f)
        {
            ShowDialogue();
            StopAllCoroutines();

            float dialogueShowTime = time == 0f ? this.dialogueShowTime : time;
            StartCoroutine(SetTempDialogueTextWithAnimation(text, dialogueShowTime));
        }

        IEnumerator SetTempDialogueTextWithAnimation(string text, float dialogueShowTime)
        {
            yield return StartCoroutine(SetTextWithAnimation(text));

            yield return StartCoroutine(CloseDialogueWithDelay(dialogueShowTime));
        }

        IEnumerator SetTextWithAnimation(string text)
        {
            int count = 1;
            WaitForSeconds interval = new WaitForSeconds(textAnimationInterval);

            while (count <= text.Length)
            {
                dialogueText.text = text.Substring(0, count);
                count++;
                yield return interval;
            }
        }

        IEnumerator CloseDialogueWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            CloseDialogue();
        }
    }

}
