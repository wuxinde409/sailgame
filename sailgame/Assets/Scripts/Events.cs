using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR// 如果在 Unity 編輯器中
        
        UnityEditor.EditorApplication.isPlaying = false;
        #else// 如果在執行應用中，使用 Application.Quit() 退出應用
        
        Application.Quit();
        #endif
    }
}
