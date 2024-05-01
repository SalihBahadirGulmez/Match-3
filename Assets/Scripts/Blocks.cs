using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public List<GameObject> blueBlocks = new List<GameObject>();
    public List<GameObject> greenBlocks = new List<GameObject>();
    public List<GameObject> pinkBlocks = new List<GameObject>();
    public List<GameObject> purpleBlocks = new List<GameObject>();
    public List<GameObject> redBlocks = new List<GameObject>();
    public List<GameObject> yellowBlocks = new List<GameObject>();
    public List<List<GameObject>> blocks2d = new List<List<GameObject>>();

    public static int row = 3;
    public static int column = 3;
    public static int numberOfColors = 2;
    public int conditionA = 4;
    public int conditionB = 6;
    public int conditionC = 7;

    private int blockNum;
    public int height = 14;

    public List<List<int>> indexArray;
    public List<List<GameObject>> blockArray = new List<List<GameObject>>();
    public List<List<int>> blockArrayAsNum = new List<List<int>>();

    int tempRandomNumber;
    GameObject block;

    List<List<int>> _indexArray = new List<List<int>>();
    List<int> subIndexArray;

    int posX;
    int posY;

    Vector2 tempPos;

    private void Start()
    {
        blocks2d.Add(blueBlocks);
        blocks2d.Add(greenBlocks);
        blocks2d.Add(pinkBlocks);
        blocks2d.Add(purpleBlocks);
        blocks2d.Add(redBlocks);
        blocks2d.Add(yellowBlocks);

        Camera.main.transform.position = new Vector3((column / 2f) - 0.5f, 0, -10);
               
        CreateBlocks();      
        indexArray = GroupFinder();
        Shuffle(indexArray);
        Conditions();
    }

    public void CreateBlocks()
    {
        for (int i = 0; i < row; i++)
        {
            blockArrayAsNum.Add(new List<int>());
            blockArray.Add(new List<GameObject>());
            for (int j = 0; j < column; j++)
            {
                tempRandomNumber = Random.Range(0, numberOfColors);
                blockArrayAsNum[i].Add(tempRandomNumber);
                block = Instantiate(blocks2d[tempRandomNumber][0], new Vector2(j, i + height), Quaternion.identity);
                block.name = i + "" + j;
                block.GetComponent<SpriteRenderer>().sortingOrder = i;
                blockArray[i].Add(block);                             
            }
        }
    }

    public List<List<int>> GroupFinder()//1.eleman blok türü olacak şekilde indexarray'e grupların indexlerini array şeklinde ekleyip en son tüm bu gruparı döndürüyor.
    {
        _indexArray.Clear();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                subIndexArray = new List<int>();

                if (IsUnique(_indexArray, i, j))
                {
                    blockNum = blockArrayAsNum[i][j];
                    subIndexArray.Add(blockNum);
                    subIndexArray.Add(i);
                    subIndexArray.Add(j);

                    SearchNeighbors(subIndexArray, i, j);

                    _indexArray.Add(subIndexArray);
                }                        
            }
        }
        return _indexArray;
    }

    public void SearchNeighbors(List<int> _subIndexArray, int _row, int _column)//komşulara bakıp grup varsa subindex e ekliyor
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i != -j && i != j 
                    && _row + i >= 0 && _row + i < row 
                    && _column + j >= 0 && _column + j < column 
                    && blockArrayAsNum[_row + i][_column + j] == blockNum)
                {
                    if (IsUniqueV2(_subIndexArray, _row, _column, i, j))
                    {
                        _subIndexArray.Add(_row + i);
                        _subIndexArray.Add(_column + j);
                        SearchNeighbors(_subIndexArray, _row + i, _column + j);
                    }
                }
            }
        }        
    }

    public bool IsUniqueV2(List<int> _subIndexArray, int _row, int _column, int neighbourR, int neighbourC) //searchneighbors da kullanılıyor, subindexarray'e eklenmesi planlanan indexler daha önceden eklendi mi diye kontrol ediyor. olmadığı durumda recursion dunc nedeniyle sonsuz döngüye girebilir.
    {          
        for (int i = 1; i < _subIndexArray.Count; i += 2)
        {
            if (_subIndexArray[i] == _row + neighbourR && _subIndexArray[i + 1] == _column + neighbourC)
            {
                return false;                    
            }
        }               
        return true;
    }

    public bool IsUnique(List<List<int>> _indexArray, int _row, int _column)//groupfinder'da kullanılıyor, indexarrayde aynı grubun 1den fazla olmasını engelliyor.
    {
        for (int i = 0; i < _indexArray.Count; i++)
        {
            for (int j = 1; j < _indexArray[i].Count; j += 2)
            {
                if (_indexArray[i][j] == _row && _indexArray[i][j + 1] == _column)
                {
                    return false;
                }
            }
        }
        return true;
    }
        
    public void Conditions()
    {
        for (int i = 0; i < indexArray.Count; i++)
        {
            if (indexArray[i].Count > conditionC * 2)
            {
                for (int j = 1; j < indexArray[i].Count; j += 2)
                {
                    posY = indexArray[i][j];
                    posX = indexArray[i][j + 1];
                    tempPos = blockArray[posY][posX].transform.position;
                    Destroy(blockArray[posY][posX]);
                    block = Instantiate(blocks2d[indexArray[i][0]][3], tempPos, Quaternion.identity);
                    block.name = posY + "" + posX;
                    block.GetComponent<SpriteRenderer>().sortingOrder = posY;
                    blockArray[posY][posX] = block;
                }
            }
            else if (indexArray[i].Count > conditionB * 2)
            {
                for (int j = 1; j < indexArray[i].Count; j += 2)
                {
                    posY = indexArray[i][j];
                    posX = indexArray[i][j + 1];
                    tempPos = blockArray[posY][posX].transform.position;                    
                    Destroy(blockArray[posY][posX]);
                    block = Instantiate(blocks2d[indexArray[i][0]][2], tempPos, Quaternion.identity);
                    block.name = posY + "" + posX;
                    block.GetComponent<SpriteRenderer>().sortingOrder = posY;
                    blockArray[posY][posX] = block;
                }
            }
            else if (indexArray[i].Count > conditionA * 2)
            {
                for (int j = 1; j < indexArray[i].Count; j += 2)
                {
                    posY = indexArray[i][j];
                    posX = indexArray[i][j + 1];
                    tempPos = blockArray[posY][posX].transform.position;
                    Destroy(blockArray[posY][posX]);
                    block = Instantiate(blocks2d[indexArray[i][0]][1], tempPos, Quaternion.identity);
                    block.name = posY + "" + posX;
                    block.GetComponent<SpriteRenderer>().sortingOrder = posY;
                    blockArray[posY][posX] = block;
                }
            }
            else
            {
                for (int j = 1; j < indexArray[i].Count; j += 2)
                {
                    posY = indexArray[i][j];
                    posX = indexArray[i][j + 1];
                    tempPos = blockArray[posY][posX].transform.position;
                    Destroy(blockArray[posY][posX]);
                    block = Instantiate(blocks2d[indexArray[i][0]][0], tempPos, Quaternion.identity);
                    block.name = posY + "" + posX;
                    block.GetComponent<SpriteRenderer>().sortingOrder = posY;
                    blockArray[posY][posX] = block;
                }
            }

        }
    }
    
    public void Shuffle(List<List<int>> _indexArray)//hiç grup olmaması durumunda blokları silip rastgele tekrar oluşturuyor.
    {  
        for (int i = 0; i < _indexArray.Count; i++)
        {
            if (indexArray[i].Count > 3)
            {
                return;
            }
        }
        RemoveBlocks(blockArray);
        CreateBlocksV2();
        indexArray = GroupFinder();
        Shuffle(indexArray);
    }

    public void RemoveBlocks(List<List<GameObject>> _blockArray)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Destroy(_blockArray[i][j]);
            }
        }
    }

    public void CreateBlocksV2()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                int tempRandomNumber = Random.Range(0, numberOfColors);
                blockArrayAsNum[i][j] = tempRandomNumber;
                block = Instantiate(blocks2d[tempRandomNumber][0], new Vector2(j, i + height), Quaternion.identity);
                block.name = i + "" + j;
                block.GetComponent<SpriteRenderer>().sortingOrder = i;
                blockArray[i][j] = block;
            }
        }
    }
}
