using UnityEngine;

public class ProceduralAnimationPreset : ScriptableObject
{
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
    [Range(1f, 5f)]
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

}
