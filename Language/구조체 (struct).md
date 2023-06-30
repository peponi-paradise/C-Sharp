## Introduction

<br>

- `struct` (구조체) 는 객체 및 메서드를 캡슐화 할 수 있는 [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)이다. `struct` 키워드로 정의한다.
- `값 형식`이므로 할당, 메서드 인수 전달 및 리턴 시 인스턴스가 복사된다.
    - `struct`는 인스턴스가 하나의 값을 의미하므로, 내부 데이터를 변경할 수 없는 형식으로 정의하는 것이 좋다.
- `값 형식`이기 때문에, 작은 크기의 읽기용 데이터 구조를 만드는 데 주로 사용한다. (데이터 자체보다 데이터의 거동이 중요한 경우, 참조 형식인 [클래스](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/class)가 더 적합하다)

<br>

## struct 정의

<br>

- `struct`는 기본적으로 다음과 같이 정의한다.
    ```cs
    public struct SerialCommunicationInformation
    {
        public string Port;
        public int Baudrate;
        public System.IO.Ports.Parity Parity;
        public int Databits;
        public System.IO.Ports.StopBits Stopbits;
        public System.IO.Ports.Handshake Handshake;
        public string STX;
        public string ETX;
    }
    ```