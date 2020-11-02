using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashObj : MonoBehaviour
{
	[SerializeField]
	private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			_image.enabled = true;
			float distans = Vector3.Distance(transform.position, other.transform.position);
			if(distans < 5)
			{
				distans = 0;
			}

			_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1 / distans);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
			_image.enabled = false;
		}
	}




	// Update is called once per frame
	void Update()
    {
        
    }
}
