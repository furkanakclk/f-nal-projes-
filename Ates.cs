using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ates : MonoBehaviour
{
    public float Mesafe;

    public float sıkma_aralıgı;

    public Image crosshair;

    bool fire = true;


    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        crosshair.color = Color.white;
        if (Physics.Raycast(transform.position, forward, out hit, Mesafe))
        {
            if (hit.distance <= Mesafe && hit.collider.gameObject.tag == "Enemy")
            {
                crosshair.color = Color.red;
                if (fire == true && (Input.GetMouseButton(0)))
                {
                    Debug.Log("Vurdun");
                    fire = false;
                    StartCoroutine(firetime());
                }
            }
        }
    }

    IEnumerator firetime ()
    {
        yield return new WaitForSeconds(sıkma_aralıgı);
        fire = true;
    }
}
