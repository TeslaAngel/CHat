using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;

public class RegistrationScript : MonoBehaviour
{
    public GameObject StartingPanel;
    public GameObject RegisPanel;

    public Text NameInput;
    public Text InviteCodeInput;
    public Text InviteCodePlaceholder;
    public Text WarningElement;

    public void StartToRegis()
    {
        StartingPanel.SetActive(false);
        RegisPanel.SetActive(true);
    }

    public void JumpToMain()//End the Registration
    {
        if (InviteCodeInput.text != InitInviteCode())
        {
            WarningElement.text = "邀请码填错啦";
            //WarningElement.text = InitInviteCode();
        }
        else //correct InviteCode
        {
            SendRegisEmail("UserName: " + NameInput.text + " InviteCode: " + InviteCodeInput.text);
            SaveRegisInfo(NameInput.text, InviteCodeInput.text, System.DateTime.Now.ToString("yyyyMMdd"));
            SceneManager.LoadScene("MainPage");
        }
    }


    public static string InitInviteCode()
    {
        string nowTime = System.DateTime.Now.ToString("yyyyMMdd");
        //Debug.Log(nowTime);
        string dateOfWeek = System.DateTime.Today.DayOfWeek.ToString();

        int dw = 0;
        switch (dateOfWeek)
        {
            case "Monday":
                dw = 1;
                break;
            case "Tuesday":
                dw = 2;
                break;
            case "Wednesday":
                dw = 3;
                break;
            case "Thursday":
                dw = 4;
                break;
            case "Friday":
                dw = 5;
                break;
            case "Saturday":
                dw = 6;
                break;
            case "Sunday":
                dw = 7;
                break;
        }

        string InviteCode = nowTime[dw - 1].ToString() + (7 - dw) + (100 - int.Parse(System.DateTime.Now.ToString("dd")));
        Debug.Log(InviteCode);
        return InviteCode;
    }


    private void SendRegisEmail(string message)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("3363154242@qq.com");
        mail.To.Add("3363154242@qq.com");
        mail.Subject = "[CHat]new CHat Registration";
        mail.Body = "SMTP message sent by CHat System: New registration | " + message;
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

    private void SaveRegisInfo(string name, string inviteCode, string registerDate)
    {
        PlayerPrefs.SetString("Name", name);
        PlayerPrefs.SetString("InviteCode", inviteCode);
        PlayerPrefs.SetString("RegisterDate", registerDate);
    }

    public static void ClearLocalInfo()
    {
        PlayerPrefs.DeleteAll();
    }

    private bool HasRegistered()
    {
        return (PlayerPrefs.HasKey("Name") && PlayerPrefs.HasKey("InviteCode") && PlayerPrefs.HasKey("RegisterDate"));
    }


    void Start()
    {
        if (HasRegistered())
        {
            SceneManager.LoadScene("MainPage");
        }
    }

}
