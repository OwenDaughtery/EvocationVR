using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandManager : MonoBehaviour
{

    public int speed = 20;
    public Transform endOfWand;
    public GameObject camera;
    public float startAngleX;
    public Vector3 startAngle;
    public ObjectPoolerManager objectPool;

    // Start is called before the first frame update
    void Start()
    {
        startAngleX = this.transform.eulerAngles.x;
        startAngle = new Vector3(startAngleX, 0f, 0f);
        endOfWand = transform.GetChild(0);
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //print(camera.transform.eulerAngles.x);
        //this.transform.eulerAngles.Set(camera.transform.eulerAngles.x, 0f, 0f);
        this.transform.eulerAngles = camera.transform.eulerAngles + startAngle;
        anyKeyPressed();
    }

    private void anyKeyPressed() {
        if (Input.GetKeyDown("e"))
        {
            shootSpell();
        }
    }

    private void shootSpell()
    {
        GameObject testSpell = objectPool.GetPooledObject("TestSpell");

        if (testSpell != null)
        {
            //set position of bullet
            Vector3 wandPos = endOfWand.position;
            Vector3 wandDirection = endOfWand.forward;
            Quaternion wandRotation = endOfWand.rotation;
            Vector3 spawnPos = wandPos;


            testSpell.transform.position = spawnPos;

            testSpell.transform.rotation = wandRotation;
            testSpell.SetActive(true);

            testSpell.GetComponent<Rigidbody>().velocity = testSpell.transform.TransformDirection(new Vector3(0, speed, 0));

        }

    }
}
