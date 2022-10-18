using UnityEngine;
using UnityEngine.SceneManagement;

namespace Autohand.Demo
{
    public class StartLevel : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
