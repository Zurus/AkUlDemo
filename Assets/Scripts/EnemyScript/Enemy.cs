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
    [Header("State Machine")]
    public EnemyState currentState;
    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;

    [Header("Death effect")]
    private float deathEffectDelay = 1f;
    public GameObject deathEffect;
    public LootTable thisLoot;

    [Header("Death Signal")]
    public Signal roomSignal;


    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void OnEnable()
    {
        transform.position = homePosition;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //this.gameObject.SetActive(false);
            //todo: изучить пул объектов https://www.youtube.com/@NightTrainCode/videos
            DeathEffect();
            MakeLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false);
            //Destroy(gameObject, 2f);
            
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            Powerup current = thisLoot.lootsPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Knock(Rigidbody2D rigidbody2, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(rigidbody2, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D rigidbody2D, float knockTime)
    {
        if (rigidbody2D != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.GetComponent<Enemy>().currentState = EnemyState.idle;
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
