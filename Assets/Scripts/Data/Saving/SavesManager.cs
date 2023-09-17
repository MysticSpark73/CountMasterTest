using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CountMasters.Game;
using UnityEngine;

namespace CountMasters.Data.Saving
{
    public class SavesManager
    {
        public void SaveCoins()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file;
                file = File.Create(Application.persistentDataPath + Parameters.path_persistent_saves_file);
                binaryFormatter.Serialize(file, Parameters.Coins);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ReadCoins()
        {
            if (!File.Exists(Application.persistentDataPath + Parameters.path_persistent_saves_file)) return;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + Parameters.path_persistent_saves_file,
                    FileMode.Open);
                Parameters.AddCoins((int) binaryFormatter.Deserialize(file));
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void KillSave()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file;
                file = File.Create(Application.persistentDataPath + Parameters.path_persistent_saves_file);
                binaryFormatter.Serialize(file, 0);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}