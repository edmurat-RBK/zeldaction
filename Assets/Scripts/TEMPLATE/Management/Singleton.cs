using UnityEngine;

namespace Management
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {

        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;

        /// <summary>
        /// Gets the instance of the Singleton.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Use to make the object a Singleton.
        /// </summary>
        protected void MakeSingleton(bool makeUndestroyableOnLoad)
        {
            if (instance == null)
            {
                instance = this as T;
                if (makeUndestroyableOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}