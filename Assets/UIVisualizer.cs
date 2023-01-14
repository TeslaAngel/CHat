using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVisualizer : MonoBehaviour
{
    public Text[] UIImages;
    public float VisualizeInterval;
    private float VisualizingInterval;
    private int Visualizing = 0; //1, appear; -1, disappear;

    private int CurrentUI = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize VingI
        VisualizingInterval = 0;
        Visualizing = 1;
        CurrentUI = 0;

        //Force Disappear
        for (int i = 0; i < UIImages.Length; i++)
        {
            UIImages[i].color = new Color(UIImages[i].color.r, UIImages[i].color.g, UIImages[i].color.b, 0);
        }
    }

    void OnEnable() //make UI Images appear in order
    {
        //Initialize VingI
        VisualizingInterval = 0;
        Visualizing = 1;
        CurrentUI = 0;

        //Force Disappear
        for (int i = 0; i < UIImages.Length; i++)
        {
            UIImages[i].color = new Color(UIImages[i].color.r, UIImages[i].color.g, UIImages[i].color.b, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Visualizing == 1)
        {
            VisualizingInterval += Time.deltaTime;

            //Appear in order
            UIImages[CurrentUI].color = new Color(UIImages[CurrentUI].color.r, UIImages[CurrentUI].color.g, UIImages[CurrentUI].color.b, VisualizingInterval / VisualizeInterval);
            if (VisualizingInterval >= VisualizeInterval)
            {
                VisualizingInterval = 0;
                CurrentUI++;

                if (CurrentUI == UIImages.Length)
                {
                    Visualizing = 0;
                    CurrentUI = 0;
                }
            }
        }


    }
}

