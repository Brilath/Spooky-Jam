using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Image sheepImage;
    [SerializeField] private float fadeInAmount;
    [SerializeField] private float fadeOutAmount;

    private void Start()
    {
        //FadeIn();
    }

    private void FadeIn()
    {
        Color currentColor;
        while (sheepImage.color.a <= fadeInAmount)
        {
            currentColor = new Color(sheepImage.color.r, sheepImage.color.g, sheepImage.color.b, sheepImage.color.a + .01f);
            sheepImage.color = currentColor;
        }
        FadeOut();
    }

    private void FadeOut()
    {
        Color currentColor;
        while (sheepImage.color.a >= fadeOutAmount)
        {
            currentColor = new Color(sheepImage.color.r, sheepImage.color.g, sheepImage.color.b, sheepImage.color.a - 1);
            sheepImage.color = currentColor;
        }
        FadeIn();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
