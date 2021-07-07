using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    class Cote
    {
        private string animal = "кошак";
        public string name;
        public float age;
        public float height;
        public float tailLength;
        public float Weight { get; set; }

        public string Animal
        {
            get { return animal;  }
        }

        public void Mew()
        {
            Debug.Log("Кличка: "+name+ ", возраст: " + age + ", масса: " + Weight+", рост: " + height+", длина хвоста: " + tailLength +
                ", животина: " + Animal);
        }

        public Cote()
        {
            name = "Хомас";
            age = 25;
            Weight = 70;
            height = 180;
            tailLength = 50;
        }

        public Cote(string name, float age, float weight, float height, float tailLength)
        {
            this.name = name;
            this.age = age;
            Weight = weight;
            this.height = height;
            this.tailLength = tailLength;
        }
    }
	// Use this for initialization
	void Start () {
        Cote tigran = new Cote();
        Cote Khajiit = new Cote("Maik",100,60,160,70);
        tigran.Mew();
        Khajiit.Mew();
    }
}
