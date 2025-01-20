using System.Linq;
using UnityEngine;

namespace Wintor.Util
{
    /// <summary>
    /// A singleton pattern that extends MonoBehavior.
    /// Initialize this the same way you would any other MonoBehavior and define an empty, protected and parameterless constructor.
    /// </summary>
    /// <typeparam name="T">Should be a reference to the object itself ("public class foo : MonoSingleton<foo>" would be the correct way of extending).</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static readonly object _lock = new object();
        private static bool _applicationIsQuitting;

        public static bool HasInstance()
        {
            return _instance != null;
        }
        
        /// <summary>
        /// Retrieve the singleton. 
        /// ***USE THIS TO GET THE SINGLETON, NOT A CONSTRUCTOR***
        /// </summary>
        public static T Instance
        {
            set => _instance = value;
            get
            {
                if (_applicationIsQuitting)
                {
                    return null;
                }

                lock (_lock)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        foreach (var go in FindObjectsOfType<T>().Where(go => !go.Equals(_instance)))
                        {
                            Destroy(go.gameObject);
                        }
                    }

                    if (_instance != null)
                    {
                        _instance.Init();
                        return _instance;
                    }

                    var singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + typeof(T);

                    //For some unit testing check if application is playing before calling DontDestroyOnLoad(singleton);
                    if (Application.isPlaying)
                    {
                        DontDestroyOnLoad(singleton);
                    }
                    _instance.Init();
                    return _instance;
                }
            }
        }

        protected virtual void Init()
        {
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        /// it will create a buggy ghost object that will stay on the Editor scene
        /// even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public virtual void OnDestroy()
        {
            _applicationIsQuitting = true;
        }

        public virtual void Awake()
        {
            _applicationIsQuitting = false;
            T inst = Instance;
        }
    }
}