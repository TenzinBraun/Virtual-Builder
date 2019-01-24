using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
//using UnityExtension;

public class ModelImporter{
    public string FTPID = "projetvr";
    public string FTPPassword = "9tUv55WX.";
	public string serverIP = "iutbg-lacielp.univ-lyon1.fr";
    public string scriptPath = "C:\\Users\\God builder\\Desktop\\Client Serveur\\CLIENTSERVEUR\\script.txt"; //path of the "script.txt" file, which is used to run the ftp script
	public string whereToRegister = "C:\\Users\\God builder\\Desktop\\Client Serveur\\CLIENTSERVEUR\\ClientModels";

    /** Imports a .obj file from the server defined in attributes, and inserts it in game
	 * @params filePathInServerDirectory : The path of the file you want to import (from the server directory).
	 * @return the object created
	 */
    public GameObject ImportModel(string filePathInServerDirectory)
	{
        ImportViaFTP ("\\projetvr\\" + filePathInServerDirectory);
        float timeAtLaunch = Time.realtimeSinceStartup;
        while (!File.Exists (whereToRegister + "\\" + filePathInServerDirectory ) && Time.realtimeSinceStartup - timeAtLaunch < 10) {}
        return ImportObj (filePathInServerDirectory);
	}


	private GameObject ImportObj(string fileName)
	{
        System.IO.StreamReader streamReader = new System.IO.StreamReader("C:\\Users\\God builder\\Desktop\\Client Serveur\\CLIENTSERVEUR\\ClientModels\\" + fileName);
        string c = streamReader.ReadToEnd();
        UnityEngine.Debug.Log(c);
        GameObject toReturn = ObjImporter.Import(c);
        toReturn.name = fileName;

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
            "\""+whereToRegister/*+"\\"*/ + objectName+"\"",
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

