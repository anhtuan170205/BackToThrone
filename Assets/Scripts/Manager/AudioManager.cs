using UnityEngine;
using System.Collections;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private AudioClip[] _backgroundMusicClips;
    [SerializeField] private AudioSource _sourceA;
    [SerializeField] private AudioSource _sourceB;

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
}
