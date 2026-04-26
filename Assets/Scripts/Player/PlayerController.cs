using System;
using KinematicCharacterController;
using TMPro;
using UnityEngine;



public class PlayerController : MonoBehaviour,ICharacterController
{
    public PlayerCamera playerCamera;
    public KinematicCharacterMotor motor;

    public TextMeshProUGUI speedDebug;
    public TextMeshProUGUI stanceDebug;
    [Header("Movement settings")]
    [Range(0f, 30f)]
    public float velocityChangeSharpnessGround;
    [Range(0f, 30f)]
    public float velocityChangeStopingSharpnessGround;
    public float moveSpeed;
    public float sprintSpeed;
    public static bool isControlledByPlayer;
    private static float currentVel;
    public static float CurrentVelMagnitude => currentVel;

    public float stanceTimer;
    [Range(0f, 1.5f)]
    public float timeSinceLastStanceTick;
   public enum Stance
    {
        Idle,Walking,Running
    }
    
    public Stance stance;
    public Stance stanceLastFrame;
  
    
    public Action<Stance,Stance> OnStanceChange;
    private void Start()
    {
        stance = stanceLastFrame = Stance.Idle;
       motor.CharacterController = this;
       isControlledByPlayer = true;
    }

    public void PostGroundingUpdate(float deltaTime)
    {
        
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        print("SEXY AND I KNMOW IT");
    }   

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
      
      Vector3 moveVec =  CreateMovementVector();
      float dot = CheckDotProduct(moveVec);
      speedDebug.text = currentVelocity.magnitude.ToString();
      float speed;
     
      if (dot < 0.7f || !PlayerInput.playerInput.reqSprint )
      {
          stance = Stance.Walking;
          speed = moveSpeed;
      }
      else
      {
          speed = sprintSpeed;
          stance = Stance.Running;
      }
      if (Mathf.Abs(PlayerInput.playerInput.keyboardZ) < 0.1f &&Mathf.Abs(PlayerInput.playerInput.keyboardX ) < 0.1f )
      {
          currentVelocity = Vector3.Lerp(currentVelocity, moveVec * speed, 1f - Mathf.Exp(-velocityChangeStopingSharpnessGround * deltaTime) );
          stance = Stance.Idle;
      }

      else
      {
          currentVelocity = Vector3.Lerp(currentVelocity, moveVec * speed, 1f - Mathf.Exp(-velocityChangeSharpnessGround * deltaTime) );
      }
      currentVel = currentVelocity.magnitude;
      
     CheckUpdateStance();
     
    }

    void CheckUpdateStance()
    {
        stanceTimer += Time.deltaTime;
        
        
        if (stance != stanceLastFrame)
        {
            print("STATE CHANGED");
            
            if (stanceTimer > timeSinceLastStanceTick)
            {
                stanceTimer = 0;
                OnStanceChange.Invoke(stance,stanceLastFrame);
                stanceLastFrame = stance;
            }
      
        }
    }

  

    Vector3 CreateMovementVector()
    {

        if (!isControlledByPlayer)
        {
            return Vector3.zero;
        }
        
        Vector3 move = playerCamera.transform.forward * PlayerInput.playerInput.keyboardZ +  playerCamera.transform.right * (PlayerInput.playerInput.keyboardX * 0.8f);
        if (move.sqrMagnitude > 1)
        {
        
            return move.normalized;
        }
        
        return move;
    }

    float CheckDotProduct(Vector3 movementVector)
    {
        Vector3 forward = playerCamera.transform.forward;
        float res = Vector3.Dot(forward.normalized, movementVector.normalized);
        return res;
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
   
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
      
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
   
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
        ref HitStabilityReport hitStabilityReport)
    {
     
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition,
        Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
      
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
     
    }
}
