using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�ܺι������� ���ι������� �����ΰ���
public class Radius
{
    public float outerRadius = 3.332823f; 
    public float innerRadius = 2.886311f; 
}


public class HexMapCreator : MonoBehaviour
{
    Radius radius = new Radius();

    //50*50 ������ ���� ������
    private int width = 50;
    private int height = 50;

    //materials
    [SerializeField] Material[] material= new Material[3];

    //������ mash ������ ���ض�
    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    HexMember[] hexMembers;

    Canvas gridCanvas;
    [SerializeField] Image gridImage;
    Image[] gridImages;

    private void Start()
    {
        gridCanvas = GetComponentInChildren<Canvas>();

        //������ ������ŭ ����
        hexMembers = new HexMember[height * width];
        gridImages = new Image[height * width];

        //x�� z�� ��ǥ�� �ȴ�
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
        position.x = (x + z * 0.5f - z / 2) * (radius.innerRadius);
        position.y = 0f;
        position.z = (z) * (radius.outerRadius * 0.75f);

        HexMember cell = hexMembers[i] = Instantiate(hexMember);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }

    private void CreateGrid(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (radius.innerRadius);
        position.y = (z) * (radius.outerRadius * 0.75f);
        position.z = 0f;

        Image image = gridImages[i] = Instantiate(gridImage);
        image.transform.SetParent(gridCanvas.transform, false);
        image.color = Color.green;
        image.transform.localPosition = position;
    }

}
