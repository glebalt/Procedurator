using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class FN : GunAnimatorHandlerBase
{





    [Header("References")] 
    [SerializeField] private PlayerController playerController;
    public RaycastTarget  raycastTarget;    
    [SerializeField] private CameraShaker shaker;
    [SerializeField] private WeaponSoundsPlayer soundsPlayer;
    [SerializeField] private Shooter connectedShooter;
    [SerializeField] private Animator handsAnimator;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float timeSinceLastShot;
    [SerializeField] private ADS adsRef;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private VisualEffect bulletTrail;
         [Header("Animation layers settings")]
     [SerializeField] private bool proceduralSos;
     [SerializeField] private bool curve;
     [SerializeField] private bool noise;

   
     public StanceChangeAnimationLayer stanceChangeLayer;
     [Header("VFX settings")] 
     public VFXPlayer vfxRef;
     public Transform smokeSpawnPivot;
     public float size;
     [Header("Reload Shake")] 
     public Vector3[] reloadShakeArr;

     public float dampOffset;

     public float smokeSize;
    public override Animator HandsAnimator {get => handsAnimator;
        set { } }
    
    public override Animator GunAnimator { get => gunAnimator;
        set { }
    }
    
    public override VisualEffect MuzzleFlashEffect { get => muzzleFlash; set{}}
    
    public override float TimeSinceLastShot { get => timeSinceLastShot;
        set { }
    }
    
    public override Shooter ConnectedShooter { get => connectedShooter;
        set { }
    }
    
    public override WeaponSoundsPlayer SoundsPlayer { get => soundsPlayer;
        set { }
    }


    private void OnEnable()
    {
        playerController.OnStanceChange += OnStateChanged;
    }

    private void OnDisable()
    {
        playerController.OnStanceChange -= OnStateChanged;
    }

    public override void CallShoot()
    {
        OnShoot();
        timeSinceLastShot = 0;
        shaker.CallShootRecoil();
        raycastTarget.ShootTarget();
    }

    void OnShoot()
    {
      VisualEffect smoke =  vfxRef.BarrelSmoke();
      smoke.transform.position  = smokeSpawnPivot.position;
      smoke.transform.rotation =  smokeSpawnPivot.rotation;
        

      smoke.Play();
        muzzleFlash.SetFloat("Size", size);
       muzzleFlash.Play();
       bulletTrail.Play();
        CheckPlayAnimationLayers();
    }

    void CheckPlayAnimationLayers()
    {
        if (curve)
        {
            if (adsRef.isADS)
            {
                connectedShooter.CallShootCurveAds();
            }
            else
            {
                connectedShooter.CallShootCurve();
            }
        }
        
        soundsPlayer.PlayShot();
        if (proceduralSos)
        {
            connectedShooter.CallShoot();
        }

        if (noise)
        {
            if (!adsRef.isADS)
            {
                connectedShooter.CallNoiseShoot();
            }
            else
            {
                connectedShooter.CallNoiseShootADS();
            }
           
        }

    }

    public override void CallReload()
    {
        StartCoroutine(Reload());
    }
    
    public void CallCameraReloadShake(int num)
    {
        shaker.CallReloadShake(reloadShakeArr[num]);
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

    public override void CallEquip()
    {
        handsAnimator.Rebind();
        gunAnimator.Rebind();
    }

    void OnStateChanged(PlayerController.Stance stance,PlayerController.Stance lastStance)
    {
        if ((stance == PlayerController.Stance.Idle || stance == PlayerController.Stance.Walking) &&
            lastStance == PlayerController.Stance.Running)
        {
            print("Run to idle");
            stanceChangeLayer.CallMediumShakeDown();
        }
        else if ((lastStance == PlayerController.Stance.Idle || lastStance == PlayerController.Stance.Walking) &&
                 stance == PlayerController.Stance.Running)
        {
            print("Idle to run");
            stanceChangeLayer.CallHeavyShakeUp();
        }
        else
        {
            //if walking => idle || idle => walking do nothing
        }
        
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

        if ( PlayerInput.playerInput.reqSprint)
        {
            handsAnimator.SetBool("Sprint", true);
        }
        else
        {
            handsAnimator.SetBool("Sprint", false);
        }
        
        if (PlayerInput.gunInput.reqShootAuto && handsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && timeSinceLastShot > timeBetweenShots)
        {
            
            CallShoot();
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
            PlayerInput.gunInput.reqShoot = false;
        }
        
        if (!PlayerInput.gunInput.holdingLMB)
        {
            if (timeSinceLastShot > dampOffset)
            {
               
                connectedShooter.CallDamp(adsRef.isADS);
            }
        }
    }
}
