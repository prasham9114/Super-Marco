using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;

    private bool animationStarted;
    private bool animationFinished;

    private int jumpCount;
    private bool jumpLeft;

    public LayerMask playerLayer;
    private GameObject player;
    private string coroutineName = "FrogJump";
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }
    // Start is called before the first frame update
    void Start()
    {
        jumpLeft = true;
        StartCoroutine(coroutineName);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position,0.5f,playerLayer))
        {
            player.GetComponent<PlayerDamage>().dealDamage(); 
        }    
    }
    void LateUpdate()
    {
        if(animationFinished && animationStarted)
        {
            animationStarted = false;   
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f,4f));
        animationStarted = true;
        animationFinished = false;
        jumpCount++;
        if (jumpLeft)
        {
            anim.Play("FrogJump");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine(coroutineName);
    }

    void AnimationFinished()
    {
        animationFinished = true;

        if (jumpLeft)
        {
            anim.Play("FrogIdle");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jumpCount == 3)
        {
            jumpCount = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }
}
