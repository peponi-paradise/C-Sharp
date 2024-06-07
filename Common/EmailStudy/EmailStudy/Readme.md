## 1. Introduction

<br>

- `System.Net.Mail`의 [SmtpClient](https://learn.microsoft.com/ko-kr/dotnet/api/system.net.mail.smtpclient?view=net-8.0)를 이용하면 쉽게 email을 보낼 수 있다.
- Email 전송을 위해 SMTP 서버가 필요한데, [naver](https://naver.com), [google](https://google.com) 등의 외부 서버를 사용하거나 IIS에 SMTP 서버를 올리는 등의 방법을 사용할 수 있다.
- 여기서는 email 전송을 수행하는 간단한 방법을 알아본다.

<br>

## 2. Example

<br>

```cs
using System.Net;
using System.Net.Mail;

namespace EmailStudy;

internal class Program
{
    private static void Main(string[] args)
    {
        // 발신자, 수신자, 제목, 내용
        MailMessage message = new("sender@naver.com", "receiver@daum.net", "SMTP Test", "This is message of mail");

        // 참조 추가
        // message.CC.Add("CC1@naver.com");

        // HTML 문서를 보내는 경우 true 설정
        // message.IsBodyHtml = true;

        // SMTP 서버 주소, 포트
        SmtpClient client = new("smtp.naver.com", 587)
        {
            // SSL 필요한 경우 사용
            EnableSsl = true,

            // 이메일 ID, password
            Credentials = new NetworkCredential("Email id", "Password")
        };

        client.Send(message);
    }
}
```

<br>

## 3. 특이사항

<br>

- Microsoft의 문서에 따르면, 최신 프로토콜을 지원하지 않기 때문에 `SmtpClient`는 사용하지 않는 것이 좋다고 한다.
- 이를 [MailKit](https://github.com/jstedfast/MailKit) 또는 다른 라이브러리로 대체하는 것을 권장하고 있다.
- 아래는 공식 문서에 올라와 있는 내용이다.
    > 많은 최신 프로토콜을 `SmtpClient` 지원하지 않으므로 새 개발에 `SmtpClient` 클래스를 사용하지 않는 것이 좋습니다. 대신 [MailKit](https://github.com/jstedfast/MailKit) 또는 다른 라이브러리를 사용합니다. 자세한 내용은 [GitHub에서 SmtpClient를 사용하면 안 됨](https://github.com/dotnet/platform-compat/blob/master/docs/DE0005.md) 을 참조하세요.

<br>

## 4. 참조 자료

<br>

- [SmtpClient 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.net.mail.smtpclient?view=net-8.0)