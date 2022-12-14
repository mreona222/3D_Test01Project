using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    Rigidbody floorRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        floorRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floorRigidbody.MovePosition(floorRigidbody.position + new Vector3(5.0f * Time.fixedDeltaTime, 0, 0));

    }
}
