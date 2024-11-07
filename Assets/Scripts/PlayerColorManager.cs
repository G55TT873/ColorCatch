using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    private Renderer playerRenderer;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();

        playerRenderer.material = new Material(playerRenderer.sharedMaterial);

        float savedR = PlayerPrefs.GetFloat("PlayerColorR", 1f);
        float savedG = PlayerPrefs.GetFloat("PlayerColorG", 1f);
        float savedB = PlayerPrefs.GetFloat("PlayerColorB", 1f);
        Color savedColor = new Color(savedR, savedG, savedB);

        playerRenderer.material.color = savedColor;

        Debug.Log("Loaded color: " + savedColor);
    }
}
