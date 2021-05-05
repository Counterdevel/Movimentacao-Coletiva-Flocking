using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject fishPrefab;                       //Pega o Prefab do peixe
    public int numFish = 20;                            //Quantidade de peixes no início
    public GameObject[] allFish;                        //Array de todos os peixes
    public Vector3 swinLimits = new Vector3(5, 5, 5);   //Espaço limite para os peixes
    public Vector3 goalPos;                             //Posição de objetivo

    [Header("Configurações do Cardume")]                //Cabeçalho
    [Range(0.0f, 5.0f)]                                 
    public float minSpeed;                              //Velocidade maxima e minima dos peixes
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;                     //Velocidade de rotação e distancia entre os peixes
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    private void Start()
    {
        allFish = new GameObject[numFish];
        for(int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),  //Usa o for para atribuir um vector3 aleatório, dentro do SwinLimits
                                                                Random.Range(-swinLimits.z, swinLimits.z));

            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);                     //Instancia os prefabs na scene
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }

    private void Update()
    {
        goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),          //Segue a posição designada
                                                        Random.Range(-swinLimits.z, swinLimits.z));
    }
}
