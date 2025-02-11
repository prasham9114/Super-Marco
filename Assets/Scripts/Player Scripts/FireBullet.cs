using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator anim;
    private bool canMove;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
       if(canMove)
        {
            Vector3 tempPostion = transform.position;
            tempPostion.x += speed * Time.deltaTime;
            transform.position = tempPostion;
        }
    }
    public float Speed
    {
        get
        { 
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Disable he bullets after 5 seconds if they dont hit anything
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }


    // Hitting the enemy with bullets
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == MyTags.BEETLE_TAG || target.gameObject.tag == MyTags.SNAIL_TAG ||
            target.gameObject.tag == MyTags.BIRD_TAG || target.gameObject.tag == MyTags.SPIDER_TAG ||
            target.gameObject.tag == MyTags.FROG_TAG || target.gameObject.tag == MyTags.BOSS_TAG)
        {
            print("bullet hit");
            anim.Play("Explode");
            canMove = false;
            StartCoroutine(DisableBullet(0.3f));
        }
    }


}
