using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;

public class Nyoba : MonoBehaviour
{
    public GameObject go;
    public NavMeshAgent agent;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = go.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("NPC")) Debug.Log("ASD");
    }
}
