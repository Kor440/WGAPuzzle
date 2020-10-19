using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//public enum GameState
//{
//    wait,
//    move,
//    win,
//    pause
//}

public enum TileKind
{
    Blocked,
    Blank,
    Normal
}

//Класс для определения типа фигуры
[System.Serializable]
public class TileType
{
    public int x; 
    public int y;
    public TileKind tileKind;
}


//Класс отвечаюший за генерацию игрового поля
public class Board : MonoBehaviour
{
    //public GameState currentState = GameState.move;

    //ширина и высота поля
    public int width;
    public int height;
    public int offSet;
    //текстурка для поля
    private BackgroundTile[,] allTiles;
    public GameObject tilePrefab;
    public GameObject blockTilePrefab;
    public GameObject[] dots;
    private bool[,] blankSpaces;
    private bool[,] blockedSpaces;
    public GameObject[,] allDots;


    public TileType[] boardLayout;

    
    private List<int> uniqueNumbers;
    private List<int> finishedList;
    //public GameObject winPanel;

    void Start()
    {

        allTiles = new BackgroundTile[width, height];
        blankSpaces = new bool[width, height];
        blockedSpaces = new bool[width, height];
        allDots = new GameObject[width, height];
        //winPanel.SetActive(false);

        uniqueNumbers = new List<int>();
        finishedList = new List<int>();
        GenerateRandomList();

        SetUp();
        SetUpBlocks();
        SetUpDots();
        //currentState = GameState.pause;
    }

    //public void NewGameButton()
    //{
    //    winPanel.SetActive(false);
    //    currentState = GameState.move;
    //    Application.LoadLevel(Application.loadedLevel);
    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    //Создаём поле 
    private void SetUp()
    {
        for(int i = 0; i< width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                //Добовляем все обьекты как обьекты поля
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";


                //Фигуры
                //int dotToUse = Random.Range(0, dots.Length);
                //GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                //dot.transform.parent = this.transform;
                //dot.name = "( " + i + ", " + j + " )";
                //allDots[i, j] = dot;
            }
        }
    }

    //Устанавливаем неподвижные блоки
    private void SetUpBlocks()
    {
        int step = 2;
        for (int i = 1; i < width; i += step)
        {
            for (int j = 0; j < height; j += step)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject block = Instantiate(blockTilePrefab, tempPosition, Quaternion.identity);
                block.transform.parent = this.transform;
                block.name = "( " + i + ", " + j + " )";
                blockedSpaces[boardLayout[i].x, boardLayout[j].y] = true;
            }
        }
    }

    //public void GenerateBlankSpace()
    //{
    //    for (int i = 0; i < boardLayout.Length; i++)
    //    {
    //        if (boardLayout[i].tileKind == TileKind.Blank)
    //        {
    //            blankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
    //        }
    //    }
    //}

    //public void GenerateBlockedSpace()
    //{
    //    for (int i = 0; i < boardLayout.Length; i++)
    //    {
    //        if (boardLayout[i].tileKind == TileKind.Blocked)
    //        {
    //            blockedSpaces[boardLayout[i].x, boardLayout[i].y] = true;
    //        }
    //    }
    //}
    private void SetUpDots()
    {
        //GenerateBlockedSpace();
        int f = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //if (!blockedSpaces[i, j])
                //{
                //    Vector2 tempPosition = new Vector2(i, j);
                //    //Создаём фигуры
                //    int dotToUse = Random.Range(0, dots.Length);
                //    GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                //    dot.transform.parent = this.transform;
                //    dot.name = "( " + i + ", " + j + " )";
                //    allDots[i, j] = dot;
                //}

                //Проверка на блокированные ячейки
                if (!blockedSpaces[i, j])
                {
                    Vector2 tempPosition = new Vector2(i, j);
                    //Создаём фигуры

                    //int dotToUse = Random.Range(0, dots.Length);

                    GameObject dot = Instantiate(dots[finishedList[f]], tempPosition, Quaternion.identity);
                    dot.transform.parent = this.transform;
                    dot.name = "( " + i + ", " + j + " )";
                    allDots[i, j] = dot;
                    f++;
                }

            }
        }
    }

    //Создаём лист всех активных фигур
    public void GenerateRandomList()
    {
        //Для рандома
        int maxColorNumbers = 3;
        int maxNumberDots = 5;
        int maxNumberBlankDots = 4;
        int maxNumbers = 15;

        //Добовляем 15 фигур по 5 трёх цветов
        for (int i = 0; i < maxColorNumbers; i++)
        {
            for (int j = 0; j < maxNumberDots; j++)
            {
                uniqueNumbers.Add(i);
            }
        }
        //Добовляем 4 пустых фигуры 
        for (int i = 3; i < maxNumberBlankDots; i++)
        {
            for (int j = 0; j < maxNumberBlankDots; j++)
            {
                uniqueNumbers.Add(i);
            }
        }

        //foreach (var x in uniqueNumbers)
        //{
        //    Debug.Log(x.ToString());
        //}

        //Рандомизируем список
        for (int i = 0; i < maxNumbers + maxNumberBlankDots; i++)
        {
            int ranNum = uniqueNumbers[UnityEngine.Random.Range(0, uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }


        //foreach (var x in finishedList)
        //{
        //    Debug.Log(x.ToString());
        //}
    }

}


