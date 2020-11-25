using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, down, left, right, jump;

    private GameObject currentKey;
    public Color32 changedKey = new Color32(39,171,249,255);
    public Color32 selectedKey = new Color32(239,116,36,255);

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Jump", KeyCode.Space);

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBind.keys["Up"]))
        {
            Debug.Log("Character moves forward");
        }
        if (Input.GetKeyDown(KeyBind.keys["Down"]))
        {
            Debug.Log("Character moves backward");
        }
        if (Input.GetKeyDown(KeyBind.keys["Left"]))
        {
            Debug.Log("Character moves left");
        }
        if (Input.GetKeyDown(KeyBind.keys["Right"]))
        {
            Debug.Log("Character moves right");
        }
        if (Input.GetKeyDown(KeyBind.keys["Jump"]))
        {
            Debug.Log("Character jumps");
        }
    }

    public void ChangeKey(GameObject clickedKey)
    {
        currentKey = clickedKey;
        if (clickedKey != null)
        {
            currentKey.GetComponent<Image>().color = selectedKey;
        }
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    private void OnGUI()
    {
        string newKey = "";
        Event e = Event.current;

        if (currentKey != null)
        {
            if (e.isKey)
            {
                newKey = e.keyCode.ToString();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (newKey != "")
            {
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                currentKey.GetComponentInChildren<Text>().text = newKey;
                currentKey.GetComponent<Image>().color = changedKey;
                currentKey = null;
                SaveKeys();
            }
        }
    }
}


