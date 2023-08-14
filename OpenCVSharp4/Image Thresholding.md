## Introduction

<br>

- `Thresholding`은 이미지를 이진화 하는 방법이다.
    - `이진화` 이미지란, 흑백으로 표현되는 이미지를 뜻한다.
- 이진화 알고리즘에서는 임계값을 기준으로 픽셀 값을 흑, 백으로 변환한다.
- `Thresholding`을 진행하기 위해서는 주로 다음과 같은 절차를 거친다.
    1. 이미지를 Gray scale로 전환 (선택사항. 컬러 이미지를 활용하는 것이 더 유리할 수도 있다.)
    2. Thresholding 진행

<br>

## Example

<br>

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
<img src="../../Blog-Image/C#/OpenCVSharp/TestImage.jpg"
alt="Original image"/>
</td>
<td>
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/Binary.jpg" alt="Binary">
</td>
<td>
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/BinaryInv.jpg" alt="BinaryInv">
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
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/Tozero.jpg" alt="Tozero">
</td>
<td>
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/TozeroInv.jpg" alt="TozeroInv">
</td>
<td>
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/Trunc.jpg" alt="Trunc">
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
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/Mask.jpg" alt="Mask">
</td>
<td>
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/Otsu.jpg" alt="Otsu">
</td>
<td>
<img src="../../Blog-Image/C#/OpenCVSharp/Image Thresholding/Triangle.jpg" alt="Triangle">
</td>
</tr>
</table>

```cs
private static void Main(string[] args)
{
    Mat original = new($@"C:\temp\TestImage.jpg");
    Mat grayScale = new();
    Mat threshold = new();

    Cv2.CvtColor(original, grayScale, ColorConversionCodes.BGR2GRAY);
    Cv2.Threshold(grayScale, threshold, 124, 255, ThresholdTypes.Binary);

    Cv2.ImShow("Converted", threshold);
    Cv2.WaitKey(0);
}
```