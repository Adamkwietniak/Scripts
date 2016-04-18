using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Reflection;

public class MultiLanguageScript : MonoBehaviour{
	public GameObject obiectWithScript;
	public bool textsInScripts = false;
	public TextsClass[] listOfClassWithoutScripts = new TextsClass[1];
	public TextInsideScripts[] listTextsInsideScript = new TextInsideScripts[1];
	private List<TextsClass> attTexts = new List<TextsClass> ();
	private List<TextInsideScripts> insiderText = new List<TextInsideScripts>();
	public static int indexOfLang = 0;
	private int actualIndex = 1;


	void Start ()
	{
		actualIndex = indexOfLang;
		indexOfLang++;
		for (int i = 0; i < listOfClassWithoutScripts.Length; i++) {
			//for (int j = 0; j < listOfClass [i].languages.Length; j++) {
			attTexts.Add (new TextsClass (listOfClassWithoutScripts [i].obiectWithText, listOfClassWithoutScripts [i].obiectWithText.GetComponent<Text>(),
				listOfClassWithoutScripts[i].languages));
			//}
		}
		for(int i = 0; i < listTextsInsideScript.Length; i++)
		{
			insiderText.Add(new TextInsideScripts(listTextsInsideScript[i].nameOfSht));
		}
	}
	void Update ()
	{
		if (actualIndex != indexOfLang) {
			for (int i = 0; i < attTexts.Count; i++) {
				attTexts [i].textsInsideObiect.text = attTexts [i].languages [indexOfLang];
			}
			if (textsInScripts == true) {
				for (int z = 0; z < listTextsInsideScript.Length; z++) {
					for (int j = 0; j < listTextsInsideScript [z].nameOfSht.Length; j++) {
						string[] str = new string[2];
						str [0] = listTextsInsideScript [z].nameOfSht [j].languagesScript [indexOfLang];
						str [1] = listTextsInsideScript [z].nameOfSht [j].nameOfVariable;
						obiectWithScript.SendMessage ("SetNewString", str);
						/*if (obiectWithScript.name == "BomberArea") {
						obiectWithScript.SendMessage ("SetNewString", str);
					}*/
					}
				}
			}
			actualIndex = indexOfLang;
		}
		if (Input.GetKeyDown (KeyCode.Home)) {
			if (indexOfLang == 0)
				indexOfLang = 1;
			else
				indexOfLang = 0;
		}
	}

}
[Serializable]
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
[Serializable]
public class TextInsideScripts{

	public ZmienneDoNapisow[] nameOfSht;

	public TextInsideScripts (ZmienneDoNapisow[] nazwaZmiennej)
	{
		this.nameOfSht = nazwaZmiennej;
	}
}
[Serializable]
public class ZmienneDoNapisow {
	public string nameOfVariable;
	public string [] languagesScript;

	public ZmienneDoNapisow (string name, string [] langs)
	{
		this.nameOfVariable = name;
		this.languagesScript = langs;
	}
}
