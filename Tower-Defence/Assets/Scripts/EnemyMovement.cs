using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    private Enemy enemy;

    public Transform agentDestination; 

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        
        SetDestination(agentDestination.position);
    }

    private void Update()
    {
        agent.destination = agentDestination.position;
        
        enemy.speed = enemy.startSpeed;
    }
    
    private void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    
    private void OnDestinationReached()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}