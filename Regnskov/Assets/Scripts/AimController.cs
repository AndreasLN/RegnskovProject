using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{

    public GameObject Crosshair;

    private Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (Crosshair != null)
        {
            target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            Crosshair.transform.position = new Vector2(target.x, target.y);
        }
    }
}
