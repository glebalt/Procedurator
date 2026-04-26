using ProceduralFPS;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private float timer;
    [Range(0.1f, 10)]
    public float freq = 1f;
    [Range(0.1f, 4)]
    public float resp = 1f;
    [Range(0.1f, 1)]
    public float damp = 0.3f;

    public float sprintAmplitude = 6f;
    public float sprintPower = 0.2f;
    public float amplitude = 6f;
    public float power;
    private SecondOrderSystem secondOrderSystem = new  SecondOrderSystem();
        
    [Header("Recoil")]
    [Range(0.1f, 10)]
    public float freqRec= 1f;
    [Range(0.1f, 4)]
    public float respRec= 1f;
    [Range(0.1f, 1)]
    public float dampRec= 0.3f;

    public float recoilTickSpeed;
    public Vector3 maxImpulseRot;
    public Vector3 impulseRot;
    private SecondOrderSystem recccoilShakeSos  = new  SecondOrderSystem();
    private Vector3 rot;
    [SerializeField] private Transform recoilLayer;


    [Header("Camera Reload Shaker")] 
    [SerializeField] private Transform reloadCameraLayer;
    private Vector3 cameraReloadRot;
    private SecondOrderSystem cameraReloadSos = new  SecondOrderSystem();
    [Range(0.1f, 10)]
    public float freqReload= 1f;
    [Range(-5f, 15)]
    public float respReload= 1f;
    [Range(0.1f, 1)]
    public float dampReload= 0.3f;
    
   public  float cameraZRot;

    private float intpdValue;

    private float tD1;

    private float tD2;

    public float smoothedCF;

   public float currentAmplitude;
   public float targetAmplitude;

   public float currentPower;
   public  float targetPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        intpdValue = 0;
    }

    public void CallReloadShake(Vector3 reloadImpulse)
    {
        cameraReloadSos.UpdateSettings(new SecondOrderSystem.Settings{response =  respReload,damping =   dampReload,frequency =  freqReload});
        cameraReloadRot += reloadImpulse;
    }

    public void ReloadShakeTick()
    {
        Vector3 reloadRes = cameraReloadSos.Update(Time.deltaTime,  cameraReloadRot);
        
        cameraReloadRot = Vector3.Lerp(cameraReloadRot, Vector3.zero, Time.deltaTime * 2);
        
        reloadCameraLayer.localRotation = Quaternion.Euler(reloadRes);
    }

    // Update is called once per frame
    void Update()
    {
      
      UpdateShakeWalking();
      TickRecoil();
      ReloadShakeTick();
    }

    void UpdateShakeWalking()
    {
        bool isIdle = PlayerController.CurrentVelMagnitude < 1.3f;
        
        
        
        SecondOrderSystem.Settings sosSettings = new SecondOrderSystem.Settings{response = resp,damping =  damp,frequency =   freq};
       targetAmplitude =  isIdle ? 0 : PlayerController.CurrentVelMagnitude > 4f ? sprintAmplitude : amplitude;
       currentAmplitude = Mathf.Lerp(currentAmplitude, targetAmplitude, Time.deltaTime * 2);
        
       targetPower = isIdle ? 0 : PlayerController.CurrentVelMagnitude > 4.7 ? sprintPower : power;
       currentPower = Mathf.Lerp(currentPower, targetPower, Time.deltaTime * 2);
       
       timer += Time.deltaTime * currentAmplitude;
            
        
       cameraZRot = Mathf.Sin(timer) ;
        
        secondOrderSystem.UpdateSettings(sosSettings);
        
 
        
        Vector3 res = secondOrderSystem.Update(Time.deltaTime, new Vector3(0, 0, cameraZRot));
        float influence = res.z * currentPower;
        
        transform.localRotation = Quaternion.Euler(influence,influence,influence);
    }

    public void CallShootRecoil()
    {
        rot += new Vector3(Random.Range(impulseRot.x,maxImpulseRot.x),Random.Range(impulseRot.y,maxImpulseRot.y),Random.Range(impulseRot.z,maxImpulseRot.z));
    }

    public void TickRecoil()
    {
        recccoilShakeSos.UpdateSettings(new SecondOrderSystem.Settings{damping =  dampRec,frequency =  freqRec,response =  respRec});
        Vector3 res = recccoilShakeSos.Update(Time.deltaTime, rot); 
        // res.x = Mathf.Clamp(res.x, 0, maxImpulseRot.x);
        // res.y = Mathf.Clamp(res.y, 0, maxImpulseRot.y);
        // res.z = Mathf.Clamp(res.z, 0, maxImpulseRot.z); 
        
        recoilLayer.localRotation = Quaternion.Euler(res);
        
      
        rot = Vector3.Lerp(rot, Vector3.zero, Time.deltaTime * recoilTickSpeed);
    }
    
    
}
