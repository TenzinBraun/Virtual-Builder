using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
//using UnityExtension;

public class ModelImporter{
    public string FTPID = "user";
    public string FTPPassword = "user";
	public string serverIP = "192.168.43.107";
    public string scriptPath = "C:\\Users\\God builder\\Desktop\\Client Serveur\\CLIENTSERVEUR\\script.txt"; //path of the "script.txt" file, which is used to run the ftp script
	public string whereToRegister = "C:\\Users\\God builder\\Desktop\\Client Serveur\\CLIENTSERVEUR\\ClientModels";

    /** Imports a .obj file from the server defined in attributes, and inserts it in game
	 * @params filePathInServerDirectory : The path of the file you want to import (from the server directory).
	 * @return the object created
	 */
    public GameObject ImportModel(string filePathInServerDirectory)
	{
		ImportViaFTP (filePathInServerDirectory);
        float timeAtLaunch = Time.realtimeSinceStartup;
		while (!File.Exists (whereToRegister + "\\" + filePathInServerDirectory ) && Time.realtimeSinceStartup - timeAtLaunch < 10) {}
        return ImportObj (filePathInServerDirectory);
	}


	private GameObject ImportObj(string fileName)
	{
        System.IO.StreamReader streamReader = new System.IO.StreamReader("C:\\Users\\God builder\\Desktop\\Client Serveur\\CLIENTSERVEUR\\ClientModels\\" + fileName);
        string c = streamReader.ReadToEnd();
        GameObject toReturn = ObjImporter.Import(c);
		toReturn.transform.position.Set (0, 0, 0);
        toReturn.name = fileName;
        toReturn.transform.localScale *= 0.001f;

		return toReturn;
	}

    private void setObjectNameInScriptFile(string objectName)
    {
        string[] toWrite = {
            "@echo off",
            "open",
            serverIP,
            FTPID,
            FTPPassword,
            "get",
            objectName,
            "\""+whereToRegister+"\\" + objectName+"\"",
            "quit"};
        System.IO.File.WriteAllLines(scriptPath, toWrite);
    }
		
	private void ImportViaFTP (string fileName) {
        setObjectNameInScriptFile(fileName);

		Process fileTransferProcess = new Process();
		fileTransferProcess.StartInfo.FileName = "ftp";
		fileTransferProcess.StartInfo.Arguments = "-s:\"" + scriptPath + "\"";
        Process.Start (fileTransferProcess.StartInfo);

    }

}

