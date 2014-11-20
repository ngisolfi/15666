using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class payloadBar : MonoBehaviour {
	[HideInInspector]
	public List<GUIQuad> quads;

	public Material beryllium;
	public Material boron;
	public Material deuterium;
	public Material helium;
	public Material lithium;
	public Material tritium;
	private TextMesh payloadText;
	private Light payloadLight;

	public float tractor_beam_distance = 0.0f;

	public int max_payload = 0;
	private int tot_length = 0;

	public float min_image_space = 0.0f;
	public float fraction_of_width = 0.0f;
	private float total_width = 0.0f;
	private bool lightIncreasing=true;

	private ShipCapacity capacityHandler;

	// Use this for initialization
	void Start () {
		capacityHandler = gameObject.GetComponent<ShipCapacity>();
		quads = new List<GUIQuad> ();
		payloadText = GameObject.Find ("payloadPercent").GetComponent<TextMesh>();
		payloadLight = GameObject.Find ("payloadLight").GetComponent<Light> ();
		// Set width that this GUI should take up on the screen
		total_width = (float)Screen.width * fraction_of_width;
	}

	void addNewQuad(string element)
	{
		GUIQuad q = new GUIQuad (element, elementToMaterial(element));
		quads.Add (q);

		int count = quads.Count - 1;

		// Set the position and scale of the new quad
		float imspace_x = min_image_space + (float)tot_length / (float)max_payload * total_width;
		quads [count].setPosition (imspace_x);
	}

	void FixedUpdate(){

		if (tot_length >= max_payload) {
			if(payloadLight.intensity<=0f){
				lightIncreasing=true;
			} else if (payloadLight.intensity>=4f){
				lightIncreasing=false;
			}
			
			if(lightIncreasing){
				payloadLight.intensity+=.2f;
			} else {
				payloadLight.intensity-=.2f;
			}
			
			return;
		}
	}

	void updateQuadPositions()
	{
//		float start = 0.0;
//		foreach(GUIQuad quad in quads){
//
//		}
	}

	public void collectResource(string element, int delta_len)
	{

		Debug.Log ("payload: " + tot_length.ToString ());

		capacityHandler.depositOre(element,delta_len);

		// Check if we are already overburdened
//		if (tot_length >= max_payload)
//			return;

//		float imspace_dx =  (float)delta_len / (float)max_payload * total_width;
//		
//		bool set = false;
//		float length_so_far = 0;
//		for (int ii = 0; ii < quads.Count; ++ii) 
//		{
//			length_so_far += quads[ii].getLength();
//
//			// Iterate through the list until we find the right color
//			if (element.ToUpper().CompareTo(quads[ii].getElement ()) == 0)
//			{
//				quads[ii].addLength(imspace_dx);
//				set = true;
//			}
//
//			else
//			{
//				// If iterator is before the specified quad, ignore and continue
//				if (!set)
//					continue;
//
//				// Otherwise, update elements in the list after specified quad with
//				// updated length
//				else
//				{
//					quads[ii].addDeltaPosition(imspace_dx);
//				}
//			}
//		}

		tot_length += delta_len;

//		int print = Mathf.FloorToInt((float)tot_length / (float)max_payload * 100.0f);
		int print = capacityHandler.percentFull();
		payloadText.text = "("+print.ToString()+"%)";

	}

	Material elementToMaterial(string element)
	{
		if (element.ToUpper ().CompareTo ("BERYLLIUM") == 0) 
		{
			return beryllium;
		}
		if (element.ToUpper ().CompareTo ("BORON") == 0) 
		{
			return boron;
		}
		if (element.ToUpper ().CompareTo ("DEUTERIUM") == 0) 
		{
			return deuterium;
		}
		if (element.ToUpper ().CompareTo ("HELIUM") == 0) 
		{
			return helium;
		}
		if (element.ToUpper ().CompareTo ("LITHIUM") == 0) 
		{
			return lithium;
		}
		if (element.ToUpper ().CompareTo ("TRITIUM") == 0) 
		{
			return tritium;
		}

		return null;
	}

	// When an on trigger event is pressed, check if
	// we need to make a new quad for the specified element
	public void enteredAtmosphere(string e)
	{
		bool exists = false;
		for (int ii = 0; ii < quads.Count; ++ii) 
		{
			if (e.ToUpper ().CompareTo (quads [ii].getElement ().ToUpper ()) == 0)
			{
				exists = true;
				break;
			}
		}

		if (!exists)
			addNewQuad (e);
	}
}

public class GUIQuad {

	private string element;
	private GameObject quad;
	private float length = 0.0f;
	private Material material;

	public GUIQuad(string e, Material m)
	{
		length = 0.0f;
		element = e.ToUpper();
		material = m;

		// Instantiate the quad for rendering
		quad = GameObject.CreatePrimitive (PrimitiveType.Quad);

		// Set the color of the new quad based on its material
		quad.GetComponent<MeshRenderer> ().material = material;

		// Set the quad to be in the GUI rendering layer
		quad.layer = 21;

		// Fix this once we begin snapping to screen size
		quad.transform.position = new Vector3 (.6f, -0.9f, 0.0f);
		quad.transform.localScale = new Vector3 (0.0f, 0.1f, 0.001f);
		addLength (0.0f);
	}

	public void setPosition(float x_abs)
	{
		Vector3 p = quad.transform.position;
		quad.transform.position = new Vector3(x_abs, p.y, p.z);
	}

	public void addDeltaPosition(float x_del)
	{
		Vector3 p = quad.transform.position;
		quad.transform.position = new Vector3(p.x + x_del, p.y, p.z);
	}

	public void addLength(float x_len)
	{
		length += x_len;
		Vector3 p = quad.transform.position;
		Vector3 s = quad.transform.localScale;

		quad.transform.position = new Vector3 (p.x + x_len / 2.0f, p.y, p.z);
		quad.transform.localScale = new Vector3 (s.x + x_len, s.y, s.z);
	}
	
	public float getLength()
	{
		return length;
	}

	public string getElement()
	{
		return element;
	}
}
