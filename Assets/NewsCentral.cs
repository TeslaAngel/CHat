using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

public class NewsCentral : MonoBehaviour
{

    public string destinatedURL;
    public string html;

    public GameObject UIPrefab;
    public Transform PanelTransform;

    public List<string> ImageName = new List<string>();
    public List<string> titleImageName = new List<string>();
    public List<string> contentImageName = new List<string>();

    public GameObject PostView;

    public void Request()
    {
        HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(destinatedURL);

        webRequest.Method = "GET";

        webRequest.UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:94.0) Gecko/20100101 Firefox/94.0";

        WebResponse webResponse = webRequest.GetResponse();
        using (Stream stream = webResponse.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                this.html = reader.ReadToEnd();
            }
        }
        System.Console.WriteLine(html);
    }

    public List<string> getTitlePicDesFromHtml(string htmlCode) //Getting Titles & images from server
    {
        string HC = htmlCode;
        List<string> des = new List<string>();
        Regex picRegex = new Regex("[a-zA-Z0-9_]+[/.]jpg");
        MatchCollection match = picRegex.Matches(htmlCode);

        for (int i = 0; i < match.Count; i++)
        {
            des.Add(match[i].Value);
            Debug.Log(des[i] + match.Count);
        }

        return des;
    }

    private void LoadTitlePics()
    {
        for(int i = 0; i < titleImageName.Count; i++)
        {
            downloadPic("https://tachatmanager.github.io/" + titleImageName[i], Application.dataPath + "/" + titleImageName[i]);
            Debug.Log("downloading " + titleImageName[i]);
        }
    }

    private void LoadContentPics()
    {
        for (int i = 0; i < contentImageName.Count; i++)
        {
            downloadPic("https://tachatmanager.github.io/" + contentImageName[i], Application.dataPath + "/" + contentImageName[i]);
            Debug.Log("downloading " + contentImageName[i]);
        }
    }

    private void CheckTitlePics()
    {
        for(int i = 0; i < titleImageName.Count; i++)
        {
            if (File.Exists(Application.dataPath + "/" + titleImageName[i]))
            {
                // read image and store in a byte array
                byte[] byteArray = File.ReadAllBytes(Application.dataPath + "/" + titleImageName[i]);
                //create a texture and load byte array to it
                // Texture size does not matter 
                Texture2D sampleTexture = new Texture2D(2, 2);
                // the size of the texture will be replaced by image size
                bool isLoaded = sampleTexture.LoadImage(byteArray);
                // apply this texure as per requirement on image or material#####
                
                GameObject NE = UIPrefab;
                if (isLoaded)
                {
                    UIPrefab.GetComponent<RImgInd>().rawImage.texture = sampleTexture;
                    //UIPrefab.GetComponent<RImgInd>().title.text = titleImageName[i].Substring(0, titleImageName[i].Length - 4);
                    UIPrefab.GetComponent<RImgInd>().title.text = "";
                    UIPrefab.GetComponent<RImgInd>().PostNum = i;
                    UIPrefab.GetComponent<RImgInd>().newsCentral = this;
                }
                Instantiate(NE, PanelTransform);
                NE.GetComponent<RectTransform>().anchorMin = new Vector2(1.05f + 0.35f * i, 0.1f);
                NE.GetComponent<RectTransform>().anchorMax = new Vector2(1.35f + 0.35f * i, 0.9f);
                NE.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                titleImageName.RemoveAt(i);
            }
        }
    }

    private void CheckContentPics(int i)
    {
        if (File.Exists(Application.dataPath + "/" + contentImageName[i]))
        {
            // read image and store in a byte array
            byte[] byteArray = File.ReadAllBytes(Application.dataPath + "/" + contentImageName[i]);
            //create a texture and load byte array to it
            // Texture size does not matter 
            Texture2D sampleTexture = new Texture2D(2, 2);
            // the size of the texture will be replaced by image size
            bool isLoaded = sampleTexture.LoadImage(byteArray);
            // apply this texure as per requirement on image or material#####

            if (isLoaded)
            {
                PostView.GetComponent<PSVgetter>().rawImage.texture = sampleTexture;
                PostView.GetComponent<PSVgetter>().postNum = i;
            }

        }
    }

    static void downloadPic(string URL, string LocPath)
    {
        WebClient WC = new WebClient();
        WC.DownloadFile(URL, LocPath);
    }


    public void openPost(int postNum)
    {
        CheckContentPics(postNum);
        PostView.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        Request();
        ImageName = getTitlePicDesFromHtml(html);
        for(int i = 0; i < ImageName.Count; i++)
        {
            if (ImageName[i].Contains("CONTENT"))
            {
                contentImageName.Add(ImageName[i]);
            }
            else
            {
                titleImageName.Add(ImageName[i]);
            }
        }
        LoadTitlePics();
        LoadContentPics();
    }

    private void Update()
    {
        CheckTitlePics();
    }

}
