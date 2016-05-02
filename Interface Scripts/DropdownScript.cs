using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropdownScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {



	public RectTransform container;
	public bool isOpen;
	Vector3 scale;
	public bool isGood = false;
	public float actualScale;

	// Use this for initialization
	void Start ()
	{
		isOpen = false;
		if(container == false)
			container = transform.FindChild ("Container").GetComponent<RectTransform> ();

	}

	// Update is called once per frame
	void Update () 
	{

		//actualScale = Time.fixedDeltaTime;
		if (isGood == false) {
			if (isOpen == true) {
				actualScale += Time.fixedDeltaTime*4; 
				if (actualScale > 1) {
					actualScale = 1;
					isGood = true;
				}
				container.localScale = new Vector3 (container.localScale.x, Mathf.Clamp (actualScale, 0, 1), container.localScale.z);
			} else {
				actualScale -= Time.fixedDeltaTime*4;
				if (actualScale < 0) {
					actualScale = 0;
					isGood = true;
				}
				container.localScale = new Vector3 (container.localScale.x, Mathf.Clamp (actualScale, 0, 1), container.localScale.z);
			}
		}
	}
	//#region IPointerEnterHandler implementation
	public void OnPointerEnter (PointerEventData eventData)
	{
		isOpen = true;
		isGood = false;
	}
	//#endregion

	//#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		isOpen = false;
		isGood = false;
	}

	//#endregion

}

