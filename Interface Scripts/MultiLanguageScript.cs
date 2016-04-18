using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MultiLanguageScript : MonoBehaviour{
	/*public TextsClass[] listOfClassWithoutScripts = new TextsClass[1];
	public TextInsideScripts[] listTextsInsideScript = new TextInsideScripts[1];
	private List<TextsClass> attTexts = new List<TextsClass> ();
	private List<TextInsideScripts> insiderText = new List<TextInsideScripts>();
	[HideInInspector]public int indexOfLang = 0;
	private int actualIndex = 0;


	void Start ()
	{
		actualIndex = indexOfLang;
		for (int i = 0; i < listOfClassWithoutScripts.Length; i++) {
			//for (int j = 0; j < listOfClass [i].languages.Length; j++) {
			attTexts.Add (new TextsClass (listOfClassWithoutScripts [i].obiectWithText, listOfClassWithoutScripts [i].obiectWithText.GetComponent<Text>(),
				listOfClassWithoutScripts[i].languages));
			//}
		}
		for(int i = 0; i < listTextsInsideScript.Length; i++)
		{
			insiderText.Add(new TextInsideScripts(listTextsInsideScript[i].nameOfScript, listTextsInsideScript[i].nameOfSht, listTextsInsideScript[i].languagesInScripts,
				listTextsInsideScript[i].languagesInScripts, listTextsInsideScript[i].thisObiect, 
//				listTextsInsideScript[i].thisObiect.GetComponent(typeof(listTextsInsideScript[i].nameOfScript)) as Component));
		}
	}
	void Update ()
	{
		if (actualIndex != indexOfLang) {
			for (int i = 0; i < attTexts.Count; i++) {
				attTexts [i].textsInsideObiect.text = attTexts [i].languages [indexOfLang];
			}
			for (int z = 0; z < listTextsInsideScript.Length; z++) {
				
				//listTextsInsideScript [z].thisObiect.Ge

			}
			actualIndex = indexOfLang;
		}
	}

}

public class TextsClass{

	public GameObject obiectWithText;
	[HideInInspector]public Text textsInsideObiect;
	public string [] languages;

	public TextsClass(GameObject objWithText, Text teksts, string [] langs)
	{
		this.obiectWithText = objWithText;
		this.textsInsideObiect = teksts;
		this.languages = langs;
	}
}
public class TextInsideScripts{
	
	public string nameOfScript;
	public string nameOfSht;
	public string[] languagesInScripts;
	public GameObject thisObiect;
	[HideInInspector]public Component [] komponent;

	public TextInsideScripts(string nazwaSkryptu, string nazwaZmiennej, string [] wielojezyczneTlumaczenie, GameObject thsObj, Component [] comp)
	{
		this.nameOfScript = nazwaSkryptu;
		this.nameOfSht = nazwaZmiennej;
		this.languagesInScripts = wielojezyczneTlumaczenie;
		this.thisObiect = thsObj;
		this.komponent = comp;
	}*/
}
