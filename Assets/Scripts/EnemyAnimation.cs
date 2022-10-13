using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = transform.parent.gameObject.GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("Health", enemyMovement.health);
    }
}
