using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Script.UI.Menu
{
    [System.Serializable]
    public static class SavingHandler
    {
        public static string DirPath = "Saved Decks";
        private static string SaveFileName = "Deck";
        private static int FileIndex = 0;
        
        public static DeckSave CreateDeckFile(List<Card.Card> deckList)
        {
            var file = new DeckSave();
            foreach (var card in deckList)
            {
                file.Deck.Add(card);
            }
            Debug.Log("Created file "+ file);
            return file;
        }

        public static void SaveDeck(DeckSave deck)
        {
            if (!DirExists())
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + DirPath);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(GetPath()); 
            formatter.Serialize(fileStream, deck);
            fileStream.Close();
        }

        public static DeckSave LoadDeck()
        {
            if (SaveExists())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream file = File.Open(GetPath(), FileMode.Open);
                    DeckSave save = (DeckSave) formatter.Deserialize(file);
                    file.Close();
                    return save;
                }
                catch(SerializationException)
                {
                    Debug.Log("Failed to load save file");
                }
            }
            return null;
        }
        private static string GetPath()
        {
    
            return Application.persistentDataPath + "/" + DirPath + "/" + SaveFileName+RNG_Name()+".dk";
      
        }

        private static string RNG_Name()
        {
            int i1 = Random.Range(0, 9);
            int i2 = Random.Range(0, 9);
            int i3 = Random.Range(0, 9);
            int i4 = Random.Range(0, 9);
            int i5 = Random.Range(0, 9);
            int i6 = Random.Range(0, 9);
            string namefix = i1.ToString() + i2.ToString() + i3.ToString() + i4.ToString() + i5.ToString() + i6.ToString();
            return namefix;

        }

        private static bool DirExists()
        {
            return Directory.Exists(Application.persistentDataPath + "/" + DirPath); 
        }

        private static bool SaveExists()
        {
            return File.Exists(GetPath());
        }

         
    }
}
