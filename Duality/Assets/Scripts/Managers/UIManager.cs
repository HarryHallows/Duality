using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Handles the data for each puzzle object and whether or not they're in use
[System.Serializable]
public class Puzzles
{
    public GameObject puzzlePrefab;
    public bool started, completed;
}

public class UIManager : MonoBehaviour
{
    private GameManager gm;
    private PoolObjectType type;
    public Puzzles puzzleInfo; //Puzzle class reference

    [SerializeField] private Spawner spawner;
    [SerializeField] private bool resetRotation;

    private void Awake()
    {
        gm = GameManager.Instance;
        spawner = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        if (resetRotation)
        {
            StartCoroutine(ResetRotation(type));
        }
    }
    #region Dialogue System

    #endregion

    #region Buttons


    //TODO : Think about how you want to handle this aspect with the main narrative
    //STRECH GOAL: Implement this mechanic
    public void PowerButton()
    {
        //If pressed objects that can become transparant activate and the lighting changes e.g. the BG colour becomes darker
    }


    public GameObject CurrentPuzzle(GameObject _currentPuzzle, PuzzleObjects _puzzleType)
    {
        switch (_puzzleType)
        {
            case PuzzleObjects.FOOTBALL:
                //Activate puzzle game
                //Call functionality of the Football Mini-Game/Puzzle
                Debug.Log("Football Puzzle Activate");
                GameManager.Instance.EnvrionmentRotate(false);
                return puzzleInfo.puzzlePrefab = _currentPuzzle;
            case PuzzleObjects.HANDBAG:
                //Activate puzzle game
                //Call functionality of the Handbag Mini-Game/Puzzle
                GameManager.Instance.EnvrionmentRotate(false);
                Debug.Log("Handbag Puzzle Activate");
                return puzzleInfo.puzzlePrefab = _currentPuzzle;
            case PuzzleObjects.PAINTING:
                //Activate puzzle game
                //Call functionality of the Painting Mini-Game/Puzzle
                GameManager.Instance.EnvrionmentRotate(false);
                Debug.Log("Painting Puzzle Activate");
                return puzzleInfo.puzzlePrefab = _currentPuzzle;
        }
        return null;
    }

    public void SpawnerToRemoveFloor(PoolObjectType _type)
    {
        type = _type;
        GameManager.Instance.EnvrionmentRotate(false);
        spawner.tileCount = 0;
        //StartCoroutine(spawner.RemoveFloor(_type));
        resetRotation = true;
    }

    private IEnumerator ResetRotation(PoolObjectType _type)
    {
        Debug.Log($"{gm.environment.transform.rotation.y} : Resetting Rotation");
        gm.ResetEnvironmentRotation();

        yield return new WaitUntil(() => gm.environment.transform.eulerAngles.y <= 0.001f && gm.environment.transform.eulerAngles.y >= -0.001f);
        Debug.Log("Calling Floor Reset Coroutine");
        spawner.RemoveFloorActivate(_type);
        resetRotation = false;
    }

    public void SpawnerToRemoveWalls(PoolObjectType _type)
    {
        GameManager.Instance.EnvrionmentRotate(false);
        spawner.StartCoroutine(spawner.RemoveWalls(_type));
    }
    #endregion

}
