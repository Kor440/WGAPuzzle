using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Отвечает за заполнение поля
public class BackgroundTile : MonoBehaviour
{
    public int width;
    public int height;
    //public GameObject[] dots;
    //public GameObject[,] allDots;

    // Start is called before the first frame update
    void Start()
    {
        Initiallize();
        //allDots = new GameObject[width, height];
    }

       
        // Update is called once per frame
        void Update()
    {
        
    }

    //Заполянет поле случайными фишками
    void Initiallize()
    {
        //int dotToUse = Random.Range(0, dots.Length);
        //GameObject dot = Instantiate(dots[dotToUse], transform.position, Quaternion.identity);
        //dot.transform.parent = this.transform;
        //dot.name = this.gameObject.name;
        //allDots[(int)transform.position.x, (int)transform.position.y] = dot;
    }
}
