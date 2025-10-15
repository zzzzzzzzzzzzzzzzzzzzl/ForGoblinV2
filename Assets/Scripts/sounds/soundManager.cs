using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    public AudioSource test;
    public List<AudioClip> attackSounds = new List<AudioClip>();
    public AudioClip buy;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip spawnUnit;
    public AudioClip summonerSummon;
    public AudioClip magiExplosion;
    public AudioClip healerHeal;
    public AudioClip impDeath;
    public AudioClip death;
    public AudioClip spendJuice;
    public AudioClip hover;
    public AudioClip rangedAttack;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioSource music;
    public void Start()
    {
        instance = this;
    }

    public void playSound(AudioClip sound, float volume)
    {
        GameObject soundObject = new GameObject("TempAudio");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(.9f, 1.1f);

        Destroy(soundObject, sound.length); // Destroy after playback        
    }
    Coroutine musicFade;
    public void setMusic(AudioClip sound){
        if(musicFade!=null){
        StopCoroutine(musicFade);
        }
        music.volume=0;
        music.clip=sound;
        music.Play();
        musicFade=StartCoroutine(FadeIn(.2f,16,music));
    }
    private IEnumerator FadeIn(float targetVolume,float fadeDuration,AudioSource audioSource)
    {
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, currentTime / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume; // Ensure the volume is set to the target volume
    }
    

bool p=false;
    void Update()
    {
 if(Input.GetKeyDown(KeyCode.Escape)){
        p=!p;
        if(p){
            setMusic(gameMusic);

        }else{
            setMusic(menuMusic);
        }
        }      
    }
}
