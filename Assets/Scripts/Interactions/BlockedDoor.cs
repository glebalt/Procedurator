using System;
using System.Collections;
using UnityEngine;

public class BlockedDoor : InteractionObject
{
  public AudioClip[] DoorBreaking;
  public AudioClip DoorFinal;
  public float timeNeeded;
  private float timer;
  public bool blocked;
  public Rigidbody plank;
  

  private void Start()
  {
    blocked = true;
  }

  IEnumerator Break(InteractionResultInfo result)
  {
    
    result.result = InteractionResult.Success;
    PlayerController.isControlledByPlayer = false;
    timer = 0;

    while (timer < timeNeeded)
    {
      timer += Time.deltaTime;
      yield return null;
    }
    blocked = false;
    PlayerController.isControlledByPlayer = true;
    UpdateObjectStateSuccess();
  }

  public void Update()
  {
    
  }

  void UpdateObjectStateSuccess()
  {
    plank.isKinematic = false;  
  }

  public override InteractionResultInfo Interact(InteractionResultInfo result)
  {    
    StartCoroutine(Break(result));
   
    return result;
  }
}
