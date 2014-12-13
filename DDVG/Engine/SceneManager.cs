namespace TheGame.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TheGame.Entities;
    using TheGame.Messages;
    /// <summary>
    /// 
    /// </summary>
    public class SceneManager 
    {
        private static SceneManager instance = null;
        private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
        private Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        private static int nextEntityID = 0;

        public Scene CurrentScene = null;

        public static SceneManager Instance {
            get {
                if (instance == null) {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        public void AddScene(Scene s) {
            _scenes.Add(s.Name, s);

            s.Initialize();
        }

        public void StartScene(string name) {
            CurrentScene = _scenes[name];
            CurrentScene.Reset();
            CurrentScene.Run();
        }

        public void GotoScene(string name) {
            CurrentScene = _scenes[name];
            CurrentScene.Run();
        }


        /// <summary>
        /// Returns true if the entity or any components handled the message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessage(BaseMessage message) {
            // We look for the entity in the scene by its ID
            Entity entity;
            if (entities.TryGetValue(message._destinationEntityId, out entity)) {
                // Entity was found, so send it the message
                return entity.SendMessage(message);
            }

            // Entity with the specified ID wasn't found
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Entity CreateEntity() {
            Entity newEntity = new Entity(SceneManager.nextEntityID++);
            entities.Add(newEntity.UniqueID, newEntity);

            return newEntity;
        }
    }
}
