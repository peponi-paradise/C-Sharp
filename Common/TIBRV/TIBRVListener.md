## Introduction

<br>

- TIBCO Rendezvous는 TIBCO社의 메시징 미들웨어이다.
- 유료 소프트웨어로, 구매하지 않고는 사용할 수 없다.
- 여러 프로그래밍 언어를 지원하여 다양한 구성으로 통신이 가능하다.
- 사용하면서 느낀 가장 큰 특징은 아래와 같다.
    - 네트워크 정보를 알 필요 없이 `Subject`를 `Listener`에 등록하여 원하는 메시지를 받을 수 있다.
        => 네트워크 시스템 구성 요소의 추가, 제거가 자유롭다
    - 생산자 측에서 메시지에 새로운 필드를 추가하더라도, 각 `Listener`들은 통신을 수정할 필요가 없다.
        => 하나의 구성 요소에서 디테일을 분리하여 운용할 수 있다.
- 아래는 `Listener` 통신 예제이다.

<br>

## Example

<br>

```cs
using TIBCO.Rendezvous;

namespace TIBRVListener
{
    internal class Program
    {
        private static Transport? _transport;
        private static List<Listener> _listeners = new();
        private static List<Dispatcher> _dispatchers = new();

        private static int _servicePort = 0;    // Remote에서 정해줌. 따로 정의되지 않으면 0
        private static string _remoteIPAddress = string.Empty;  // Remote 주소. 비워두고 사용하면 rvd에 잡히는 primary로 사용
        private static string _multicastIPAddress = string.Empty;   // Remote에서 정해줌. 멀티캐스트 사용하는 경우에 입력
        private static string _daemonAddress = string.Empty;    // Remote의 daemon을 쓴다면 주소 정해줌, 로컬이면 tcp 입력
        private static int _daemonPort = 0;     // Remote의 daemon을 쓴다면 정해줌, 로컬이면 7500 (Auto-Start)

        // Demo params
        private static List<string> _demoSubjects = new();

        static void Main(string[] args)
        {
            SetupDemo();

            Console.WriteLine("Connect to remote");

            if (Connect()) Console.WriteLine("Ready to listen");
            else
            {
                Console.WriteLine("Failed to listen");
                return;
            }

            Console.WriteLine("Press enter to exit");
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Disconnect();
                        return;

                    default:
                        continue;
                }
            }
        }

        static bool Connect()
        {
            TIBCO.Rendezvous.Environment.Open();

            // Check connect
            try
            {
                // 가운데 Network parameter `;` 사용 주의
                _transport = new NetTransport($":{_servicePort}", $"{_remoteIPAddress};{_multicastIPAddress}", $"{_daemonAddress}:{_daemonPort}");
            }
            catch (RendezvousException e)
            {
                Console.WriteLine($"Create transport error - {e.Message}");
                return false;
            }

            // Try to create listener
            try
            {
                foreach (var subject in _demoSubjects)
                {
                    var listener = new Listener(new Queue(), _transport, subject, new object());
                    listener.MessageReceived += Receive;
                    var dispatcher = new Dispatcher(listener.Queue);
                    _listeners.Add(listener);
                    _dispatchers.Add(dispatcher);
                }
            }
            catch
            {
                Console.WriteLine("Failed to create listener and dispatcher");
                return false;
            }

            return true;
        }

        static bool Disconnect()
        {
            foreach (var dispatcher in _dispatchers) dispatcher.Destroy();
            foreach (var listener in _listeners) listener.Destroy();
            _transport?.Destroy();
            _transport = null;
            TIBCO.Rendezvous.Environment.Close();
            Console.WriteLine("Disconnected");
            return true;
        }

        // 여러 메시지가 동시에 들어올 때 콘솔 출력이 꼬여 lock 설정. 실제 운용에서는 lock 필요 없음
        private static object _locker = new();

        static void Receive(object listener, MessageReceivedEventArgs message)
        {
            Listener listenObject = (Listener)listener;
            lock (_locker)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{listenObject.Subject} received");
                Console.WriteLine($"Send subject : {message.Message.SendSubject}, size : {message.Message.Size}, field count : {message.Message.FieldCount}");
                Console.WriteLine($"Message : {message.Message}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            }
        }

        static void SetupDemo()
        {
            _daemonAddress = "tcp";
            _daemonPort = 7500;

            for (int i = 0; i < 3; i++) _demoSubjects.Add($"Demo{i}");
        }
    }
}
```

<br>

## 참조 자료

<br>

- [TIBCO Rendezvous® 8.6.0](https://docs.tibco.com/products/tibco-rendezvous-8-6-0)
- [TIBCO Rendezvous용 BizTalk 어댑터의 메시지](https://learn.microsoft.com/ko-kr/biztalk/core/messages-in-biztalk-adapter-for-tibco-rendezvous)