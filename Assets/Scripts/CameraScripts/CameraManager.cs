using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject cam;
    public List<GameObject> gameObjects;

    private void Start()
    {
        cam.GetComponent<CameraFollowing>().ChangeTarget(gameObjects[0], false);
    }
}
