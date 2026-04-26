using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class SimpleTarget : MonoBehaviour
{
    private Animator animator;
    public float Hp;
    public float respawnTimer;
    public bool isDead;
    public AudioSource source;
    public float volume;
    
    
    public AudioClip[] hitClip;
    public AudioClip[] dieClip;
    public AudioClip[] respawnClip;
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckHp();
    }

    void CheckHp()
    {
        
    }


    public void OnHit(float damage)
    {
        source.pitch = Random.Range(0.8f, 1.2f);
        source.volume = volume;
        if (isDead) return;
        Hp-= damage;
        if (Hp <= 0)
        {
            source.PlayOneShot(dieClip[Random.Range(0, dieClip.Length)]);
            animator.SetTrigger("Die");
            StartCoroutine(Respawn());
            isDead = true;
        }
        else
        {
            source.PlayOneShot(hitClip[Random.Range(0, hitClip.Length)]);
            
        }
    
    }

    IEnumerator Respawn()
    {   
        float timer = 0f;
        while (timer < 2f)
        {
            respawnTimer = timer;
            timer += Time.deltaTime;
            yield return null;
        }
        source.volume = volume;
        source.PlayOneShot(respawnClip[Random.Range(0, respawnClip.Length)]);
        Hp = 100f;
        isDead = false;
    }
}
