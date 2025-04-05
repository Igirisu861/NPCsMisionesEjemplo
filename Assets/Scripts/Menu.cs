using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onClickStart()
    {
        SceneManager.LoadScene("NPCLevel");
    }
}
