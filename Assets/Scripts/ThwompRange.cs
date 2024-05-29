using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompRange : MonoBehaviour
{
    private GameObject thwomp;
    private Thwomp thwompScript;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent != null)
        {
            Transform parentTransform = transform.parent;
            thwomp = parentTransform.gameObject;
            thwompScript = parentTransform.GetComponent<Thwomp>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            thwompScript.isActif = true;
            gameObject.SetActive(false);
        }
    }
}
