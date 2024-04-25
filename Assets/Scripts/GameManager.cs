using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
