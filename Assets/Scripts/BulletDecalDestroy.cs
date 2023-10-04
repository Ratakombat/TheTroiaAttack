using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDecalDestroy : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroyDecal;
    void Start()
    {
        InvokeRepeating("VerifyTimeToDestroyDecal", timeToDestroyDecal, 1f);
    }

     private void VerifyTimeToDestroyDecal(){
        timeToDestroyDecal -= 1f;

        if (timeToDestroyDecal <= 0)
        {
            Destroy(gameObject);
        }
    }
}
