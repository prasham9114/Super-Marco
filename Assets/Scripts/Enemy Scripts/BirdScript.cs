using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BIrdScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;

    private Vector3 moveDir = Vector3.left;
    private Vector3 originPos;
    private Vector3 targetPos;

    private float speed;
    public GameObject birdEgg;
    public LayerMask playerLayer;
    private bool attacked;

    private bool canMove;

    // Start is called before the first frame update

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        originPos = transform.position;
        originPos.x += 6;

        targetPos = transform.position;
        targetPos.x -= 6;

        canMove = true;
        speed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        dropTheEgg();
    }

    void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDir * speed * Time.smoothDeltaTime);

            if(transform.position.x >= originPos.x)
            {
                moveDir = Vector3.left;
                changeDirection(-transform.localScale.x); 
            }
            else if(transform.position.x <= targetPos.x)
            {
                moveDir = Vector3.right;
                changeDirection(-transform.localScale.x);
            }
        }
    }

    void changeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void dropTheEgg()
    {
        if(!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f,
                    transform.position.z), quaternion.identity);

                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.BULLET_TAG)
        {
            anim.Play("BirdDead");

            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;

            canMove = false;

            StartCoroutine(BirdDead());
        }
    }
}
