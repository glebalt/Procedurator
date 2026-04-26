using ProceduralFPS;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    SecondOrderSystem position = new SecondOrderSystem();
    SecondOrderSystem rotation = new SecondOrderSystem();
    
    SecondOrderSystem positionWR = new SecondOrderSystem();
    SecondOrderSystem rotationWR= new SecondOrderSystem();

    public ADS adsRef;
   private float timerZRot;
   private float timerXRot;
   private float timerYPos;
   private float timerYRot;
   private float timerZPos;
   private float timerXPos;
    public WeaponSwaySettings swaySettings;
    public Transform weaponSwayLayer;
    public Transform proceduralWalkRunLayer;
     void Start()
     {
         swaySettings.timerXRot = 0;
         swaySettings.timerYPos = 2;
         swaySettings.timerZRot = 0.5f;
     }

     // Update is called once per frame
     void Update()
     {
         
         float isPlayerAdsing = adsRef.isADS ? 0.3f : 1;
         float isPlayerStill = PlayerController.CurrentVelMagnitude > 1 ? 1 : 0;
         float finalMod = isPlayerAdsing * isPlayerStill;
        ProceduralWalkSprint(finalMod);
        
       CalculateWeaponSway(isPlayerAdsing);
         
    
         
      
         
     
         
      
       
     }

     void CalculateWeaponSway(float isPlayerAdsing)
     {
         position.UpdateSettings(new SecondOrderSystem.Settings{frequency = swaySettings.freqIdle,damping =  swaySettings.dampIdle,response = swaySettings.respIdle});
         rotation.UpdateSettings(new SecondOrderSystem.Settings{frequency = swaySettings.freqIdle,damping =  swaySettings.dampIdle,response = swaySettings.respIdle});

         
         float yPos = (Input.GetAxis("Mouse Y") * swaySettings.i_yPosIdleStr);
         float zPos = PlayerCamera.RotX * swaySettings.i_zPosIdleStr;
         Vector3 reqPos = new  Vector3(0,yPos,zPos );
         
         reqPos.x *= isPlayerAdsing * swaySettings.strengthPos;
         reqPos.y *= isPlayerAdsing * swaySettings.strengthPos;
         reqPos.z *= isPlayerAdsing * swaySettings.strengthPos;
         reqPos.z = Mathf.Clamp(reqPos.z, swaySettings.zPosMin, swaySettings.zPosMax);
         
         Vector3 posRes = position.Update(Time.deltaTime,reqPos);
         weaponSwayLayer.localPosition = posRes;

         float xRot = Input.GetAxis("Mouse Y") + Input.GetAxis("Vertical");
         float yRot = -Input.GetAxis("Mouse X") ;
         float zRot = Input.GetAxis("Mouse X") + Input.GetAxis("Horizontal");
         
         Vector3 reqRot = new Vector3(xRot,yRot,zRot);
         
                  
         reqRot.x *= isPlayerAdsing  * swaySettings.strengthRot;
         reqRot.y *= isPlayerAdsing  * swaySettings.strengthRot;
         reqRot.z *= isPlayerAdsing *  swaySettings.strengthRot;

         Vector3 rotRes = rotation.Update(Time.deltaTime,reqRot);

         

         weaponSwayLayer.transform.localRotation= Quaternion.Euler(rotRes);

     }

    

     void ProceduralWalkSprint(float mod)
     {
         
             positionWR.UpdateSettings(new SecondOrderSystem.Settings{frequency = swaySettings.freqWR,damping =  swaySettings.dampWR,response = swaySettings.respWR});
         rotationWR.UpdateSettings(new SecondOrderSystem.Settings{frequency = swaySettings.freqWR,damping =  swaySettings.dampWR,response = swaySettings.respWR});

           
         if (PlayerController.CurrentVelMagnitude > 2)
         {
             timerZRot  += Time.deltaTime;
             timerXRot  += Time.deltaTime;
             timerYPos += Time.deltaTime;
            timerYRot += Time.deltaTime * 0.3f;
            timerZPos += Time.deltaTime * 0.56f;
         }  
         else
         {
             timerZRot = 0;
             timerXRot = 1f;
             timerYPos = 0.5f;
             timerYRot = 0.74f;
             timerZPos = 0.5f;
             timerXPos = 0f;

         }
         
         swaySettings.timerXRot = timerXRot;
         swaySettings.timerYPos = timerYPos;
         swaySettings.timerZRot = timerZRot;
         swaySettings.timerYRot = timerYRot;
         swaySettings.timerZPos = timerZPos;
         swaySettings.timerXPos = timerXPos;

         bool sprint = PlayerInput.playerInput.reqSprint;
         float zFreq = sprint ? swaySettings.sinZRotFreqSprint : swaySettings.sinZRotFreq;
         float xFreq = sprint ? swaySettings.sinXPosFreqSprint : swaySettings.sinXPosFreq;
         float xRotFreq = sprint ? swaySettings.sinXRotFreqSprint : swaySettings.sinXRotFreq;
         float yFreq = sprint ? swaySettings.sinYposFreqSprint : swaySettings.sinYposFreq;
         float yrFreq = sprint ? swaySettings.sinYRotFreqSprint : swaySettings.sinYRotFreq;
         float zPFreq = sprint ? swaySettings.sinZposFreqSprint : swaySettings.sinZPosFreq;
         
         float zMod = sprint ? swaySettings.sinZrModSprint : swaySettings.sinZrMod;
         float xRMod = sprint ? swaySettings.sinXrModSprint : swaySettings.sinXrMod;
         float xMod = sprint ? swaySettings.sinXpModSprint : swaySettings.sinXpMod;
         float yMod = sprint ? swaySettings.sinYpModSprint : swaySettings.sinYpMod;
         float yRMod = sprint ? swaySettings.sinYrModSprint : swaySettings.sinYrMod;
         float zPMod = sprint ? swaySettings.sinZpModSprint : swaySettings.sinZpMod;
         
         
         swaySettings.sinValZRot = Mathf.Sin(swaySettings.timerZRot * zFreq) * zMod ;
         swaySettings.sinValXPos = Mathf.Sin(swaySettings.timerXRot * xFreq) * xMod ;
         swaySettings.sinValYPos = Mathf.Sin(swaySettings.timerYPos * yFreq) * yMod ;
         swaySettings.sinValYRot = Mathf.Sin(swaySettings.timerYRot * yrFreq) * yRMod ;
         swaySettings.sinValZPos = Mathf.Sin(swaySettings.timerZPos * zPFreq) * zPMod ;
         swaySettings.sinValXRot = Mathf.Sin(swaySettings.timerXRot * xRotFreq) * xRMod ;
         
         swaySettings.sinValZPos *= mod * swaySettings.strengthPos;
         swaySettings.sinValXPos *= mod * swaySettings.strengthPos;
         swaySettings.sinValYPos *= mod * swaySettings.strengthPos;
         swaySettings.sinValYRot *= mod * swaySettings.strengthRot;
         swaySettings.sinValXRot *= mod * swaySettings.strengthRot;
         swaySettings.sinValZRot *= mod * swaySettings.strengthRot;
         
         Vector3 reqPos = new Vector3(swaySettings.sinValXPos, swaySettings.sinValYPos, swaySettings.sinValZPos);
         Vector3 reqRot = new Vector3(swaySettings.sinValXRot ,swaySettings.sinValYRot,swaySettings.sinValZRot);
         
         Vector3 posRes = positionWR.Update(Time.deltaTime, reqPos);
         Vector3 rotRes = rotationWR.Update(Time.deltaTime, reqRot);
         
         proceduralWalkRunLayer.transform.localPosition = posRes;
         proceduralWalkRunLayer.transform.localRotation = Quaternion.Euler(rotRes);
     }
}
