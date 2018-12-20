
using System.Linq;
using UnityEngine;

namespace GameFramework
{
    public class PlayerSpawn : MonoBehaviour
    {
        /// <summary>
        /// True if PlayerSpawn does not collide with any non-trigger colliders
        /// </summary>
        public bool IsFree
        {
            get
            {
                Vector3 center = transform.position;
                Collider[] overlaps =
                    Physics.OverlapCapsule(center + Vector3.down * 0.5f, center + Vector3.up * 0.5f, 0.5f);
                return overlaps.Count(x => !x.isTrigger) == 0;
            }
        }
        
        private void OnDrawGizmos()
        {
            Vector3 center = transform.position;

            bool valid = true;
            if (Application.isEditor)
            {
                valid = IsFree;
            }
            
            Gizmos.DrawIcon(center, "GameFramework/T_Icon_Flag", true);
            if (!valid)
            {
                Gizmos.DrawIcon(center, "GameFramework/T_Icon_Wrong", true);
            }

            Gizmos.color = valid ? new Color(0.0f, 1.0f, 0.5f) : Color.red;

            Gizmos.DrawWireSphere(center + Vector3.up * 0.5f, 0.5f);
            Gizmos.DrawWireSphere(center - Vector3.up * 0.5f, 0.5f);


            Vector3 topLeft = center + (Vector3.up * 0.5f) + (Vector3.left * 0.5f);
            Vector3 bottomLeft = topLeft + Vector3.down;
            Vector3 topRight = topLeft + Vector3.right;
            Vector3 bottomRight = topRight + Vector3.down;
            Vector3 topFront = center + (Vector3.up * 0.5f) + (Vector3.forward * 0.5f);
            Vector3 bottomFront = topFront + Vector3.down;
            Vector3 topBack = topFront + Vector3.back;
            Vector3 bottomBack = topBack + Vector3.down;

            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(topFront, bottomFront);
            Gizmos.DrawLine(topBack, bottomBack);
        }
    }
}
