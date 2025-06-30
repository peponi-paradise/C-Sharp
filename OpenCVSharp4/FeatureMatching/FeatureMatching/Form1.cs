// image 1 : https://pixabay.com/ko/photos/%EB%AA%A8%EB%82%98%EB%A6%AC%EC%9E%90-%ED%8E%98%EC%9D%B8%ED%8A%B8-%EB%93%B1-%EB%AF%B8%EC%88%A0-67506/
// image 2 : https://pixabay.com/ko/photos/%EC%98%81%EC%83%81-%EA%B7%B8%EB%A6%BC-%EB%AF%B8%EC%88%A0%EA%B4%80-%ED%99%94%EA%B0%80-1053852/

using OpenCvSharp;

namespace FeatureMatching;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-feature-matching-";

    public Form1()
    {
        InitializeComponent();

        using var image1 = LoadImage();
        using var image2 = LoadImage();

        BFMatching(image1, image2);
        FLANNMatching(image1, image2);
        BFKnnMatching(image1, image2);
        FLANNKnnMatching(image1, image2);
    }

    private void BFMatching(Mat image1, Mat image2)
    {
        using var kaze = KAZE.Create();
        using var descriptor1 = new Mat();
        using var descriptor2 = new Mat();

        // 특징점 검출 및 디스크립터 생성
        kaze.DetectAndCompute(image1, null, out var keyPoints1, descriptor1);
        kaze.DetectAndCompute(image2, null, out var keyPoints2, descriptor2);

        // Brute-Force Matcher 생성
        // normType : 디스크립터 간의 거리를 측정하는 방법을 지정. 실수 디스크립터는 L2를, 이진 디스크립터는 Hamming을 주로 사용
        // crossCheck : true인 경우 양쪽 매칭 결과가 일치하는 것만 남김. KnnMatch()와는 같이 사용할 수 없음
        using var matcher = new BFMatcher(crossCheck: true);

        // BFMatcher는 전수 조사 방식으로 매칭 수행
        // 각 디스크립터에 대해 가장 유사한 디스크립터를 찾아 매칭
        var matches = matcher.Match(descriptor1, descriptor2);

        using var detected = new Mat();

        // DrawMatchesFlags.NotDrawSinglePoints 를 설정하여 매칭된 포인트만 그림
        Cv2.DrawMatches(image1, keyPoints1, image2, keyPoints2, matches, detected, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);

        Cv2.ImShow("BFMatching", detected);
        Cv2.ImWrite($"{_fileNamePrefix}bfmatching.jpg", detected);
    }

    private void FLANNMatching(Mat image1, Mat image2)
    {
        using var kaze = KAZE.Create();
        using var descriptor1 = new Mat();
        using var descriptor2 = new Mat();

        // 특징점 검출 및 디스크립터 생성
        kaze.DetectAndCompute(image1, null, out var keyPoints1, descriptor1);
        kaze.DetectAndCompute(image2, null, out var keyPoints2, descriptor2);

        // 실수형 디스크립터에 사용
        // trees : K-D 트리의 수를 지정. 값이 클수록 정확도 높아짐
        var indexParams = new OpenCvSharp.Flann.KDTreeIndexParams(5);

        // 이진 디스크립터에 사용
        // tableNumber : 해시 테이블의 수. 클수록 정확도 높아짐
        // keySize : 해시 함수의 길이. 값이 클수록 정확도 높아짐
        // multiProbeLevel : Multi-probing 수준 지정. 클수록 정확도 높아짐
        // var binaryIndexParams = new OpenCvSharp.Flann.LshIndexParams(20, 15, 2);

        // 이웃 검색 시 사용할 사용할 파라미터
        // checks : 확인할 인덱스 노드의 수. 클수록 정확도 높아짐
        // eps : 근사 검색 설정값 (0 ~ 1). 값이 작을수록 더 정확한 검색을 수행
        // sorted : 결과를 정렬 후 반환
        var searchParams = new OpenCvSharp.Flann.SearchParams(50);

        // Fast Library for Approximate Nearest Neighbors Matcher 생성
        using var matcher = new FlannBasedMatcher(indexParams, searchParams);

        // FlannBasedMatcher는 근사 최근접 이웃 탐색 알고리즘 사용
        // 각 디스크립터에 대해 가장 유사한 디스크립터를 찾아 매칭
        var matches = matcher.Match(descriptor1, descriptor2);

        using var detected = new Mat();

        // DrawMatchesFlags.NotDrawSinglePoints 를 설정하여 매칭된 포인트만 그림
        Cv2.DrawMatches(image1, keyPoints1, image2, keyPoints2, matches, detected, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);

        Cv2.ImShow("FLANNMatching", detected);
        Cv2.ImWrite($"{_fileNamePrefix}flannmatching.jpg", detected);
    }

    private void BFKnnMatching(Mat image1, Mat image2)
    {
        using var kaze = KAZE.Create();
        using var descriptor1 = new Mat();
        using var descriptor2 = new Mat();

        // 특징점 검출 및 디스크립터 생성
        kaze.DetectAndCompute(image1, null, out var keyPoints1, descriptor1);
        kaze.DetectAndCompute(image2, null, out var keyPoints2, descriptor2);

        // Brute-Force Matcher 생성
        // normType : 디스크립터 간의 거리를 측정하는 방법을 지정. 실수 디스크립터는 L2를, 이진 디스크립터는 Hamming을 주로 사용
        // crossCheck : true인 경우 양쪽 매칭 결과가 일치하는 것만 남김. KnnMatch()와는 같이 사용할 수 없음
        using var matcher = new BFMatcher();

        // BFMatcher는 전수 조사 방식으로 매칭 수행
        // k : 각 쿼리 디스크립터에 대해 가장 가까운 k개의 트레인 디스크립터를 찾음
        var matches = matcher.KnnMatch(descriptor1, descriptor2, 2);

        var matched = new List<DMatch>();

        // Lowe's Ratio Test
        // 가장 가까운 매칭이 두 번째로 가까운 매칭 거리 * threshold보다 작을 때 잘 매칭된 것으로 간주
        foreach (var match in matches)
        {
            // 0.8 : Threshold value
            if (match.Length > 1 && match[0].Distance < 0.8 * match[1].Distance)
                matched.Add(match[0]);
        }

        using var detected = new Mat();

        // DrawMatchesFlags.NotDrawSinglePoints 를 설정하여 매칭된 포인트만 그림
        Cv2.DrawMatches(image1, keyPoints1, image2, keyPoints2, matched, detected, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);

        Cv2.ImShow("BFKnnMatching", detected);
        Cv2.ImWrite($"{_fileNamePrefix}bfknnmatching.jpg", detected);
    }

    private void FLANNKnnMatching(Mat image1, Mat image2)
    {
        using var kaze = KAZE.Create();
        using var descriptor1 = new Mat();
        using var descriptor2 = new Mat();

        // 특징점 검출 및 디스크립터 생성
        kaze.DetectAndCompute(image1, null, out var keyPoints1, descriptor1);
        kaze.DetectAndCompute(image2, null, out var keyPoints2, descriptor2);

        // 실수형 디스크립터에 사용
        // trees : K-D 트리의 수를 지정. 값이 클수록 정확도 높아짐
        var indexParams = new OpenCvSharp.Flann.KDTreeIndexParams(5);

        // 이진 디스크립터에 사용
        // tableNumber : 해시 테이블의 수. 클수록 정확도 높아짐
        // keySize : 해시 함수의 길이. 값이 클수록 정확도 높아짐
        // multiProbeLevel : Multi-probing 수준 지정. 클수록 정확도 높아짐
        // var binaryIndexParams = new OpenCvSharp.Flann.LshIndexParams(20, 15, 2);

        // 이웃 검색 시 사용할 사용할 파라미터
        // checks : 확인할 인덱스 노드의 수. 클수록 정확도 높아짐
        // eps : 근사 검색 설정값 (0 ~ 1). 값이 작을수록 더 정확한 검색을 수행
        // sorted : 결과를 정렬 후 반환
        var searchParams = new OpenCvSharp.Flann.SearchParams(50);

        // Fast Library for Approximate Nearest Neighbors Matcher 생성
        using var matcher = new FlannBasedMatcher(indexParams, searchParams);

        // FlannBasedMatcher는 근사 최근접 이웃 탐색 알고리즘 사용
        // k : 각 쿼리 디스크립터에 대해 가장 가까운 k개의 트레인 디스크립터를 찾음
        var matches = matcher.KnnMatch(descriptor1, descriptor2, 2);

        var matched = new List<DMatch>();

        // Lowe's Ratio Test
        // 가장 가까운 매칭이 두 번째로 가까운 매칭 거리 * threshold보다 작을 때 잘 매칭된 것으로 간주
        foreach (var match in matches)
        {
            // 0.8 : Threshold value
            if (match.Length > 1 && match[0].Distance < 0.8 * match[1].Distance)
                matched.Add(match[0]);
        }

        using var detected = new Mat();

        // DrawMatchesFlags.NotDrawSinglePoints 를 설정하여 매칭된 포인트만 그림
        Cv2.DrawMatches(image1, keyPoints1, image2, keyPoints2, matched, detected, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);

        Cv2.ImShow("FLANNKnnMatching", detected);
        Cv2.ImWrite($"{_fileNamePrefix}flannknnmatching.jpg", detected);
    }

    private Mat LoadImage()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "All files|*.*",
            InitialDirectory = $@"C:\Study\Blog\public\posts\nuget-package\",
            CheckPathExists = true,
            CheckFileExists = true,
            Multiselect = true,
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return Cv2.ImRead(dialog.FileName);
        }

        return null!;
    }
}