using UnityEngine;
[CreateAssetMenu(menuName = "WeaponRecoilPresets/RealisticSimple")]
public class RealisticSimplePreset : WeaponRecoilData
{
  
  
  private void Reset()
  {

    tickSpeed = 10f;
    freq = 1f;
    resp = 4f;
    damp = 0.568f;
    
    strengthMin = 0.8f;
    strengthMax = 1f;
    
    freqP = 1f;
    respP = 1f;
    dampP = 0.728f;
    
    freqN = 1f;
    respN = 1f;
    dampN = 0.3f;
    
    freqD = 1f;
    respD = 1f;
    dampD = 0.3f;

    impulseRot = new(1, 1, 1);
    impulsePos = new();
    
    impulseRotADS = new(0,-1,1);
    impulsePosADS = new(0,0,1);

    noiseImpulsePos = new(0.2f,0,0);
    noiseImpulsePosADS = new();
    noiseImpulseRot = new(2,1,5);
    noiseImpulseRotADS = new(0,0.5f,2);
    
    dampRot = new(0,2,0);
    dampRotAds = new(0,1,0);
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
