using System;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Weapon recoil data")]
public class WeaponRecoilData : ScriptableObject
{
  [Header("IDLE curves regeneration values")]
  [SerializeField] private float rotXSer = 0f;
  [SerializeField] private float rotYSer = 0f;
  [SerializeField] private float rotZSer = 0f;

  [SerializeField] private float posXSer = 0f;
  [SerializeField] private float posYSer = 0f;
  [SerializeField] private float posZSer = 0f;
  [Header("ADS curves regeneration values")]
  [SerializeField] private float rotXAdsSer= 0f;
  [SerializeField] private float rotYAdsSer= 0f;
  [SerializeField] private float rotZAdsSer= 0f;
  
  [SerializeField] private float posXAdsSer = 0f;
  [SerializeField] private float posYAdsSer = 0f;
  [SerializeField] private float posZAdsSer = 0f;
  
  public AnimationCurve rotX;
  public AnimationCurve rotY;
  public AnimationCurve rotZ;

  public AnimationCurve posX;
  public AnimationCurve posY;
  public AnimationCurve posZ;
  
  public AnimationCurve rotXads;
  public AnimationCurve rotYads;
  public AnimationCurve rotZads;

  public AnimationCurve posXads;
  public AnimationCurve posYads;
  public AnimationCurve posZads;

  public AnimationCurve pushbackZ;
  public float tickSpeed;
  [Header("Procedural Rot")]
  [Range(0.1f, 10f)]
  public float freq = 1f;
  [Range(-10f, 20f)]
  public float resp = 1f;
  [Range(0.1f, 1f)]
  public float damp = 0.3f;

  public float strengthMin = 1f;
  [Range(.2f, 5f)]
  public float strengthMax = 2f;
  
  [Header("Procedural Pos")]
  [Range(0.1f, 10f)]
  public float freqP = 1f;
  [Range(0.1f, 4f)]
  public float respP = 1f;
  [Range(0.1f, 1f)]
  ////
  /// ///
  public float dampP = 0.3f;
  [Header("Procedural Noise")]
  [Range(0.1f, 10f)]
  public float freqN = 1f;
  [Range(0.1f, 4f)]
  public float respN = 1f;
  [Range(0.1f, 1f)]
  public float dampN = 0.3f;
  
  [Header("Procedural Damp")]
  [Range(0.1f, 10f)]
  public float freqD = 1f;
  [Range(-15f, 15f)]
  public float respD = 1f;
  [Range(0.1f, 1f)]
  public float dampD = 0.3f;


  
  [Header("POS/ROT idle")]
  public Vector3 impulseRot;
  public Vector3 impulsePos;
  public Vector3 debugRot;
  [Header("POS/ROT Ads")]
  public Vector3 impulseRotADS;
  public Vector3 impulsePosADS;
  public Vector3 debugRotADS;
  [Header("Additional")]
  public Vector3 noiseImpulsePos;
  public Vector3 noiseImpulsePosADS;
  public Vector3 noiseImpulseRot;
  public Vector3 noiseImpulseRotADS;
  public Vector3 noiseMaxPos;
  [Header("Damp rot")]
  public Vector3 dampRot;
  public Vector3 dampRotAds;
  public float dampError = 1f;
  public int noiseRoundTo = 1;
  
  private void Reset()
  {
    rotX = CurveCreator.RecoilLike(-2f);
    rotY = CurveCreator.RecoilLike();
    rotZ = CurveCreator.RecoilLike();
    posX = CurveCreator.RecoilLike(0.1f);
    posY = CurveCreator.RecoilLike();
    posZ = CurveCreator.RecoilLike(-0.5f);
    
    rotXads = CurveCreator.RecoilLike(-1f);
    rotYads = CurveCreator.RecoilLike(0); 
    rotZads = CurveCreator.RecoilLike(0); 
    
    posXads = CurveCreator.RecoilLike(0);
    posYads = CurveCreator.RecoilLike(0);
    posZads = CurveCreator.RecoilLike(-0.1f); 
  }
  
#if UNITY_EDITOR
  [ContextMenu("Regenerate Curves")]
  private void Regenerate()
  {
    rotX = CurveCreator.RecoilLike(rotXSer);
    rotY =  CurveCreator.RecoilLike(rotYSer);
    rotZ = CurveCreator.RecoilLike(rotZSer);
    
    posX = CurveCreator.RecoilLike(posXSer);
    posY = CurveCreator.RecoilLike(posYSer);
    posZ = CurveCreator.RecoilLike(posZSer);
    
    rotXads = CurveCreator.RecoilLike(rotXAdsSer);
    rotYads = CurveCreator.RecoilLike(rotYAdsSer);
    rotZads = CurveCreator.RecoilLike(rotZAdsSer);
    
    posXads = CurveCreator.RecoilLike(posXAdsSer);
    posYads = CurveCreator.RecoilLike(posYAdsSer);
    posZads =  CurveCreator.RecoilLike(posZAdsSer);
      
  }
#endif

}


public class CurveCreator
{
  public static AnimationCurve RecoilLike()
  {
    AnimationCurve curve = new AnimationCurve(new Keyframe(0f,0f),
      new Keyframe(0.05f,-2f),
      new Keyframe(0.1f,0f),
      new Keyframe(0.15f,1f),
      new Keyframe(0.35f,-0.35f),
      new Keyframe(0.65f,0.1f),
      new Keyframe(1f,0f));

    for (int i = 0; i < curve.length; i++)
    {
      AnimationUtility.SetKeyLeftTangentMode(curve, i, AnimationUtility.TangentMode.ClampedAuto);
      AnimationUtility.SetKeyRightTangentMode(curve, i, AnimationUtility.TangentMode.ClampedAuto);
    }
    
    return curve;
  }




  
  public static AnimationCurve RecoilLike(float impulse)
  {
    AnimationCurve curve = new AnimationCurve(new Keyframe(0f,0f),
      new Keyframe(0.05f,impulse),
      new Keyframe(0.1f,0f),
      new Keyframe(0.15f,-impulse / 10f),
      new Keyframe(0.35f,impulse / 50f),
      new Keyframe(0.55f,-impulse / 50f),
      new Keyframe(0.75f,0f));

    for (int i = 0; i < curve.length; i++)
    {
      AnimationUtility.SetKeyLeftTangentMode(curve, i, AnimationUtility.TangentMode.ClampedAuto);
      AnimationUtility.SetKeyRightTangentMode(curve, i, AnimationUtility.TangentMode.ClampedAuto);
    }
    
    return curve;
  }
}