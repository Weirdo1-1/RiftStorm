using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float parallaxEffect;

    private Transform camTransform;
    private Vector3 lastCamPos;
    void Start()
    {
        camTransform = Camera.main.transform;
        lastCamPos = camTransform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 deltaMovement = camTransform.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect, deltaMovement.y * parallaxEffect, 0);
        lastCamPos = camTransform.position;
    }
}
