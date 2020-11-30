using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
	[SerializeField, Tooltip("強調表現に使用する2Dエフェクトを選択")]
	private GameObject _spriteImage = null;
	private SpriteRenderer _sprite= null;

    // Start is called before the first frame update
    void Start()
    {
		GameObject flash = Instantiate(_spriteImage, transform.position, Quaternion.Euler(30, 45, 0));
		_spriteImage = flash;
		_sprite = _spriteImage.GetComponent<SpriteRenderer>();
		_sprite.enabled = false;
    }

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Light")
		{
			_sprite.enabled = true;
			float distans = Vector3.Distance(transform.position, other.transform.position + new Vector3(0,-1,0));
			if(distans < 5)
			{
				distans = 0;
			}

			_sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1 / distans);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Light")
		{
			_sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0);
			_sprite.enabled = false;
		}
	}


	// Update is called once per frame
	void Update()
    {
        
    }
}
