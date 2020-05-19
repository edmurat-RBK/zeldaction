using UnityEngine;
using System.Collections;
using Manager;


public class NavigationPoint : MonoBehaviour
{
	private const float SWITCH_RANGE = 1f;

	public bool isCheckpoint = true;
    [HideInInspector]
    public bool isExit = false;
	public int iD = 0;
    [HideInInspector]
	public string exitToScene;
    [HideInInspector]
    public int entryPointID = 0;
    [HideInInspector]
    public bool mustPCFaceLeft;
    [SerializeField]
    private Sprite newSprite;
    private Animator anim;

	/// <summary>
	/// true when the PC has been some distance away from it (to avoid trigger when the PC just spawns in it)
	/// </summary>
	bool isOn;

	static public event System.Action<NavigationPoint> OnTriggered;


	void Awake()
	{
		isOn = false;
	}


	void Start()
	{
		//if this nav point is only an entry point, once PCPositioner has positionned the PC (i.e. before the Start() method of NavigationPoint executes), we can deactivate the object
		if (!isCheckpoint && !isExit)
		{
			gameObject.SetActive(false);
		}

        anim = GetComponent<Animator>();
    }


	void Update()
	{
		if (!isOn)
		{
			maySwitchOn();
		}

        if (SvgManager.SvgData.currCheckpointID == this.iD)
        {
            anim.SetBool("IsSaved", true);
            anim.SetBool("IsReset", false);
        }
        else
        {
            anim.SetBool("IsSaved", false);
            anim.SetBool("IsReset", true);
        }

    }


	/// <summary>
	/// Switch ON if PC is far enough from it
	/// </summary>
	void maySwitchOn()
	{
		Vector2 d = transform.position - PlayerManager.Instance.transform.position;
		if (d.sqrMagnitude > SWITCH_RANGE * SWITCH_RANGE)
		{
			isOn = true;
		}
	}


	void OnTriggerEnter2D(Collider2D otherColl)
	{
		if (!isOn)
			return;

		if (otherColl.gameObject.tag == "Player")
		{
			if (OnTriggered != null)
				OnTriggered(this);

			//if it is a checkpoint we switch it off till the PC moves away from it, to avoid saving it repeatedly
			if (!isCheckpoint)
				isOn = false;

            otherColl.GetComponent<PlayerHealth>().health = otherColl.GetComponent<PlayerHealth>().maximumHealth;
            otherColl.GetComponent<HealthBar>().HealthSysteme();
            anim.SetBool("IsSaved", true);
            GetComponent<SpriteRenderer>().sprite = newSprite;

        }
	}

}
