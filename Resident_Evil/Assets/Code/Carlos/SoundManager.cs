using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Acceso global desde cualquier script: SoundManager.Instance.PlaySFX(...)
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfxSource2;
    [SerializeField] private AudioSource musicSource;

    [Header("Sonidos registrados")]
    [SerializeField] private SoundEntry[] sounds;

    private Dictionary<string, AudioClip> soundDict = new();

    private void Start()
    {
        PlayMusic("Mansion_Music");
    }

    private void Awake()
    {
        // Singleton: una sola instancia que sobrevive al cambio de escena
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var entry in sounds)
            if (!soundDict.ContainsKey(entry.name))
                soundDict.Add(entry.name, entry.clip);
    }

    public void PlayMusic(string soundName, bool loop = true)
    {
        if (!TryGetClip(soundName, out var clip)) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void FadeIn(string soundName, float duration = 1f, bool loop = true)
    {
        if (!TryGetClip(soundName, out var clip)) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = 0f;
        musicSource.Play();
        StartCoroutine(FadeVolume(musicSource, 0f, 1f, duration));
    }

    public void FadeOut(float duration = 1f)
    {
        StartCoroutine(FadeVolume(musicSource, musicSource.volume, 0f, duration, stopOnEnd: true));
    }

    public void SetSFXVolume(float v) => sfxSource.volume = Mathf.Clamp01(v);
    public void SetMusicVolume(float v) => musicSource.volume = Mathf.Clamp01(v);

    private bool TryGetClip(string soundName, out AudioClip clip)
    {
        if (soundDict.TryGetValue(soundName, out clip)) return true;
        Debug.LogWarning($"[SoundManager] '{soundName}' no encontrado.");
        return false;
    }

    private IEnumerator FadeVolume(AudioSource source, float from, float to, float duration, bool stopOnEnd = false)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        source.volume = to;
        if (stopOnEnd) source.Stop();
    }

    public void PlaySFX(string soundName, int sfxChannel = 1)
    {
        if (!TryGetClip(soundName, out var clip)) return;

        AudioSource source = sfxChannel == 2 ? sfxSource2 : sfxSource;
        source.clip = clip;
        source.Play();
    }


    public bool IsPlayingSFX(string soundName, int sfxChannel = 1)
    {
        if (!TryGetClip(soundName, out var clip)) return false;
        AudioSource source = sfxChannel == 2 ? sfxSource2 : sfxSource;
        return source.isPlaying && source.clip == clip;
    }
}

[System.Serializable]
public class SoundEntry
{
    public string name;
    public AudioClip clip;
}