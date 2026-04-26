using System;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{
   


    public LayerMask layerTarget;

    public float currentDamage;

    public void ShootTarget()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layerTarget))
        {
            SimpleTarget target = hit.transform.GetComponent<SimpleTarget>();
            
            target.OnHit(currentDamage);
        }

    }
}
