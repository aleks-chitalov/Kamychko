using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float enemyMaxHealth;
    public float enemyHealth;
    public string enemyName;
    public int enemyBaseAttack;
    public float enemyMoveSpeed;

    private void Awake()
    {
        this.enemyHealth = enemyMaxHealth;
    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void EnemyStagger(float staggerTime)
    {
        StartCoroutine(StaggerCo(staggerTime));
    }
    private IEnumerator StaggerCo(float staggerTime)
    {
        currentState = EnemyState.stagger;
        yield return new WaitForSeconds(staggerTime);
        currentState = EnemyState.idle;
    }
    
}