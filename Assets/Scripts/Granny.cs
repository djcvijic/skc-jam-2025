using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granny : MonoBehaviour
{
    [SerializeField] private Collider2D catchCollider;
    [SerializeField] private Cat closestCat;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<Transform> reversedWaypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float endWaitTime = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform sprite;

    private int currentWaypoint = 1;
    private bool forwardMotion = true;
    private bool inMotion = false;
    private Coroutine attackCoroutine;

    public bool IsAttacking => attackCoroutine != null;

    private void Start()
    {
        StartCoroutine(MoveThroughWaypoints(waypoints, currentWaypoint));
    }

    private void Update()
    {
        closestCat = GetClosestCatInMischief();
        if (closestCat)
        {
            StopAllCoroutines();
            StartCoroutine(MoveTowardsCat());
        }
    }

    private Cat GetClosestCatInMischief()
    {
        Cat closestCat = null;
        float closestDistance = float.MaxValue;
        var cats = FindObjectsOfType<Cat>();

        foreach (var cat in cats)
        {
            if (!cat.InMischief) continue;
            var distance = Vector3.Distance(cat.transform.position, transform.position);
            if (distance <= closestDistance)
            {
                closestCat = cat;
                closestDistance = distance;
            }
        }

        return closestCat;
    }

    private IEnumerator MoveThroughWaypoints(List<Transform> path, int startingWaypoint)
    {
        while (IsAttacking)
        {
            yield return null;
        }
        
        animator.Play("GrannyWalk");
        inMotion = true;
        for (int i = startingWaypoint; i < path.Count; i++)
        {
            Transform target = path[i];

            while (Vector3.Distance(transform.position, target.position) > 0.000001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                if (transform.position.x < target.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (transform.position.x > target.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                yield return null;
            }

            currentWaypoint++;

            if (i == path.Count - 1)
            {
                inMotion = false;
                animator.Play("GrannyIdle");
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
                animator.Play("GrannyIdle");

            yield return new WaitForSeconds(waitTime);
            animator.Play("GrannyWalk");
        }
    }

    private IEnumerator MoveTowardsCat()
    {
        while (IsAttacking)
        {
            yield return null;
        }

        while (closestCat && closestCat.InMischief)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, closestCat.transform.position, speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (closestCat != null && closestCat.InMischief)
        {
            var cat = other.GetComponentInParent<Cat>();
            if (cat != null)
            {
                if (cat.InMischief)
                {
                    StartGrannyAttack(cat.playerId);
                }
            }
        }
    }

    private void StartGrannyAttack(int playerId)
    {
        App.Instance.Notifier.TriggerGrannyFight(playerId);
        animator.Play("GrannyAttack");
        
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(FinishAttack());
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(App.Instance.GameSettings.GrannyFightDuration);
        
        animator.Play("GrannyWalk");
        attackCoroutine = null;
    }
}