using UnityEngine;
using UnityEngine.AI;


namespace RPGGame
{
    public class Util : MonoBehaviour
    {
        //Debug.Log()를 간편하게 사용할 수 있도록 하는 유틸리티 클래스
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log($"{message}");
#endif
        }

        public static void LogRed(object message)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=red>{message}</color>");
#endif
        }
        public static void LogGreen(object message)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=green>{message}</color>");
#endif
        }
        public static void LogBlue (object message)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=blue>{message}</color>");
#endif
        }

        // 목적지에 도착했는지를 확인할때 사용하는 함수
        public static bool IsArrived(Transform selfTransform, Vector3 destination, float offset = 0.1f)
        {
            return Vector3.Distance(selfTransform.position, destination) < offset;
        }

        // center를 기준으로 range 범위에서 이동 가능한 위치를 랜덤으로 구한 뒤 반환하는 함수
        public static bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int ix = 0; ix < 30; ix++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }

            }

            result = Vector3.zero;
            return false;
        }

        // 시야 판정에 사용되는 함수 (시야각, 시야거리 검사)
        public static bool IsInSight(Transform selfTransform, Transform targetTransform, float sightAngle, float sightRange)
        {
            Vector3 direction = (targetTransform.position - selfTransform.position).normalized;
            if(Vector3.Angle(selfTransform.forward,direction) < sightAngle)
            {
                if (Vector3.Distance(selfTransform.position, targetTransform.position) < sightRange)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
