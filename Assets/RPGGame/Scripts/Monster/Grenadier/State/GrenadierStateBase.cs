using UnityEngine;


namespace RPGGame
{
    public class GrenadierStateBase : MonoBehaviour
    {
        protected GrenadierStateManager manager;

        protected Transform refTransform;

        protected virtual void OnEnable()
        {
            if (manager == null)
            {
                manager = GetComponent<GrenadierStateManager>();

                if (refTransform == null)
                {
                    refTransform = transform;
                }
            }
        }

        protected virtual void Update()
        {

        }

        protected virtual void OnDisable()
        {


        }
    }
}
