using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 _offset;

    public Transform playerTransform;

    public float rotationSpeed = 5f;
    [Header("Smoothing values")]
    [Range(0.0f, 1f)]
    public float smoothing = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion turnAngleHor = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        //Quaternion turnAngleVer = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.right); 

        _offset = /*turnAngleVer * */ turnAngleHor *_offset;

        Vector3 updatedPosition = playerTransform.position + _offset;

        transform.position = Vector3.Slerp(transform.position, updatedPosition, smoothing);

        transform.LookAt(playerTransform);
    }
}
