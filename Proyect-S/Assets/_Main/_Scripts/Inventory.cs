using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public HandgunScriptLPFP handgunScript;
    public GameObject uiKnife;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            collision.gameObject.SetActive(false);
            uiKnife.SetActive(true);
            handgunScript.withoutKnife = false;
        }
    }
}
