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

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        print("0");
        if (other.gameObject.tag == "Knife")
        {
            Destroy(other.gameObject);
            uiKnife.SetActive(true);
            handgunScript.withoutKnife = false;
        }


    }

    void Update()
    {
        
    }
}
