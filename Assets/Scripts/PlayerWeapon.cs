using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 20f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Wall") || other.transform.CompareTag("Monster"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Monster"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject, 0.2f);
        }
    }
}
