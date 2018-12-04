using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieController : MonoBehaviour{

    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    List<Transform> Pattern = new List<Transform>();
    int indexlist;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        if (Pattern.Count != 0) indexlist = 0;
	}
	
	// Update is called once per frame
	void Update () {
        CheakIfPlayer();
        StartCoroutine("Patrouille", Pattern[indexlist]);

    }

    void CheakIfPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            if (gameObject.GetComponentInChildren<FieldOfView>().seePlayer)
            {
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    PlayerManager.instance.player.GetComponent<Stats>().Die();
                }
            }
        }
    }

    IEnumerator Patrouille(Transform objectif)
    {
        agent.SetDestination(objectif.position);
        indexlist += 1;
        yield return null;
    }

    IEnumerator Idle(Transform objectif)
    {
        agent.SetDestination(objectif.position);
        indexlist += 1;
        yield return null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrot, Time.deltaTime * 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
