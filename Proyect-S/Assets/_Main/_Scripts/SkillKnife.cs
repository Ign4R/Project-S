using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillKnife : MonoBehaviour
{
    public GameObject knife;
    Rigidbody rb;
    public GameObject cam;
    public Transform position;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            knife.transform.rotation = Quaternion.LookRotation(transform.forward);
            knife.GetComponent<Rigidbody>().isKinematic = true;
            Throw();
        }
    }
   

    void Throw()
    {
        knife.transform.position = position.transform.position;
        knife.GetComponent<Rigidbody>().isKinematic = false;
        knife.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 1000);

    }
}
