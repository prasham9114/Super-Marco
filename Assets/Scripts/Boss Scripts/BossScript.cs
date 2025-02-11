using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject stone;
    public Transform attackInstantiate;
   
    private Animator anim;

    private string coroutineName = "startAttack";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutineName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        // negative because we are throwing to the left 
        // add force adds a force to the rigid body
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }
    void backToIdle()
    {
        anim.Play("BossIdle");
    }

    public void deactivateBoss() { 
        StopCoroutine(coroutineName);
        enabled = false;
    }
    IEnumerator startAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("BossAttack");
        StartCoroutine(coroutineName);
    }
}
