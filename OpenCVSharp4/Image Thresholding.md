## Introduction

<br>

- `Thresholding`은 이미지를 이진화 하는 방법이다.
    - `이진화` 이미지란, 흑백으로 표현되는 이미지를 뜻한다.
- 이진화 알고리즘에서는 임계값을 기준으로 픽셀 값을 흑, 백으로 변환한다.
- `Thresholding`을 진행하기 위해서는 주로 다음과 같은 절차를 거친다.
    1. 이미지를 Gray scale로 전환 (선택사항. 컬러 이미지를 활용하는 것이 더 유리할 수도 있다.)
    2. Thresholding 진행

<br>

## Global thresholding

<br>

```cs
private static void Main(string[] args)
{
    Mat original = new("TestImage.jpg");
    Mat grayScale = new();
    Mat threshold = new();

    // Gray scale로 변경
    Cv2.CvtColor(original, grayScale, ColorConversionCodes.BGR2GRAY);

    // Threshold(inputImg, outputImg, threshold, maxval, thresholdType)
    Cv2.Threshold(grayScale, threshold, 124, 255, ThresholdTypes.Binary);
}
```
```cs
// 컬러 이미지로 진행하는 경우

private static void Main(string[] args)
{
    Mat original = new("TestImage.jpg");
    Mat threshold = new();

    // Threshold(inputImg, outputImg, threshold, maxval, thresholdType)
    Cv2.Threshold(original, threshold, 124, 255, ThresholdTypes.Binary);
}
```

<table>
<tr>
<th>
Original image
</th>
<th>
Binary
</th>
<th>
BinaryInv
</th>
</tr>
<tr>
<td>
<img src="./TestImage.jpg"
alt="Original image"/>
</td>
<td>
<img src="./Image Thresholding/Binary.jpg" alt="Binary">
</td>
<td>
<img src="./Image Thresholding/BinaryInv.jpg" alt="BinaryInv">
</td>
</tr>

<tr>
<th>
Tozero
</th>
<th>
TozeroInv
</th>
<th>
Trunc
</th>
</tr>
<tr>
<td>
<img src="./Image Thresholding/Tozero.jpg" alt="Tozero">
</td>
<td>
<img src="./Image Thresholding/TozeroInv.jpg" alt="TozeroInv">
</td>
<td>
<img src="./Image Thresholding/Trunc.jpg" alt="Trunc">
</td>
</tr>

<tr>
<th>
Mask
</th>
<th>
Otsu
</th>
<th>
Triangle
</th>
</tr>
<tr>
<td>
<img src="./Image Thresholding/Mask.jpg" alt="Mask">
</td>
<td>
<img src="./Image Thresholding/Otsu.jpg" alt="Otsu">
</td>
<td>
<img src="./Image Thresholding/Triangle.jpg" alt="Triangle">
</td>
</tr>
</table>

<br>

### Cv2.Threshold()

<br>

- `Cv2.Threshold(src, dst, threshold, maxval, type)`의 파라미터는 다음과 같다.
    - src : Threshold를 수행할 원본 이미지
    - dst : 결과 이미지
    - threshold : 임계값
    - maxval : 임계값을 넘었을 때 적용할 값 (binary에 사용)
    - type : Threshold type

<br>

### ThresholdTypes

<br>

|ThresholdTypes|Description|
|---|---|
|Binary|`threshold` 값보다 크면 `maxval`, 아니면 0|
|BinaryInv|`threshold` 값보다 크면 0, 아니면 `maxval`|
|Tozero|`threshold` 값보다 크면 그대로, 아니면 0|
|TozeroInv|`threshold` 값보다 크면 0, 아니면 그대로|
|Trunc|`threshold` 값보다 크면 `threshold`, 아니면 그대로|
|Mask|모든 픽셀 0으로 (검정 이미지, 마스크용)|
|Otsu|[Otsu 알고리즘](https://en.wikipedia.org/wiki/Otsu%27s_method) 적용 (Gray scale만 가능)|
|Triangle|[Triangle 알고리즘](https://www.kitware.com/histogram-based-thresholding/) 적용 (Gray scale만 가능)|

<br>

## Local thresholding

<br>

- 앞의 `Global thresholding` 방법은 이미지의 모든 픽셀에 일괄적으로 임계값을 지정하여 문제가 발생할 수 있다. 이 때, 대안으로 적용할 수 있는 방법이 `Local thresholding`이다.
- `Local thresholding`은 이미지를 각각의 작은 구역으로 나누어 thresholding 하는 기법이다.
- `OpenCVSharp`은 이를 위해 `Cv2.AdaptiveThreshold()` 메서드를 지원한다.
    - 메서드 호출 시 `ThresholdTypes`를 지정해줘야 하는데, Binary 2가지만 지원한다.

<br>

```cs
private static void Main(string[] args)
{
    Mat original = new("TestImage.jpg");
    Mat grayScale = new();
    Mat threshold = new();

    // Gray scale로 변경
    Cv2.CvtColor(original, grayScale, ColorConversionCodes.BGR2GRAY);

    // AdaptiveThreshold(inputImg, outputImg, maxval, adaptiveType, thresholdType, blockSize, c)
    Cv2.AdaptiveThreshold(grayScale, threshold, 255,
                          AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary,
                          15, 2);
}
```

<table>
<tr>
<th>
MeanC
</th>
<th>
GaussianC
</th>
</tr>
<tr>
<td>
<img src="./Image Thresholding/MeanC.jpg"
alt="MeanC"/>
</td>
<td>
<img src="./Image Thresholding/GaussianC.jpg" alt="GaussianC">
</td>
</tr>
</table>

<br>

### Cv2.AdaptiveThreshold()

<br>

- `Cv2.AdaptiveThreshold(src, dst, maxval, adaptiveType, thresholdType, blockSize, c)`의 파라미터는 다음과 같다.
    - src : Threshold를 수행할 원본 이미지
    - dst : 결과 이미지
    - maxval : 임계값을 넘었을 때 적용할 값
    - adaptiveType : AdaptiveThreshold type
    - thresholdType : Threshold type (Binary, BinaryInv)
    - blockSize : Thresholding을 적용할 구역 크기
    - c : 평균 또는 가중평균에서 차감할 값

<br>

### AdaptiveThresholdTypes

<br>

|AdaptiveThresholdTypes|Description|
|---|---|
|MeanC|주변영역의 평균값 - c|
|GaussianC|가우시안 평균값 - c|

<br>

## 참조 자료

<br>

- [Image Thresholding](https://docs.opencv.org/4.x/d7/d4d/tutorial_py_thresholding.html)
- [이미지 임계처리](https://opencv-python.readthedocs.io/en/latest/doc/09.imageThresholding/imageThresholding.html)