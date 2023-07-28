using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarObjeto : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;
    private bool podertomar = false;
    private bool boton = false;
    private bool tomado = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedObject != null)
        {
            if(Input.GetKey("x") || !boton)
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;
                tomado = false;
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Objeto"))
        {
            if(pickedObject == null)
            {
                podertomar = true;
                if(Input.GetKey("z") || boton)
                {
                    other.GetComponent<Rigidbody>().useGravity = false;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    other.transform.position = handPoint.transform.position;
                    other.gameObject.transform.SetParent(handPoint.gameObject.transform);
                    pickedObject = other.gameObject;
                    tomado = true;
                    podertomar = false;
                    boton = true;
                }
                
            }
        }
        
    }

    public void OnClick()
    {
        if(podertomar)
        {
            boton = true;
        }
        if(tomado)
        {
            boton = false;
        }
        
    }
}
