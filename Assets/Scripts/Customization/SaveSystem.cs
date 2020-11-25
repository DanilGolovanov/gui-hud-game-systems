using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	public static void SavePlayer(Player player)
	{
		//initialize binary formatter which converts data to machine code
		BinaryFormatter formatter = new BinaryFormatter();
		//create a path for save file in the project directory
		string path = Application.dataPath + "/player.sav";
		//create file stream to save info char by char to the file
		FileStream stream = new FileStream(path, FileMode.Create);
		//get the data
		PlayerData data = new PlayerData(player);
		//convert data to machine code
		formatter.Serialize(stream, data);
		//close the stream after stopping transferring the data
		stream.Close();
	}

	public static PlayerData LoadPlayer()
	{
		string path = Application.dataPath + "/player.sav";

		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file was not found in " + path);
			return null;
		}
	}
}
