using System.Collections;
using ProceduralFPS;
using UnityEngine;

public class StanceChangeAnimationLayer : MonoBehaviour
{
    private SecondOrderSystem sosPos = new SecondOrderSystem();
    
    private SecondOrderSystem sosRot = new SecondOrderSystem();
    
    private SecondOrderSystem.Settings settings =  new SecondOrderSystem.Settings();

    [Header("Settings SOS")] 
    public float damp;
    public float resp;
    public float freq;
    [Header("Medium Shake")]
    public Vector3 posImpluseMedium;
    public Vector3 rotImpulseMedium;
    public Vector3 posImpluseHeavy;
    public Vector3 rotImpulseHeavy;
    
    private Coroutine currentCoroutine;
    private Vector3 currentRot;
    private Vector3 currentPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        settings.damping = damp;
        settings.response = resp;
        settings.frequency = freq;
        Tick();
    }

    IEnumerator MediumShake()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        currentCoroutine = null;
     
    }

    public void CallMediumShakeDown()
    {
        currentPos += posImpluseMedium;
        currentRot += rotImpulseMedium;

        if (currentCoroutine == null)
        {
            StartCoroutine(MediumShake());
        }
     
    }
    
    public void CallHeavyShakeUp()
    {
        currentPos += posImpluseHeavy;
        currentRot += rotImpulseHeavy;

        if (currentCoroutine == null)
        {
            StartCoroutine(MediumShake());
        }
     
    }
    
    

    public void Tick()
    {
       sosPos.UpdateSettings(settings);
       sosRot.UpdateSettings(settings);
        
        currentPos =Vector3.Lerp(currentPos, Vector3.zero, Time.deltaTime * 5f);
      currentRot =  Vector3.Lerp(currentRot, Vector3.zero, Time.deltaTime * 5f);
        
        
        Vector3 resPos  = sosPos.Update(Time.deltaTime,currentPos);
        Vector3 resRot = sosRot.Update(Time.deltaTime,currentRot);
        
        transform.localPosition = resPos;
        transform.localRotation = Quaternion.Euler(resRot);
        
      
    }
}
