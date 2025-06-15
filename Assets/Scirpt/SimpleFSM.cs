using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SimpleFSM : FSM
{
    public enum FSMState
    {
        None,
        Run,
        Move
    }
    
    [SerializeField] FSMState currentstate;

    private float curspeed;
    private Vector3 pointPosisition;
    private Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool IsWaiting;
    private GameObject player;

    [SerializeField] private float chaseRange = 20f; // Jarak untuk mulai mengejar

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }
    protected override void fsm_init()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentstate = FSMState.Move;
        curspeed = 0.5f;
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.RandomRange(0, 100);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        foreach (var point in GameObject.FindGameObjectsWithTag("Waypoint"))

        {
            Waypoints.Add(point);
        }
        int index = Random.RandomRange(0, Waypoints.Count);
        Vector3 tujuan = Waypoints[index].transform.position;
        pointPosisition = tujuan;
        Debug.Log(GameObject.FindGameObjectsWithTag("Waypoint"));
        agent.destination = tujuan;
        Dest = tujuan;
        
    }

    protected override void fsm_update()
    {

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance < chaseRange)
        {
            currentstate = FSMState.Run;
        }
        else if (currentstate == FSMState.Run && playerDistance >= chaseRange + 5f)
        {
            // Player menjauh, kembali ke Move
            currentstate = FSMState.Move;
            FindNextPoint();
        }
        switch (currentstate)
        {
            case FSMState.Move:
                UpdateMove();
                break;
            default:
                UpdateRun();
                break;

        }
     
        Debug.Log(pointPosisition);
        Debug.Log(transform.position);
    }
    private void UpdateRun()
    {
        if (player == null) return;

        agent.destination = player.transform.position;
        curspeed = 1f; // Animasi lari
        animator.SetFloat("Blend", curspeed, 0.1f, Time.deltaTime);
    }
    private void FindNextPoint()
    {
        if (Waypoints.Count == 0)
        {
            Debug.LogWarning("Waypoints kosong, tidak bisa mencari tujuan baru!");
            return;
        }

        int randIndex = UnityEngine.Random.Range(0, Waypoints.Count);
        Vector3 randomOffset = Vector3.zero; // Bisa pakai Random.insideUnitSphere * radius kalau mau variasi

        Dest = Waypoints[randIndex].transform.position + randomOffset;
        agent.destination = Dest; // <-- PENTING!
        pointPosisition = Dest;

        Debug.Log($"Next point: {Waypoints[randIndex].name} - Posisi: {Dest}");
    }



    private bool IsInCurrentRange(Vector3 pos)
    {
        float xpos = Mathf.Abs(pos.x - transform.position.x);
        float zpos = Mathf.Abs(pos.z - transform.position.z);
        if (xpos <= 50 && zpos <= 50) return true;

        return false;

    }
    private void UpdateMove()
    {

        if (!IsWaiting && Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance + 0.5f)
        {
            StartCoroutine(WaitBeforeNextMove());

        }
        if (!IsWaiting)
        {
            agent.destination = Dest;

            curspeed = (agent.velocity.magnitude > 0.1f) ? 0.5f : 0f;
            animator.SetFloat("Blend", curspeed, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Blend", 0f);
        }



    }

    private IEnumerator WaitBeforeNextMove()
    {
        IsWaiting = true;
        curspeed = 0f;
        animator.SetFloat("Blend", 0f);

        yield return new WaitForSeconds(3f);

        FindNextPoint();

        curspeed = 0.5f;
        IsWaiting = false;
    }
    protected override void fsm_fixedUpdate()
    {

    }
}
