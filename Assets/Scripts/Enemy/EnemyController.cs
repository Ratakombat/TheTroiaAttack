using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private float timeOfLastAttack = 0f;
    private bool hasStopped = false;
    private Animator anim = null;
    private EnemyStats stats = null;
    private Transform target;


    private void Start() {
        GetReferences();
    }

    private void Update() {
        MoveToTarget();
    }

    private void MoveToTarget(){
        agent.SetDestination(target.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(distanceToTarget <= agent.stoppingDistance){
            anim.SetFloat("Speed", 0f);

            //Attack

            if(!hasStopped){
                hasStopped = true;
                timeOfLastAttack = Time.time;
            }

               
   
            if (Time.time >= timeOfLastAttack + stats.attackSpeed)
            {   
                timeOfLastAttack = Time.time;
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                AttackTarget(targetStats);
            }
            
        }
        else
        {
            if(hasStopped){
                hasStopped = false;
            }
        }
    }  

    private void RotateToTarget(){
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void AttackTarget(CharacterStats statsToDamage){
        anim.SetTrigger("Attack");

        stats.DealDamage(statsToDamage);


    }
    private void GetReferences(){
        agent = GetComponent<NavMeshAgent>();
        target = PlayerController.instance;

        anim = GetComponentInChildren<Animator>();  

        stats = GetComponent<EnemyStats>();
    }


}
