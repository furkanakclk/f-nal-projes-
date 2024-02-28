using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSkip : MonoBehaviour
{
    public GameObject tanks;
    Vector3 mesafe;
    float yEkseni;
    void Start()
    {
        mesafe = tanks.transform.position - transform.position;
    }

    private void Update()

    {
        yEkseni += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, yEkseni, 0);
    }

    private void LateUpdate()
    {
        transform.position = tanks.transform.position - mesafe;
    }
}