using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace SpaceBHGame.UI_Control
{
    public struct PlayerData
    {
        readonly string PlayerName;
        readonly long score;
        //readonly Difficulty difficulty;

        public PlayerData(string name, long highscore, Difficulty diff)
        {
            PlayerName = name;
            score = highscore;
            //difficulty = diff;
        }

        public long Score
        {
            get { return score; }
        }
        public string Name
        {
            get { return PlayerName; }
        }
        //public Difficulty Difficulty
        //{
        //    get { return difficulty; }
        //}
    }

    class ScoreboardControl
    {
        List<PlayerData> totalData;
        string dataFolder = AppDomain.CurrentDomain.BaseDirectory + "\\Game Data\\Scores";
        string filename = "Scores.lst";
        long highscore = 0;

        public long Highscore
        {
            get { return highscore; }
        }

        public ScoreboardControl()
        {
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            totalData = new List<PlayerData>();
            LoadScores();
        }

        public void SaveScores()
        {
            
        }

        private void LoadScores()
        {
            PlayerData data;


            // Get the path of the save game
            string fullpath = Path.Combine(dataFolder, filename);

            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);

            while (stream.Length != stream.Position)
            {
                try
                {
                    // Read the data from the file
                    XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
                    data = (PlayerData)serializer.Deserialize(stream);

                    if (data.Score > highscore)
                        highscore = data.Score;

                    totalData.Add(data);
                }
                finally
                {
                    // Close the file
                    stream.Close();
                }
            }
        }

        public void NewScore(string name, long score, Difficulty difficulty)
        {
            PlayerData player;
            int index = PlayerExists(name, difficulty);

            if (index != -1) // Player data exists
            {
                if (totalData[index].Score < score)
                {
                    totalData.RemoveAt(index);
                    player = new PlayerData(name, score, difficulty);
                    totalData.Add(player);
                }
            }
            else
            {
                player = new PlayerData(name, score, difficulty);
                totalData.Add(player);
            }
        }

        private int PlayerExists(string name, Difficulty difficulty)
        {
            for (int i = 0; i < totalData.Count; i++)
            {
                if (totalData[i].Name == name)
                {
                    //if (totalData[i].Difficulty == difficulty)
                        //return i;
                }
            }
            return -1;
        }
    }
}
