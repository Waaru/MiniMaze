using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieController : MonoBehaviour {

    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            if (gameObject.GetComponentInChildren<FieldOfView>().seePlayer)
            {
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    // HE ATTAQUE
                    // FACE ME
                }
            }
        }
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
