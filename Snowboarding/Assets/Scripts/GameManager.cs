using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public enum GameState { menu, instruction, cutscene, getReady, playing, oops, gameOverWin };

public class GameManager : MonoBehaviour
{
    // Singleton Declaration
    public static GameManager S;

    // Game State
    public GameState gameState;

    // UI variables
    public Text messageOverlay;

    // vehicle related
    public GameObject vehicle;
    private Vector3 VehStartPos;
    private Quaternion VehStartRot;

    // Reference to other game objects
    private inputData _inputData;

    private void Awake()
    {
        // Singleton Definition
        if (GameManager.S)
        {
            Destroy(this.gameObject);
        } else
        {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.menu;

        // Reset text message or any UI
        // messageOverlay.enabled = true;
        // messageOverlay.text = "";

        // Input
        _inputData = vehicle.GetComponent<inputData>();

        // init board location for reset
        VehStartPos = vehicle.gameObject.transform.position;
        VehStartRot = vehicle.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update() // USED FOR DETECTING INPUT AT CERTAIN STATES
    {
        if (gameState == GameState.menu)
        {
            messageOverlay.enabled = true;
            messageOverlay.text = "menu";
            // TODO Menu UI
            if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonVal) && buttonVal)
            {
                gameState = GameState.instruction;
            }
        }
        else if (gameState == GameState.instruction)
        {
            bool isCalibrated = false; // private since only used here
            // TODO try to do a single call to callibration that does everything

            // final "A" press triggers cutscene
            if (isCalibrated && _inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonVal) && buttonVal)
            {
                gameState = GameState.cutscene;
            }
        }
        else if (gameState == GameState.cutscene)
        {
            // TODO play cutscene
            ResetRound();
        }
        else if (gameState == GameState.oops)
        {
            // TODO UI
            if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonVal) && buttonVal)
            {
                ResetRound();
            }
        }
        else if (gameState == GameState.gameOverWin)
        {
            // TODO UI
            // TODO option 1: restart round (start up hill)
            // TODO option 2: restart whole game
        }
    }

    private void ResetRound() // want to do same thing as reset board
    {
        // reset board, score, enemy
        vehicle.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        vehicle.gameObject.transform.position = VehStartPos;
        vehicle.gameObject.transform.rotation = VehStartRot;
        // TODO destroy curr yeti, re-instantiate the yeti prefab

        // go back into getReady state
        gameState = GameState.getReady;
        StartCoroutine(GetReadyState());
    }

    private void StartRound()
    {
        gameState = GameState.playing;
        // TODO get the yeti gameobject, call the start chasing funciton in it
        // TODO in SnoboardVehicle script, change it to can only control board when
                // the state is playing/instruction/gameOverWin
    }

    public IEnumerator GetReadyState()
    {
        // TODO get ready UI
        yield return new WaitForSeconds(2.0f);
        // Turn off UI
        StartRound();
    }

    public void PlayerStuck() // TODO SnowboardVehicle call this funciton whenever stuck
    {
        // UI?
        StartCoroutine(OopsState());
    }

    public IEnumerator OopsState()
    {
        gameState = GameState.oops;
        // TODO Yeti catch up
        yield return new WaitForSeconds(2.0f);
        ResetRound();
    }

    public void PlayerHitGoal ()// TODO SnowboardVehicle call this funciton whenever hit goal box
    {
        // TODO UI
        gameState = GameState.gameOverWin;
    }
}
