using UnityEngine;
using UnityEngine.VFX;

public abstract class GunAnimatorHandlerBase : MonoBehaviour
{
   public abstract Animator HandsAnimator { get; set; }
   public abstract Animator GunAnimator { get; set; }
   
   public abstract VisualEffect MuzzleFlashEffect { get; set; }
   public abstract WeaponSoundsPlayer SoundsPlayer { get; set; }
   
   public abstract Shooter ConnectedShooter { get; set; }
   public abstract float TimeSinceLastShot { get; set; }

   public abstract void CallShoot();
   
   public abstract void CallReload();

   public abstract void CallEquip();

   public abstract void UpdateAnimations();

   
}
