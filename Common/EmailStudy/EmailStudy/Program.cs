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