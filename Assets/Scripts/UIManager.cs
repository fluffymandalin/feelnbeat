using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI debugText1; // Reference to the first Text element
    public TextMeshProUGUI debugText2; // Reference to the second Text element

    public void UpdateDebugText1(string message)
    {
        debugText1.text = message;
    }

    public void UpdateDebugText2(string message)
    {
        debugText2.text = message;
    }
}
