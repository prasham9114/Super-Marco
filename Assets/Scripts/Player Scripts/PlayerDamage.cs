using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayerDamage : MonoBehaviour
{
    private int lifeScoreCount;
    private bool canDamage;

    private Text lifeText;

    // Start is called before the first frame update
    void Awake()
    {
        canDamage = true;
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x " + lifeScoreCount;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void dealDamage()
    {
        print("dealt damage");
        if (canDamage)
        {
            lifeScoreCount--;

            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x " + lifeScoreCount;
            }
            if (lifeScoreCount == 0)
            {
                Time.timeScale = 0f;
                
                SceneManager.LoadScene("GameOver");
            }
            canDamage = false;
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(3f);
        canDamage = true;
    }
    
  

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.WATER_TAG)
        {           
            SceneManager.LoadScene("GameOver");
        }
    }
}
