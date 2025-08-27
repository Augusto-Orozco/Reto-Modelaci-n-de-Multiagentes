using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform[] waypoints;
    public Color rayColor = Color.white;

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;

        Transform[] path_nodes = transform.GetComponentsInChildren<Transform>();

        waypoints = new Transform[path_nodes.Length - 1];
        for (int j = 1; j < path_nodes.Length; j++)
        {
            waypoints[j - 1] = path_nodes[j];
        }

        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 pos = waypoints[i].position;

            if (i > 0)
            {
                Vector3 prev = waypoints[i - 1].position;
                Gizmos.DrawLine(pos, prev);
            }
            Gizmos.DrawWireSphere(pos, 0.3f);
        }
    }
}
