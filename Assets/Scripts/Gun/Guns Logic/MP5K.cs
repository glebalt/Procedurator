using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class MP5K : GunAnimatorHandlerBase
{
    [SerializeField] private PlayerController playerController;
    public RaycastTarget raycastTarget;
    public Animator gunAnimator;
    public Animator handsAnimator;
    public WeaponSoundsPlayer soundsPlayer;
    public Shooter connectedShooter;
    
    public float timeSinceLastShot;
    public float timeBetweenShots;

    public override Animator HandsAnimator {get => handsAnimator;
        set { } }
    public override Animator GunAnimator { get => gunAnimator;
        set { }
    }
    [SerializeField] private CameraShaker shaker;
   
    public StanceChangeAnimationLayer stanceChangeLayer;
    public override VisualEffect MuzzleFlashEffect { get => muzzleFlash; set{}}
    public override WeaponSoundsPlayer SoundsPlayer { get; set; }
    public override Shooter ConnectedShooter { get; set; }
    public LayerMask layerMask;
    public override float TimeSinceLastShot { get => timeSinceLastShot;
        set { }
    }
    public VisualEffectAsset visualEffectBlood;
    public TextMeshProUGUI debugText;
    public ADS adsRef;
    public bool proceduralSos;
    public bool curve;
    public bool noise;
    public float dampOffset;

    [Header("VFX")]
    public VFXPlayer vfxRef;
    public Transform smokeSpawnPivot;
    public VisualEffect muzzleFlash;
    public float muzzleFlashSize;
    public VisualEffect bulletTrail;
    [Header("Reload Shake")] 
    public Vector3[] reloadShakeArr;
    public override void CallShoot()
    {
        shaker.CallShootRecoil();
        OnShoot();
        timeSinceLastShot = 0;
        raycastTarget.ShootTarget();
    }

    void OnShoot()
    {
        VisualEffect smoke =  vfxRef.BarrelSmoke();
        smoke.transform.position  = smokeSpawnPivot.position;
        smoke.transform.rotation =  smokeSpawnPivot.rotation;
        

        smoke.Play();
        muzzleFlash.SetFloat("Size", muzzleFlashSize);
        muzzleFlash.Play();
        bulletTrail.Play();
        CheckPlayAnimationLayers();
     
    }

   

    private void OnEnable()
    {
        playerController.OnStanceChange += OnStateChanged;
    }

    private void OnDisable()
    {
        playerController.OnStanceChange -= OnStateChanged;
    }

    private void Start()
    {
     
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
            if (adsRef.isADS)
            {
                connectedShooter.CallNoiseShootADS();
            }
            else
            {
                connectedShooter.CallNoiseShoot();
            }
      
        }

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
    public void CallCameraReloadShake(int num)
    {
        shaker.CallReloadShake(reloadShakeArr[num]);
    }

    public override void CallReload()
    {
        StartCoroutine(Reload());
    }

    public override void CallEquip()
    {
        handsAnimator.Rebind();
        gunAnimator.Rebind();
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
