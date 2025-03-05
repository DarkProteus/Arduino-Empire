using System.Collections.Generic;
using UnityEngine;

public class PickCharController : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;
    private List<GameObject> _cylinders = new List<GameObject>();
    private GameObject _currentHoveredCharacter = null;
    public static GameObject pickedChar;
    private void Start()
    {
       foreach(var character in _characters)
       {
            GameObject cylinder = character.transform.Find("Cylinder").gameObject;
            _cylinders.Add(cylinder);
            cylinder.SetActive(false);
        }
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            if (!hitObject.tag.Equals("Plane"))
            {
                foreach (var character in _characters)
                {
                 
                    if (hitObject == character)
                    {

                        if (_currentHoveredCharacter != character)
                        {
                            SetHighlight(_currentHoveredCharacter, false); 
                            _currentHoveredCharacter = character;
                            SetHighlight(_currentHoveredCharacter, true);
                        }

                        if (Input.GetMouseButtonDown(0)) 
                        {
                            print($"Character: {character.name}");
                            pickedChar = character;
                        }
                        return;
                    }
                }
            }
        }
        SetHighlight(_currentHoveredCharacter, false);
        _currentHoveredCharacter = null;
    }

    private void SetHighlight(GameObject character, bool state)
    {
        if (character != null)
        {
            Transform cylinder = character.transform.Find("Cylinder");
            if (cylinder != null)
            {
                cylinder.gameObject.SetActive(state);
            }
        }
    }
}


