using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {
	bool topMouthHits;
	bool bottomMouthHits;
	Mesh mesh;
	float speed = 6;
	Vector3 upperMouthPos;
	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		GetComponent<MeshRenderer> ().material = new Material(GetComponent<MeshRenderer> ().material);
		if (Random.Range (0, 10) > 7) {
			Texture text = (Texture)Resources.Load ("IceCream2");
			GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", text);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.left * Time.deltaTime*speed;
		if (topMouthHits && bottomMouthHits && speed>0) {
			//float scaleFactor = 0.2f;
			float scaleFactor = 0.5f+(transform.position.x-upperMouthPos.x)/transform.localScale.x;
			//Destroy(gameObject);
			mesh = GetComponent<MeshFilter>().mesh;
			var verts = mesh.vertices;
			List<Vector3> vertList = new List<Vector3>(verts);
			vertList.Sort((v1,v2)=>v1.x.CompareTo(v2.x));
			var secondLeftX = vertList[1].x;
			for(int i = 0; i < verts.Length; i++) {
				if (verts[i].x<secondLeftX+0.01f) {
					verts[i] = new Vector3(0.5f-scaleFactor,verts[i].y,verts[i].z);
				}
			}
			//Mesh neMesh = (Mesh) Instantiate(mesh);
			GetComponent<BoxCollider>().center = new Vector3(Mathf.Abs (0.5f-scaleFactor/2),GetComponent<BoxCollider>().center.y,GetComponent<BoxCollider>().center.z);
			GetComponent<BoxCollider>().size = new Vector3(scaleFactor,GetComponent<BoxCollider>().center.y,GetComponent<BoxCollider>().center.z);
			GetComponent<Rigidbody>().useGravity = true;
			Director.Instance.Level.score += 1-scaleFactor;
			mesh.vertices = verts;
			speed = -6;
			if (scaleFactor<0.9f) {
				Director.Instance.Sounds.PlaySound(Director.Instance.Sounds.eatSound);
			}
			topMouthHits = false;
			bottomMouthHits = false;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "UpperMouthInner")
		{
			upperMouthPos = col.gameObject.GetComponentInParent<Hero>().upperMouthPoint.position;
			topMouthHits = true;
		}
		if(col.gameObject.name == "LowerMouthInner")
		{
			bottomMouthHits = true;
		}
	}
	void OnCollisionExit (Collision col)
	{
		if(col.gameObject.name == "UpperMouthInner")
		{
			topMouthHits = false;
		}
		if(col.gameObject.name == "LowerMouthInner")
		{
			bottomMouthHits = false;
		}
	}
}
