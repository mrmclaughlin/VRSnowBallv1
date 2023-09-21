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
	private GameObject spawnedSnowballPilePrefab;
	public GameObject snowballPrefab;  // Drag your snowball prefab here
    public int numberOfSnowballs = 50; // Number of snowballs in the pile
    public float areaRadius = 10f;      // Radius of the pile
	public int rows = 5;
    public int columns = 5;
    public int height = 5;
    public float spacing = 2;
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
		if (rightHandRig != null)
        {
            Debug.Log("rightHandRig found: " + rightHandRig.gameObject.name);
        }
        else
        {
            Debug.LogWarning("No rghtHandRig found in the scene.");
        }
		
		spawnedSnowballPilePrefab = PhotonNetwork.Instantiate("SnowballPile",transform.position,transform.rotation);
		spawnedSnowballPilePrefab.transform.position += offset;
		
	Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
       
	   GameObject NetworkPlayerRig = GameObject.Find("NetworkPlayer"+photonView.ViewID); // Replace with your XR origin's name
	   
	   Debug.Log(NetworkPlayerRig);
	   //NetworkPlayerRig.transform.position += offset;
	    for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                for (int z = 0; z < columns; z++)
                {
                    Vector3 position = new Vector3(
                        x * spacing,
                        y * spacing,
                        z * spacing
                    );
					position += offset;
	 		 
			GameObject aSnowball =Instantiate(snowballPrefab, position + transform.position, Quaternion.identity);
			aSnowball.name = "snowball" + photonView.ViewID; 
			Renderer renderer = aSnowball.GetComponent<Renderer>();
            aSnowball.transform.SetParent(spawnedSnowballPilePrefab.transform);
			if (renderer != null)
            {
                renderer.material.color = randomColor;
            }
			
                }
            }
		}
	   
	   /*
	   
    
        for (int i = 0; i < numberOfSnowballs; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaRadius, areaRadius),
                Random.Range(0, areaRadius),
                Random.Range(-areaRadius, areaRadius)
            );

            GameObject aSnowball = Instantiate(snowballPrefab, randomPosition + transform.position, Quaternion.identity);
			aSnowball.name = "snowball" + i;

		 Renderer rend = aSnowball.GetComponent<Renderer>();
        // Change the material color to red
		 rend.material.color = randomColor;
		 randomColor.a = 0.5f;
			aSnowball.transform.SetParent(spawnedSnowballPilePrefab.transform);
        }
		*/
			
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
			//MapPosition(rightHand,rightHandRig);
			GameObject NetworkPlayer = photonView.gameObject;
			NetworkPlayer.name = "NetworkPlayer" + photonView.ViewID;
			spawnedSnowballPilePrefab.name ="SnowBallPile" + photonView.ViewID;
			spawnedSnowballPilePrefab.transform.SetParent(NetworkPlayer.transform);
    
		}
        
    }
	void MapPosition(Transform target,Transform rigTransform)
	{
		//InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
		//InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
		target.position = rigTransform.position;
		target.rotation = rigTransform.rotation;
	}
}
