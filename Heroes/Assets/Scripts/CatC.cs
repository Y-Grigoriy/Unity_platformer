using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatC : MonoBehaviour
{

    //private int a = 3;
    //private float b = 5.2f;
    //private byte r = 2;

    private int key;
    private string nam;
    private int N= 200;
    private int[] warehouseCount=new int[3];
    private string[] warehouse = {"apples","oranges","tomatos"};

    class Cotet
    {
        public string name;
        public float age;
        public float weight;
        public float height;
        public float tailLength;

        public void Mew()
        {
            Debug.Log("Кличка: " + name + " возраст " + age + " масса " + weight + " рост " + height + " Длина хвоста " + tailLength);
        }
    }

    abstract class Vehicle
    {
        public string name;

        public virtual void Beep()
        {
            Debug.Log("Б");
        }
    }

    class Bus : Vehicle
    {
        public override void Beep()
        {
            Debug.Log("Буууп");
        }
    }

    class Car : Vehicle
    {
        public override void Beep()
        {
            Debug.Log("Бип");
        }
    }

    class Tractor : Vehicle
    {
        public override void Beep()
        {
            Debug.Log("Посторонись!");
        }
    }
    // Use this for initialization
    void Start()
    {

        Vehicle Zaporogec = new Car();
        Zaporogec.name = "Zaporogec";
        Zaporogec.Beep();
        Vehicle Ikarus = new Bus();
        Ikarus.name = "Ikarus";
        Ikarus.Beep();
        Vehicle Uralec = new Tractor();
        Uralec.name = "Uralec";
        Uralec.Beep();

        /*int s = a * a;
            Debug.Log("Square: "+s);
            Debug.Log("Square of rectangular: " + a*b);
            Debug.Log("Square of circle: " + Mathf.PI*Mathf.Pow(r,2));*/

        //Message();
        Debug.Log(CircleSquare(3.6f));

        /*int ar =Random.Range(0, 200), br =Random.Range(0, 200);
            Debug.Log(ar+"qu"+br);
            Debug.Log(Checks(ar, br));*/

        #region Warehouse
        for (int j=0; j<warehouseCount.Length; j++)
        {
            warehouseCount[j] = Random.Range(0, N);
            Debug.Log(warehouse[j] + " = " + warehouseCount[j]);
        }
        for (int j = 1; j < warehouseCount.Length; j++)
        {
            key = warehouseCount[j];
            nam = warehouse[j];
            int i = j - 1;
            while (i >= 0 && warehouseCount[i] < key)
            {
                warehouseCount[i + 1] = warehouseCount[i];
                warehouse[i + 1] = warehouse[i];
                i = i - 1;
            }
            warehouseCount[i + 1] = key;
            warehouse[i + 1] = nam;
        }
        string sheet = warehouseCount.Select(i => i.ToString()).Aggregate((i, j) => i + " " + j);
        Debug.Log(sheet);
        Debug.Log(string.Join(", ", warehouse));
        #endregion

        /*int number = 1;
        float thickness = 0.001f;
        while (thickness < 300000000)
        {
            number++;
            thickness *= 2;
        }
        Debug.Log(number);*/

        Cotet Catwoman = new Cotet();
        Catwoman.name = "Хома";
        Catwoman.age = 23;
        Catwoman.weight = 60;
        Catwoman.height = 170;
        Catwoman.tailLength = 0;
        Catwoman.Mew();
    }

    float CircleSquare(float r)
    {
        return Mathf.PI * Mathf.Pow(r, 2);
    }

    /*bool Checks(int a, int b)
    {
        if (a < b)
            return false;
        else
            return true;
    }

    void Message()
    {
        Debug.Log("Любимый Hello World!");
    }*/
}
