using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public enum GameMode { Fixed, Jump };
    public GameMode gameMode;
    public RectTransform parent;
    public Camera cam;

    public Button plr;
    public int player;
    public int score;

    void Start()
    {
        score = 0;
        plr.onClick.AddListener(IncreaseScore);
        Debug.Log("test");
    }

    void IncreaseScore()
    {
        score++;
        if (gameMode == GameMode.Jump)
        {
            Jump();
        }
    }

    void Jump()
    {
        float base_w = plr.GetComponent<RectTransform>().rect.width / 2;
        float base_h = plr.GetComponent<RectTransform>().rect.width / 2;
        float width = /*parent.rect.width*/ Screen.width - plr.GetComponent<RectTransform>().rect.width / 2;
        float height = /*parent.rect.height*/ Screen.height - plr.GetComponent<RectTransform>().rect.height / 2;

        Debug.Log("width: " + width + " height: " + height);

        float old_x = plr.GetComponent<RectTransform>().rect.x;
        float old_y = plr.GetComponent<RectTransform>().rect.y;

        float new_x = Random.Range(base_w, width);
        float new_y = Random.Range(base_h, height);

        while(Mathf.Abs(new_x - old_x) <= plr.GetComponent<RectTransform>().rect.width 
            && Mathf.Abs(new_y - old_y) <= plr.GetComponent<RectTransform>().rect.height)
        {
            new_x = Random.Range(base_w, width);
            new_y = Random.Range(base_h, height);
        }
        Vector3 newViewPos = cam.ScreenToWorldPoint(new Vector3(new_x, new_y, 0));
        plr.GetComponent<RectTransform>().position = new Vector3(newViewPos.x, newViewPos.y, 0);
    }
}
