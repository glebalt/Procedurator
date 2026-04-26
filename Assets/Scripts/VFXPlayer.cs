using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayer : MonoBehaviour
{
    public VisualEffectAsset bloodAsset;
 
    private VisualEffect eff;
    public VisualEffectAsset barrelSmokeAsset;
    public VisualEffectAsset bulletTrailAsset;
    List<VisualEffect> effects = new List<VisualEffect>();
    private void Start()
    {
      gameObject.AddComponent<VisualEffect>();
    }

    public virtual void CallInit()
    {
        Debug.LogWarning("No implementation in child class!!!");
    }

    private void Update()
    {
       Tick();
    }

    public static Blood SpawnBlood(Vector3 pos,VisualEffectAsset asset)
    {
        GameObject gmb = new GameObject("Blood");
        gmb.AddComponent<Blood>();
        Blood blood = gmb.GetComponent<Blood>();
        gmb.AddComponent<VisualEffect>();
        blood.effect = gmb.GetComponent<VisualEffect>();
        blood.requestedPos = pos;
        blood.effect.visualEffectAsset = asset;
  
        return blood;
    }

   public VisualEffect BarrelSmoke()
   {
       GameObject gmb = new GameObject("BarrelSmoke");
       gmb.AddComponent<VisualEffect>();
       VisualEffect effect =  gmb.GetComponent<VisualEffect>();
        StartCoroutine(DestroyVFX(gmb));
       effect.visualEffectAsset = barrelSmokeAsset;
       return effect;
   }

   public VisualEffect BulletTrail()
   {
       GameObject gmb = new GameObject("Trail");
       gmb.AddComponent<VisualEffect>();
       VisualEffect trail =  gmb.GetComponent<VisualEffect>();
       effects.Add(trail);
       trail.visualEffectAsset = bulletTrailAsset;
       return trail;
   }

   
   void Tick()
   {
       foreach (VisualEffect effect in effects)
       {
           if (effect.aliveParticleCount < 1)
           {
               Destroy(effect.gameObject);
           }
       }
   }


   IEnumerator DestroyVFX(GameObject gmb)
   {
       yield return new WaitForSeconds(0.2f);
       Destroy(gmb);
   }

}


public class Blood : VFXPlayer
{
    public VisualEffect effect;
    public Vector3 requestedPos;
    IEnumerator Init()
    {
      
    
            transform.position = requestedPos;  
        effect.Play();
        
        while (effect.aliveParticleCount < 1)
        {
            yield return null;
        }
        
        
        while (effect.aliveParticleCount > 1)
        {
            yield return null;
        }
            
       Destroy(this.gameObject);
            
    }

    public void CallInit()
    {
        StartCoroutine(Init());
    }

      
}




