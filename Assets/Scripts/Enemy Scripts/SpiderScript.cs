using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D myBody;

    private Vector3 moveDir;
    private string coroutine_name = "ChangeMovement";
  
    private GameObject player;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag(MyTags.PLAYER_TAG);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        moveDir = Vector3.down;
        StartCoroutine(coroutine_name);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDir * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if (moveDir == Vector3.down)
        {
            moveDir = Vector3.up;
        }
        else
        {
            moveDir = Vector3.down;
        }

        StartCoroutine(coroutine_name);
    }
    IEnumerator spiderDead() 
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.BULLET_TAG)
        {
            anim.Play("SpiderDead");
            myBody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(spiderDead());
            StopCoroutine(coroutine_name);
        }

        if(target.tag == MyTags.PLAYER_TAG)
        {
            player.GetComponent<PlayerDamage>().dealDamage();
        }
    }

}
