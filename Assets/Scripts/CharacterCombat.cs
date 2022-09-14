using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    CharacterStats myStats;
    CharacterStats opponentStats;


    public float attackDelay = 0.6f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    public bool InCombat { get; private set; }
    public event System.Action OnAttack;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if(Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;

        }
    }
    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {
            opponentStats = targetStats;

            // triggers the OnAttack callback that has the animator subscribed to it.
            if (OnAttack != null)
            {
                OnAttack(); // CharacterAnimator.cs contains animation logic that is called when this is invoked.
            }
            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }
  /*   // this is very crude, will use events instead to determine when to deal damage based on the current animation playing.
    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        
    }*/

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());
        if (opponentStats.currentHealth <= 0f)
        {
            InCombat = false;
        }
    }
}
