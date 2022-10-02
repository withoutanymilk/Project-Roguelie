using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyBinds : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, down, left, right, fire, reload;

    private GameObject currentKey;

    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(43, 134, 170, 255);


    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Mouse0")));
        keys.Add("Reload", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Reload", "R")));

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        fire.text = keys["Fire"].ToString();
        reload.text = keys["Reload"].ToString();

    }

    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Fire"]))
        {
            Debug.Log("Fire");
        }
        if (Input.GetKeyDown(keys["Reload"]))
        {
            Debug.Log("Reload");
        }
    }
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}