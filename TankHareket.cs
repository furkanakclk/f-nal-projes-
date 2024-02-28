using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHareket : MonoBehaviour
{
    public float hizEndeksi = 100f;
    float yEkseni;
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        Vector3 hareket = new Vector3(0, 0f, Input.GetAxis("Vertical"));
        yEkseni += Input.GetAxis("Horizontal");
        transform.localRotation = Quaternion.Euler(0, yEkseni, 0);
        transform.Translate(hareket * Time.deltaTime * hizEndeksi);
    }
}
