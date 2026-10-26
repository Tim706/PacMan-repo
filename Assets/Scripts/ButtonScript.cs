using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public void Level1loader()
    {
        SceneManager.LoadScene("PacManLvl1");
    }

    public void Level2loader()
    {
        SceneManager.LoadScene("OtherScene");
    }

    public void LevelStartScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
