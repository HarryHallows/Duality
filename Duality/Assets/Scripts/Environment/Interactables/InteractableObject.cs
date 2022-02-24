using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleObjects
{
    FOOTBALL,
    HANDBAG,
    PAINTING,
}

// RESPONSIBLE FOR OBJECTS THAT CAN BE INTERACTABLE
public class InteractableObject : MonoBehaviour
{
    public PuzzleObjects puzzleObj;

    private UIManager um;
    public bool hovering, larger;
    public bool selected;
    [SerializeField] private Vector3 originalPosition;
    public Vector3 temporaryPosition;

    public float desiredScale;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        um = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hovering)
        {
            StartCoroutine(Hovering());
        }
        else
        {
            StartCoroutine(Unhovered());
        }
    }

    //Scales up the current object when cursor casts a raycast onto this game object.
    private IEnumerator Hovering()
    {
        if (transform.localScale.y > 1)
        {
            larger = true;
        }

        if (transform.localScale.y < desiredScale)
        {
            transform.localScale += new Vector3(1.25f, 1.25f, 1.25f) * Time.deltaTime;
        }

        yield return null;
    }

    //Calls unhovered when the player removes the cursur from this object, returning scale to 1
    private IEnumerator Unhovered()
    {
        if (transform.localScale.y <= 1)
        {
            larger = false;
        }

        if (transform.localScale.y > 1f)
        {
            transform.localScale -= new Vector3(1.25f, 1.25f, 1.25f) * Time.deltaTime;
        }
        yield return null;
    }

    public bool SelectedObject(bool _selected)
    {
        //Calls the UI Manager to handle the main canvas interactions through the usage of an enum
        um.CurrentPuzzle(gameObject, puzzleObj);
        return selected = _selected;
    }
}
