using UnityEngine;
using System.Collections;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [Header("Background Music")]
    [SerializeField] private AudioClip[] _backgroundMusicClips;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _coinPickupClip;
    [SerializeField] private AudioClip _rockCollisionClip;
    [SerializeField] private AudioClip _footstepClip;
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _speedUpClip;
    [SerializeField] private AudioClip _explosionClip;
    [SerializeField] private AudioClip _checkpointClip;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _sourceA;
    [SerializeField] private AudioSource _sourceB;
    [SerializeField] private AudioSource _loopSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [SerializeField] private float _crossfadeDuration = 2f;

    private AudioSource _activeSource;
    private AudioSource _inactiveSource;
    

    private Coroutine _musicCoroutine;
    private int _lastClipIndex = -1;

    protected override void Awake()
    {
        base.Awake();
        _activeSource = _sourceA;
        _inactiveSource = _sourceB;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.InGame)
        {
            _musicCoroutine = StartCoroutine(MusicLoop());
        }
        else
        {
            if (_musicCoroutine != null)
            {
                StopCoroutine(_musicCoroutine);
            }
            StopAllMusic();
        }
    }

    private IEnumerator MusicLoop()
    {
        while (true)
        {
            AudioClip nextClip = GetRandomClip();

            yield return StartCoroutine(Crossfade(nextClip, _crossfadeDuration));

            yield return new WaitUntil(() => !_activeSource.isPlaying);
        }
    }

    private AudioClip GetRandomClip()
    {
        if (_backgroundMusicClips.Length == 0) return null;

        int index;
        do
        {
            index = Random.Range(0, _backgroundMusicClips.Length);
        }
        while (index == _lastClipIndex);

        _lastClipIndex = index;
        return _backgroundMusicClips[index];
    }

    private IEnumerator Crossfade(AudioClip newClip, float duration)
    {
        if (newClip == null) yield break;

        _inactiveSource.clip = newClip;
        _inactiveSource.volume = 0f;
        _inactiveSource.Play();

        float time = 0f;

        float startVolume = _masterVolume;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            _activeSource.volume = Mathf.Lerp(startVolume, 0f, t);
            _inactiveSource.volume = Mathf.Lerp(0f, _masterVolume, t);

            yield return null;
        }

        _activeSource.Stop();

        var temp = _activeSource;
        _activeSource = _inactiveSource;
        _inactiveSource = temp;
    }

    private void StopAllMusic()
    {
        _activeSource.Stop();
        _inactiveSource.Stop();
    }

    public void PlaySfx(AudioClip clip, float volume = 1f, bool loop = false)
    {
        if (clip == null) return;
        if (loop)
        {
            _loopSource.clip = clip;
            _loopSource.volume = volume;
            _loopSource.loop = true;
            _loopSource.Play();
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }

    public void PlayCoinPickupSfx(float volume = 1f)
    {
        PlaySfx(_coinPickupClip, volume);
    }

    public void PlayRockCollisionSfx(float volume = 1f)
    {
        PlaySfx(_rockCollisionClip, volume);
    }

    public void PlayFootstepSfx(float volume = 0.25f)
    {
        PlaySfx(_footstepClip, volume, true);
    }

    public void StopFootstepSfx()
    {
        _loopSource.Stop();
    }

    public void PlayJumpSfx(float volume = 10f)
    {
        PlaySfx(_jumpClip, volume);
    }

    public void PlaySpeedUpSfx(float volume = 1f)
    {
        PlaySfx(_speedUpClip, volume);
    }

    public void PlayExplosionSfx(float volume = 1f)
    {
        PlaySfx(_explosionClip, volume);
    }

    public void PlayCheckpointSfx(float volume = 1f)
    {
        PlaySfx(_checkpointClip, volume);
    }
}
