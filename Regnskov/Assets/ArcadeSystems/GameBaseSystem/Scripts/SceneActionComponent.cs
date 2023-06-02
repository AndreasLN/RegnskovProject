using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace GameBaseSystem
{
    public class SceneActionComponent : MonoBehaviour
    {
        public SceneAction sceneAction;
        public bool fadeOut = true;
        public Vector3 characterPosition;


        private Scene current;
        private Scene loaded;
        public Scene Current 
        {
            get
            {
                return current;
            }
        }

        public Scene Loaded
        {
            get
            {
                return loaded;
            }
            set
            {
                loaded = value;
            }
        }

        void Start()
        {
            CustomGameManager gameManager = Resources.FindObjectsOfTypeAll<CustomGameManager>()[0];

            Image fade = gameManager.fade;

            Animator anim = fade.GetComponent<Animator>();

            anim.SetBool("Fade", false);

            current = gameObject.scene;
        }
        public void Activate(Vector3 characterPos)
        {

            characterPosition = characterPos;

            print("hello");

            StartCoroutine(ActivateSceneAction());
        }

        public void ActivatePrevious()
        {
            StartCoroutine(ActivateSceneAction());
        }

        public void UnloadCurrent()
        {

            StartCoroutine(ActivateSceneAction(true));
        }

        public void ReloadScene ()
        {
            /*
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            void OnSceneUnloaded(Scene scene)
            {
                if (pendingSceneReload == scene.name)
                    SceneManager.LoadScene(pendingSceneReload, LoadSceneMode.Additive);
            }
            */
        }

        IEnumerator ActivateSceneAction(bool unloadCurrent = false)
        {
            float activationTime = Time.time;

            while (activationTime + sceneAction.delay > Time.time)
            {
                yield return null;
            }

            sceneAction.activationTime = Time.time;
            sceneAction.progressTime = Time.time;

            if (sceneAction.sceneActionType == SceneActionType.Load && !unloadCurrent)
            {

                if (fadeOut)
                {
                    CustomGameManager gameManager = Resources.FindObjectsOfTypeAll<CustomGameManager>()[0];

                    Image fade = gameManager.fade;

                    Animator anim = fade.GetComponent<Animator>();

                    anim.SetBool("Fade", true);

                    yield return new WaitUntil(() => fade.color.a == 1);
                }

                PlayerMovement player = Resources.FindObjectsOfTypeAll<PlayerMovement>()[0];

                Vector3 playerPos = player.gameObject.transform.position;

                player.gameObject.transform.position = new Vector3(characterPosition.x, characterPosition.y, characterPosition.z);


                SceneActionManager.instance.LoadScene(this);
            }
            else if (sceneAction.sceneActionType == SceneActionType.Unload || unloadCurrent)
            {
                SceneActionManager.instance.UnloadScene(this, unloadCurrent);
            }
            else if (sceneAction.sceneActionType == SceneActionType.Reload)
            {
                SceneActionManager.instance.UnloadScene(this);
            }
        }

    }
}

