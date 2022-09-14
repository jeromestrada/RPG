using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    protected Animator animator; // access to the animator, for attack cancelling
    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if( distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            
        }

        if(distance <= agent.stoppingDistance)
        {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if(targetStats != null)
            {
                combat.Attack(targetStats);
                animator.ResetTrigger("attack_cancel");
            }
            
            FaceTarget();
        }
        if(distance > agent.stoppingDistance) // if the target moves out of range, stop the animation.
        {
            animator.SetTrigger("attack_cancel");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
