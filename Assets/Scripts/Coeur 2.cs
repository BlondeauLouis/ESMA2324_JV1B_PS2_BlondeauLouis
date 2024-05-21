using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coeur2 : MonoBehaviour
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
        if (playerController.health < 2)
        {
            uiImage.enabled = false;
        }

        if (playerController.health >= 2)
        {
            uiImage.enabled = true;
        }
    }
}
