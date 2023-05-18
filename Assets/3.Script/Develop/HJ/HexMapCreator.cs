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

    public int mapSize;

    //materials (바다=0, 숲=1, 평원=2)
    [SerializeField] Material[] materials= new Material[3];


    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    private HexMember[] hexMembers;


    //이동횟수를 표시할 캔버스
    private Canvas gridCanvas;
    [SerializeField] private Image gridImage; //나중에 이미지 교체할거임
    private Image[] gridImages; //인덱스 정보로 관리할예정 (hexMembers[0] =짝꿍= gridImages[0])


    private void Start()
    {
        mapSize = height * width;

        gridCanvas = GetComponentInChildren<Canvas>();

        //지정한 개수만큼 생성
        hexMembers = new HexMember[mapSize];
        gridImages = new Image[mapSize];


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
        position.z = z * (hexSixe.hexHeight * 0.75f);

        HexMember hex = hexMembers[i] = Instantiate(hexMember);
        hex.transform.SetParent(transform, false);
        hex.transform.localPosition = position;

        hex.SetHexMemberData(x, z, i, SetMaterial(x, z, i));


        ////이웃 설정
        //if (x > 0)
        //{
        //    hex.SetNeighbor(HexDirection.W, hexMembers[i - 1]);
        //}
        //if (z > 0)
        //{
        //    if ((z & 1) == 0)
        //    {
        //        hex.SetNeighbor(HexDirection.SE, hexMembers[i - width]);
        //        if (x > 0)
        //        {
        //            hex.SetNeighbor(HexDirection.SW, hexMembers[i - width - 1]);
        //        }
        //    }
        //    else
        //    {
        //        hex.SetNeighbor(HexDirection.SW, hexMembers[i - width]);
        //        if (x < width - 1)
        //        {
        //            hex.SetNeighbor(HexDirection.SE, hexMembers[i - width + 1]);
        //        }
        //    }
        //}
    }

    //땅이 될 노드를 지정한다 (나중에 시간이 되면 [랜덤 맵 생성] 도전!~)
    private int SetMaterial(int x, int z, int i)
    {
        if (z < 34 && z > 15 && x > 40 && x < 57)
        {
            ChangeMaterial(i, 1);
            return 1;
        }
        else if (z < 35 && z > 23 && x > 33 && x < 47)
        {
            ChangeMaterial(i, 1);
            return 1;
        }
        else if (z < 24 && z > 18 && x > 36 && x < 41)
        {
            ChangeMaterial(i, 1);
            return 1;
        }

        if (z < 46 && z > 27 && x > 25 && x < 40)
        {
            ChangeMaterial(i, 2);
            return 2;
        }
        else if (z < 57 && z > 35 && x > 28 && x < 52)
        {
            ChangeMaterial(i, 2);
            return 2;
        }

        return 0;
    }

    //매개변수로 전달받는 index의 Material를 바꾼다
    private void ChangeMaterial(int i, int materialsNumber)
    {
        Renderer renderer = hexMembers[i].gameObject.GetComponent<Renderer>();
        if (renderer.material != materials[materialsNumber])
        {
            renderer.material = materials[materialsNumber];
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
