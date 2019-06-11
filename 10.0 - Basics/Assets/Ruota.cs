using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruota : MonoBehaviour
{
    public float rotationSpeed, movementSpeed;


    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.05f, 0.1f, 0) * 10 * rotationSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, 0.1f) * Time.deltaTime * movementSpeed, Space.World);
    }

    void FixedUpdate() {
        
    }

    private void LateUpdate() {
        
    }
}
