using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Weapon sway data asset")]
public class WeaponSwaySettings: ScriptableObject
{

    public bool isEnabled;  
    [Range(0.1f, 10f)]
    public float freqIdle= 1f;
    [Range(-10f, 19f)]
    public float respIdle = 1f;
    [Range(0.1f, 1f)]
    public float dampIdle = 0.3f;
    
    [Range(0.1f, 10f)]
    public float freqWR= 1f;
    [Range(-10f, 19f)]
    public float respWR = 1f;
    [Range(0.1f, 1f)]
    public float dampWR = 0.3f;

    public float strengthPos;
    public float strengthRot;
    public float zPosModMouse;

    [Header("WeaponIdleSway")] 
    public float i_yPosIdleStr;
    public float i_zPosIdleStr;
    public float i_xRotIdleStr;
    public float i_yRotIdleStr;
    public float i_zRotIdleStr;
    public float zPosMin;
    public float zPosMax;
    [Header("Pos strength")]
    public float yPosStr;
    public float xRotStr;
    public float yRotStr;
    public float zRotStr;
    public float xPosStr;
    public float zPosStr;
    [Header("Procedural walk/idle")]
    public float timerZRot;
    public float timerXRot;
    public float timerXPos;
    public float timerYPos;
    public float timerYRot;
    public float  timerZPos;
    [Header("Walking")]
    public float sinYposFreq;
    public float sinXPosFreq;
    public float sinXRotFreq;
    public float sinZPosFreq;  
    public float sinZRotFreq;
    public float sinYRotFreq;
    public float sinYrMod;
    public float sinYpMod;
    public float sinZpMod;
    public float sinZrMod;
    public float sinXpMod;
    public float sinXrMod;
    [Header("Sprinting")]
    public float sinYposFreqSprint;
    public float sinZposFreqSprint;
    public float sinXPosFreqSprint;
    public float sinXRotFreqSprint;
    public float sinZRotFreqSprint;
    public float sinYRotFreqSprint;
    public float sinYpModSprint;
    public float sinZpModSprint;
    public float sinXpModSprint;
    public float sinZrModSprint;
    public float sinXrModSprint;
    
    public float sinYrModSprint;
    [Header("Final Values")]
    public float sinValZRot;
    public float sinValXRot;
    public float sinValYPos;
    public float sinValYRot;
    public float sinValZPos;
    public float sinValXPos;

}
