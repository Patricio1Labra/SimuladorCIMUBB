using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaInversa : MonoBehaviour
{
    //Variables de bones, huesos y punto de objetivo
     //[] Caja que guarda todas las bones
    [SerializeField]
    Transform[] bones; //De tipo transform porque hablamos de posición, rotación y escala del objeto.
    float[] bonesLengths; //Guardamos longitudes (escala) de los huesos
    
    [SerializeField]
    Transform targetPosition;
    
    [SerializeField]
    Transform limitador; //Limitra el ángulo del desplazamiento del codo de nuestro brazo
    
    //Variables del metodo FABRIK (iteracion)
    [SerializeField]
    int solverIterations = 5; //En una iteracion aplica el método completo, se hacen 5 para que
                              //sea más exacto el movimiento

    private void Start()
    {
        //Se calculan las longitudes de los huesos para ser tomadas en el metodo FABRIK para
        //que tenga sentido y no parezcan que se parten.
        bonesLengths = new float[bones.Length];

        //En un for analizamos la longitud de cada hueso en caso de que sea el ultimo hueso
        //entonces la longitud será 0 porque no existe hueso en la ultima articulacion
        for(int i = 0; i < bones.Length; i++)
        {
            if(i < bones.Length - 1)
            {
                //Aquí va recorriendo hueso por hueso y se obtiene la manitud entre bones
                //que dan la longitud de los huesos
                bonesLengths[i] = (bones[i + 1].position - bones[i].position).magnitude;
            }
            else
            {
                bonesLengths[i] = 0f;
            }
        }
    }

    private void Update()
    {
        SolveIK();
    }

    //MÉTODO FABRIK
    // Nota:Algoritmo que clacula posiciones pero no las rotaciones
    //Invertir el orden en que se analiza las posiciones, es decir, comenzando del Objetivo a la articulación 1 (raíz)
    private Vector3[] SolveInversePositions (Vector3[] forwardPositions)
    {
        Vector3[] inversePositions = new Vector3[forwardPositions.Length]; //Almacenamiento

        //se calcula las posiciones ideales desde el último hasta el primer hueso en base las posiciones actuales
        for (int i = (forwardPositions.Length - 1); i >= 0; i--)
        {
            if(i == forwardPositions.Length- 1)
            {
                //Si es el último hueso, la posición prima es la misma que la posición objetivo
                inversePositions[i]= targetPosition.position;
            }
            else
            {
                Vector3 posPrimaSiguiente = inversePositions[i+ 1];
                Vector3 posBaseActual =forwardPositions[i];
                Vector3 direccion = (posBaseActual - posPrimaSiguiente).normalized; //Almacena un vector unitario
                                                                                    //desde posPrimaSiguiente
                float longitud = bonesLengths[i];
                inversePositions[i] = posPrimaSiguiente + (direccion * longitud);
            }
        }
        return inversePositions;
    }

    private Vector3[] SolveForwardPositions (Vector3[] inversePositions)
    {
        Vector3[] forwardPositions = new Vector3[inversePositions.Length];
        //Calculamos las posiciones 'reales' desde el primer hasta el último hueso en base a las posiciones 'ideales'
        for (int i = 0; i < inversePositions.Length; i++)
        {
            if (i == 0)
            {
                //Si es el primer hueso, la posición es la misma que la posición del primer hueso base
                forwardPositions[i] = bones[0].position;
            }
            else
            {
                Vector3 posPrimaActual = inversePositions[i];
                Vector3 posPrimaSegundaAnterior = forwardPositions[i - 1];
                Vector3 direccion = (posPrimaActual - forwardPositions[i - 1]).normalized;   //Almacena un vector unitario
                                                                                            //desde posPrimaSegundaAnterior hasta
                                                                                            //posPrimaActual y normalizamos
                float longitud = bonesLengths[i - 1];//Long del hueso anterior
                forwardPositions[i] = posPrimaSegundaAnterior + (direccion * longitud);
            }
        }
        return forwardPositions;
    }

    private void SolveIK()
    {
        Vector3[] finalBonesPositions = new Vector3[bones.Length];

        //Guardamos las posiciones actuales de los huesos en una lista de posiscionesFinalesHuesos
        for(int i = 0; i < bones.Length; i++)
        {
            finalBonesPositions[i] = bones[i].position;
        }

        //Itertamos el Método FABRIK tantas veces como la variable solverIteration tenga
        for (int i = 0; i < solverIterations; i++)
        {
            //Calculamos valores primos en base a las posiciones actuales de los huesos y con el
            //resultado de esta función se los pasaremos a la función que calcula los primos segundos
            //у estas serán las posiciones resultantes a guardar en la variable de posiciones finales
            finalBonesPositions = SolveForwardPositions(SolveInversePositions(finalBonesPositions));
        }
            
        //Tras iterar el método le asignamos las posiciones obtenidas a cada hueso y calculamos las
        //rotaciones para que cada hueso apunte al siguiente
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].position = finalBonesPositions[i];

            //Aplicamos rotaciones
            if(i != bones.Length - 1)
            {
                bones[i].rotation = Quaternion.LookRotation(finalBonesPositions[i + 1] - bones[i].position);
            }
            else
            {
                bones[i].rotation = Quaternion.LookRotation(targetPosition.position - bones[i].position);
            }
        }

        /* if(limitador != null)
        {
            for (int i = 1; i < bones.Length - 1; i++)
            {
                Plane projectionPlane = new Plane(bones[i + 1].position - bones[i - 1].position, bones[i - 1].position);
                Vector3 projectedBonePosition = projectionPlane.ClosestPointOnPlane(bones[i].position);
                Vector3 projectedPole = projectionPlane.ClosestPointOnPlane(limitador.position);
                float angleOnPlane = Vector3.SignedAngle(projectedBonePosition - bones[i - 1].position, projectedPole - bones[i - 1].position, bones[i - 1].position);
                bones[i].position = Quaternion.AngleAxis(angleOnPlane, projectionPlane.normal) * (bones[i].position - bones[i - 1].position);
            }
        } */
    }

}