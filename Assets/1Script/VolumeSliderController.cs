using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // 슬라이더 값이 바뀔 때마다 OnVolumeChanged 함수 호출
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        // 슬라이더 초기값을 현재 볼륨으로 설정
        volumeSlider.value = AudioListener.volume;
    }

    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // 전체 볼륨 조절
    }
}