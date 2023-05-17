using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//가로 세로
public class HexSixe
{
    public float hexHeight = 3.332823f; 
    public float hexWidth = 2.886311f; 
}


public class HexMapCreator : MonoBehaviour
{
    HexSixe hexSixe = new HexSixe();

    //80*80 육각형 맵을 만들자
    private int width = 80;
    private int height = 80;

    //materials (숲=0, 평원=1, 바다=2)
    [SerializeField] Material[] materials= new Material[3];


    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    private HexMember[] hexMembers;

    Canvas gridCanvas;
    [SerializeField] Image gridImage;
    Image[] gridImages;

    private void Start()
    {
        gridCanvas = GetComponentInChildren<Canvas>();

        //지정한 개수만큼 생성
        hexMembers = new HexMember[height * width];
        gridImages = new Image[height * width];


        //x와 z가 좌표가 된다
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHex(x, z, i++);
            }
        }
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateGrid(x, z, i++);
            }
        }
    }

    private void CreateHex(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
        position.y = -0.5f;
        position.z = (z) * (hexSixe.hexHeight * 0.75f);

        HexMember hex = hexMembers[i] = Instantiate(hexMember);
        hex.transform.SetParent(transform, false);
        hex.xNum = x;
        hex.zNum = z;
        hex.index = i;
        hex.transform.localPosition = position;

        SetMaterial(x, z, i);
    }

    private void SetMaterial(int x, int z, int i)
    {
        if (z < 34 && z > 15 && x > 40 && x < 57)
        {
            ChangeMaterial(i, 0);
        }
        else if (z < 35 && z > 23 && x > 33 && x < 47)
        {
            ChangeMaterial(i, 0);
        }
        else if (z < 24 && z > 18 && x > 36 && x < 41)
        {
            ChangeMaterial(i, 0);
        }

        if (z < 46 && z > 27 && x > 25 && x < 40)
        {
            ChangeMaterial(i, 1);
        }
        else if (z < 57 && z > 35 && x > 28 && x < 52)
        {
            ChangeMaterial(i, 1);
        }
    }

    private void ChangeMaterial(int i, int materialsNumber)
    {
        Renderer renderer = hexMembers[i].gameObject.GetComponent<Renderer>();
        if (renderer.material != materials[materialsNumber])
        {
            renderer.material = materials[materialsNumber];
            hexMembers[i].isGround = true;
            hexMembers[i].Setelevation();
        }
    }

    private void CreateGrid(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
        position.y = (z) * (hexSixe.hexHeight * 0.75f);
        position.z = 0f;

        Image image = gridImages[i] = Instantiate(gridImage);
        image.transform.SetParent(gridCanvas.transform, false);
        image.color = Color.green;
        image.transform.localPosition = position;
    }

}
