using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RImgInd : MonoBehaviour
{
    public Text title;
    public RawImage rawImage;
    public int PostNum;
    public NewsCentral newsCentral;

    public void Open()
    {
        newsCentral.openPost(PostNum);
    }

}
