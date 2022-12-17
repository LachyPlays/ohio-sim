using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingChareterV2: MonoBehaviour
{
    public GameObject OhioPerson_save;
    public Camera Cam;
    public GameObject SpawnPoint;
    public float y;
    private float z = 1.99f;
    private float x = 11.62f;
    private Rigidbody rb;
    public GameObject OhioPerson;
    public Transform eyes;



    // Start is called before the first frame update
    void Start()
    {

        rb = OhioPerson.GetComponent<Rigidbody>();
        eyes = OhioPerson.transform.Find("Eyes");
        eyes.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        eyes = OhioPerson.transform.Find("Eyes");
        
    
        try
        {
            y += 0.01f;
            OhioPerson.transform.position = new Vector3(x, y, z);
        }
        catch
        {

        }

        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (raycastHit.transform.gameObject.name == "Thing Variant")
                {

                    eyes.gameObject.SetActive(true);
                }
                else if (raycastHit.transform.gameObject.name == "Thing Save(Clone)")
                {

                    eyes.gameObject.SetActive(true);
                }
                else
                {
                    eyes.gameObject.SetActive(false);
                }

            }
            else if(Input.GetMouseButtonUp(0))
            {
                eyes.gameObject.SetActive(false);
            }
        }
        
            


       


    }



    void spawn()
    {

      
    }

   
        

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Del")
        {
            OhioPerson = Instantiate(OhioPerson_save, SpawnPoint.transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            y = -5f;
            
            if(OhioPerson.active == false)
            {
                OhioPerson.SetActive(true);
            }


        }


        
    }
}
