using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    //void Update()
    //{
    //    navMeshAgent.destination = target.transform.position;
    //}

    public void OnDetectObject(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            navMeshAgent.destination = collider.transform.position;
        }
    }
}
