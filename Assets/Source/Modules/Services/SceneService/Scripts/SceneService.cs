using UnityEngine.SceneManagement;

namespace Modules.Services.Scripts
{
    public class SceneService : ISceneService
    {
        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
