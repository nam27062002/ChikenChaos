using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public static class Loader
    {
        public enum Scene
        {
            GameScene,
            LoadingScene,
            MainMenuScene,
        }
        private static Scene targetScene;

        public static void Load(Scene targetScene)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
            
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}
