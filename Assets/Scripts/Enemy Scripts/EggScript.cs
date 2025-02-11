using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == MyTags.PLAYER_TAG)
        {
            player.GetComponent<PlayerDamage>().dealDamage();
        }

        gameObject.SetActive(false);
    }
}
