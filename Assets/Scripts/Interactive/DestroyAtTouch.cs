using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtTouch : MonoBehaviour
{
    public int item_index;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(GameDefine.PlayerTag))
        {
            Debug.Log("touch and destroy");
            ItemData temp = ConfigManager.Instance.GetItemData(item_index);
            other.gameObject.GetComponent<PlayerController>().AddItem(item_index);
            Destroy(gameObject);

            if (temp.type == ItemType.Stroy && temp.stroyId != -1)
            {
                UIManager.Instance.PopPanel<int>(GameDefine.StoryPanel, temp.stroyId);
            }
            else
            {
                UIManager.Instance.PopTypewriterSentence("你捡到了" + temp.name, 0.2f, other.transform);
            }

        }
    }


}
