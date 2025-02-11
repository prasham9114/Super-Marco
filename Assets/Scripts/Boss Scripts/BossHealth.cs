using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    private Animator anim;
    private int health= 10;
    private bool canDamage;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
      
    }


    IEnumerator WaitForDamage() 
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(canDamage)
        {
            if (target.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;
                if (health == 0)
                {
                    GetComponent<NewBehaviourScript>().deactivateBoss();  
                    anim.Play("BossDead");
                   
                    StartCoroutine(VanishBoss());
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

    IEnumerator VanishBoss() {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
