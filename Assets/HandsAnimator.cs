using System.Collections;
using UnityEngine;

public class HandsAnimator : MonoBehaviour
{
 
    public Transform root;
  

    public GunAnimatorHandlerBase gunAnimatorHandler;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gunAnimatorHandler.UpdateAnimations();
    }

    

    
}
