using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDoubleJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().AddDoubleJump();
            this.gameObject.SetActive(false);
        }
    }
}
