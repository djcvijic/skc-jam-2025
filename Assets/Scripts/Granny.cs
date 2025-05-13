using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Granny : MonoBehaviour
{
    [SerializeField] private Collider2D catchCollider;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<Transform> reversedWaypoints;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform sprite;

    private Cat closestCat;
    private int currentWaypoint = 1;
    private bool forwardMotion;
    private bool inMotion;
    private Coroutine patrollingCoroutine;
    private Coroutine chasingCoroutine;
    private Coroutine attackCoroutine;

    private bool IsPatrolling => patrollingCoroutine != null;
    private bool IsChasingCat => chasingCoroutine != null;
    private bool IsAttacking => attackCoroutine != null;

    private void Update()
    {
        if (IsAttacking) return;

        closestCat = FindNearestCatInMischief();
        if (closestCat != null)
        {
            TryStartChasingCat();
            return;
        }

        TryStartPatrolling();
    }

    private Cat FindNearestCatInMischief()
    {
        var myPosition = transform.position;
        return FindObjectsByType<Cat>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .Where(x => x.InMischief)
            .Select(x => new Tuple<Cat, float>(x, Vector3.Distance(x.transform.position, myPosition)))
            .OrderBy(x => x.Item2)
            .FirstOrDefault()?.Item1;
    }

    private void TryStartChasingCat()
    {
        if (patrollingCoroutine != null) StopCoroutine(patrollingCoroutine);
        patrollingCoroutine = null;
        if (IsChasingCat) return;

        chasingCoroutine = StartCoroutine(MoveTowardsCat());
    }

    private void TryStartPatrolling()
    {
        if (IsPatrolling) return;

        if (!inMotion)
        {
            currentWaypoint = 1;
            forwardMotion = !forwardMotion;
        }

        patrollingCoroutine = StartCoroutine(forwardMotion
            ? MoveThroughWaypoints(waypoints, currentWaypoint)
            : MoveThroughWaypoints(reversedWaypoints, currentWaypoint));
    }

    private IEnumerator MoveThroughWaypoints(List<Transform> path, int startingWaypoint)
    {
        if (IsAttacking || IsChasingCat)
        {
            patrollingCoroutine = null;
            yield break;
        }

        inMotion = true;
        for (int i = startingWaypoint; i < path.Count; i++)
        {
            animator.Play("GrannyWalk");
            Transform target = path[i];

            while (Vector3.Distance(transform.position, target.position) > 0.000001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position,
                    App.Instance.GameSettings.GrannySpeed * Time.deltaTime);
                SetRotation(target);
                yield return null;
            }

            currentWaypoint++;
            animator.Play("GrannyIdle");
            if (i < path.Count - 1)
                yield return new WaitForSeconds(App.Instance.GameSettings.GrannyWaitTime);
        }

        inMotion = false;
        yield return new WaitForSeconds(App.Instance.GameSettings.GrannyEndWaitTime);

        currentWaypoint = startingWaypoint;
        forwardMotion = !forwardMotion;
        patrollingCoroutine = null;
    }

    private void SetRotation(Transform target)
    {
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator MoveTowardsCat()
    {
        animator.Play("GrannyWalk");
        while (!IsAttacking && closestCat != null && closestCat.InMischief)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, closestCat.transform.position,
                    App.Instance.GameSettings.GrannySpeed * Time.deltaTime);
            yield return null;
        }

        chasingCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var cat = other.GetComponentInParent<Cat>();
        if (cat != null)
        {
            if (IsAttacking) return;
            if (closestCat == null) return;
            if (!closestCat.InMischief) return;

            if (patrollingCoroutine != null) StopCoroutine(patrollingCoroutine);
            patrollingCoroutine = null;
            if (chasingCoroutine != null) StopCoroutine(chasingCoroutine);
            chasingCoroutine = null;
            attackCoroutine = StartCoroutine(GrannyAttack(closestCat.playerId));
        }
    }

    private IEnumerator GrannyAttack(int playerId)
    {
        App.Instance.Notifier.TriggerGrannyFight(playerId);
        animator.Play("GrannyAttack");
        yield return new WaitForSeconds(App.Instance.GameSettings.GrannyFightDuration);

        attackCoroutine = null;
    }
}