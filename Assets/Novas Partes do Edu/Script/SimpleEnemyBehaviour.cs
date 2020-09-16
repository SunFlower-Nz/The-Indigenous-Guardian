using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyBehaviour : DestructiveBase
{
    public int attack;
    public float distanceToFollow;
    public Animator animator;
    public float stunTime;

    private NavMeshAgent nmAgent;
    private PlayerBehaviour player;
    private float currentDistancePlayer;
    private Vector3 playerPosition;
    private bool inStun;
    private float currentStunTime;

    public float timeToAttack;
    private float currentTimeToatttack;
    private bool canAttack;

    // Start is called before the first frame update
    new void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
        currentAttackRate = attackRate;
    }

    // Update is called once per frame
    new void Update()
    {
        if (!inStun && !isDead)
        {
            playerPosition = player.transform.position;
            currentDistancePlayer = Vector3.Distance(transform.position, playerPosition);

            if (currentDistancePlayer < distanceToFollow)
            {
                nmAgent.SetDestination(playerPosition);
            }

            if (currentDistancePlayer <= nmAgent.stoppingDistance && nmAgent.velocity == Vector3.zero)
            {

                if (currentAttackRate >= attackRate)
                {
                    currentAttackRate = 0;
                    animator.SetTrigger("attack");

                    transform.LookAt(player.transform.position);
                    player.ApplyDamage(attack);
                }
                currentTimeToatttack += Time.deltaTime;

                if (currentTimeToatttack > timeToAttack && canAttack)
                {
                    currentTimeToatttack = 0;
                    player.ApplyDamage(attack);
                    canAttack = false;
                }
            }

            animator.SetFloat("Velocity", nmAgent.velocity.magnitude);
        }
        else
        {
            if (inStun)
            {
                currentStunTime += Time.deltaTime;

                if (currentStunTime > stunTime)
                {
                    currentStunTime = 0;
                    inStun = false;
                }
            }
        }
        animator.SetBool("inStun", inStun);
        currentAttackRate += Time.deltaTime;
    }
    public override void OnDestroyed()
    {
        animator.SetTrigger("die");
        GetComponent<Collider>().enabled = false;
    }
    public override void OnApplyDamage()
    {
        inStun = true;
        animator.SetTrigger("hit");

    }
}
