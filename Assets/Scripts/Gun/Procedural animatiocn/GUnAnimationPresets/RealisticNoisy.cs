using UnityEngine;
[CreateAssetMenu(menuName = "WeaponRecoilPresets/RealisticNoisy")]
public class RealisticNoisy : WeaponRecoilData
{
   
  
  private void Reset()
  {

    tickSpeed = 10f;
    freq = 1f;
    resp = 4f;
    damp = 0.568f;
    
    strengthMin = 3f;
    strengthMax = 4f;
    
    freqP = 1f;
    respP = 1f;
    dampP = 0.31f;
    //
    freqN = 1f;
    respN = 1f;
    dampN = 0.4f;
    
    freqD = 1f;
    respD = -10f;
    dampD = 0.2f;

    impulseRot = new(-2, -2, -2);
    impulsePos = new();
    
    impulseRotADS = new(-0.5f,-1,1);
    impulsePosADS = new(0,0,1);

    noiseImpulsePos = new(0.7f,0.5f,1);
    noiseImpulsePosADS = new(0.1f,0.1f,0.2f);
    noiseImpulseRot = new(2,1,5);
    noiseImpulseRotADS = new(0,0.5f,2);
    noiseMaxPos = new(0, 0,10f);
    
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
