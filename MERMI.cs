using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MERMI : MonoBehaviour

if (Input.GetMouseButtonDown(0))
{
    GameObject top = Instantiate(mermi, pivot.position, Quaternion.identity);
    top.GetComponent<Rigidbody>().AddForce(transform.forward* 150f, ForceMode.Impulse); print("Mermi Yaratýldý");
    Destroy(top.gameObject, 2f);
}