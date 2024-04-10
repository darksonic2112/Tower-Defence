using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10;
    
    [HideInInspector]
    public float speed = 10f;

    public float startHealth = 100;

    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [HeaderAttribute("Unity Stuff")] 
    public Image healthBar;

    private bool isDead = false;
    private NavMeshAgent navMeshAgent;
    
    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.speed = startSpeed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
        navMeshAgent.speed = speed;
    }


    void Die()
    {
        isDead = true;
        
        PlayerStats.Money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        
        Destroy(gameObject);
    }

}