using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;

public class EditSender : MonoBehaviour
{

    private string passageBody;
    public GameObject Sender;
    public Text Input;

    private void SendPassageEmail(string passage)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("3363154242@qq.com");
        mail.To.Add("3363154242@qq.com");
        mail.Subject = "[CHat]new Passage Sent by CHat user " + PlayerPrefs.GetString("Name");
        mail.Body = "SMTP message sent by CHat System: New passage \n " + passage;
        //mail.Attachments.Add(new Attachment("screen.png"));

        SmtpClient smtpServer = new SmtpClient("smtp.qq.com");
        smtpServer.Credentials = new System.Net.NetworkCredential("3363154242@qq.com", "qqtnhxxfwejfchig") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

        smtpServer.Send(mail);
        Debug.Log("success");
    }

    public void activateSender()
    {
        Sender.SetActive(true);
    }

    public void send()
    {
        SendPassageEmail(Input.text);
        Sender.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Sender.SetActive(false);
    }
}
