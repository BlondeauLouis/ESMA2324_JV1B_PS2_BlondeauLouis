using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    public float parallaxSpeed = 0.5f; // Vitesse de défilement du parallaxe
    private float startPosX;
    private float lengthX;
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void FixedUpdate()
    {
        float dist = (virtualCamera.transform.position.x * parallaxSpeed);

        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);
    }
}
