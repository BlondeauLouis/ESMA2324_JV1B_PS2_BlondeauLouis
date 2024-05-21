using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Coeur : MonoBehaviour
{
    private PlayerController playerController;
    public Image uiImage;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.health < 3)
        {
            uiImage.enabled = false;
        }

        if (playerController.health == 3)
        {
            uiImage.enabled = true;
        }
    }
}
