using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform player;
    public Camera cam;
    public LayerMask layerMask;

    private void Start()
    {
        cam = Camera.main;
        
    }

    public void RaycastInteract()
    {
        if (Physics.Raycast(player.position, cam.transform.forward, out RaycastHit hit,10f,layerMask,QueryTriggerInteraction.Collide)
            && Input.GetKeyDown(KeyCode.E))
        {

            if (hit.transform.gameObject.TryGetComponent(out InteractionObject i))
            {
               InteractionResultInfo info  = new InteractionResultInfo();
              i.Interact(info);
            }
        }
    }

    private void Update()
    {
        RaycastInteract();
    }
}
