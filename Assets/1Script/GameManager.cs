//=============== 오디오 소스 ================================

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private AudioSource bgmSource;
    private AudioSource effectSource;

    [Header("배경 음악")]
    public AudioClip bgm;

    [Header("효과음")]
    public AudioClip clickSound;
    public AudioClip buttonSound;
    public AudioClip escapeSound;

    void Awake()
    {
        // 싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 오디오 소스 2개 설정: BGM + SFX
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.clip = bgm;
        bgmSource.volume = 0.5f;

        effectSource.playOnAwake = false;
        effectSource.volume = 1f;

        bgmSource.Play();
    }

    /// <summary>
    /// 효과음 재생 함수 (외부에서 호출 가능)
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            effectSource.PlayOneShot(clip);
    }


    public void PlayButtonSound()
    {
        PlaySFX(buttonSound);
    }

    public void PlayEscapeSound()
    {
        PlaySFX(escapeSound);
    }
}
