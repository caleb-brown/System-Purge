using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Platform : MonoBehaviour
{
    public Sprite sprite;
    public int addNum;
    public GameObject prefab;
    public bool isParent = true;
    int oldNum = 0;

	// Update is called once per frame
	void Update ()
    {
        //Updates all of the children's SpriteRender sprites
        foreach (SpriteRenderer i in GetComponentsInChildren<SpriteRenderer>())
        {
            i.sprite = sprite;
        }

        /*Vector3 curPos = this.transform.position;
        float scaleX = this.transform.lossyScale.x;

        if (this.gameObject.transform.childCount < addNum && !Application.isPlaying && isParent)
        {
            for (int i = 0; i < addNum - oldNum; i++)
            {
                curPos = curPos + new Vector3(scaleX, 0);
                GameObject childObj = Instantiate(prefab, curPos, Quaternion.identity) as GameObject;
                childObj.GetComponent<Platform>().isParent = false;
                childObj.transform.parent = transform;
                curPos = childObj.transform.position;
                transform.GetChild(transform.childCount);
            }
            for (int i = 0; i < oldNum - addNum; i++)
            {
                Destroy(transform.GetChild(transform.childCount));
            }
            oldNum = addNum;
        }*/
	}
}
