using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granny : MonoBehaviour
{
    [SerializeField] private Cat cat;
    
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<Transform> reversedWaypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float endWaitTime = 5f;
    
    private int currentWaypoint = 1;
    private bool forwardMotion = true;
    private bool inMotion = false;

    private void Start()
    {
        StartCoroutine(MoveThroughWaypoints(waypoints, currentWaypoint));
    }

    private void Update()
    {
        if (cat.InMischief)
        {
            StopAllCoroutines();
            StartCoroutine(MoveTowardsCat());
        }
    }

    private IEnumerator MoveThroughWaypoints(List<Transform> path, int startingWaypoint)
    {
        inMotion = true;
        for (int i = startingWaypoint; i < path.Count; i++)
        {
            Transform target = path[i];
            
            while (Vector3.Distance(transform.position, target.position) > 0.000001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                yield return null;
            }
            currentWaypoint++;

            if (i == path.Count - 1)
            {
                inMotion = false;
                yield return new WaitForSeconds(endWaitTime);
                currentWaypoint = 1;
                if (forwardMotion)
                {
                    forwardMotion = !forwardMotion;
                    StartCoroutine(MoveThroughWaypoints(reversedWaypoints, startingWaypoint));
                }
                else
                {
                    forwardMotion = !forwardMotion;
                    StartCoroutine(MoveThroughWaypoints(waypoints, startingWaypoint));
                }
            }
            else
                yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveTowardsCat()
    {
        while (cat.InMischief)
        {
            transform.position = Vector3.MoveTowards(transform.position, cat.transform.position, speed * Time.deltaTime);
            yield return null;
        }

        if (!inMotion)
        {
            currentWaypoint = 1;
            forwardMotion = !forwardMotion;
        }
        if (forwardMotion)
        {
            StartCoroutine(MoveThroughWaypoints(waypoints, currentWaypoint));
        }
        else
        {
            StartCoroutine(MoveThroughWaypoints(reversedWaypoints, currentWaypoint));
        }
    }
}
