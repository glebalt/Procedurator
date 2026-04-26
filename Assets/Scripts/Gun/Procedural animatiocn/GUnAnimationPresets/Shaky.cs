using UnityEngine;
[CreateAssetMenu(menuName = "WeaponRecoilPresets/Shaky")]
public class Shaky : WeaponRecoilData
{
  
  
  private void Reset()
  {

    tickSpeed = 5f;
    
    freq = 6f;
    resp = -10f;
    damp = 0.3f;
    
    strengthMin = 0.1f;
    strengthMax = 1f;
    
    freqP = 3f;
    respP = 1.6f;
    dampP = 0.3f;
    
    freqN = 3f;
    respN = 3f;
    dampN = 0.15f;
    
    freqD = 5f;
    respD = -15f;
    dampD = 0.2f;

    impulseRot = new(-2, 0, 0);
    impulsePos = new(0,0.5f,0);
    
    impulseRotADS = new(-0.5f,0.3f,-0.5f);
    impulsePosADS = new(0,0,0);

    noiseImpulsePos = new();
    noiseImpulsePosADS = new();
    noiseImpulseRot = new(1,0.3f,1);
    noiseImpulseRotADS = new();
    
    dampRot = new(0,1,-1);
    dampRotAds = new(0,0.3f,-0.5f);
    dampError = 1f;
    
    
    
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
}
