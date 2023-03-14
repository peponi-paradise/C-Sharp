//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Diagnostics.Contracts;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles;
//using OpenCvSharp;

//namespace OpenCVVideo
//{
//    public partial class Form1 : Form
//    {
//        private new VideoCapture Capture = new VideoCapture();
//        private bool IsStop = true;
//        private SynchronizationContext SyncContext;

//        private List<DateTime> FPS = new List<DateTime>();
//        private System.Timers.Timer FPSTimer;

//        public Form1()
//        {
//            InitializeComponent();
//            RetrievalMode.Items.AddRange(Enum.GetNames(typeof(RetrievalModes)));
//            RetrievalMode.SelectedIndex = 0;
//            ContourApproximationMode.Items.AddRange(Enum.GetNames(typeof(ContourApproximationModes)));
//            ContourApproximationMode.SelectedIndex = 0;
//            SyncContext = SynchronizationContext.Current;
//            FPSTimer = new System.Timers.Timer();
//            FPSTimer.Interval = 100;
//            FPSTimer.Elapsed += WriteTimer_Elapsed;
//            FPSTimer.Start();
//        }

//        private void WriteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) => FPS.RemoveAll(frame => frame.AddSeconds(1) < DateTime.Now);

//        private void Processing()
//        {
//            IsStop = false;
//            bool isCamera = Capture.CaptureType == CaptureType.Camera ? true : false;
//            double videoFPS = isCamera ? 30 : Capture.Fps;
//            //VideoWriter videoWriter = new VideoWriter($@"C:\TestWriteVideo.avi", VideoWriter.FourCC(Capture.FourCC), videoFPS, new OpenCvSharp.Size() { Width = Capture.FrameWidth, Height = Capture.FrameHeight });

//            double sleepTimeBase = (1000 / videoFPS) - 15.6 > 0 ? (1000 / videoFPS) - 15.6 : 0;
//            int videoSleepTime = (int)Math.Round(sleepTimeBase);
//            int indexForGC = 0;

//            while (!IsStop)
//            {
//                bool useFilter = false;
//                bool useThreshold = false;
//                bool useCanny = false;
//                bool useContour = false;
//                SyncContext.Send(delegate
//                {
//                    useFilter = UseFilter.Checked;
//                    useThreshold = UseThreshold.Checked;
//                    useCanny = UseCanny.Checked;
//                    useContour = UseContour.Checked;
//                }, null);

//                FPS.Add(DateTime.Now);
//                int finalFPS = (int)(1000 / FPS.Count);

//                Mat frame = new Mat();
//                Capture.Read(frame);
//                if (!isCamera) if (frame.Empty()) { IsStop = true; break; }
//                Mat result = frame.Clone();

//                if (useFilter || useThreshold || useCanny || useContour) frame = frame.CvtColor(ColorConversionCodes.RGB2GRAY);
//                if (useFilter) frame = FilterProcessing(frame);
//                if (useThreshold) frame = ThresholdProcessing(frame);
//                if (useCanny) frame = CannyProcessing(frame);
//                if (useContour) result = ContourProcessing(frame, result);
//                if (!useContour) Cv2.PutText(frame, $"FPS : {finalFPS}", new OpenCvSharp.Point(0, 50), HersheyFonts.HersheySimplex, 1, Scalar.Blue);
//                else Cv2.PutText(result, $"FPS : {finalFPS}", new OpenCvSharp.Point(0, 50), HersheyFonts.HersheySimplex, 1, Scalar.Blue);

//                var image = useContour ? OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result) : OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);

//                //videoWriter.Write(frame);

//                SyncContext.Post(delegate
//                {
//                    //if (!useContour) pictureBox1.Image = image;  // videoWriter.Write(frame); }
//                    //else { pictureBox1.Image =  }//videoWriter.Write(result); }
//                    pictureBox1.Image = image;
//                    frame.Release();
//                    result.Release();
//                }, null);

//                if (!isCamera) Thread.Sleep(videoSleepTime);

//                if (indexForGC++ == 60)
//                {
//                    GC.Collect();
//                    GC.WaitForPendingFinalizers();
//                    indexForGC = 0;
//                }
//            }
//            //videoWriter.Release();
//            Capture.Release();
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//        }

//        private enum FilterType
//        {
//            Median,
//            Gaussian
//        }

//        private Mat FilterProcessing(Mat frame)
//        {
//            FilterType filterType = FilterType.Median;
//            int filterkSize = 0;
//            int filterSigma = 0;
//            SyncContext.Send(delegate
//            {
//                filterType = UseMedian.Checked ? FilterType.Median : FilterType.Gaussian;
//                filterkSize = (int)FilterkSize.Value;
//                filterSigma = (int)FilterSigma.Value;
//            }, null);

//            if (filterType == FilterType.Median) Cv2.MedianBlur(frame, frame, filterkSize);
//            else Cv2.GaussianBlur(frame, frame, new OpenCvSharp.Size(filterkSize, filterkSize), filterSigma, filterSigma, BorderTypes.Default);
//            return frame;
//        }

//        //private enum ThresholdType
//        //{
//        //    Binary,
//        //    Otsu,
//        //    Adaptive_Mean,
//        //    Adaptive_Gaussian,
//        //}

//        private Mat ThresholdProcessing(Mat frame)
//        {
//            ThresholdTypes thresholdType = ThresholdTypes.Binary;
//            int threshold = 0;
//            SyncContext.Send(delegate
//            {
//                thresholdType = UseBinary.Checked ? ThresholdTypes.Binary : UseInvert.Checked ? ThresholdTypes.BinaryInv : ThresholdTypes.Otsu;
//                threshold = (int)ThresholdValue.Value;
//            }, null);

//            Cv2.Threshold(frame, frame, threshold, 255, thresholdType);
//            return frame;
//        }

//        private Mat CannyProcessing(Mat frame)
//        {
//            int canny1 = 0;
//            int canny2 = 0;
//            int cannyAperture = 0;
//            SyncContext.Send(delegate
//            {
//                canny1 = CannyThreshold1.Value;
//                canny2 = CannyThreshold2.Value;
//                cannyAperture = (int)CannyApertureSize.Value;
//            }, null);

//            Cv2.Canny(frame, frame, canny1, canny2, cannyAperture, true);
//            return frame;
//        }

//        private enum ContourType
//        {
//            Polygon,
//            Rect,
//            Ellipse,
//            Circle,
//        }

//        private Mat ContourProcessing(Mat frame, Mat result)
//        {
//            ContourType contourType = ContourType.Polygon;
//            RetrievalModes retrievalMode = RetrievalModes.Tree;
//            ContourApproximationModes contourAppMode = ContourApproximationModes.ApproxTC89KCOS;
//            double polygonApprox = 0;
//            int arcLengthMin = 0;
//            int areaMin = 0;
//            int areaMax = 0;

//            SyncContext.Send(delegate
//            {
//                contourType = UsePolygon.Checked ? ContourType.Polygon : UseRect.Checked ? ContourType.Rect : UseEllipse.Checked ? ContourType.Ellipse : ContourType.Circle;
//                retrievalMode = (RetrievalModes)Enum.Parse(typeof(RetrievalModes), RetrievalMode.SelectedItem.ToString());
//                contourAppMode = (ContourApproximationModes)Enum.Parse(typeof(ContourApproximationModes), ContourApproximationMode.SelectedItem.ToString());
//                polygonApprox = (double)PolygonApprox.Value;
//                arcLengthMin = ArcLength.Value;
//                areaMin = AreaMin.Value;
//                areaMax = AreaMax.Value;
//            }, null);

//            Cv2.FindContours(frame, out var contours, out var hierarchy, retrievalMode, contourAppMode);

//            List<OpenCvSharp.Point[]> nContours = new List<OpenCvSharp.Point[]>();
//            double arcLength = 0;
//            double areaSize = 0;
//            foreach (var p in contours)
//            {
//                switch (contourType)
//                {
//                    case ContourType.Polygon:
//                        arcLength = Cv2.ArcLength(p, true);
//                        if (arcLength > arcLengthMin) nContours.Add(Cv2.ApproxPolyDP(p, arcLength * polygonApprox / 100, true));
//                        break;

//                    default:
//                        arcLength = Cv2.ArcLength(p, true);
//                        areaSize = Cv2.ContourArea(p, true);
//                        if (arcLength > arcLengthMin && areaSize > areaMin && areaSize < areaMax)
//                        {
//                            switch (contourType)
//                            {
//                                case ContourType.Rect:
//                                    Cv2.Rectangle(result, Cv2.BoundingRect(p), Scalar.Red);
//                                    break;

//                                case ContourType.Ellipse:
//                                    try
//                                    {
//                                        if (p.Length >= 5) Cv2.Ellipse(result, Cv2.FitEllipse(p), Scalar.Red);
//                                    }
//                                    catch
//                                    { }
//                                    break;

//                                case ContourType.Circle:
//                                    Cv2.MinEnclosingCircle(p, out var center, out var radius);
//                                    Cv2.Circle(result, (int)center.X, (int)center.Y, (int)radius, Scalar.Red);
//                                    break;
//                            }
//                        }
//                        break;
//                }
//            }
//            if (contourType == ContourType.Polygon) Cv2.DrawContours(result, nContours, -1, Scalar.Red);

//            return result;
//        }

//        private void LoadVideo_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//            if (ofd.ShowDialog() == DialogResult.OK)
//            {
//                var fileName = ofd.FileName;
//                Capture.Release();
//                GC.Collect();
//                GC.WaitForPendingFinalizers();
//                Capture = new VideoCapture(fileName);
//                Thread thread = new Thread(Processing);
//                thread.IsBackground = true;
//                thread.Start();
//            }
//        }

//        private void OpenCam_Click(object sender, EventArgs e)
//        {
//            Capture.Release();
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//            VideoCapturePara param = new VideoCapturePara(VideoAccelerationType.Any, 1);
//            Capture = new VideoCapture(0, VideoCaptureAPIs.ANY, param);
//            Contrast.Value = (int)Capture.Contrast;
//            Gamma.Value = (int)Capture.Gamma;
//            Saturation.Value = (int)Capture.Saturation;
//            Exposure.Value = (int)Capture.Exposure;
//            Sharpness.Value = (int)Capture.Sharpness;
//            Thread thread = new Thread(Processing);
//            thread.IsBackground = true;
//            thread.Start();
//        }

//        private void StopPlay_Click(object sender, EventArgs e)
//        {
//            if (!IsStop) IsStop = true;
//        }

//        private void Contrast_Scroll(object sender, EventArgs e)
//        {
//            if (Capture.IsOpened())
//            {
//                if (Capture.CaptureType == CaptureType.Camera)
//                {
//                    Capture.Contrast = Contrast.Value;
//                }
//            }
//        }

//        private void Gamma_Scroll(object sender, EventArgs e)
//        {
//            if (Capture.IsOpened())
//            {
//                if (Capture.CaptureType == CaptureType.Camera)
//                {
//                    Capture.Gamma = Gamma.Value;
//                }
//            }
//        }

//        private void Saturation_Scroll(object sender, EventArgs e)
//        {
//            if (Capture.IsOpened())
//            {
//                if (Capture.CaptureType == CaptureType.Camera)
//                {
//                    Capture.Saturation = Saturation.Value;
//                }
//            }
//        }

//        private void Exposure_Scroll(object sender, EventArgs e)
//        {
//            if (Capture.IsOpened())
//            {
//                if (Capture.CaptureType == CaptureType.Camera)
//                {
//                    Capture.Exposure = Exposure.Value;
//                }
//            }
//        }

//        private void Sharpness_Scroll(object sender, EventArgs e)
//        {
//            if (Capture.IsOpened())
//            {
//                if (Capture.CaptureType == CaptureType.Camera)
//                {
//                    Capture.Sharpness = Sharpness.Value;
//                }
//            }
//        }
//    }
//}