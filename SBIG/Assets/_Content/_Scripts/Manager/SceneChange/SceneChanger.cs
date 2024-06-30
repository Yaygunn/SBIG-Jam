using UnityEngine.SceneManagement;
using Utilities.Singleton;

namespace Managers.SceneChange
{
    public class SceneChanger : Singleton<SceneChanger>
    {
        public void GoSampleScene()
        {
            LoadScene("SampleScene");
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}