using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Weapon sound data asset")]
public class WeaponSoundsData : ScriptableObject
{
    public AudioClip magIn;
    public AudioClip magOut;
    public AudioClip[] reload;
    public AudioClip[] equip;
    public float reloadVoulume;
    public AudioClip armMovement1;
    public AudioClip armMovement2;

    public AudioClip boltPull;
    public AudioClip boltRelease;
    
    public AudioClip[] bulletEject;

    public AudioClip[] shotsCore;
    public AudioClip[] shotsTail;
    public AudioClip shotMech;
    public AudioClip shotBass;
}
