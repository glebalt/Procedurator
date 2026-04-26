using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SV98 : GunAnimatorHandlerBase
{
    public Transform wepRef;
    public Transform leftHand;
    public Transform rightHand;


    public Animator gunAnimator;
    public Animator handsAnimator;
    public WeaponSoundsPlayer soundsPlayer;
    public Shooter connectedShooter;
    
    public override Animator GunAnimator
    {
        get { return gunAnimator;}
        set {}
    }

    public override VisualEffect MuzzleFlashEffect { get; set; }

    public override Animator HandsAnimator
    {
        get { return handsAnimator;} set{} }

    public override Shooter ConnectedShooter
    {
        get { return connectedShooter;} set{} }

    public override float TimeSinceLastShot
    {
        get; set;
    }

    public override WeaponSoundsPlayer SoundsPlayer
    {
        get { return soundsPlayer;} set{} }

  

   public override void CallEquip()
   {
       handsAnimator.Rebind();
       gunAnimator.Rebind();
   }

   public override void CallReload()
   {
       StartCoroutine(Reload());
   }
   
   private void ChangeRoot(Transform parent)
    {
        wepRef.parent = parent;
    }

  

    IEnumerator Reload()
    {
        handsAnimator.SetTrigger("Reload");
        gunAnimator.SetTrigger("Reload");
        while (!handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            yield  return null;
        }
        
        while (handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            yield  return null;
        }
        
       
    }

    public void CallBoltReload()
    {
        if (handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            StartCoroutine(BoltReload());
        }
  
    }

    

    IEnumerator BoltReload()
    {
      
        ChangeRoot(leftHand);
        handsAnimator.SetTrigger("BoltReload");
        gunAnimator.SetTrigger("BoltReload");
        
        while (!handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("BoltReload"))
        {
            yield  return null;
        }
        
        while (handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("BoltReload"))
        {
            yield  return null;
        }
        
     
        ChangeRoot(rightHand);
    }


    public override  void CallShoot()
    {
            soundsPlayer.PlayShot();
            StartCoroutine(Shoot());
    }

    public void Shake()
    {
        connectedShooter.CallShoot();
    }

    IEnumerator Shoot()
    {
        handsAnimator.SetTrigger("Shoot");
        while (!handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
          yield return null;
        }
    
        
        while (handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            yield return null;
        }
        
        CallBoltReload();
    }
    
    public override void UpdateAnimations()
    {
        if (PlayerInput.gunInput.reqReload && handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            CallReload();
        }
        else
        {
            PlayerInput.gunInput.reqReload = false;
        }
        
        if (PlayerInput.gunInput.reqShoot && handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
         
            CallShoot();
        }
        else
        {
            PlayerInput.gunInput.reqShoot = false;
        }
    }
 

   
}
