
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
   [SerializeField] float levelLoadDelay = 2f;
   [SerializeField] AudioClip crash;
   [SerializeField] AudioClip success;

   [SerializeField] ParticleSystem successPaticles;
   [SerializeField] ParticleSystem crashParticles;

   AudioSource audioSource;

   bool istransitioning = false;

   
   void Start()
   {
      audioSource = GetComponent<AudioSource>();
   }

   void OnCollisionEnter(Collision other)
   {
      if (istransitioning) 
      {
         return; 
      }

      switch(other.gameObject.tag)
      {
         case "Friendly":
            Debug.Log("this is friendly");
            break;
         case "Finish":
            StartSuccessSequence();
            break;
         default:
            StartCrashSequence();
            break;
      }
   }

   void StartSuccessSequence()
   {
     istransitioning = true;
     audioSource.Stop();
     audioSource.PlayOneShot(success);
     successPaticles.Play();
     GetComponent<movement>().enabled = false;
     Invoke("LoadNextLevel", levelLoadDelay);
   }

   void StartCrashSequence()
   {
      istransitioning = true;
      audioSource.Stop();
      audioSource.PlayOneShot(crash);
      crashParticles.Play();
      GetComponent<movement>().enabled = false;
      Invoke("ReloadLevel", levelLoadDelay);
   }

   void ReloadLevel()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex);
   }

   void LoadNextLevel()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = currentSceneIndex + 1;
      if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
      {
         nextSceneIndex = 0;
      }
      SceneManager.LoadScene(nextSceneIndex);
   }
}
