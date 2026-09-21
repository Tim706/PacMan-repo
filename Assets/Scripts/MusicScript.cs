using UnityEngine;

public class MusicScript: MonoBehaviour
{

    [Header("Music Source")] 
    public AudioSource music;

    [Header("Music Clips")]
    public AudioClip mainMenu;
    public AudioClip sceneStart;
    public AudioClip goblinNormal;
    public AudioClip goblinScared;
    public AudioClip goblinDead;
    
    [Header("Clip Volume")]
    [Range(0f, 2f)] public float mainMenuVolume = 1.15f; 
    [Range(0f, 2f)] public float sceneStartVolume = 1.0f;
    [Range(0f, 2f)] public float goblinNormalVolume = 1.0f;
    [Range(0f, 2f)] public float goblinScaredVolume = 1.0f;
    [Range(0f, 2f)] public float goblinDeadVolume = 1.0f;
    
    [Header("Music States")]
    public bool inGame = false;
    public bool goblinIsScared = false;
    public bool goblinIsDead = false;
    public float gameAge = 0f;

    private AudioClip playingMusicClip;

    private void CheckPlaying(AudioClip clip, float volume)
    {
        if (clip == playingMusicClip)
        {
            return;
        }
        
        playingMusicClip = clip;
        music.clip = clip;
        music.loop = true;
        music.volume = volume;
        //music.time = 0.8f;
        music.Play();
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (inGame)
        {
            gameAge += Time.deltaTime;
        }

        AudioClip chooseClip;
        float chooseVolume;
        
        if (!inGame)
        {
            chooseClip = mainMenu;
            chooseVolume = mainMenuVolume;
        }
        else if (gameAge < 3f)
        {
            chooseClip = sceneStart;
            chooseVolume = sceneStartVolume;
        }
        else if (goblinIsScared)
        {
            chooseClip = goblinScared;
            chooseVolume = goblinScaredVolume;
        }
        else if (goblinIsDead)
        {
            chooseClip = goblinDead;
            chooseVolume = goblinDeadVolume;
        }
        else
        {
            chooseClip = goblinNormal;
            chooseVolume = goblinNormalVolume;
        }
        
        CheckPlaying(chooseClip, chooseVolume);
    }
    
}
