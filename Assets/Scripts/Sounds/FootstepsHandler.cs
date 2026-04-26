using UnityEngine;

public class FootstepsHandler : MonoBehaviour
{
    public AudioClip[] Footsteps;
    public AudioSource FootstepsSource;
    public AudioClip[] ClothClips;
    public AudioSource ClothSource;
    private SoundsEmitter footSoundsEmitter;
    SoundsEmitter  clothSoundsEmitter;
    private SoundsEmitter footSoundsEmitterSprint;
    private SoundsEmitter clothSoundsEmitterSprint;

    [Header("Settings")]
    public float walkingBound;
    public float sprintBound;
    public float sprintMagnitudeLowerBound;
    public float walkMagnitudeLowerBound;
    public float current;
    void Start()
    {
      Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        
        current = PlayerController.CurrentVelMagnitude;
        if (PlayerController.CurrentVelMagnitude > walkMagnitudeLowerBound &&  PlayerController.CurrentVelMagnitude < sprintMagnitudeLowerBound)
        {
            print("walk");
            footSoundsEmitter.UpdateTimer(walkingBound);
            clothSoundsEmitter.UpdateTimer(walkingBound);
            footSoundsEmitter.PlayClip();
            clothSoundsEmitter.PlayClip();
        }
        else if (PlayerController.CurrentVelMagnitude > sprintMagnitudeLowerBound)
        {
            print("sprint");
            footSoundsEmitter.UpdateTimer(sprintBound);
            clothSoundsEmitter.UpdateTimer(sprintBound);
            footSoundsEmitter.PlayClip();
            clothSoundsEmitter.PlayClip();
        }
    }

    void Initialize()
    {
        footSoundsEmitter = new SoundsEmitter(FootstepsSource, Footsteps, walkingBound);
        clothSoundsEmitter = new SoundsEmitter(ClothSource, ClothClips, walkingBound);
        footSoundsEmitterSprint = new SoundsEmitter(FootstepsSource, Footsteps, sprintBound);
        clothSoundsEmitterSprint = new SoundsEmitter(ClothSource, ClothClips, sprintBound);
    }
}
