using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class exittegamebro : MonoBehaviour
{

    public Button startgameButton;
    public Button quitgameButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button btn = startgameButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
