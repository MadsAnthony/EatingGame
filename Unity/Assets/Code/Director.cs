using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	public static Director Instance;
	
	void Awake() {
		Instance = this;
		Cursor.visible = false;
	}

	public CustomCursor CustomCursor;
	public SoundsDatabase Sounds;
	public LevelCode Level;
	public Hero Hero;
}
