using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    [Header("SFX Source")]
    public AudioSource SFXSource;
    
    
    [Header("SFX Clips")]
    public AudioClip walkingClip;
    public AudioClip cleaningClip; //eating pellets
    public AudioClip cleanGoblinClip; //eating ghost
    public AudioClip gainWaterClip; //eating cherry
    public AudioClip wallHitClip;
    public AudioClip deathClip;
    
    [Header("Clip Volume")]
    [Range(0f, 2f)] public float walkingVolume = 0.9f;  
    [Range(0f, 2f)] public float cleaningVolume = 1.2f;  
    [Range(0f, 2f)] public float cleanGoblinVolume = 1.0f;
    [Range(0f, 2f)] public float gainWaterVolume = 1.25f;  
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
    
    public void PlayMoving() => PlaySFX(walkingClip, walkingVolume, loop: true);
    public void PlayCleaning() => PlaySFX(cleaningClip, cleaningVolume, loop: true);
    public void PlayCleanGoblin() => PlaySFX(cleanGoblinClip, cleanGoblinVolume);
    public void PlayGainWater() => PlaySFX(gainWaterClip, gainWaterVolume);
    public void PlayWallHit() => PlaySFX(wallHitClip, wallHitVolume);
    public void PlayDeath() => PlaySFX(deathClip, deathVolume);
    
    void Start()
    {
        
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PlayMoving();
        if (Input.GetKeyDown(KeyCode.A)) PlayCleaning();
        if (Input.GetKeyDown(KeyCode.W)) PlayCleanGoblin();
        if (Input.GetKeyDown(KeyCode.D)) PlayGainWater();
        if (Input.GetKeyDown(KeyCode.E)) PlayWallHit();
        if (Input.GetKeyDown(KeyCode.F)) PlayDeath();
    }
}
