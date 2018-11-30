using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonIA : MonoBehaviour {

    //Navigation et destination
    private NavMeshAgent nav;
    public Transform[] targets;
    int dest;
    private Transform destination;
    public Transform player;

    //Etat for couroutine
    protected enum Etat {Idle, Patrole, Chase };
    Etat actualState;

    //Stat
    private float vitesse;
    public float Vitesse
    {
        get { return vitesse; }
        set
        {
            vitesse = value;
            Debug.Log("Vitesse changée");
        }
    }
    bool RoutineIsStart = false;

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        actualState = Etat.Patrole;
	}
	
	// Update is called once per frame
	void Update () {
        vitesse = 10f;
        //StopAllCoroutines();
        if (!RoutineIsStart)
        {
            RoutineIsStart = true;
            switch (actualState)
            {
                case Etat.Idle:
                    break;
                case Etat.Patrole:
                    //if (targets.Length == 0) break;
                    destination = targets[dest];
                    StartCoroutine("Patrouille", destination);
                    if (dest == targets.Length - 1) dest = 0;
                    else dest += 1;
                    break;
                case Etat.Chase:
                    StartCoroutine("Chase" + player);
                    break;
            }
        }
    }
    
    //Routine
    private IEnumerator Patrouille(Transform Dest)
    {
        while (true)
        {
            Vector3 dest = Dest.position;
            nav.SetDestination(dest);
            yield return new WaitForSeconds(5f);
            RoutineIsStart = false;
        }
    }

    private IEnumerator Chase(Transform targetPlayer)
    {
        Vector3 dest = targetPlayer.position;
        nav.SetDestination(dest);
        yield return null;
    }
}
