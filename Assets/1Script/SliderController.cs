using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderController : MonoBehaviour
{
    public Slider slider; // 연결된 Slider 컴포넌트
    public Image fillImage; // 슬라이더의 Fill 이미지 (색상 변경용)
    public float randomChangeInterval = 5f; // 슬라이더 값 변경 간격 (초)
    public float lerpDuration = 0.5f; // 슬라이더 값이 부드럽게 바뀌는 시간 (초)

    private float targetValue; // 목표 슬라이더 값
    private Coroutine lerpCoroutine; // 실행 중인 코루틴 추적

    private void Start()
    {
        // slider가 null이면 현재 오브젝트에서 자동으로 가져옴
        if (slider == null) slider = GetComponent<Slider>();

        targetValue = slider.value; // 시작 시 현재 슬라이더 값을 저장

        // 주기적으로 랜덤값을 설정하는 코루틴 시작
        StartCoroutine(RandomizeSliderValueRoutine());

        // 초기 색상 설정
        UpdateFillColor(targetValue);
    }

    private IEnumerator RandomizeSliderValueRoutine()
    {
        // 무한 루프로 일정 시간마다 랜덤 값 생성
        while (true)
        {
            yield return new WaitForSeconds(randomChangeInterval);

            float newValue = Random.Range(0f, 1f); // 새 목표값 생성

            // 이전에 실행 중이던 코루틴이 있으면 중단
            if (lerpCoroutine != null) StopCoroutine(lerpCoroutine);

            // 새로운 목표값으로 슬라이더를 부드럽게 이동
            lerpCoroutine = StartCoroutine(LerpSliderValue(newValue));
        }
    }

    private IEnumerator LerpSliderValue(float newValue)
    {
        float startValue = slider.value; // 현재 값 저장
        float elapsed = 0f;

        // 지정된 시간(lerpDuration) 동안 보간
        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / lerpDuration); // 보간 비율 (0~1)
            float value = Mathf.Lerp(startValue, newValue, t); // 선형 보간
            slider.value = value; // 슬라이더 값 적용
            UpdateFillColor(value); // 색상 갱신
            yield return null;
        }

        // 마지막에 정확한 목표값으로 설정 (잔차 제거용)
        slider.value = newValue;
        UpdateFillColor(newValue);
    }

    // 슬라이더 값에 따라 Fill 색상 변경
    private void UpdateFillColor(float value)
    {
        if (value < 0.33f)
            fillImage.color = Color.green; // 낮은 값은 초록
        else if (value < 0.66f)
            fillImage.color = Color.yellow; // 중간은 노랑
        else
            fillImage.color = Color.red; // 높은 값은 빨강
    }
}
