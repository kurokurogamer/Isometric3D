using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugEditUI : MonoBehaviour
{
    private Dictionary<TestItemBase, int> _itemList;
    private Dictionary<int, int> _itemDate;
    private bool _debugMode;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void NnmPuls(TestItemBase baseItem, int num)
    {
        if(_itemDate.ContainsKey(baseItem.ItemID))
		{
            _itemDate.Add(baseItem.ItemID, 0);
		}
	}

    // Update is called once per frame
    void Update()
    {

        if(_debugMode)
		{
            gameObject.SetActive(true);
		}
        else
		{
            gameObject.SetActive(false);
		}
    }
}
