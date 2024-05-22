## 1. Introduction

<br>

- `lock` 문은 하나의 스레드만 블록에 접근 가능하도록 하며, 다음과 같이 동작한다.
    1. 잠금을 획득
    2. `lock` 블록 실행
    3. 잠금 해제
    - 실행되는 동안 다른 스레드는 해제가 될 때까지 대기
- `lock` 블록에는 [await 식](https://peponi-paradise.tistory.com/entry/C-Language-Async-Await)을 사용할 수 없다.
- `lock`의 잠금 객체로 다음 유형은 사용하지 않도록 한다.
    - this
    - Type
    - string

<br>

## 2. Lock statement

<br>

- `lock` 문은 다음과 같이 사용한다.
    ```cs
    object _key = new();

    lock (_key)
    {
    }
    ```
- 만약 하나의 변수에 멀티스레드 접근을 하는 경우, 아래와 유사한 결과를 얻을 수 있다.
    ```cs
    public class Program
    {
        private static object _key = new();
        private static bool _isLocked = false;

        static async Task Main(string[] args)
        {
            List<Task> tasks = new();
            for (int i = 0; i < 10; i++) tasks.Add(Task.Run(WithLock));
            await Task.WhenAll(tasks);

            Console.WriteLine();

            tasks = new();
            for (int i = 0; i < 10; i++) tasks.Add(Task.Run(WithoutLock));
            await Task.WhenAll(tasks);
        }

        public static void WithLock()
        {
            lock (_key)
            {
                _isLocked = !_isLocked;
                Console.WriteLine(_isLocked);
            }
        }

        public static void WithoutLock()
        {
            _isLocked = !_isLocked;
            Console.WriteLine(_isLocked);
        }
    }
    ```
    ```cs
    // WithLock result
    True
    False
    True
    False
    True
    False
    True
    False
    True
    False
    ```
    ```cs
    // WithoutLock result
    True
    False
    False
    False
    True
    False
    True
    False
    True
    True
    ```

<br>

## 3. 참조 자료

<br>

- [lock 문 - 공유 리소스에 대한 단독 액세스 확인](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/lock)