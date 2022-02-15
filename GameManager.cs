using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; } = null;

        [Tooltip("player script in main camera (head)")]
        public Player player;

        public List<ScriptableObject> scriptableObjects;

        private void Awake()
        {
            if (null == Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.HighTop;
            OVRManager.useDynamicFixedFoveatedRendering = true;

            SceneManager.LoadScene("Middle", LoadSceneMode.Additive);
            SceneManager.LoadScene("Stage1", LoadSceneMode.Additive);
        }

    }
}
