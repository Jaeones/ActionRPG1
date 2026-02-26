using UnityEngine;


namespace RPGGame
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float destroyTime = 2f;
        public void Destroy()
        {
            GameObject.Destroy(gameObject, destroyTime);
        }
    }

}
