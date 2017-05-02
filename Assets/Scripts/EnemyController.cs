﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float speed; 
    public int HPEnemy;
    private Animator animatorEnemy;
    public GameObject attackArea;

    private GameController score;
    private Rigidbody2D rb2dEnemy;

    public AudioClip[] enemySounds;

    public Animator GetAnimatorEnemy
    {
        get { return animatorEnemy; }
        set { animatorEnemy = value; }
    }

    public Rigidbody2D GetRb2dEnemy
    {
        get { return rb2dEnemy; }
        set { rb2dEnemy = value; }
    }
   
    bool facingRight;
    public bool FacingRight
    {
        get { return facingRight; }
        set { facingRight = value; }
    }
    void Start()
    {
        animatorEnemy = GetComponent<Animator>();
        rb2dEnemy = GetComponent<Rigidbody2D>();
        score = GameObject.Find("GameController").GetComponent<GameController>();
    }
    
    public void Dead()
    {
        if (HPEnemy <= 0)
        {
            Destroy(gameObject);
            score.score++;
        }
    }

    void Update()
    {
        Dead();
    }
    public void HurtEnemy()
    {
        HPEnemy--;
    }

    public void CheckCollision(Collider2D col)
    {
        if (col.tag == "Player")
        {
            animatorEnemy.SetTrigger("attackEnemy");
            rb2dEnemy.velocity = Vector2.zero;
        }
        if (col.tag == "DeathArea")
        {
            HurtEnemy();
            SoundController.instanceSound.PlaySingle(enemySounds[0]);
        }
        if (col.tag == "DeathAreaSkill")    
            HPEnemy = 0;
    }
    public void SetVellocity(bool left)
    {
        rb2dEnemy = GetComponent<Rigidbody2D>();
        if (left)
        {
            transform.localScale = new Vector2(1, 1);
            rb2dEnemy.velocity = new Vector2(speed, 0f);
            facingRight = true;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            rb2dEnemy.velocity = new Vector2(-speed, 0f);
            facingRight = false;
        }
    }
}
