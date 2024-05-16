using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private float patrolSpeed;
    private int currentPatrolIndex = 0;
    private bool movingForward = true;

    private Rigidbody2D rb;
    private Creature creature;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        creature = GetComponent<Creature>();
        patrolSpeed = creature.speed;
    }

    void Update()
    {
        // Check if the creature should patrol
        if (!creature.isHooked && !creature.wasAttacked)
        {
            PatrolMovement();
        }
        // else
        // {
        //     rb.velocity = Vector2.zero;
        // }
    }

    private void PatrolMovement()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = (targetPatrolPoint.position - transform.position).normalized;
        rb.velocity = direction * patrolSpeed;

        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.2f)
        {
            if (movingForward)
            {
                if (currentPatrolIndex < patrolPoints.Length - 1)
                {
                    currentPatrolIndex++;
                }
                else
                {
                    movingForward = false;
                    currentPatrolIndex--;
                }
            }
            else
            {
                if (currentPatrolIndex > 0)
                {
                    currentPatrolIndex--;
                }
                else
                {
                    movingForward = true;
                    currentPatrolIndex++;
                }
            }
        }
    }
}
