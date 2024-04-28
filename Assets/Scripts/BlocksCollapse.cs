using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCollapse : MonoBehaviour
{
    Blocks blocksSc;
    private bool reBuild;
    GameObject block;
    int tempNum1;
    int tempNum2;
    Vector2 tempPosition;

    private BGmusic BGmusicSc;
    void Start()
    {
        blocksSc = GameObject.Find("Blocks").GetComponent<Blocks>();
        BGmusicSc = GameObject.Find("Dont Destroy/Audio").GetComponent<BGmusic>();
    }

    void OnMouseDown()
    {
        DestroyObject();
        if (reBuild)
        {
            blocksSc.indexArray = blocksSc.GroupFinder();
            blocksSc.Shuffle(blocksSc.indexArray);
            blocksSc.Conditions();
        }
    }

    public void DestroyObject()
    {
        reBuild = false;
        for (int i = 0; i < blocksSc.indexArray.Count; i++)
        {
            if (blocksSc.indexArray[i].Count > 3)
            {
                for (int j = 1; j < blocksSc.indexArray[i].Count; j += 2)
                {
                    if (blocksSc.indexArray[i][j] + "" + blocksSc.indexArray[i][j + 1] == gameObject.name)
                    {
                        BGmusicSc.PopSound();
                        for (int k = 1; k < blocksSc.indexArray[i].Count; k += 2)
                        {                          
                            tempNum1 = blocksSc.indexArray[i][k];
                            tempNum2 = blocksSc.indexArray[i][k + 1];
                            Destroy(blocksSc.blockArray[tempNum1][tempNum2]);
                            blocksSc.blockArrayAsNum[tempNum1][tempNum2] = 10;
                        }
                        reBuild = true;
                        ReBuild();
                        return;
                    }
                }
            }
        }
    }

    public void DestroyObject2() //todo düzenle
    {
        reBuild = false;
        for (int i = 0; i < blocksSc.indexArray.Count; i++)
        {
            if (blocksSc.indexArray[i].Count > 3)
            {
                for (int j = 1; j < blocksSc.indexArray[i].Count; j += 2)
                {
                    if (blocksSc.indexArray[i][j] + "" + blocksSc.indexArray[i][j + 1] == gameObject.name)
                    {
                        for (int k = 1; k < blocksSc.indexArray[i].Count; k += 2)
                        {
                            List<int> temp = new List<int>();
                            temp.Add(blocksSc.indexArray[i][k]);
                            temp.Add(blocksSc.indexArray[i][k + 1]);
                            array.Add(temp);
                            Destroy(blocksSc.blockArray[blocksSc.indexArray[i][k]][blocksSc.indexArray[i][k + 1]]);
                        }
                        reBuild = true;
                        ReBuild2();
                        return;
                    }
                }
            }
        }
    }
    List<List<int>> array = new List<List<int>>();
    List<int> subArray = new List<int>();
    int column;
    int upperLimit;

    public void ArrangeArrays(List<int> sortedSubArray, int currentIndex, int currentRow, int distance = 1)//todo düzenle
    {
        if (currentRow + distance < Blocks.row - 1)
        {
            blocksSc.blockArrayAsNum[currentRow][column] = blocksSc.blockArrayAsNum[currentRow + distance][column];

            blocksSc.blockArray[currentRow][column] = blocksSc.blockArray[currentRow + distance][column];
            blocksSc.blockArray[currentRow][column].GetComponent<SpriteRenderer>().sortingOrder = currentRow;
            blocksSc.blockArray[currentRow][column].name = currentRow + "" + column;
        }
        else return;
        
        if (currentIndex == sortedSubArray.Count - 1)
        {
            ArrangeArrays(sortedSubArray, currentIndex, currentRow +1, distance);
        }
        else if (currentRow == sortedSubArray[currentIndex + 1] - 1)
        {
            ArrangeArrays(sortedSubArray, currentIndex + 1, currentRow + 1, distance + 1);
        }
        else ArrangeArrays(sortedSubArray, currentIndex, currentRow + 1, distance);
    }
    public void ReBuild2() //todo düzenle
    {
        for (int i = 0; i < array.Count; i++)
        {
            if (array[i] == null) continue;
            for (int j = i+1; j < array.Count; j++)
            {
                if (array[i][1] == array[j][1])
                {
                    subArray.Add(array[j][0]);
                    array[j] = null;
                }

            }
            column = array[i][1];
            if (subArray.Count > 0)
            {
                subArray.Add(array[i][0]);
                subArray.Sort();
                for (int j = 0; j < subArray.Count; j++)
                {
                    if(j == subArray.Count - 1) upperLimit = Blocks.row -1;
                    else upperLimit = subArray[j + 1];

                    for (int row = subArray[j]; row < upperLimit; row++)
                    {

                        blocksSc.blockArrayAsNum[row - j][column] = blocksSc.blockArrayAsNum[row + 1][column];

                        blocksSc.blockArray[row - j][column] = blocksSc.blockArray[row + 1][column];
                        blocksSc.blockArray[row - j][column].GetComponent<SpriteRenderer>().sortingOrder = row - j;
                        blocksSc.blockArray[row - j][column].name = (row - j) + "" + column;
                    }
                }
                for (int k = Blocks.row - subArray.Count; k < Blocks.row; k++)
                {
                    int tempRandomNumber = Random.Range(0, Blocks.numberOfColors);
                    blocksSc.blockArrayAsNum[k][column] = tempRandomNumber;
                    Vector2 tempPosition = new Vector2(column, 2 + blocksSc.height);
                    if (k - 1 >= 0 && blocksSc.blockArray[k - 1][column].transform.position.y > blocksSc.height)
                    {
                        tempPosition.y = blocksSc.blockArray[k - 1][column].transform.position.y + 2;
                    }
                    GameObject block = Instantiate(blocksSc.blocks2d[tempRandomNumber][0], tempPosition, Quaternion.identity);
                    block.name = k + "" + column;
                    block.GetComponent<SpriteRenderer>().sortingOrder = k;
                    blocksSc.blockArray[k][column] = block;
                }
                subArray.Clear();
            }
            else
            {
                for (int row = array[i][0]; row < Blocks.row - 1; row++)
                {
                    blocksSc.blockArrayAsNum[row][column] = blocksSc.blockArrayAsNum[row + 1][column];

                    blocksSc.blockArray[row][column] = blocksSc.blockArray[row + 1][column];
                    blocksSc.blockArray[row][column].GetComponent<SpriteRenderer>().sortingOrder = row;
                    blocksSc.blockArray[row][column].name = row + "" + column;
                }
                int tempRandomNumber = Random.Range(0, Blocks.numberOfColors);
                blocksSc.blockArrayAsNum[Blocks.row - 1][column] = tempRandomNumber;
                Vector2 tempPosition = new Vector2(column, 2 + blocksSc.height);
                if (Blocks.row - 2 >= 0 && blocksSc.blockArray[Blocks.row - 2][column].transform.position.y > blocksSc.height)
                {
                    tempPosition.y = blocksSc.blockArray[Blocks.row - 2][column].transform.position.y + 2;
                }
                GameObject block = Instantiate(blocksSc.blocks2d[tempRandomNumber][0], tempPosition, Quaternion.identity);
                block.name = (Blocks.row - 1) + "" + column;
                block.GetComponent<SpriteRenderer>().sortingOrder = Blocks.row;
                blocksSc.blockArray[Blocks.row - 1][column] = block;
            }
        }
    }
    public void ReBuild()
    {
        for (int j = 0; j < blocksSc.blockArrayAsNum[0].Count; j++)
        {
            int numOfNewObj = 0;

            for (int i = 0; i < blocksSc.blockArrayAsNum.Count; i++)
            {
                if (blocksSc.blockArrayAsNum[i][j] == 10)
                {
                    numOfNewObj++;
                    for (int k = i + 1; k < blocksSc.blockArrayAsNum.Count; k++)
                    {
                        if (blocksSc.blockArrayAsNum[k][j] != 10)
                        {
                            blocksSc.blockArrayAsNum[i][j] = blocksSc.blockArrayAsNum[k][j];
                            blocksSc.blockArrayAsNum[k][j] = 10;
                            numOfNewObj--;
                            blocksSc.blockArray[i][j] = blocksSc.blockArray[k][j];
                            blocksSc.blockArray[i][j].GetComponent<SpriteRenderer>().sortingOrder = i;
                            blocksSc.blockArray[i][j].name = i + "" + j;
                            break;
                        }
                    }                  
                }             
            }
            for (int i = Blocks.row - numOfNewObj; i < Blocks.row; i++)
            {                   
                int tempRandomNumber = Random.Range(0, Blocks.numberOfColors);
                blocksSc.blockArrayAsNum[i][j] = tempRandomNumber;
                tempPosition = new Vector2(j, 2 + blocksSc.height);
                if (i - 1 >= 0 && blocksSc.blockArray[i - 1][j].transform.position.y > blocksSc.height)
                {
                    tempPosition.y = blocksSc.blockArray[i - 1][j].transform.position.y + 2;
                }
                block = Instantiate(blocksSc.blocks2d[tempRandomNumber][0], tempPosition, Quaternion.identity);
                block.name = i + "" + j;
                block.GetComponent<SpriteRenderer>().sortingOrder = i;
                blocksSc.blockArray[i][j] = block;
            }
        }
    }
}
