using System.Collections.Generic;


namespace GXPEngine.Control
{
	internal class SceneManager
	{
		private readonly Dictionary<string, Scene> SceneMap = new Dictionary<string, Scene>();
		private Scene currentScene;
		private readonly GameObject parent;

		public SceneManager(GameObject game)
		{
			parent = game;
		}

		public void AddScene(string sceneKey, Scene scene)
		{
			SceneMap.Add(sceneKey, scene);
		}
		public void RemoveScene(string scene)
		{
			SceneMap.Remove(scene);
		}
		public Scene GetScene(string sceneKey) => SceneMap[sceneKey];

		public void SwitchScene(string scene)
		{
			Scene newScene = GetScene(scene);

			// Remove current scene from hierarchy
			currentScene?.Unload();

			// Add new scene to hierarchy
			newScene.Load(parent);
			currentScene = newScene;
		}
		public void Reload()
		{
			currentScene.Initialize();
		}
	}
}
