using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public int level; //niveau d'arri�re plan
    private float initialPositionx;
    private float initialPositiony;
    // Start is called before the first frame update
    void Start()
    {
        initialPositionx = gameObject.transform.position.x; //r�cup�re la position initiale en x
        initialPositiony = gameObject.transform.position.y; //r�cup�re la position initiale en y
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x + (initialPositionx*level), initialPositiony*level, 5) / level; //calcule la position vis�e
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing); //effectue le mouvement
    }
}

//Merci Julien !!!!