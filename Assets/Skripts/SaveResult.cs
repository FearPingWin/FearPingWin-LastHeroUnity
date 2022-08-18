using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


public static  class SaveResult

{   //helper function for testing, delete file
    public static void deleteFile(string pathFile){
    File.Delete (Application.persistentDataPath + pathFile);
    }
    /*writing a List from the results to a file in binaron form in topresults.lh
    data - List with result 
    */
    public static void saveResult(List<ResultData> data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/topresults.lh";
        FileStream stream = new FileStream(path, FileMode.Create);
        List<ResultData> saveData= new List<ResultData>(data);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }
    /*loading data from topresults.lh
    returns a List of values ResultData, if the file exists
    */
    public static List<ResultData> loadResults(){
        string path = Application.persistentDataPath + "/topresults.lh";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<ResultData> loadData= formatter.Deserialize(stream) as List<ResultData>;
            stream.Close();
            return loadData;
        }else {
            return null;
        }
    }
    /*saving settings in config.lh
    fieldSide-game board width
    roundTime-round time in seconds
    movementSpeed-enemy tank speed
    nrShots-the number of shots per second of an enemy tank
    projectileSpeed-enemy tank shot speed
    */
    public static void saveConfig(int fieldSide, int roundTime, float movementSpeed,int nrShots,int projectileSpeed){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/config.lh";
        FileStream stream = new FileStream(path, FileMode.Create);
        Config saveData= new Config(fieldSide,roundTime,movementSpeed,nrShots,projectileSpeed);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }
    /*loading settings from config.lh
    returns object Config, if the file exists
    */
    public static Config loadConfig(){
        string path = Application.persistentDataPath + "/config.lh";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Config loadData= formatter.Deserialize(stream) as Config;
            stream.Close();
            return loadData;
        }else {
            Debug.Log("error");
            return null;
        }
    }

}
