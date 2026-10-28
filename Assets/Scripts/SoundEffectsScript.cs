using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    [Header("SFX Source")]
    public AudioSource SFXSource;
	public AudioSource OneShotSource;
    
    
    [Header("SFX Clips")]
    public AudioClip walkingClip;
    public AudioClip cleaningClip; //eating pellets
    public AudioClip cleanGoblinClip; //eating ghost
    public AudioClip gainWaterClip; //eating cherry
    public AudioClip wallHitClip;
    public AudioClip deathClip;
    
    [Header("Clip Volume")]
    [Range(0f, 2f)] public float walkingVolume = 0.7f;  
    [Range(0f, 2f)] public float cleaningVolume = 1.2f;  
    [Range(0f, 2f)] public float cleanGoblinVolume = 1.0f;
    [Range(0f, 2f)] public float gainWaterVolume = 1.8f;  
    [Range(0f, 2f)] public float wallHitVolume = 1.15f;    
    [Range(0f, 2f)] public float deathVolume = 1.0f;       
    
    
    private AudioClip playingSFXClip;

    public void PlaySFX(AudioClip clip, float volume = 1f, bool loop = false)
    {
        if (clip == playingSFXClip && SFXSource.isPlaying)
        {
            return;
        }
        
        playingSFXClip = clip;
        SFXSource.clip = clip;
        SFXSource.loop = loop;
        SFXSource.volume = volume;
        SFXSource.Play();

    }
	
	public void PlayOneShotSFX(AudioClip clip, float volume = 1f)
	{
    	OneShotSource.PlayOneShot(clip, volume);
	}    

    public void PlayMoving() => PlaySFX(walkingClip, walkingVolume, loop: true);
    public void PlayCleaning() => PlaySFX(cleaningClip, cleaningVolume, loop: true);
    public void PlayCleanGoblin() => PlaySFX(cleanGoblinClip, cleanGoblinVolume);
    public void PlayGainWater() => PlayOneShotSFX(gainWaterClip, gainWaterVolume);
    public void PlayWallHit() => PlayOneShotSFX(wallHitClip, wallHitVolume);
    public void PlayDeath() => PlaySFX(deathClip, deathVolume);
    
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
}
