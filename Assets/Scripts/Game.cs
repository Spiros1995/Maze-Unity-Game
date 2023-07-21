using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Characters.FirstPerson;


public class Game : MonoBehaviour
{
    public AudioSource BlockBreak;
    public AudioSource PickaxeHit;
    public AudioSource Teleport;
    public string hitTransformBefore;
    private Transform pickaxe;
    private int l;
    private int n;
    private int k;
    private String[] trimmedLine;
    public Text myText;
    private System.Random rnd;
    private System.Random emptyRnd;
    private int color;
    private int count = 0;
    private Transform character;
    public List<Cubes> cubes = new List<Cubes>();
    private Cubes cubeObject;
    private string[] newLines = new string[192];
    int countEmpty = 0;
    private ArrayList emptyLevel1 = new ArrayList();
    public List<Cubes> teleportSpots = new List<Cubes>();
    float time = 0.0f;
    private int pickaxesUsed = 0;
    private int pickaxeLifes = 100;
    public static int pickaxesLeft = 0;
    public static int finalScore = 0;

    void Start()
    {
        // Use file.maz to create the maze
        String mazPath = Environment.CurrentDirectory + "\\Assets\\file.maz";
        colorPickaxe(); 
        character = GameObject.Find("FPSController").GetComponent<Transform>();
        rnd = new System.Random();
        string[] lines = System.IO.File.ReadAllLines(@mazPath);
        l = Int32.Parse(lines[0].Split('=')[1]); ;
        n = Int32.Parse(lines[1].Split('=')[1]); ;
        k = Int32.Parse(lines[2].Split('=')[1]); ;
        pickaxesLeft = k;
        int p = 0;

        /* Parse through file */

        for (int i = 0; i< lines.Length; i++) {
            if (lines[i].Contains("LEVEL") || lines[i].Contains("END") || lines[i].Contains("L") || lines[i].Contains("N") || lines[i].Contains("K"))
            {

            }
            else {
                newLines[p] = lines[i].Replace("  ", " ");
                p++;
            }
        }

        // Start Generating cubes. Different Ints are assigned to different materials and colors
        // 95 is floor and boarders, which are unbreakable
        // 7 is transparent, also unbreakbale 

        /* BOARDER SQUARES WORKING AS UNBREAKABLE COLLIDERS*/
        //BOTTOM
        for (int i = 0; i < n; i++) {
            for (int j = 0; j< n; j++) {
                cubeObject = new Cubes();
                cubeObject.createCube(0.0f + (2f * j), 1.0f, 0.0f + (2f * i), 95);
                cubes.Add(cubeObject);

            }
        }
        //TOP
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                cubeObject = new Cubes();
                cubeObject.createCube(0.0f + (2f * j), 2*(l+1) + 1, 0.0f + (2f * i), 7);
                cubes.Add(cubeObject);

            }
        }
        //LEFT
        for (int i = 0; i < l+2; i++)
        {
            for (int j = 0; j < n; j++)
            {
                cubeObject = new Cubes();
                cubeObject.createCube(-2.0f, 0.0f + (2f * i), 0.0f + (2f * j), 95);
                cubes.Add(cubeObject);

            }
        }
        //RIGHT 
        for (int i = 0; i < l + 2; i++)
        {
            for (int j = 0; j < n; j++)
            {
                cubeObject = new Cubes();
                cubeObject.createCube(0.0f + (2f * j), 0.0f + (2f * i), (2f*n) , 95);
                cubes.Add(cubeObject);

            }
        }
        //Other LEFT 
        for (int i = 0; i < l + 2; i++)
        {
            for (int j = 0; j < n; j++)
            {
                cubeObject = new Cubes();
                cubeObject.createCube(0.0f + (2f * j), 0.0f + (2f * i), -2.0f, 95);
                cubes.Add(cubeObject);

            }
        }

        //Other RIGHT 
        for (int i = 0; i < l + 2; i++)
        {
            for (int j = 0; j < n; j++)
            {
                cubeObject = new Cubes();
                cubeObject.createCube((2f * n), 0.0f + (2f * i), 0.0f + (2f * j), 95);
                cubes.Add(cubeObject);

            }
        }

        //BLOCKS (Maze)
        // 0 is Red. 1 is Green. B is Blue. 3,4,5 are different materials
        //-1 is an Empty block. The player spawns in a random empty block
        //6 is Black and it is used as a teleporter block

        for (int i = 1; i < l+1; i++)
        {
            for (int j = 0; j < n; j++)
            {

                trimmedLine = newLines[count].Split(' ');
                count++;
                for (int k = 0; k < n; k++)
                {

                    if (trimmedLine[k].Equals("R"))
                    {
                        color = 0;
                    }
                    else if (trimmedLine[k].Equals("G"))
                    {
                        color = 1;
                    }
                    else if (trimmedLine[k].Equals("B"))
                    {
                        color = 2;
                    }
                    else if (trimmedLine[k].Equals("T1"))
                    {
                        color = 3;
                    }
                    else if (trimmedLine[k].Equals("T2"))
                    {
                        color = 4;
                    }
                    else if (trimmedLine[k].Equals("T3"))
                    {
                        color = 5;
                    }
                    else if (trimmedLine[k].Equals("E"))
                    {
                        color = -1;

                        if (i == 1) {

                            countEmpty++;

                        }

                    }
                    else if (trimmedLine[k].Equals("W")) {
                        color = 6;
                    }
                    else
                    {
                        Debug.Log("Wrong input" + trimmedLine[k] + "<-");
                        color = 999;
                    }
                    if (color != 999)
                    {
                        cubeObject = new Cubes();
                        cubeObject.createCube(0.0f + (2f * j), 1.0f + (2f * i), 0.0f + (2f * k), color);
                        cubes.Add(cubeObject);

                    }
                    if (i == 1 && trimmedLine[k].Equals("E"))
                    {
                        emptyLevel1.Add(cubeObject.getPosition());

                    }
                    if (trimmedLine[k].Equals("W"))
                    {
                        teleportSpots.Add(cubeObject);

                    }
                }
            }
        }

        // Pick a random empty spot on 1st floor to spawn player
        emptyRnd = new System.Random();
        int pos = rnd.Next(0, emptyLevel1.Count);
        character.transform.position = (Vector3)emptyLevel1[pos];  
    }

    void Update()
    {
        if (GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled == true)
        {
            if (pickaxesLeft <= 0)
            {
                GameObject.Find("Pickaxe_LOD111").GetComponent<MeshRenderer>().enabled = false;
            }
            else {
                GameObject.Find("Pickaxe_LOD111").GetComponent<MeshRenderer>().enabled = true;
            }
            /* time calc for score*/
            time += Time.deltaTime;
            /* Show score and pickaxes left on screen*/
            GameObject.Find("Text").GetComponent<Text>().text = "Score: " + calculateScore();
            GameObject.Find("PickaxesText").GetComponent<Text>().text = "Pickaxes: " + pickaxesLeft;

            /* Raycast initialise, drawline for debugging */

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 0.5f);
                if (hit.transform.name != hitTransformBefore)
                {
                    hitTransformBefore = hit.transform.name;
                }
            }

            if (hit.transform != null)
                hitTransformBefore = hit.transform.name;

            /* BREAK CUBE CODE */

            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.H)) && pickaxesLeft > 0) // Left Click or H
            {
                /* Change text to in game instructions */
                GameObject.Find("Instructions").GetComponent<Text>().text = "Press X to end the game anytime";
                /* Pickaxe hit animation trigger */
                GameObject.Find("Pickaxe").GetComponent<Animator>().SetTrigger("New Trigger");

                /*RAYCAST RESULT*/
                System.String RaycastReturn = hit.collider.gameObject.name;

                if (RaycastReturn == "Cube")
                {

                    /* Find which cube from the list it is to change it's lifes/break it */
                    for (int i = 0; i < cubes.Count; i++)
                    {
                        if (cubes[i].getCube() == hit.collider.gameObject)
                        {
                            PlayPickaxeHit();
                            /* Hit a cube so damage pickaxe */
                            pickaxeLifes -= 10;
                            if (pickaxeLifes == 0)
                            {
                                pickaxesUsed++;
                                pickaxesLeft--;
                                if (pickaxesUsed < k)
                                {
                                    pickaxeLifes = 100;
                                }
                            }
                            colorPickaxe();
                            /* Remove 1 life from block */
                            cubes[i].hit();
                            /* Check if block is dead */
                            if (cubes[i].getLifes() <= 0 && cubes[i].getColor() != 95)
                            {
                                /* Break cube to pieces */
                                PlayBlockBreak();
                                cubes[i].breakToLittleBlocks();
                                /* Random pickaxe drop */
                                int dropRate = rnd.Next(0, 100);
                                if (dropRate <= 20)
                                {
                                    GameObject.Find("RndPickaxe").GetComponent<MeshRenderer>().enabled = true;
                                    GameObject.Find("RndPickaxe").GetComponent<Transform>().position = cubes[i].getPosition();

                                }
                                /* Destroy cube */
                                Destroy(cubes[i].getCube());
                            }

                        }
                    }
                }

            }
        }

        /* Change between cameras */
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled == true) {
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
                GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = false;
                GameObject.Find("OutsideCamera").GetComponent<Camera>().enabled = true;
                GameObject.Find("OutsideCamera").GetComponent<keyboardorbit>().enabled = true;
                for (int i=0; i<cubes.Count; i++)
                {
                    if (cubes[i].getCube()!=null) {
                        cubes[i].getCube().GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 0.6f);

                    }
                }
            }else if (GameObject.Find("OutsideCamera").GetComponent<Camera>().enabled == true)
            {
                GameObject.Find("OutsideCamera").GetComponent<Camera>().enabled = false;
                GameObject.Find("OutsideCamera").GetComponent<keyboardorbit>().enabled = false;
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
                GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = true;
                for (int i = 0; i < cubes.Count; i++)
                {
                    if (cubes[i].getCube() != null)
                    {

                        cubes[i].getCube().GetComponent<Renderer>().material.color += new Color(0, 0, 0, 0.6f);
                    }
                }
            }
        }

        /* Get to Game Over Screen from whichever level */
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (GameObject.Find("FPSController").GetComponent<Transform>().position.y > 2*l + 0.5f)
            {
                finalScore = calculateScore();
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            else
            {
                finalScore = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }

        /* Get to Game Over Screen from last level */
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameObject.Find("FPSController").GetComponent<Transform>().position.y > 2*l + 0.5f)
            {
                finalScore = calculateScore();
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
    }
    
    // Real time score display
    public int calculateScore() {
        int score = (n * n) - (int)time - (pickaxesUsed * 50);
        return score;
    }

    // Changing the color of the pickaxe depending on uses left
    public void colorPickaxe() {
        if (pickaxeLifes >= 70)
        {
            Material redMat = Resources.Load("Red", typeof(Material)) as Material;
            GameObject.Find("Pickaxe_LOD111").GetComponent<MeshRenderer>().material = redMat;
        }
        else if (pickaxeLifes >= 40)
        {
            Material bordoMat = Resources.Load("Bordo", typeof(Material)) as Material;
            GameObject.Find("Pickaxe_LOD111").GetComponent<MeshRenderer>().material = bordoMat;
        }
        else
        {
            Material blackMat = Resources.Load("Black", typeof(Material)) as Material;
            GameObject.Find("Pickaxe_LOD111").GetComponent<MeshRenderer>().material = blackMat;
        }
    }

    // Sounds for different actions
    public void PlayBlockBreak()
    {
        BlockBreak = GameObject.Find("BlockBreak").GetComponent<AudioSource>();
        BlockBreak.Play();
    }

    public void PlayPickaxeHit()
    {
        PickaxeHit = GameObject.Find("PickaxeHit").GetComponent<AudioSource>();
        PickaxeHit.Play();
    }

    public void PlayTeleport()
    {
        Teleport = GameObject.Find("Teleport").GetComponent<AudioSource>();
        Teleport.Play();
    }

    // Function for teleporting blocks
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            GameObject.Find("Point Light").GetComponent<Light>().intensity = 25;
            PlayTeleport();
            for (int i = 0; i < teleportSpots.Count-1; i++) {
                if (teleportSpots[i].getPosition() == other.gameObject.GetComponent<Transform>().position) {
                    character.transform.position = teleportSpots[i+1].getPosition();
                    Debug.Log("Teleported to floor " + (i+2));
                }
            }
        }
    }

    // Turn off the bright light from the teleporting animation
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Cube")
        {
            GameObject.Find("Point Light").GetComponent<Light>().intensity = 1;
        }
    }
}