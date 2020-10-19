using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

//Класс для создания бокового меню
public class SideBoard : MonoBehaviour
{

    public int width = 5;
    public int height = 6;

    //текстурка для поля
    public GameObject tilePrefab;
    public GameObject[] sideDots;
    private BackgroundTile[,] allSideTiles;
    public GameObject[] allSideDots;
    //public GameObject[,] winDots;

    private int maxNumbers = 3;
    private List<int> uniqueNumbers;
    private List<int> finishedList;

    void Start()
    {
        allSideTiles = new BackgroundTile[width, height];
        allSideDots = new GameObject[3];
        //winDots = new GameObject[5, 5];
        uniqueNumbers = new List<int>();
        finishedList = new List<int>();
        //WinDots();
        GenerateRandomList();
        SetUp();
    }

    //Генерируем лист с уникальными значениями для верхних фишек
    public void GenerateRandomList()
    {
        for (int i = 0; i < maxNumbers; i++)
        {
            uniqueNumbers.Add(i);
        }
        for (int i = 0; i < maxNumbers; i++)
        {
            int ranNum = uniqueNumbers[UnityEngine.Random.Range(0, uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
        
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    //Создаём верхнее поле
    private void SetUp()
    {
        int j = 0;
        for (int i = 0; i < width; i += 2)
        {
            Vector2 tempPosition = new Vector2(i, height);
            GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
            //Добовляем все обьекты как обьекты поля
            backgroundTile.transform.parent = this.transform;
            backgroundTile.name = "( " + i + ", " + height + " )";
            
            //Создаём фигуры как обьекты поля
            GameObject dot = Instantiate(sideDots[finishedList[j]], tempPosition, Quaternion.identity);
            dot.transform.parent = this.transform;
            dot.name = "( " + i + ", " + height + " )";
            
            allSideDots[j] = dot;
            j += 1;
        }    
    }


}
