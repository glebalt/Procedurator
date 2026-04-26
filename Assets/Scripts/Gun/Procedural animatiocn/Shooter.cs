using System;
using System.Collections;
using System.Collections.Generic;
using ProceduralFPS;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    private SecondOrderSystem shootRot = new  SecondOrderSystem();
    
    private SecondOrderSystem shootPos = new  SecondOrderSystem();
    private SecondOrderSystem shootNoisePos = new  SecondOrderSystem();
    private SecondOrderSystem shootNoiseRot = new  SecondOrderSystem();
    private SecondOrderSystem shootDamp = new  SecondOrderSystem();
    
    public AnimationCurve rotXCurve;
    public List<AnimationCurve> curves = new List<AnimationCurve>();
    private float length;

    public WeaponRecoilData recoilData;
    private bool isBusy;
    private float timer;
    public HandsAnimator connectedAnimator;
    public Transform noiseLayer;
    public Transform curveLayer;
    public Transform dampLayer;
    
   
    
    public Vector3 rot;
    public Vector3 temoRotRes;
    private Vector3 noisePos;
    private Vector3 noiseRot;
    
    private Vector3 dampRot;
    private Vector3 noisePosTarget;
    private Vector3 noiseRotTarget;
    private Vector3 dampTarget;
    public ADS adsRef;
    private Vector3 pos;
    public float timeBetweenShots = 0.5f;
    public float tickSpeed;
   public GunAnimatorHandlerBase connectedWeapon;
    private Coroutine curveLayerCoroutine;

    private Vector3 currentRecoilPos;
    private Vector3 currentRecoilRot;

    private float denominator;
    [Range(0.01f,2f)]
    public float procRecoilFactor;
    [Range(0.01f,2f)]
    public float curveRecoilFactor;

    private bool isDampCalled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        denominator = 1;
         noisePosTarget = Vector3.zero;
         
        noisePos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
        Tick();
        TickDamp();
      
    }

    public bool CallShoot()
    {
        isDampCalled = false;
        if (!isBusy)
        {
      
            recoilData.impulseRotADS.y *= -1;
            recoilData.impulseRotADS.z *= -1;
            recoilData.impulsePosADS.x *= -1;
            
            recoilData.impulseRot.y *= -1;
            recoilData.impulseRot.z *= -1;
            recoilData.impulsePos.x *= -1;

           
            
            if (adsRef.isADS)
            {
                currentRecoilPos = new Vector3(GetRandomValue(recoilData.impulsePosADS.x),
                    Random.Range(recoilData.impulsePosADS.y /2f,recoilData.impulsePosADS.y),Random.Range(recoilData.impulsePosADS.z /2f,recoilData.impulsePosADS.z));
            
                currentRecoilRot = new Vector3(recoilData.impulseRotADS.x,
                    GetRandomValue(recoilData.impulseRotADS.y), GetRandomValue(recoilData.impulseRotADS.z));
                currentRecoilPos *= procRecoilFactor;
                currentRecoilRot *= procRecoilFactor;
                
                if (recoilData.impulseRotADS.y * currentRecoilRot.y < 0)
                {
                    currentRecoilRot.y *= -1;
                }
                if (recoilData.impulseRotADS.z * currentRecoilRot.z < 0)
                {
                    currentRecoilRot.z *= -1;
                }


                
                recoilData.debugRotADS = currentRecoilRot;
                rot += currentRecoilRot;
                pos += currentRecoilPos;
            }
            else
            {
                currentRecoilPos = new Vector3(GetRandomValue(recoilData.impulsePos.x),
                    Random.Range(recoilData.impulsePos.y /2f,recoilData.impulsePos.y), Random.Range(recoilData.impulsePos.z /2f,recoilData.impulsePos.z));
            
                currentRecoilRot = new Vector3(recoilData.impulseRot.x,
                    GetRandomValue(recoilData.impulseRot.y), GetRandomValue(recoilData.impulseRot.z));

                currentRecoilPos *= procRecoilFactor;
                currentRecoilRot *= procRecoilFactor;

                if (recoilData.impulseRot.y * currentRecoilRot.y < 0)
                {
                    currentRecoilRot.y *= -1;
                }
                if (recoilData.impulseRot.z * currentRecoilRot.z < 0)
                {
                    currentRecoilRot.z *= -1;
                }
             
             
                recoilData.debugRot = currentRecoilRot;
                

                rot += currentRecoilRot;
                pos += currentRecoilPos;
            }
           
            StartCoroutine(Shoot());
            return true;
        }
        return false;
    }

  

    public void CallNoiseShoot()
    {
        shootNoisePos.UpdateSettings(new SecondOrderSystem.Settings{response = recoilData.respN,frequency = recoilData.freqN,damping = recoilData.dampN});
        shootNoiseRot.UpdateSettings(new SecondOrderSystem.Settings{response = recoilData.respN,frequency = recoilData.freqN,damping = recoilData.dampN});
      
      
            noisePosTarget = new Vector3(GetRandomValue(recoilData.noiseImpulsePos.x),
                GetRandomValue(recoilData.noiseImpulsePos.y),
                recoilData.pushbackZ.Evaluate(connectedWeapon.TimeSinceLastShot > .5 ? -5 : connectedWeapon.TimeSinceLastShot ));

            noiseRotTarget = new Vector3(GetRandomValue(recoilData.noiseImpulseRot.x),
                GetRandomValue(recoilData.noiseImpulseRot.y), GetRandomValue(recoilData.noiseImpulseRot.z));
            
            noiseRotTarget *=  procRecoilFactor;
            noisePosTarget *= procRecoilFactor;
            
            noisePos += noisePosTarget;
            noiseRot += noiseRotTarget;
            
    }
    
    public void CallNoiseShootADS()
    {
        shootNoisePos.UpdateSettings(new SecondOrderSystem.Settings{response = recoilData.respN,frequency = recoilData.freqN,damping = recoilData.dampN});
        shootNoiseRot.UpdateSettings(new SecondOrderSystem.Settings{response = recoilData.respN,frequency = recoilData.freqN,damping = recoilData.dampN});
        
        noisePosTarget = new Vector3(GetRandomValue(recoilData.noiseImpulsePosADS.x),
            GetRandomValue(recoilData.noiseImpulsePosADS.y),   GetRandomValue(recoilData.noiseImpulsePosADS.z) );
        noiseRotTarget = new Vector3(GetRandomValue(recoilData.noiseImpulseRotADS.x),
            GetRandomValue(recoilData.noiseImpulseRotADS.y), GetRandomValue(recoilData.noiseImpulseRotADS.z));
        
        noiseRotTarget *=  procRecoilFactor;
        noisePosTarget *= procRecoilFactor;
            
        noisePos += noisePosTarget;
        noiseRot += noiseRotTarget;
    }

    public void CallDamp(bool ads)
    {
        if (isDampCalled) return;
        isDampCalled = true;
        print("DAMP CALLED");
        bool lessThanZeroY = currentRecoilRot.y > 0;
        bool lessThanZeroZ =currentRecoilRot.z> 0;
        shootDamp.UpdateSettings(new SecondOrderSystem.Settings{response = recoilData.respD,frequency = recoilData.freqD,damping = recoilData.dampD});
        if (ads)
        {
            float finalY = recoilData.dampRotAds.y;
            float finalZ = recoilData.dampRotAds.z;
            if (lessThanZeroY)
            {
                finalY = recoilData.dampRotAds.y * -1;
            }
            else
            {
                finalY = recoilData.dampRotAds.y;
            }
            
            if (lessThanZeroZ)
            {
                finalZ = recoilData.dampRotAds.z * -1;
            }
            else
            {
                finalZ = recoilData.dampRotAds.z ;
            }
            
       
            
            dampRot +=  new Vector3(recoilData.dampRotAds.x,finalY,finalZ) * recoilData.dampError * procRecoilFactor;
        }
        else
        {
            
            float finalY = recoilData.dampRot.y;
            float finalZ = recoilData.dampRot.z;
            if (lessThanZeroY)
            {
                finalY = recoilData.dampRot.y * -1;
            }
            else
            {
                finalY = recoilData.dampRot.y;
            }
            
            if (lessThanZeroZ)
            {
                finalZ = recoilData.dampRot.z * -1;
            }
            else
            {
                finalZ = recoilData.dampRot.z ;
            }
           
          
           
            dampRot +=  new Vector3(recoilData.dampRot.x,finalY,finalZ) * recoilData.dampError * procRecoilFactor;
        }
        
    
            
        
      

    }
    
  


  


    public void CallShootCurve()
    {
        if (curveLayerCoroutine != null)
        {
            StopCoroutine(curveLayerCoroutine);
        }
       
        curveLayerCoroutine = StartCoroutine(ShootCurve());
    }
    
    public void CallShootCurveAds()
    {
        if (curveLayerCoroutine != null)
        {
            StopCoroutine(curveLayerCoroutine);
        }
       
        curveLayerCoroutine = StartCoroutine(ShootCurveADS());
    }
    

    IEnumerator ShootCurve()
    {
        float timer = 0;
       
        while (timer < rotXCurve.length)
        {
            Vector3 targetPos = new Vector3(recoilData.posX.Evaluate(timer),0,recoilData.posZ.Evaluate(timer));
            Vector3 targetRot = new Vector3(recoilData.rotX.Evaluate(timer), 0, recoilData.rotZ.Evaluate(timer));
            
            targetRot *= curveRecoilFactor;
            targetPos *= curveRecoilFactor;
            
            curveLayer.transform.localRotation = Quaternion.Euler(targetRot);
            curveLayer.transform.localPosition = targetPos;
            timer += Time.deltaTime;
            yield return null;
        }
       
    }
    
    IEnumerator ShootCurveADS()
    {
        float timer = 0;
       
        while (timer < rotXCurve.length)
        {
            Vector3 targetPos = new Vector3(recoilData.posXads.Evaluate(timer),0,recoilData.posZads.Evaluate(timer));
            Vector3 targetRot = new Vector3(recoilData.rotXads.Evaluate(timer), 0, recoilData.rotZads.Evaluate(timer));

            targetRot *= curveRecoilFactor;
            targetPos *= curveRecoilFactor;
            
            curveLayer.transform.localRotation = Quaternion.Euler(targetRot);
            curveLayer.transform.localPosition = targetPos;
            timer += Time.deltaTime;
            yield return null;
        }
       
    }

    IEnumerator Shoot()
    {
        
        isBusy = true;
        timer = 0;
        shootRot.UpdateSettings(new SecondOrderSystem.Settings{frequency = recoilData.freq,damping = recoilData.damp,response =recoilData.resp});
        shootPos.UpdateSettings(new SecondOrderSystem.Settings{frequency = recoilData.freqP,damping = recoilData.dampP,response =recoilData.respP});
        while (timer < timeBetweenShots)
        {
          
            timer += Time.deltaTime;
          
            yield return null;
        }
        isBusy = false;
        
     
    }

 

    public void Tick()
    {   
      
        
            noisePos =  Vector3.Lerp(noisePos,Vector3.zero,Time.deltaTime * recoilData.tickSpeed);
          noiseRot = Vector3.Lerp(noiseRot,Vector3.zero,Time.deltaTime * recoilData.tickSpeed);
       
            rot = Vector3.Lerp(rot,Vector3.zero,Time.deltaTime * recoilData.tickSpeed);
            pos = Vector3.Lerp(pos,Vector3.zero,Time.deltaTime * recoilData.tickSpeed);
            
            
            Vector3 noiseRes = shootNoisePos.Update(Time.deltaTime, noisePos);
            Vector3 noiseRotRes = shootNoiseRot.Update(Time.deltaTime, noiseRot);
            
        temoRotRes = shootRot.Update(Time.deltaTime,rot);
        Vector3 posRes = shootPos.Update(Time.deltaTime,pos);   
        
            noiseLayer.localPosition = noiseRes;
            noiseLayer.localRotation = Quaternion.Euler(noiseRotRes);
            
        transform.localRotation = Quaternion.Euler(temoRotRes);
       transform.localPosition = posRes;
    }


    public void TickDamp()
    {
        dampRot = Vector3.Lerp(dampRot,Vector3.zero,Time.deltaTime * recoilData.tickSpeed);
        Vector3 dampResRot = shootDamp.Update(Time.deltaTime, dampRot);
        dampLayer.localRotation = Quaternion.Euler(dampResRot);
    }

    float GetRandomValue(float range)
    {
        float oneThird =range / 3f;
        float val =  Random.Range(-oneThird, oneThird);

        if (val >= 0)
        {
            val +=  oneThird;
        }
        else
        {
            val -=  oneThird;
        }

        val *= Random.Range(recoilData.strengthMin, recoilData.strengthMax);
        val = Mathf.Clamp(val, -range, range);

        
        
        return (float)Math.Round(val ,recoilData.noiseRoundTo);
    }
}
