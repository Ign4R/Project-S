using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private LayerMask _hittableMask;
    private void OnTriggerEnter(Collider other)
    {
        if ((_hittableMask & 1 << other.gameObject.layer) != 0)
        {
            victory.SetActive(true);
            
        }
    }
}
