using System;
using System.Collections.Generic;


/// <summary>
/// Type that holds data that are serialized and stored in PlayerPrefs
/// </summary>
[Serializable]
public class SvgSerializableData
{
	public string currentSceneName;
	public int currCheckpointID;
	/// <summary>
	/// List and status of upgrades as saved.
	/// WARNING: do not use to retrieve current status, use UpgradesManager.List instead!
	/// </summary>
	public List<SvgSerializableUpgrade> upgrades;


	//Constructor
	public SvgSerializableData() { }


	/// <summary>
	/// Recreate the List SvgData.upgrades with the list of upgrades in memory (i.e. UpgradesManager.List)
	/// </summary>
	public void GenerateSvgUpgsList()
	{
		upgrades = new List<SvgSerializableUpgrade>();
		foreach (KeyValuePair<string, bool> entry in UpgradesManager.List)
		{
			upgrades.Add(new SvgSerializableUpgrade(entry.Key, entry.Value));
		}
	}
}


[Serializable]
public class SvgSerializableUpgrade
{
	public string name;
	public bool status;


	//Constructor 1
	public SvgSerializableUpgrade() { }


	//Constructor 2
	public SvgSerializableUpgrade(string myName, bool myStatus)
	{
		name = myName;
		status = myStatus;
	}
}

