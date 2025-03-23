using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject ButtonP1;

    public EventSystem ES;

    public GameManager GM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ES = EventSystem.current;
        GM = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ES.currentSelectedGameObject == null)
        {
            ES.SetSelectedGameObject(ButtonP1);
        }
    }


    public void OnClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnVolumeChange(float volume)
    {
        GM.SetAutioVolume(volume);
    }
}
