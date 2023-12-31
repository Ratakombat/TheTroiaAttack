using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
 
    [SerializeField] private GameObject bulletDecal;
    

    private float speed = 50f;
    private float timeToDestroy = 3f;
    public Vector3 target { get; set; }
    public bool hit { get; set;}
    


   

    private void OnEnable() {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.01f){
            Destroy(gameObject);
        }
    }


    public void OnCollisionEnter(Collision other) 
    {

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        else{
            
            ContactPoint contact = other.GetContact(0);
            GameObject.Instantiate(bulletDecal, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
            Destroy(gameObject);
        }

      

   
          Debug.Log(other.gameObject);

    }
}
