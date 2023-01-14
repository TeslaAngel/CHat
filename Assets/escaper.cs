using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escaper : MonoBehaviour
{
    public string jumpscene;

    private void Start()
    {
        Debug.Log(RegistrationScript.InitInviteCode());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            if(jumpscene == "")
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(jumpscene);
            }
        }
    }
}
