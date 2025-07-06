using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AmbientAudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource milkAudioSource;
    [SerializeField] private AudioSource staticAudioSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip milkAudio;
    [SerializeField] private AudioClip staticAudio;
    
    [Header("Static Settings")]
    [SerializeField] private float staticFadeInDuration = 10f; // Time to reach full volume
    [SerializeField] private float staticMaxVolume = 1f; // Maximum volume for static
    [SerializeField] private AnimationCurve staticVolumeCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    [Header("Milk Audio Settings")]
    [SerializeField] private float milkAudioVolume = 1f;
    
    [Header("Events")]
    public UnityEngine.Events.UnityEvent onMilkAudioFinished;
    
    private Coroutine staticFadeCoroutine;
    
    void Start()
    {
        SetupAudioSources();
        PlayAmbientSounds();
    }
    
    void SetupAudioSources()
    {
        // Setup milk audio source
        if (milkAudioSource == null)
        {
            GameObject milkObj = new GameObject("MilkAudioSource");
            milkObj.transform.SetParent(transform);
            milkAudioSource = milkObj.AddComponent<AudioSource>();
        }
        
        // Setup static audio source
        if (staticAudioSource == null)
        {
            GameObject staticObj = new GameObject("StaticAudioSource");
            staticObj.transform.SetParent(transform);
            staticAudioSource = staticObj.AddComponent<AudioSource>();
        }
        
        // Configure audio sources
        milkAudioSource.playOnAwake = false;
        
        staticAudioSource.playOnAwake = false;
        staticAudioSource.loop = true;
        staticAudioSource.volume = 0f; // Start at 0
    }
    
    void PlayAmbientSounds()
    {
        // Play milk audio
        if (milkAudio != null)
        {
            milkAudioSource.clip = milkAudio;
            milkAudioSource.volume = milkAudioVolume;
            milkAudioSource.Play();
        }
        
        // Play static audio with fade in
        if (staticAudio != null)
        {
            staticAudioSource.clip = staticAudio;
            staticAudioSource.Play();
            staticFadeCoroutine = StartCoroutine(FadeInStatic());
        }
    }
    
    IEnumerator FadeInStatic()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < staticFadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / staticFadeInDuration;
            
            // Use animation curve for more control over fade
            float curveValue = staticVolumeCurve.Evaluate(normalizedTime);
            staticAudioSource.volume = curveValue * staticMaxVolume;
            
            yield return null;
        }
        
        // Ensure we end at max volume
        staticAudioSource.volume = staticMaxVolume;
    }
    
    IEnumerator MonitorMilkAudioCompletion()
    {
        // Wait for the audio to finish
        yield return new WaitWhile(() => milkAudioSource.isPlaying);
        onMilkAudioFinished?.Invoke();
    }
    
    // Public methods for control
    public void StopAllAudio()
    {
        if (milkAudioSource != null) milkAudioSource.Stop();
        if (staticAudioSource != null) staticAudioSource.Stop();
        if (staticFadeCoroutine != null) StopCoroutine(staticFadeCoroutine);
    }
    
    public void PauseAllAudio()
    {
        if (milkAudioSource != null) milkAudioSource.Pause();
        if (staticAudioSource != null) staticAudioSource.Pause();
    }
    
    public void ResumeAllAudio()
    {
        if (milkAudioSource != null) milkAudioSource.UnPause();
        if (staticAudioSource != null) staticAudioSource.UnPause();
    }
    
    public void SetStaticVolume(float volume)
    {
        if (staticAudioSource != null)
        {
            staticAudioSource.volume = Mathf.Clamp01(volume);
        }
    }
    
    public void SetMilkVolume(float volume)
    {
        if (milkAudioSource != null)
        {
            milkAudioSource.volume = Mathf.Clamp01(volume);
        }
    }
    
    void OnDestroy()
    {
        StopAllAudio();
    }
}