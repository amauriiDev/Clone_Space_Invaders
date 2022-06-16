using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Master : MonoBehaviour
{
    #region Singleton
    private static Master instance;
    public static Master Instance { get { return instance; } }

    private void Awake() {
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }
    #endregion

    public GameController gameController {get; private set;}    // Reference to the GameController
    public AudioController audioController {get; private set;}   // Reference to the AudioController
    public GameObject bullet;        // Reference to prefab bullet
    public Text txtScore;            // Reference to the Score Text (Canvas)
    public Text txtLives;            // Reference to the Lives Text (Canvas)

    void Start() {
        gameController = GetComponentInChildren<GameController>();      // Assigning the component GameController
        audioController = GetComponentInChildren<AudioController>();    // Assigning the component AudioController
    }
}
