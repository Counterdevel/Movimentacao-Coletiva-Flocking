using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingManager myManager;                                    //Instancia a classe FlockingManager
    float speed;                                                         //Variavel de velocidade

    private void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);   //Atribuindo a velocidade aleatóriamente entre a velocidade minima e maxima
    }

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);              //aplica a velocidade 
    }
}
