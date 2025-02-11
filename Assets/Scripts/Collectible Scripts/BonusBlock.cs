using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{

    public Transform bottomCollision;
    private Animator anim;
    public LayerMask playerLayer;
    private Vector3 moveDir = Vector3.up;
    private Vector3 originPos;
    private Vector3 animPos;
    private bool startAnim;
    private bool canAnimate;
    private GameObject player;
    private AudioSource audioManager;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
        canAnimate = true;
        audioManager = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        checkForPos();
        animateUpDown();
    }

    void checkForPos()
    {
        if (canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, playerLayer);
            if (hit)
            {
                if (hit.collider.tag == MyTags.PLAYER_TAG)
                {
                    player.GetComponent<ScoreScript>().addScore(Random.Range(10, 100))  ;
                    anim.Play("BonusBlockIdle");
                    audioManager.Play();
                    startAnim = true;
                    canAnimate = false;
                }
            }
        }
    }

    void animateUpDown()
    {
        if(startAnim)
        {
            transform.Translate(moveDir * Time.smoothDeltaTime);

            if(transform.position.y >= animPos.y)
            {
                moveDir = Vector3.down;
            }
            else if(transform.position.y <= originPos.y)
            {
                startAnim = false; 
                moveDir = Vector3.up;
            }
        }
    }
}
