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
- 아래는 `Sender` 통신 예제이다.

<br>

## Example

<br>

```cs
using TIBCO.Rendezvous;

namespace TIBRVSender
{
    internal class Program
    {
        private static Transport? _transport;

        private static string _daemonAddress = string.Empty;    // Remote의 daemon을 쓴다면 주소 정해줌, 로컬이면 tcp 입력
        private static int _daemonPort = 0;     // Remote의 daemon을 쓴다면 정해줌, 로컬이면 7500 (Auto-Start)

        // Demo params
        private static List<string> _demoSubjects = new();
        private static List<string> _demoFields = new();

        static void Main(string[] args)
        {
            SetupDemo();

            Console.WriteLine("Connect to daemon");

            if (Connect()) Console.WriteLine("Ready to send");
            else
            {
                Console.WriteLine("Failed to connect");
                return;
            }

            while (true)
            {
                Console.WriteLine("Input X to exit");
                Console.WriteLine("Input demo message");
                var sendMessage = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(sendMessage))
                {
                    if (sendMessage.ToUpper() == "X")
                    {
                        Disconnect();
                        return;
                    }
                    else
                    {
                        SendMessage(sendMessage);
                    }
                }
            }
        }

        static bool Connect()
        {
            TIBCO.Rendezvous.Environment.Open();

            // Check connect
            try
            {
                _transport = new NetTransport(":0", ";", $"{_daemonAddress}:{_daemonPort}");
            }
            catch (RendezvousException e)
            {
                Console.WriteLine($"Create transport error - {e.Message}");
                return false;
            }
            return true;
        }

        static bool Disconnect()
        {
            _transport?.Destroy();
            _transport = null;
            TIBCO.Rendezvous.Environment.Close();
            Console.WriteLine("Disconnected");
            return true;
        }

        static void SendMessage(string sendMessage)
        {
            foreach (var subject in _demoSubjects)
            {
                Message message = new();
                message.SendSubject = subject;
                foreach (var field in _demoFields) message.AddField(field, sendMessage);

                try
                {
                    _transport?.Send(message);
                    Console.WriteLine($"TIBRV Send - Subject : {message.SendSubject}");
                    Console.WriteLine($"TIBRV Send - message : {message}");
                }
                catch (RendezvousException e)
                {
                    Console.WriteLine($"Failed to send a message - {e.Message}");
                }
            }
        }

        static void SetupDemo()
        {
            _daemonAddress = "tcp";
            _daemonPort = 7500;

            for (int i = 0; i < 3; i++)
            {
                _demoSubjects.Add($"Demo{i}");
                _demoFields.Add($"Field{i}");
            }
        }
    }
}
```

<br>

## 참조 자료

<br>

- [TIBCO Rendezvous® 8.6.0](https://docs.tibco.com/products/tibco-rendezvous-8-6-0)
- [TIBCO Rendezvous용 BizTalk 어댑터의 메시지](https://learn.microsoft.com/ko-kr/biztalk/core/messages-in-biztalk-adapter-for-tibco-rendezvous)