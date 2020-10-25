using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(string.Compare(collision.gameObject.tag, "Player") == 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
