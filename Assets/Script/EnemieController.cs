using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieController : MonoBehaviour{

    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    //pour pattrouillé
    public List<Transform> Pattern = new List<Transform>();
    int indexlist;
    
    //Pour le changement d'etat
    enum etat {idle, pattrouile, attaque};
    etat currentState;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        if (Pattern.Count != 0) indexlist = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(currentState == etat.idle) {
            Vector3 direction = (gameObject.transform.localRotation.eulerAngles - transform.position).normalized;
            Quaternion lookrot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrot, Time.deltaTime * 2f);
        }
        else if (currentState == etat.pattrouile)
        {
            StartCoroutine("Patrouille", Pattern[indexlist]);
            if (Vector3.Distance(Pattern[indexlist].transform.position, gameObject.transform.position) <= 2) {
                currentState = etat.idle;
                indexlist++;
            }
        }
        else if (currentState == etat.attaque) {
            CheakIfPlayer();
        }
        
    }

    //Verifier en permanence si le joueur est present
    void FixedUpdate()
    {
        CheakIfPlayer();
    }

    //Lors de l'etat pattrouille
    IEnumerator Patrouille(Transform objectif)
    {
        agent.SetDestination(objectif.position);
        indexlist += 1;
        yield return new WaitForSeconds(5f);
    }

    //Fonction qui cheak si le player est dans le champs de vision
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
                currentState = etat.attaque;
            }
            else
            {
                currentState = etat.pattrouile;
            }
        }
        else
        {
            currentState = etat.pattrouile;
        }
    }
    
    //Une fois dans l'etat de poursuite/attaque , il suit le joueur du regard
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrot, Time.deltaTime * 2f);
    }

    //uniquement pour l'editor afin de voir le champ d'action
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
