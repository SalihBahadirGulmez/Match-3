using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text rowSize;
    public Text columnSize;
    public Text numberOfColor;

    private int maxRowSize = 10;
    private int minRowSize = 3;
    private int maxColumnSize = 10;
    private int minColumnSize = 3;
    private int maxNumberOfColor = 6;
    private int minNumberOfColor = 2;


    public GameObject audioSlider;
    public Slider audioSliderComponent;
    public BGmusic BGmusicSc;

    RaycastHit2D hit;
    Ray ray;
    Vector2 position = new Vector2(0,0);
    Vector2 mousePos;

    EventSystem e;

    bool canSliderStatusChange = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSlider.activeSelf)
        {
            BGmusicSc.MusicVolume(audioSliderComponent.value);
        }

        if (Input.GetMouseButtonDown(0) && canSliderStatusChange)
        {
            audioSlider.SetActive(false);

        }
    }



    public void LoadGameSecene()
    {
        SceneManager.LoadScene("game");
    }
    public void LeftRowButton()
    {
        if(Blocks.row > minRowSize)
        {
            Blocks.row -= 1;
            rowSize.text = Blocks.row.ToString();
        }
    }
    public void RightRowButton()
    {
        if (Blocks.row < maxRowSize)
        {
            Blocks.row += 1;
            rowSize.text = Blocks.row.ToString();
        }
    }
    public void LeftColumnButton()
    {
        if (Blocks.column > minColumnSize)
        {
            Blocks.column -= 1;
            columnSize.text = Blocks.column.ToString();
        }
    }
    public void RightColumnButton()
    {
        if (Blocks.column < maxColumnSize)
        {
            Blocks.column += 1;
            columnSize.text = Blocks.column.ToString();
        }
    }
    public void LeftColorButton()
    {
        if (Blocks.numberOfColors > minNumberOfColor)
        {
            Blocks.numberOfColors -= 1;
            numberOfColor.text = Blocks.numberOfColors.ToString();
        }
    }
    public void RightColorButton()
    {
        if (Blocks.numberOfColors < maxNumberOfColor)
        {
            Blocks.numberOfColors += 1;
            numberOfColor.text = Blocks.numberOfColors.ToString();
        }
    }

    public void Audio()
    {
        if(!audioSlider.activeSelf) audioSlider.SetActive(true);
        else audioSlider.SetActive(false);
    }
    public void Clicked()
    {
        StartCoroutine(AudioSliderController());
    }
    IEnumerator AudioSliderController()
    {
        canSliderStatusChange = false;
        yield return null;
        canSliderStatusChange = true;
    }
}
