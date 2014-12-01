using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SimpleIK : MonoBehaviour {
	//number of iterations to solve IK
	public int iterations = 5;

	//slows the rotation per iteration
	[Range(0.01f, 1)]
	public float dampening = 1;

	//Location to try to place the endTransform at
	public Transform target;

	//The transform that represents the end of the final limb
	public Transform endTransform;

	//The limits of rotation for the intermediate limbs
	public Node[] angleLimits = new Node[0];

	//Dictionary for quick access later
	Dictionary<Transform, Node> nodes;

	//Serializable so that it's usable before hitting play. Stores the angle limits for a given tranform
	[System.Serializable]
	public class Node{
		public Transform transform;
		public float min;
		public float max;
	}

	//Does stuff
	void OnValidate(){
		foreach (Node n in angleLimits) {
			n.min = ClampAngle (n.min, -360, 360);
			n.max = ClampAngle (n.max, -360, 360);
		}
	}

	// Set up the quick-access dictionary
	void Start () {
		nodes = new Dictionary<Transform, Node> (angleLimits.Length);
		foreach (Node n in angleLimits) {
			nodes[n.transform] = n;
		}
	}
	//run IK
	void LateUpdate () 
	{
		if (!Application.isPlaying) {
			Start ();
		}
		
		if (target == null || endTransform == null) {
			return;
		}
		
		for (int i = 0; i < iterations; ++i) {
			CalculateIK ();
		}
	}
	//Calculate IK
	void CalculateIK(){
		Transform n = endTransform.parent;
		
		while (true) {
			RotateTowardsTarget(n);

			if (n == gameObject.transform){
				break;
			}
			n = n.parent;
		}
	}
	//Rotates the given transform towards the target
	void RotateTowardsTarget( Transform t ){
		Vector2 toTarget = target.position - t.position;
		Vector2 toEnd = endTransform.position - t.position;
		
		float angle = SignedAngle(toEnd, toTarget);
		
		angle *= dampening;
		
		angle = t.eulerAngles.z - angle;

		if (nodes.ContainsKey (t)) {
			Node n = nodes[t];
			float parentRotation = t.parent ? t.parent.eulerAngles.z : 0;
			angle -= parentRotation;
			angle = ClampAngle (angle, n.min, n.max);
			angle += parentRotation;

		}
		//ClampAngle (angle);

		t.rotation = Quaternion.Euler (0, 0, angle);
	}
	
	public static float SignedAngle(Vector3 a, Vector3 b){
		float angle = Vector3.Angle(a, b);
		//float sign = Mathf.Sign (Vector3.Dot(Vector3.back,Vector3.Cross (a, b)));
		float sign = Mathf.Sign (Vector3.Dot(Vector3.back,Vector3.Cross (a, b)));
		return angle * sign;
	}
	
	float ClampAngle(float angle, float min, float max){
		angle = Mathf.Abs((angle % 360) + 360) % 360;
		//angle = angle % 360;
		angle = Mathf.Clamp(angle, min, max);
		return angle;
	}
}/**/
/*
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SimpleIK : MonoBehaviour
{
	public int iterations = 5;
	
	[Range(0.01f, 1)]
	public float damping = 1;
	
	public Transform target;
	public Transform endTransform;
	
	public Node[] angleLimits = new Node[0];
	
	Dictionary<Transform, Node> nodeCache; 
	[System.Serializable]
	public class Node
	{
		public Transform transform;
		public float min;
		public float max;
	}
	
	void OnValidate()
	{
		// min & max has to be between 0 ... 360
		foreach (var node in angleLimits)
		{
			node.min = Mathf.Clamp (node.min, 0, 360);
			node.max = Mathf.Clamp (node.max, 0, 360);
		}
	}
	
	void Start()
	{
		// Cache optimization
		nodeCache = new Dictionary<Transform, Node>(angleLimits.Length);
		foreach (var node in angleLimits)
			if (!nodeCache.ContainsKey(node.transform))
				nodeCache.Add(node.transform, node);
	}
	
	void LateUpdate()
	{
		if (!Application.isPlaying)
			Start();
		
		if (target == null || endTransform == null)
			return;
		
		int i = 0;
		
		while (i < iterations)
		{
			CalculateIK ();
			i++;
		}
		
		endTransform.rotation = target.rotation;
	}
	
	void CalculateIK()
	{		
		Transform node = endTransform.parent;
		
		while (true)
		{
			RotateTowardsTarget (node);
			
			if (node == transform)
				break;
			
			node = node.parent;
		}
	}
	
	void RotateTowardsTarget(Transform transform)
	{		
		Vector2 toTarget = target.position - transform.position;
		Vector2 toEnd = endTransform.position - transform.position;
		
		// Calculate how much we should rotate to get to the target
		float angle = SignedAngle(toEnd, toTarget);
		
		// Flip sign if character is turned around
		angle *= Mathf.Sign(transform.root.localScale.x);
		
		// "Slows" down the IK solving
		angle *= damping; 
		
		// Wanted angle for rotation
		angle = -(angle - transform.eulerAngles.z);
		
		// Take care of angle limits 
		if (nodeCache.ContainsKey(transform))
		{
			// Clamp angle in local space
			var node = nodeCache[transform];
			float parentRotation = transform.parent ? transform.parent.eulerAngles.z : 0;
			angle -= parentRotation;
			angle = ClampAngle(angle, node.min, node.max);
			angle += parentRotation;
		}
		
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
	
	public static float SignedAngle (Vector3 a, Vector3 b)
	{
		float angle = Vector3.Angle (a, b);
		float sign = Mathf.Sign (Vector3.Dot (Vector3.back, Vector3.Cross (a, b)));
		
		return angle * sign;
	}
	
	float ClampAngle (float angle, float min, float max)
	{
		angle = Mathf.Abs((angle % 360) + 360) % 360;
		return Mathf.Clamp(angle, min, max);
	}
} /**/



