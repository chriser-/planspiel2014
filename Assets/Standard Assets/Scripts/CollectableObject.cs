using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
    private Collider coll;

	public float bananenZeit;
	public float dosenZeit;
	public float burgerZeit;

	private Texture loadingBarTextur;
	private Texture bananenTextur;
	private Texture dosenTextur;
	private Texture burgerTextur;
	// Use this for initialization
	void Start () {
		bananenZeit = 0.0f;
		dosenZeit = 0.0f;
		burgerZeit = 0.0f;
		bananenTextur =(Texture) Resources.Load("GUI/banane", typeof(Texture));
		loadingBarTextur = (Texture) Resources.Load("GUI/bar", typeof(Texture));
		dosenTextur = (Texture) Resources.Load("GUI/dose", typeof(Texture));
		burgerTextur= (Texture) Resources.Load("GUI/burger", typeof(Texture));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        coll = collider;
        Debug.Log("triggerenter: " + this.gameObject.name);
        StartCoroutine(this.gameObject.name);
        if (this.gameObject.renderer != null)
            this.gameObject.renderer.enabled = false;
        this.gameObject.collider.enabled = false;
        //Destroy(this.gameObject);
    }

    IEnumerator Banane()
    {
        Debug.Log("Bananenfunktion");
        coll.gameObject.GetComponent<CharacterMotor>().movement.canClimb = true;
		bananenZeit = 10.0f;
		for (int i = 0; i< 100; i++)
		{
        	yield return new WaitForSeconds(0.1f);
			bananenZeit -= 0.1f;
		}
		bananenZeit = 0.0f;
        coll.gameObject.GetComponent<CharacterMotor>().movement.canClimb = false;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
        
        //coll.gameObject.GetComponent<CharacterMotor>().jumping.enabled = false;
    }

    IEnumerator ColaDose() {
        Debug.Log("Colafunktion");
        coll.gameObject.GetComponent<CharacterMotor>().movement.maxForwardSpeed = 20.0f;
		dosenZeit = 10.0f;
		for (int i = 0; i< 100; i++)
		{
			yield return new WaitForSeconds(0.1f);
			dosenZeit -= 0.1f;
		}
		dosenZeit = 0.0f;

        coll.gameObject.GetComponent<CharacterMotor>().movement.maxForwardSpeed = 10.0f;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
    }

    IEnumerator Cheeseburger()
    {
        Debug.Log("Cheeseburgerfuntkion");
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = false;
        }
        coll.gameObject.GetComponent<CharacterMotor>().movement.gravity = 51.0f;
		burgerZeit = 10.0f;
		for (int i = 0; i< 100; i++)
		{
			yield return new WaitForSeconds(0.1f);
			burgerZeit -= 0.1f;
		}
		burgerZeit = 0.0f;

        coll.gameObject.GetComponent<CharacterMotor>().movement.gravity = 50.0f;
        this.gameObject.collider.enabled = true;
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = true;
        }
        
    }

	void OnGUI()
	{

		if(bananenZeit > 0.0f)
		{
			GUI.DrawTexture(new Rect(10, 10, 60, 60), bananenTextur, ScaleMode.StretchToFill, true, 10.0F);
			GUI.DrawTexture(new Rect(10,70,60*(bananenZeit/10.0f),10), loadingBarTextur, ScaleMode.StretchToFill, true, 10.0F);
		}

		if(dosenZeit > 0.0f)
		{
			GUI.DrawTexture(new Rect(80, 10, 60, 60), dosenTextur, ScaleMode.StretchToFill, true, 10.0F);
			GUI.DrawTexture(new Rect(80,70,60*(dosenZeit/10.0f),10), loadingBarTextur, ScaleMode.StretchToFill, true, 10.0F);
		}

		if(burgerZeit > 0.0f)
		{
			GUI.DrawTexture(new Rect(150, 10, 60, 60), burgerTextur, ScaleMode.StretchToFill, true, 10.0F);
			GUI.DrawTexture(new Rect(150,70,60*(burgerZeit/10.0f),10), loadingBarTextur, ScaleMode.StretchToFill, true, 10.0F);
		}

	}

}
