using System.Timers;
using ProceduralFPS;
using UnityEngine;

public class ADS : MonoBehaviour
{
    public Vector3 adsPos;
    public Vector3 adsRot;  
    private SecondOrderSystem secondOrderSystem =  new SecondOrderSystem();
    private Vector3 currentVel;
    private Vector3 currentVelRot;
    [SerializeField] private Camera mainCamera;
    public float targetIdleFov;
    public float targetAdsFov;
    public float currentFov;
    public bool isADS;
    public float smoothTime;
    private float currentVelocityFov;
    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.gunInput.reqADS)
        {
            currentFov = Mathf.SmoothDamp(currentFov,targetAdsFov, ref currentVelocityFov, smoothTime);
            isADS = true; 
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, adsPos, ref currentVel, 0.1f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(adsRot), Time.deltaTime * 5f);
        }
        else
        {
            currentFov = Mathf.SmoothDamp(currentFov,targetIdleFov, ref currentVelocityFov, smoothTime);
            isADS = false;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(0,0,0), ref currentVel, 0.1f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime * 5f);
        }

        mainCamera.fieldOfView = currentFov;
    }
    
    
}
