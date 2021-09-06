using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Reference to main camera
    public Camera mainCamera;
    //Reference to player
    public Transform mainPlayer;

    //The current active camera
    public Camera activeCamera;
    public float distanceFromPlayer;

    [Range(0,1)]
    public float slur;
    public Vector3 targetLocation;
    public bool cameraFollow;
    public Vector2 standardOffsets;

    private void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, slur);
    }

    public IEnumerator FollowPlayer()
    {
        //Initial position to find player
        targetLocation = new Vector3(mainPlayer.transform.position.x, mainPlayer.transform.position.y, distanceFromPlayer);

        yield return new WaitForSeconds(1.0f);

        while (cameraFollow)
        {
            targetLocation = new Vector3(mainPlayer.transform.position.x+ (Input.GetAxis("Horizontal") * standardOffsets.x), mainPlayer.transform.position.y+standardOffsets.y, distanceFromPlayer);
            yield return null;
        }


        yield return null;


    }



}
