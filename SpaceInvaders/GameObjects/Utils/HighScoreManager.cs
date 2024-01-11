using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.GameObjects.Utils
{
    /// <summary>
    /// Classe qui gère les classements des scores des joueurs dans le jeu et les sauvegarde dans un fichier (sous forme de texte : "PlayerID:Score").
    /// </summary>
    internal static class HighScoreManager
    {
        #region fields
        /// <summary>
        /// Nom du fichier de sauvegarde des scores.
        /// </summary>
        static String FileName = @".\highscores.txt";

        /// <summary>
        /// Dictionnaire contenant les scores des joueurs.
        /// </summary>
        static Dictionary<String, int> Scores = new Dictionary<String, int>();
        #endregion


        #region methods
        /// <summary>
        /// Fonction qui charge les scores des joueurs depuis le fichier de sauvegarde.
        /// </summary>
        public static void LoadScores()
        {
            if (File.Exists(FileName))
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("File : " + FileName + " exists! :)");
                using (Stream stream = File.Open(FileName, FileMode.Open))
                {
                    Scores = SerializerFileManager.Deserialize<Dictionary<String, int>>(stream);
                }
            }
            else
            {
                // Crée un nouveau fichier s'il n'existe pas
                SaveScores();
            }
        }

        /// <summary>
        /// Fonction qui sauvegarde les scores des joueurs dans le fichier de sauvegarde.
        /// </summary>
        public static void SaveScores()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("Saving scores...");
            using (Stream stream = File.Open(FileName, FileMode.Create))
            {
                Console.WriteLine("File : " + FileName + " created! :)");
                SerializerFileManager.Serialize<Dictionary<String, int>>(Scores, stream);
            }
        }


        /// <summary>
        /// Fonction qui ajoute ou met à jour le score d'un joueur.
        /// </summary>
        public static void UpdateScore(String playerID, int score)
        {
            if (Scores.ContainsKey(playerID))
            {
                Scores[playerID] = score;
            }
            else
            {
                Scores.Add(playerID, score);
            }

            SaveScores();
        }

        /// <summary>
        /// Fonction qui renvoie les scores.
        /// </summary>
        public static Dictionary<String, int> GetScores()
        {
            return Scores.OrderByDescending(pair => pair.Value).Take(Scores.Count).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Fonction qui renvoie le top 6 des scores.
        /// </summary>
        public static Dictionary<String, int> GetTopScores()
        {
            return Scores.OrderByDescending(pair => pair.Value).Take(6).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Fonction qui déssine un classement des scores (graphiquement avec Graphics).
        /// </summary>
        public static void DrawHighScore(Graphics graphics, Size gameSize)
        {
            Dictionary<String, int> topScores = GetTopScores();
            int i = 1;
            foreach (KeyValuePair<String, int> score in topScores)
            {
                graphics.DrawString(i + ". " + score.Key + " : " + score.Value, new Font("Arial", 10), Game.whiteBrush, new PointF(gameSize.Width / 2 -40, 70 + (i * 20)));
                i++;
            }

            // draw total number of players
            graphics.DrawString("Total players : " + Scores.Count, new Font("Arial", 12), Game.whiteBrush, new PointF(gameSize.Width / 2 - 53, i * 20 + 75));
        }
        #endregion
    }
}


