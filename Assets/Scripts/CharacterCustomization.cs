using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject playerModel;
    public Slider redSlider, greenSlider, blueSlider;
    private Renderer playerRenderer;
    public Button saveButton;

    private void Start()
    {
        playerRenderer = playerModel.GetComponent<Renderer>();

        playerRenderer.material = new Material(playerRenderer.sharedMaterial);

        float savedR = PlayerPrefs.GetFloat("PlayerColorR", 1f);
        float savedG = PlayerPrefs.GetFloat("PlayerColorG", 1f);
        float savedB = PlayerPrefs.GetFloat("PlayerColorB", 1f);
        Color savedColor = new Color(savedR, savedG, savedB);

        playerRenderer.material.color = savedColor;

        redSlider.value = savedR;
        greenSlider.value = savedG;
        blueSlider.value = savedB;

        redSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });

        saveButton.onClick.AddListener(SaveColor);
    }

    public void UpdateColorPreview()
    {
        Color selectedColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        playerRenderer.material.color = selectedColor;
    }

    public void SaveColor()
    {
        PlayerPrefs.SetFloat("PlayerColorR", playerRenderer.material.color.r);
        PlayerPrefs.SetFloat("PlayerColorG", playerRenderer.material.color.g);
        PlayerPrefs.SetFloat("PlayerColorB", playerRenderer.material.color.b);
        PlayerPrefs.Save();
        Debug.Log("Color Saved: " + playerRenderer.material.color);
    }

    public void ContinueToNextScene()
    {
        SaveColor();
        SceneManager.LoadScene("NextSceneName");
    }
}
