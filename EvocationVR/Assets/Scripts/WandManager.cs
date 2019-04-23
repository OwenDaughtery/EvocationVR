using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandManager : MonoBehaviour
{

    public int speed;
    public Transform endOfWand;
    public GameObject camera;
    public float startAngleX;
    public Vector3 startAngle;
    public List<GameObject> activeSpells;
    public Dictionary<string, float> spellCosts = new Dictionary<string, float>();
    public ManaManager manaManager;

    // Start is called before the first frame update
    void Start()
    {
        startAngleX = this.transform.eulerAngles.x;
        startAngle = new Vector3(startAngleX, 0f, 0f);
        endOfWand = transform.GetChild(0);
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        spellCosts.Add("TestSpell", 1);
        spellCosts.Add("ArcSpell", 2);
        spellCosts.Add("WardSpell", 1.25f);
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
            shootSpell("TestSpell");
        }
        if (Input.GetKeyDown("q")) {
            shootSpell("ArcSpell");
        }
        if (Input.GetKeyDown("r")) {
            shootSpell("WardSpell");
        }
    }

    private void shootSpell(string tag)
    {
        if (tag == "TestSpell" && manaManager.subtractMana(spellCosts[tag]))
        {
            GameObject testSpell = ObjectPoolerManager.SharedInstance.GetPooledObject(tag);
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
        else if (tag=="ArcSpell" && manaManager.subtractMana(spellCosts[tag])) {
        }
        else if(tag=="WardSpell" && manaManager.subtractMana(spellCosts[tag])){
            GameObject wardSpell = ObjectPoolerManager.SharedInstance.GetPooledObject(tag);
            if (wardSpell != null)
            {
                //set position
                Vector3 wandPos = endOfWand.position;
                Vector3 wandDirection = endOfWand.forward;
                float direction = this.gameObject.transform.eulerAngles.y+90f;
                //Quaternion wandRotation = endOfWand.rotation;
                Vector3 spawnPos = wandPos;
                

                wardSpell.transform.position = spawnPos;

                wardSpell.transform.localEulerAngles = new Vector3(0f,direction,0f);
                wardSpell.SetActive(true);

                //testSpell.GetComponent<Rigidbody>().velocity = testSpell.transform.TransformDirection(new Vector3(0, speed, 0));

            }
        }


    }
}
