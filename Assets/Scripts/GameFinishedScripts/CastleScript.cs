using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.PLAYER_TAG)
        {
            SceneManager.LoadScene("GameWon");
        }
    }
}
