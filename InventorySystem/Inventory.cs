using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	public SortedDictionary<GameObject,int> InvDictionary = new SortedDictionary<GameObject,int>();

	public GameObject TestObject;

	private bool InventoryOpen = false;
	private int SortType = 0;
	private string[] SortTypeList = new string[] {"Name","Quantity"};
	private string SortedType = "null";

	public int SelectedItem;

	void Start () {

	}

	void Update () {
		if (Input.GetButtonDown("Inventory") && InventoryOpen) {
			InventoryOpen = false;
		}
		else if (Input.GetButtonDown("Inventory") && !InventoryOpen) {
			InventoryOpen = true;
		}
	}

	public void AddRemoveItem(GameObject ObjKey, int ObjAmount) {
		InvDictionary.Add(ObjKey,ObjAmount);
	}

	void OnGUI() {
		if (InventoryOpen) {
			GUI.BeginGroup(new Rect(Screen.width/10,
			                             Screen.height/10,
			                             (Screen.width/10)*3,
			                    		(Screen.height/10)*8));
			GUI.Box(new Rect(0,0,(Screen.width/10)*3,(Screen.height/10)*8),"Inventory");
			GUI.Label(new Rect(32,32,64,24), "Sort by:");
			SortType = GUI.Toolbar(new Rect(96,32,128,24), SortType,SortTypeList);
			if (InvDictionary.Count > 0) {
				GameObject[] objs = new GameObject[InvDictionary.Count];
				if (SortType == 0 && SortedType != "key") {
					//Items ordered by name
					InvDictionary.OrderBy(key => key.Key);
					SortedType = "key";
					Debug.Log("Sorting by name");
				}
				else if (SortType == 1 && SortedType != "value") {
					//Items ordered by quantity
					InvDictionary.OrderBy(key => key.Value);
					SortedType = "value";
					Debug.Log("Sorting by value");
				}
				InvDictionary.Keys.CopyTo(objs,0);
				string[] ObjsString = objs.OfType<GameObject>().Select(o => o.ToString()).ToArray();
				SelectedItem = GUI.SelectionGrid(new Rect(16,64,360,64),SelectedItem,ObjsString,5);
			}
			GUI.EndGroup();
		}
	}
}
