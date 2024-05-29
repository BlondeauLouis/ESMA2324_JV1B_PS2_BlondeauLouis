using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;

    public Button moveLeftButton;
    public Button moveRightButton;
    public Button jumpButton;
    public Button tagButton;
    public Button glideButton;

    public Text moveLeftButtonText;
    public Text moveRightButtonText;
    public Text jumpButtonText;
    public Text tagButtonText;
    public Text glideButtonText;

    private string waitingForKey = null;
    private Dictionary<string, KeyCode> keyMappings;

    private void Start()
    {
        // Initialiser les sliders avec les valeurs actuelles
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        fullscreenToggle.isOn = Screen.fullScreen;

        // Configurer la liste des résolutions
        Resolution[] resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Initialiser les boutons avec les valeurs actuelles
        keyMappings = new Dictionary<string, KeyCode>
        {
            { "MoveLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft", "LeftArrow")) },
            { "MoveRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight", "RightArrow")) },
            { "Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")) },
            { "Tag" , (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Tag", "E")) },
            { "Glide", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Glide", "LeftShift")) }
        };

        moveLeftButtonText.text = keyMappings["MoveLeft"].ToString();
        moveRightButtonText.text = keyMappings["MoveRight"].ToString();
        jumpButtonText.text = keyMappings["Jump"].ToString();
        tagButtonText.text = keyMappings["Tag"].ToString();
        glideButtonText.text = keyMappings["Glide"].ToString();

        // Ajouter des listeners
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        moveLeftButton.onClick.AddListener(() => StartListeningForKey("MoveLeft"));
        moveRightButton.onClick.AddListener(() => StartListeningForKey("MoveRight"));
        jumpButton.onClick.AddListener(() => StartListeningForKey("Jump"));
        tagButton.onClick.AddListener(() => StartListeningForKey("Tag"));
        glideButton.onClick.AddListener(() => StartListeningForKey("Glide"));
    }

    void Update()
    {
        if (waitingForKey != null)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    SetKeyBinding(waitingForKey, keyCode);
                    waitingForKey = null;
                    break;
                }
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        // Suppose que vous avez un gestionnaire audio pour appliquer ce volume
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        // Suppose que vous avez un gestionnaire audio pour appliquer ce volume
        AudioManager.Instance.SetSFXVolume(volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetKeyBinding(string key, KeyCode newKey)
    {
        keyMappings[key] = newKey;
        PlayerPrefs.SetString(key, newKey.ToString());

        switch (key)
        {
            case "MoveLeft":
                moveLeftButtonText.text = newKey.ToString();
                break;
            case "MoveRight":
                moveRightButtonText.text = newKey.ToString();
                break;
            case "Jump":
                jumpButtonText.text = newKey.ToString();
                break;
            case "Tag":
                tagButtonText.text = newKey.ToString();
                break;
            case "Glide":
                glideButtonText.text = newKey.ToString();
                break;
        }

        Debug.Log($"Key binding set: {key} = {newKey}");
    }

    private void StartListeningForKey(string key)
    {
        waitingForKey = key;
    }
}
