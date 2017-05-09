using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelData
{
    public LevelData(string levelName)
    {
        string data = PlayerPrefs.GetString(levelName);
        if (data == "")
            return;
        string[] allData = data.Split('&');
        BestTime = float.Parse(allData[0]);
        SilverTime = float.Parse(allData[1]);
        GoldTime = float.Parse(allData[2]);
    }
    public float BestTime { set; get; }
    public float GoldTime { set; get; }
    public float SilverTime { set; get; }
}
public class MainMenu : MonoBehaviour
{
    private bool nextLevelLocked = false;
    private int[] skinCost = new int[] {0, 150, 150, 150,
                                        300, 300, 300, 300,
                                        500, 500, 500, 500,
                                        1000, 1250, 1500, 2000};
    private float CAMERA_SPEED = 3.0f;
    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;
    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;
    public Text currencyText;

    public Material playerMaterial;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;
    private void Start()
    {
        ChangePlayerSkin(GameManager.Instance.currentSkinIndex);
        currencyText.text = "Currency : " + GameManager.Instance.currency.ToString();

        cameraTransform = Camera.main.transform;
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate (levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);
            LevelData level = new LevelData(thumbnail.name);
            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (level.BestTime != 0.0f)?level.BestTime.ToString("f") : "";

            container.transform.GetChild(1).GetComponent<Image>().enabled = nextLevelLocked;
            container.GetComponent<Button>().interactable = !nextLevelLocked;
            if (level.BestTime == 0.0f)
            {
                nextLevelLocked = true;
            }
            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }

        int textureIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("Player");
        foreach (var texture in textures)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopButtonContainer.transform, false);
            int index = textureIndex;
            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = skinCost[index].ToString();
            if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
            }
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
            textureIndex += 1;
        }
    }
    private void Update()
    {
        if (cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_SPEED * Time.deltaTime);
        }
    }
    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }
    private void ChangePlayerSkin(int index)
    {
        if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
        {
            float x = (index % 4) * 0.25f;
            float y = 0.75f - ((int)index / 4) * 0.25f;
            playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
            GameManager.Instance.currentSkinIndex = index;
            GameManager.Instance.Save();
        }
        else
        {
            int cost = skinCost[index];
            if (cost <= GameManager.Instance.currency)
            {
                GameManager.Instance.currency -= cost;
                GameManager.Instance.Save();
                currencyText.text = "Currency : " + GameManager.Instance.currency.ToString();
                shopButtonContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                GameManager.Instance.skinAvailability += 1 << index;
                GameManager.Instance.Save();
                ChangePlayerSkin(index);
            }
            
        }
    }
    
}
