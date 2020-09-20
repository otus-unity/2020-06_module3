
using UnityEngine;
using UnityEngine.AI;

public class BotUtility : MonoBehaviour
{
    PlayerWeapon weapon;
    PlayerAnimation playerAnimation;
    NavMeshAgent navMeshAgent;
    Transform shootSource;

    void Awake()
    {
        weapon = GetComponent<PlayerWeapon>();
        weapon.SetWeapon(0);

        playerAnimation = GetComponent<PlayerAnimation>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;

        shootSource = GetComponentInChildren<Gun>().source;
    }

    void Update()
    {
        if (IsNavigating()) {
            Vector3 dir = navMeshAgent.desiredVelocity.normalized;
            transform.rotation = Quaternion.LookRotation(dir);
            playerAnimation.SetMove(dir);
        }
    }

    T FindClosest<T>()
        where T : Component
    {
        Vector3 myPosition = transform.position;
        T closest = null;
        float closestSqrDistance = 0.0f;
        foreach (var obj in FindObjectsOfType<T>()) {
            if (obj.gameObject == gameObject)
                continue;

            if (obj is AmmoBox ammoBox && !ammoBox.IsActive)
                continue;
            if (obj is HealthBox healthBox && !healthBox.IsActive)
                continue;

            float sqrDist = (obj.transform.position - myPosition).sqrMagnitude;
            if (closest == null || sqrDist < closestSqrDistance) {
                closest = obj;
                closestSqrDistance = sqrDist;
            }
        }
        return closest;
    }

    public HealthBox FindClosestHealth()
    {
        return FindClosest<HealthBox>();
    }

    public AmmoBox FindClosestAmmo()
    {
        return FindClosest<AmmoBox>();
    }

    public PlayerAnimation FindClosestPlayer()
    {
        return FindClosest<PlayerAnimation>();
    }

    public float GetDistanceToClosestEnemy()
    {
        var closestEnemy = FindClosestPlayer();
        if (closestEnemy == null)
            return Mathf.Infinity;
        return (closestEnemy.transform.position - transform.position).magnitude;
    }

    public bool NavigateTo(Component target)
    {
        if (navMeshAgent == null || target == null)
            return false;

        navMeshAgent.SetDestination(target.transform.position);
        navMeshAgent.isStopped = false;

        return true;
    }

    public bool IsNavigating()
    {
        if (navMeshAgent.isStopped)
            return false;

        if (!navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= 1.0f &&
            (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 0.000001f))
                return false;

        return true;
    }

    public bool Attack(Component target)
    {
        if (target == null)
            return false;

        navMeshAgent.isStopped = true;

        Vector3 start = transform.position;
        Vector3 end = target.transform.position;
        start.y += 1.0f;
        end.y += 1.0f;

        Vector3 direction = (end - start).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        shootSource.rotation = Quaternion.LookRotation(direction);

        Debug.DrawLine(start, end, Color.white, 0.7f);

        return weapon.Shoot();
    }
}
