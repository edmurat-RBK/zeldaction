using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Reflection;


/// <summary>
/// Handles savegame data, and related methods to load, update, and save it
/// </summary>
public class SvgManager : MonoBehaviour
{
	//constants
	const string SavedGameName = "F2MV2018New";

	static public SvgSerializableData SvgData; //contains saved data


	void Awake()
	{
		LoadSvgData();
	}


	void OnEnable()
	{
		NavigationPoint.OnTriggered += HandleOnNavTrigger;
		UpgradesManager.OnUnlocked += HandleOnUpgradeUnlocked;
	}


	void OnDisable()
	{
		NavigationPoint.OnTriggered -= HandleOnNavTrigger;
		UpgradesManager.OnUnlocked -= HandleOnUpgradeUnlocked;
	}


	void HandleOnNavTrigger(NavigationPoint navTrigger)
	{
		//if the navigation trigger is an exit, we save next scene's name as well as the corresponding entry point ID
		if (navTrigger.isExit)
		{
			UpdateSpawnpoint(navTrigger.exitToScene, navTrigger.entryPointID);
		}
		//if the navigation trigger is a checkpoint, we save its ID and the currently loaded scene's name
		else if (navTrigger.isCheckpoint)
		{
			UpdateSpawnpoint(SceneManager.GetActiveScene().name, navTrigger.iD);
		}
		//updateHPs((int)PCHealthManager.Me.hp);
	}


	void HandleOnUpgradeUnlocked(string upgradeName)
	{
		UpdateUpgradeList();
	}


	/// <summary>
	/// Initializes SvgManager.SvgData with serialized entry from PlayerPrefs (or create a blank instance if no saved game yet)
	/// </summary>
	private void LoadSvgData()
	{
		if (PlayerPrefs.HasKey(SavedGameName))
		{
			SvgData = JsonUtility.FromJson<SvgSerializableData>(PlayerPrefs.GetString(SavedGameName));
			//We overwrite the Upgrades list that is used in the game, with the values of the saved data
			UpgradesManager.UpdateFromSavedData(SvgData.upgrades);

			//TEST
#if UNITY_EDITOR
			print("Savegame read from PlayerPrefs: Current scene name = " + SvgData.currentSceneName
						+ " | Checkpoint ID = " + SvgData.currCheckpointID); //TEST
			DisplayUnserializedDEBUG(); //TEST
#endif
		}
		else
		{
			print("No savegame => creating a new SvgData in memory and using Upgrades.List currently in memory.");//TEST
			GenerateNewSvgData(); //As there is no savegame we create a new SvgData (and we fill its upgrades member with Upgrades.List) 
		}
	}


	void GenerateNewSvgData()
	{
		SvgData = new SvgSerializableData();
		//We initialize a new upgrade list to be saved, with Upgrades.List currently in memory
		SvgData.GenerateSvgUpgsList();
	}


	void UpdateSpawnpoint(string sceneName, int checkpointID)
	{
		SvgData.currentSceneName = sceneName;
		SvgData.currCheckpointID = checkpointID;
        SaveSvgData();
	}


	void UpdateUpgradeList()
	{
		SvgData.GenerateSvgUpgsList();

		SaveSvgData();
	}


	/// <summary>
	/// Serializes and saves SvgData to PlayerPrefs
	/// </summary>
	void SaveSvgData()
	{
		//Serializing svgData and saving it to PlayerPrefs' key
		string serializedSvgData = JsonUtility.ToJson(SvgData);
		print("Saved string: " + serializedSvgData);//TEST
		PlayerPrefs.SetString(SavedGameName, serializedSvgData);

		//Saving to disk
		PlayerPrefs.Save();
	}


#if UNITY_EDITOR

	//DEBUG
	[UnityEditor.MenuItem("IT Debug Tools/Display savegame data as currently in memory [!RUNTIME ONLY]")]
	static void DisplayUnserializedDEBUG()
	{
		FieldInfo[] fields = SvgData.GetType().GetFields(/*BindingFlags.Public | BindingFlags.Instance*/);
		string str = "SvgData fields as in memory: ";
		foreach (FieldInfo a in fields)
		{
			str += "\nvar " + a.ToString() + ", value = " + a.GetValue(SvgData);
		}
		//special case for 'upgrades' as it's not recognized by GetFields
		str += "\nupgrades: ";
		foreach (SvgSerializableUpgrade upg in SvgData.upgrades)
		{
			str += "\n   name: " + upg.name + ", status: " + upg.status;
		}
		print(str);
	}


	//DEBUG
	[UnityEditor.MenuItem("IT Debug Tools/Display saved data serialized in PlayerPrefs")]
	static void DisplaySerializedDEBUG()
	{
		string str = "We display PlayerPrefs' " + SavedGameName + " data:";
		str += PlayerPrefs.HasKey(SavedGameName) ? PlayerPrefs.GetString(SavedGameName) : " ! NO SAVED DATA IN PLAYERPREFS !";
		print(str);
	}


	//DEBUG
	[UnityEditor.MenuItem("IT Debug Tools/Delete savegame data [!WARNING!]")]
	static void DeleteSavegameDEBUG()
	{
		PlayerPrefs.DeleteKey(SavedGameName);
		print("Deleted " + SavedGameName + " key in PlayerPrefs!!!");
	}

    private void Update()
    {
        if (Input.GetKeyDown("L"))
        {
            DeleteSavegameDEBUG();
        }
    }

#endif

}