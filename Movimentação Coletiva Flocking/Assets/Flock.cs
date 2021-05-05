using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingManager myManager;                                    //Instancia a classe FlockingManager
    float speed;                                                         //Variavel de velocidade
    bool turning = false;

    private void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);   //Atribuindo a velocidade aleatóriamente entre a velocidade minima e maxima
    }

    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position,             //Limita o espaço da navegação dos peixes
                              myManager.swinLimits * 2); 

        if(!b.Contains(transform.position))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
        if(turning)
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(direction),
                                 myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if(Random.Range(0,100)<10)                                  //Calcula a velocidade para cada peixe
            {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
                if(Random.Range(0,100)<20)
                {
                    ApplyRules();                                                   //Chamando Metodo para o update
                }
            }
        }

        transform.Translate(0, 0, Time.deltaTime * speed);              //aplica a velocidade 
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;                                        //Pega as informações do script

        Vector3 vcentre = Vector3.zero;                                 //Calcula os pontos medios entre os peixes
        Vector3 vavoid = Vector3.zero;                                  //Evita a colisão entre os peixes
        float gSpeed = 0.1f;                                            //Aciona a velocidade
        float nDistance;                                                //Calcula a distancia entre os peixes
        int groupSize = 0;                                              //Calcula quantos peixes fazem parte de um grupo

        foreach(GameObject go in gos)                                   //loop para atribuir os calculos de distancia dos peixes para criação de grupos
        {
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0)                                 //Evita que aja colisão
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if(groupSize > 0)                                               //Se o tamanho do grupo for maior que 0, calcula a quantidade de itens dentro do cardume e o ponto medio entre eles
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)                               //Suaviza a rotação, para ficar de maneira mais realista
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                                     Quaternion.LookRotation(direction),
                                     myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
