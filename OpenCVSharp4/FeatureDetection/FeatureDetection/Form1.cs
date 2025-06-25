using OpenCvSharp;
using OpenCvSharp.Features2D;
using OpenCvSharp.XFeatures2D;

// image 1 : https://pixabay.com/ko/photos/%EB%AA%A8%EB%82%98%EB%A6%AC%EC%9E%90-%ED%8E%98%EC%9D%B8%ED%8A%B8-%EB%93%B1-%EB%AF%B8%EC%88%A0-67506/
// image 2 (나중에 matching용으로 사용해보자) : https://pixabay.com/ko/photos/%EC%98%81%EC%83%81-%EA%B7%B8%EB%A6%BC-%EB%AF%B8%EC%88%A0%EA%B4%80-%ED%99%94%EA%B0%80-1053852/

namespace FeatureDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-feature-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        SIFTDetection(image);
        SURFDetection(image);
        ORBDetection(image);
        KAZEDetection(image);
        AKAZEDetection(image);
        BRISKDetection(image);
    }

    private void SIFTDetection(Mat image)
    {
        // nFeatures : 검출할 최대 특징점의 수
        // nOctaveLayers : 각 옥타브 (스케일 레벨) 에 있는 레이어의 수 (각 레이어 별로 블러 강도가 다름)
        // contrastThreshold : 특징점의 contrast 임계값, 클수록 대비가 낮은 영역의 약한 특징점이 걸러짐
        // edgeThreshold : 값이 클수록 엣지와 유사한 특징점들이 더 많이 유지됨
        // sigma : 옥타브 #0 의 이미지에 적용되는 Gaussian smoothing sigma
        using var sift = SIFT.Create();

        var keyPoints = sift.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("SIFT", detected);
    }

    private void SURFDetection(Mat image)
    {
        // hessianThreshold : 헤시안 키포인트 검출에 사용되는 임계값, 높을수록 더 강한 특징점만 검출
        // nOctaves : 이미지 피라미드 옥타브 (스케일 레벨) 의 수
        // nOctaveLayers : 각 옥타브에 있는 레이어의 수 (각 레이어 별로 블러 강도가 다름)
        // extended : 디스크립터의 크기 결정. true로 설정 시 128차원 (기본값), false로 설정 시 64차원
        // upright : 방향성 계산 여부 결정. true로 설정 시 회전 불변성이 없는 대신 속도가 빠름
        using var surf = SURF.Create(200);

        var keyPoints = surf.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("SURF", detected);
    }

    private void ORBDetection(Mat image)
    {
        // nFeatures : 검출할 최대 특징점의 수
        // scaleFactor : 이미지 피라미드 층간 스케일 비율
        // nLevels : 피라미드 레벨 (옥타브) 수
        // edgeThreshold : 이미지 테두리 영역의 특징점을 검출하지 않을 경계 영역의 크기
        // firstLevel : 피라미드의 첫 레벨 지정
        // wtaK : BRIEF 디스크립터 생성 시 사용하는 샘플링 쌍의 수 (2~4)
        // scoreType : 특징점 계산 방식. Harris (기본값) 또는 Fast
        // patchSize : BRIEF 디스크립터 계산 시 패치 크기. edgeThreshold 값보다 크거나 같아야 함
        // fastThreshold : scoreType을 Fast로 사용할 때의 임계값. 낮을 수록 더 많은 특징점 검출
        using var orb = ORB.Create(1000);

        var keyPoints = orb.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("ORB", detected);
    }

    private void KAZEDetection(Mat image)
    {
        // extended : 디스크립터의 크기 결정. true로 설정 시 128차원, false로 설정 시 64차원 (기본값)
        // upright : 방향성 계산 여부 결정. true로 설정 시 회전 불변성이 없는 대신 속도가 빠름
        // threshold : 특징점 검출에 사용하는 임계값. 높을수록 더 강한 특징점만 검출
        // nOctaves : 이미지 피라미드 옥타브 (스케일 레벨) 의 수
        // nOctaveLayers : 각 옥타브에 있는 레이어의 수 (각 레이어 별로 블러 강도가 다름)
        // diffusivity : 비선형 확산 필터링에 사용되는 확산 함수 유형
        using var kaze = KAZE.Create();

        var keyPoints = kaze.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("KAZE", detected);
    }

    private void AKAZEDetection(Mat image)
    {
        // descriptorType : 생성할 디스크립터의 유형
        // descriptorSize : 디스크립터 크기 지정 (bit 단위)
        // descriptorChannels : 디스크립터 계산에 사용될 이미지 채널 수
        // threshold : 특징점 검출에 사용하는 임계값. 높을수록 더 강한 특징점만 검출
        // nOctaves : 이미지 피라미드 옥타브 (스케일 레벨) 의 수
        // nOctaveLayers : 각 옥타브에 있는 레이어의 수 (각 레이어 별로 블러 강도가 다름)
        // diffusivity : 비선형 확산 필터링에 사용되는 확산 함수 유형
        using var akaze = AKAZE.Create();

        var keyPoints = akaze.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("AKAZE", detected);
    }

    private void BRISKDetection(Mat image)
    {
        // thresh : FAST 특징점 검출에 사용하는 임계값. 높을수록 더 강한 특징점만 검출
        // octaves : 이미지 피라미드 옥타브 (스케일 레벨) 의 수
        // patternScale : 디스크립터 생성 시 사용되는 샘플링 패턴의 스케일 인자
        using var brisk = BRISK.Create();

        var keyPoints = brisk.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("BRISK", detected);
    }

    private Mat LoadImage()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "All files|*.*",
            InitialDirectory = $@"C:\Study\Blog\public\posts\nuget-package\",
            CheckPathExists = true,
            CheckFileExists = true,
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return Cv2.ImRead(dialog.FileName);
        }

        return null!;
    }
}