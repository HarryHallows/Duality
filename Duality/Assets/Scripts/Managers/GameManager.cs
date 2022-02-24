using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameAreas
{
    GUIDEPOST,
    HOUSE_ENTRACE,
    HOUSE_KITCHEN,
    HOUSE_BEDROOM,
    HOUSE_BATHROOM,
    HOSPITAL,
    PARK,
}

public class GameManager : Singleton<GameManager>
{
    public GameAreas areas;

    [SerializeField] private GameObject floorTilePrefab;
    [SerializeField] private GameObject wallTilePrefab;

    public GameObject environment;

    private Spawner spawner;
    public bool combatState;
    public bool rotateEnvironment;
    public int wallPlacedCount;


    [SerializeField] private bool wallPooled;

    public bool outsideScene;

    public Material floorMaterial, wallMaterial;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        spawner = FindObjectOfType<Spawner>();
        environment = GameObject.FindGameObjectWithTag("EnvironmentRoot");
        CurrentArea(areas);
    }

    public bool Combat(bool _combatState)
    {
        return combatState = _combatState;
    }

    public bool EnvrionmentRotate(bool _envrionmentReady)
    {
        return rotateEnvironment = _envrionmentReady;
    }

    public void RotateEnvironment(float _mousePosition)
    {
        Quaternion _desiredRotation = Quaternion.Euler(environment.transform.eulerAngles.x, _mousePosition, environment.transform.eulerAngles.z);
        environment.transform.rotation = Quaternion.Slerp(environment.transform.rotation, _desiredRotation, 0.03f);
    }

    public void ResetEnvironmentRotation()
    {
        Quaternion _resetRotation = Quaternion.Euler(environment.transform.eulerAngles.x, 0, environment.transform.eulerAngles.z);
        environment.transform.rotation = Quaternion.Lerp(environment.transform.rotation, _resetRotation, .1f);
    }

    public GameAreas CurrentArea(GameAreas _area)
    {
        //checking which current area is active and altering the specific material
        switch (_area)
        {
            case GameAreas.GUIDEPOST:
                floorMaterial.color = new Color32(145, 145, 145, 225);
                break;
            case GameAreas.HOUSE_ENTRACE:
                floorMaterial.color = new Color32(53, 59, 72, 225);
                break;
            case GameAreas.HOUSE_KITCHEN:
                break;
            case GameAreas.HOUSE_BEDROOM:
                break;
            case GameAreas.HOUSE_BATHROOM:
                break;
            case GameAreas.PARK:
                floorMaterial.color = new Color32(76, 209, 55, 225);
                break;
        }

        return _area;
    }

}
