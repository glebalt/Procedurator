using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSoundsPlayer : MonoBehaviour
{
    public WeaponSoundsData soundData;
    public AudioSource layer1;
    public AudioSource layer2;
    public AudioSource layer3;
    public AudioSource layer4;
    public float volume;
    
    public List<AudioSource> sources;
    private void Start()
    {
        layer1 = gameObject.AddComponent<AudioSource>();
        layer2 = gameObject.AddComponent<AudioSource>();
        layer3 = gameObject.AddComponent<AudioSource>();
        layer4 = gameObject.AddComponent<AudioSource>();
        sources.Add(layer1);
        sources.Add(layer2);
        sources.Add(layer3);
        sources.Add(layer4);

      
    }

    void UpdateSettings()
    {
        for (int i = 0; i < sources.Count; i++)
        {
            sources[i].spatialBlend = 0f;
            sources[i].playOnAwake = false;
            sources[i].volume = volume;
        }
    }

 

    void PlayReload(int i)
    {
        UpdateSettings();
        layer1.volume = soundData.reloadVoulume;
        layer1.PlayOneShot(soundData.reload[i]);
    }

    public void PlayShot()
    {
        UpdateSettings();
        layer1.clip = soundData.shotsCore[Random.Range(0, soundData.shotsCore.Length)]; 
        layer2.pitch = Random.Range(0.9f, 1.1f);
        layer2.volume = volume / 2f;
        layer2.clip = soundData.shotsTail[Random.Range(0, soundData.shotsTail.Length)];
        layer3.clip = soundData.shotBass;
        layer4.clip = soundData.shotMech;
        layer1.Play();
        layer2.Play();
        layer3.Play();
        layer4.Play();
    }

   
    
    public void PlayEquip(int i)
    {
        
        
        UpdateSettings();
        layer1.PlayOneShot(soundData.equip[i]);
     
    }

    public void PlayBoltReload(int i)
    {
        UpdateSettings();
        switch (i)
        {
            case 1:
                layer1.PlayOneShot(soundData.boltPull);
                break;
                case 2:
                layer1.PlayOneShot(soundData.bulletEject[Random.Range(0, soundData.bulletEject.Length)]);
                break;
                case 3:
                    layer1.PlayOneShot(soundData.boltRelease);
                break;
        }
    }
}
