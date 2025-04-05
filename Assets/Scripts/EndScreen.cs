using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public void onClickRestart()
    {
        SceneManager.LoadScene("NPCLevel");
    }
    public void onClickQuit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
