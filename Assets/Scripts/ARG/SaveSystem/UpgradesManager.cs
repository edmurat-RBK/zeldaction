using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Class that contains the list of upgrades and their respective status (unlocked or not), and sends an event when an upgrade has just been unlocked
/// </summary>
public class UpgradesManager : MonoBehaviour
{
	static public event System.Action<string> OnUnlocked;


	static public Dictionary<string, bool> List = new Dictionary<string, bool>()
	{
		//true if unlocked
		{ "as bucket", false },
		{ "finishShaman", false },
		{ "vache", false },
		{ "milieu", false },
		{ "volcan", false },
		//{ "life 2", false },
  //      { "mana 2", false },
		//{ "teleport", false },
		//{ "fireball 2", false },
  //      { "life 3", false }
	};


	/// <summary>
	/// Overwrite List with the saved values
	/// </summary>
	/// <param name="svgList">The saved upgrade list passed as parameter</param>
	static public void UpdateFromSavedData(List<SvgSerializableUpgrade> svgList)
	{
		List = new Dictionary<string, bool>();

		foreach (SvgSerializableUpgrade upg in svgList)
		{
			List.Add(upg.name, upg.status);
		}
	}


	//Don't use this method directly in your code, UpgradeObject handles it
	static public void DoOnUpgradePicked(string upgradeName)
	{
		if (!List.ContainsKey(upgradeName))
		{
			Debug.LogErrorFormat("<color=red>ERROR: upgrade name '" + upgradeName + "' doesn't correspond to any key in UpgradesManage.List</color>");//DEBUG
			return;
		}

		List[upgradeName] = true;

		if (OnUnlocked != null)
			OnUnlocked(upgradeName);
	}
}
