using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AttendanceScript
{
	public Camera eventCamera;
	public bool switchCamera;
	public GameObject eventTrigger;
	public GameObject msgZeCanvas;
	public bool canvasView;
	obslObj obslObj = new obslObj ();
	public int lengthOfTab;
	public obslObj [] AttendanceItems = new obslObj[10];
	public bool czyRbEnbl;
	public bool czySieWylacza;

}
[Serializable]
public class obslObj 
{
	public GameObject items;
	public bool isStone;
	public Rigidbody rbItem;
	public AudioSource audioSource;

}
