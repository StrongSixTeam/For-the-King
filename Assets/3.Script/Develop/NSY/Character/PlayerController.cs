using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //¿©±â¼­ºÎÅÍ
    [Header("Node")]
    public HexMember targetNode;
    public HexMember middletargetNode;
    public List<HexMember> targetNodes = new List<HexMember>();

    bool moveSwitch = false;

    //[SerializeField] private float movementDuration = 1, rotationDuration = 0.3f;


    public void Update()
    {

        if (Input.GetMouseButton(0))
        {
            MouseInput();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSwitch = true;
            StartCoroutine(MoveTargetNode());
            // StartCoroutine(RotationCoroutine());
        }

        if (moveSwitch &&
            transform.position == targetNodes[targetNodes.Count - 1].transform.position)
        {
            //targetNodes.Clear();
            moveSwitch = false;
            Debug.Log("µµÂø");
        }
    }

    private void MouseInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            targetNode = hit.transform.gameObject.GetComponent<HexMember>();
            targetNodes.Add(targetNode);
        }
    }

    //¿©±â±îÁö´Â ¿¡ÀÌ½ºÅ¸ ÂÊ¿¡¼­ ±¸ÇÏ´Ï±î Å×½ºÆ® ³¡³ª¸é ÁÖ¼®Ã³¸®ÇÏ°Å³ª Áö¿ì¼Åµµ µË´Ï´ç



    private IEnumerator MoveTargetNode()
    {
        for (int i = 0; i < targetNodes.Count;)
        {

            Rotation(i);
            while (Vector3.Distance(transform.position, targetNodes[i].transform.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetNodes[i].transform.position, 10f * Time.deltaTime);
                yield return null;
            }
            transform.position = targetNodes[i].transform.position;
            i++;
        }
        targetNodes.Clear();
        yield break;

        //¸®½ºÆ® ¾È¿¡ ÀÖ´Â ¹è¿­ ¸®½ºÆ® ¾È¿¡ ÀÖ´Â ¸®½ºÆ®µéÀ» ½Ï ´Ù Áö¿ì´Â ÇÔ¼ö, 
        //¸®¹«ºê´Â ÁöÁ¤ÇÑ ¾î¶² °ÍµåÀ» –E ¼ö ÀÖ°í 
        //for¹®À» ´Ù µ¹°í ³ª¼­ Å¬¸®¾î´Â 
        /*
         
         ±×·¡¼­ Å¬¸®¾îÇÏ´Â À§¯‚¸¦ ¹Ù²ãÁÜ
         ³ëµå°¡ ÀÎµ¦½º ¹üÀ§¸¦ ¹þ¾î³­ °÷À» µ¹°í ÀÖÀ½
         */
    }


    private void Rotation(int index)
    {
        Vector3 direction = targetNodes[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
}
