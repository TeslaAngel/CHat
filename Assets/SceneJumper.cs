using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper : MonoBehaviour
{
    public string sceneForJump;

    public void jump()
    {
        SceneManager.LoadScene(sceneForJump);
    }

    public void jump(string sfj)
    {
        SceneManager.LoadScene(sfj);
    }
}
