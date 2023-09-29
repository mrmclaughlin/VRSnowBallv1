using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;
 
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
	public Transform head;
	public Transform leftHand;
	public Transform rightHand;
	private PhotonView photonView;
	
		public int xMin =-10;
		public int yMin=-10;
		public int zMin=-10;
		public int xMax=10;
		public int yMax=10;
		public int zMax=10;
		private Transform headRig;
		private Transform leftHandRig;
		private Transform rightHandRig;
		private GameObject GameObjectRig;
		
    // Start is called before the first frame update
    void Start()
    {
		
		Vector3 offset = new Vector3( -2, 0,   2);
		Vector3 offset2 = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax),  Random.Range(zMin, zMax));
		
        photonView = GetComponent<PhotonView>();
		 XROrigin  rig = FindObjectOfType<XROrigin>();
		  
		
	 
		 headRig = GameObject.Find("XR Rig/Camera Offset/Main Camera").transform;
		leftHandRig = GameObject.Find("XR Rig/Camera Offset/LeftHand Controller").transform;
		rightHandRig = GameObject.Find("XR Rig/Camera Offset/RightHand Controller").transform;
		
		
		
	Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
       
	   GameObject NetworkPlayerRig = GameObject.Find("NetworkPlayer"+photonView.ViewID); // Replace with your XR origin's name
	   
	   Debug.Log(NetworkPlayerRig);
	   //NetworkPlayerRig.transform.position += offset;
	    
	   
	  
			
    }

    // Update is called once per frame
    void Update()
    {
		if (photonView.IsMine)
		{
			rightHand.gameObject.SetActive(false);
			leftHand.gameObject.SetActive(false);
			head.gameObject.SetActive(false);
			MapPosition(head, headRig);
			MapPosition(leftHand,leftHandRig);
			MapPosition(rightHand,rightHandRig);
			GameObject NetworkPlayer = photonView.gameObject;
			NetworkPlayer.name = "NetworkPlayer" + photonView.ViewID;
			
		}
        
    }
	void MapPosition(Transform target,Transform rigTransform)
	{
		target.position = rigTransform.position;
		target.rotation = rigTransform.rotation;
	}
}
