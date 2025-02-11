using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // speed of the camera resetting to centre the player is
    public float resetSpeed = 0.5f;

    // default speed of camera 
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    // current position of the target 
    private Transform target;

    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    float offsetZ;
    private bool followsPlayer;

    private void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);  
        cameraBounds = myCol.bounds; 
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(followsPlayer)
        {
            Vector3 aheadTargetPosition = target.position + Vector3.forward * offsetZ;

            if (aheadTargetPosition.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, 
                    aheadTargetPosition, ref currentVelocity, cameraSpeed);

                transform.position = new Vector3(newCameraPosition.x,
                    transform.position.y,newCameraPosition.z);

                lastTargetPosition = target.position;
            }
        }
    }
}
