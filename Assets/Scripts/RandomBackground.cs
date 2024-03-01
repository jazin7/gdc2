using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour
{
    // Array of background image names
    private string[] backgroundImageNames = {
        "00032-602337138",
        "00034-3464884723",
        "00035-3464884724",
        "00037-3464884726",

        "00038-3464884727",
        "00039-3464884728",
        "00040-3464884729",
        "00041-3464884730",

        "00042-3464884731",
        "00043-3464884732",
        "00044-3464884733",
        "00045-3464884734",


        "00046-3464884735",
        "00047-3464884736",
        "00048-3464884737",
        "00049-3464884738",

        "00050-3464884739",
        "00051-3464884740",
        "00052-3464884741",
        "00053-3464884742",

    };

    void Start()
    {
        // Choose a random image name from the array
        string chosenImageName = backgroundImageNames[Random.Range(0, backgroundImageNames.Length)];

        Sprite chosenImage = Resources.Load<Sprite>(chosenImageName);

        // Get the Image component of this GameObject and set its sprite to the chosen image
        GetComponent<Image>().sprite = chosenImage;
    }
}
