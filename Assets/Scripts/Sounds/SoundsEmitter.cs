using UnityEngine;

public class SoundsEmitter
{
   private AudioSource audioSource;
   private  AudioClip[] audioClips;
   public float timer;
   public float boundary;

   public SoundsEmitter(AudioSource _audioSource, AudioClip[] _audioClips,float bound)
   {
      audioSource = _audioSource;
      audioClips = _audioClips;
      boundary =  bound;
      
   }
   
   public void UpdateTimer(float bound)
   {
      boundary = bound;
      timer += Time.deltaTime;
   }

   public void PlayClip()
   {
      if (timer > boundary )
      {
         
         audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
         timer = 0;
      }
      
   }
}

