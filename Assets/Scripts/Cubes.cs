using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Cubes : MonoBehaviour
{
    private System.Random rnd;
    private int color = -999;
    private Vector3 position;
    private int lifes;
    public GameObject Cube;
    public MeshRenderer mesh;

    public Cubes() {
        rnd = new System.Random();
        lifes = 3;
    }

    public GameObject createCube(float i, float j, float l, int color)
    {
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (color == 0)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Cube.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (color == 1)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Cube.transform.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (color == 2)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Cube.transform.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (color == 3)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Material newMat = Resources.Load("T1", typeof(Material)) as Material;
            Cube.transform.GetComponent<MeshRenderer>().material = newMat;
        }
        else if (color == 4) {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Material newMat = Resources.Load("T2", typeof(Material)) as Material;
            Cube.transform.GetComponent<MeshRenderer>().material = newMat;
        }
        else if (color == 5) {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Material newMat = Resources.Load("T3", typeof(Material)) as Material;
            Cube.transform.GetComponent<MeshRenderer>().material = newMat;
        }
        else if (color == 6)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Cube.transform.GetComponent<MeshRenderer>().material.color = Color.black;
            Cube.transform.GetComponent<MeshRenderer>().GetComponent<BoxCollider>().isTrigger = true;
        }
        else if (color == 7)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.name = "BORDER";
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Cube.transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
            Cube.transform.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (color == 95)
        {
            Cube.GetComponent<MeshRenderer>().material = transparentMaterial();
            Cube.transform.position = new Vector3(i, j, l);
            Cube.transform.transform.localScale += new Vector3(1, 1, 1);
            Material newMat = Resources.Load("T4", typeof(Material)) as Material;
            Cube.transform.GetComponent<MeshRenderer>().material = newMat;
        }
        else {

        }
        this.color = color;
        position = new Vector3(i, j, l);
        return Cube;
    }

    public void hit() {
        lifes--;
    }

    public void transformX(float x) {
        Vector3 temp = new Vector3(x, 0, 0);
        Cube.transform.position += temp;
    }

    public GameObject getCube()
    {
        return Cube;
    }

    public int getLifes() {
        return lifes;
    }

    public int getColor() {
        return color;
    }

    public Vector3 getPosition() {
        return position;
    }

    // When blocks are destroyed they leave small blocks behind
    public void breakToLittleBlocks() {
        for (int i = 0; i < 5; i++) {
            GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Cube.AddComponent<Rigidbody>();
            Cube.transform.position = position + new Vector3 (0+i* (float) rnd.NextDouble(), 0, 0 + i * (float)rnd.NextDouble());
            Cube.transform.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
            Cube.transform.GetComponent<MeshRenderer>().material.color = this.Cube.transform.GetComponent<MeshRenderer>().material.color;
            Cube.transform.GetComponent<MeshRenderer>().material = this.Cube.transform.GetComponent<MeshRenderer>().material;
            Destroy(Cube, 5);
        }
    }

    public Material transparentMaterial() {
        Material m = new Material(Shader.Find("Standard"));
        m.SetFloat("_Mode", 2);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = 3000;
        return m;
    }

}
