using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPadManager : MonoBehaviour
{
    public TextMeshProUGUI textLabel;
    public GameObject player;
    public Camera keypadCam;

    private bool focusOnKeypad;
    private bool canSwitch = true;

    private void OnTriggerEnter(Collider other) {
        textLabel.text = "Press E";
    }

    private void OnTriggerExit(Collider other) {
        textLabel.text = "";
    }

    private void OnTriggerStay(Collider other) {
        if(Input.GetKeyDown(KeyCode.E) && canSwitch && !focusOnKeypad)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerMovement>().cam.GetComponent<Camera>().enabled = false;

            keypadCam.enabled = true;

            textLabel.text = "";

            focusOnKeypad = true;
            StartCoroutine(WaitForSwitch(0.1f));
        }
        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && canSwitch && focusOnKeypad)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerMovement>().cam.GetComponent<Camera>().enabled = true;

            keypadCam.enabled = false;

            textLabel.text = "Press E";

            focusOnKeypad = false;
            
            StartCoroutine(WaitForSwitch(0.1f));
        }
    }

    IEnumerator WaitForSwitch(float seconds)
    {
        canSwitch = false;

        yield return new WaitForSeconds(seconds);

        canSwitch = true;
    }
}
