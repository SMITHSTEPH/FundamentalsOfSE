using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication2.Models
{
    public class ProcessModel
    {
        //fields
        enum ProcessModels{Waterfall, IterativeWaterfall, RAD, COTS}; //holds all of the possible process models that we are using
        //enum ProcessTableNames: string { Waterfall="WaterfallTable2", IterativeWaterfall("WaterfallIterationTable") };
        private RegistrationEntities1 DB = new RegistrationEntities1(); //instance of process model Database

        //STEPHS STRING
        //private string ConnectionStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Stephanie\\Source\\Repos\\FundamentalsOfSE\\WebApplication2\\WebApplication2\\App_Data\\Registration.mdf; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework";
        
        //Azure String
        private string ConnectionStr = "Server=tcp:vc6a15pj3w.database.windows.net,1433;Database=RegistrationEntities1;User ID=bbergeron@vc6a15pj3w;Password=BBErgero123!$*;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

    //BRADS STRING
    //private string ConnectionStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Brad\\Source\\Repos\\FundamentalsOfSE\\WebApplication2\\WebApplication2\\App_Data\\Registration.mdf; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework";

    private SqlConnection Connection;
        ArrayList ProcessModelsList = new ArrayList();
        //properties
        public string[,] Questions { get; set; }
        public string[,] MultipleChoiceAnswers { get; set; }
        public string[] Answers { get; set; }
        public string[] AnswersTest { get; set; }
        public string  Result { get; set; }
        //stores the scores
        private static int[] WaterfallScores { get; set; }
        private static int[] IterativeWaterfallScores { get; set; }
        private static int[] COTSScores { get; set; }
        private static int[] RADScores { get; set; }
        private static int[] AgileSCores { get; set; }
        private static Dictionary<string, int[]> WinningScores;
        /**
        Constructor populates an ArrayList with all of the ProcessModel tables in the database,
        Establishes an SQL Connection, and
        Reads in all of the Process Model questions and multiple choice answers and stores them each in array
        **/
        public ProcessModel()
        {
           foreach (ProcessModels processModel in Enum.GetValues(typeof(ProcessModels)))
           {
                ProcessModelsList.Add(processModel.ToString()); //adding all of the process models to the arraylist
           }
           Connection = new SqlConnection(ConnectionStr); //establishing a connection
           InitializeQuestionsAndAnswers();
           WaterfallScores=InitializeScores(TableName.WaterFallPModel.ToString());
           IterativeWaterfallScores=InitializeScores(TableName.IterativeWaterfallPModel.ToString());
           RADScores=InitializeScores(TableName.RADTablePModel.ToString());
           COTSScores=InitializeScores(TableName.COTSTablePModel.ToString());
           CreateScoreDictionary(); //adding all of the winning scores to a dictionary
        } //end of constructor
        /**
        performs the query and stores the results in a 2D array
        maybe this should be in its own class?
        **/
        private string[,] ReadQuery(string query, int rows, int columns, int offset)
        {
            string[,] TableData=new string[rows, columns]; //where the data extracted from the table will be stored
            Connection.Open();
            Debug.Print(query)
;           SqlCommand Command = new SqlCommand(query, Connection);
            SqlDataReader MyDataSet= Command.ExecuteReader();
            int Rows = 0;
            while(MyDataSet.Read()) //while there is data from the table left to read
            {
                for (int i = 0; i < columns; i++)
                {
                    TableData[Rows, i] = MyDataSet.GetValue(i+offset).ToString();
                }
                if (Rows < rows) { Rows++; } //error prevention
            }
            Connection.Close();
            return TableData;
        }
        public bool IsWithinRange(int[] pModel, int score)
        {
            Array.Sort(pModel);
            return score >= pModel[0] && score <= pModel[pModel.Length - 1] ? true : false;
        }
        private int ComputeMedian(int[] pModel)
        {
            Array.Sort(pModel);
            for(int i=0; i<pModel.Length; i++) { Debug.Write(pModel[i].ToString() + ", "); }
            return pModel.Length % 2 == 0 ? (Convert.ToInt32(pModel[pModel.Length / 2 - 1]) + Convert.ToInt32(pModel[pModel.Length / 2])) / 2 : Convert.ToInt32(pModel[pModel.Length / 2]);
        }
        private int ComputeDistanceFromMedian(int myScore, int[] pModel)
        {
            Debug.Print("Score is: " + myScore.ToString());
            int median=ComputeMedian(pModel);
            Debug.Print("median is: " + median.ToString());
            int distance= Math.Abs(myScore - ComputeMedian(pModel));
            Debug.Print("Distance is: " + distance.ToString());
            Debug.Print("-------------------------------------");
            return distance;
        }
        /**
        Load the entire Questions table into A 2D array so that is can be passed
            off to the Questions View
        **/
        public int TrainData(string winner)
        {
            string PM = ""; //mapping the winning table to the correct process model
            if (winner == TableName.COTSTablePModel.ToString()) { PM = ProcessModels.COTS.ToString(); }
            else if(winner ==TableName.WaterFallPModel.ToString()) { PM = ProcessModels.Waterfall.ToString(); }
            else if (winner==TableName.IterativeWaterfallPModel.ToString()) { PM = ProcessModels.IterativeWaterfall.ToString(); }
            else { PM = ProcessModels.RAD.ToString(); }

            EliminateProcessModels();
            if(!ProcessModelsList.Contains(PM))
            {
                Debug.Print("You selected incorrectly!!");
                return 0;
            }
            else
            {
                int score=ComputeScore("SELECT * FROM " + winner, "SELECT COUNT(*) FROM " + winner);
                string[,] SIdMax = ReadQuery("SELECT MAX(SId) FROM " + TableName.PModelScore.ToString(), 1, 1, 0);
                int SIdVal= SIdMax[0,0] == "" ? 0 : Convert.ToInt32(SIdMax[0, 0]);
                InsertIntoDatabase(SIdVal + 1, score, winner);
                return score;
            }
        }
        private Boolean ReadHighPriority(string query, string queryCount)
        {
            string[,] Size=ReadQuery(queryCount, 1, 1, 0);
            int Rows = Convert.ToInt32(Size[0, 0]);

            string[,] PModel = new string[Rows, 3]; //hard coding the number of columns for now
            PModel = ReadQuery(query, PModel.GetLength(0), PModel.GetLength(1), 0);
            
            for(int i=0; i<PModel.GetLength(0); i++)
            {
                if (Answers[i].Equals(PModel[i, 1].Trim())) {return true;}
            }
            return false;
        }
        public void EliminateProcessModels()
        {
           if(ReadHighPriority("SELECT * FROM " + TableName.WaterFallPModel.ToString() + " WHERE Priority=5", "SELECT COUNT(*) FROM " + TableName.WaterFallPModel.ToString() + " WHERE Priority=5")) { ProcessModelsList.Remove(ProcessModels.Waterfall);}
           if(ReadHighPriority("SELECT * FROM "  + TableName.IterativeWaterfallPModel.ToString() + " WHERE Priority=5", "SELECT COUNT(*) FROM " + TableName.IterativeWaterfallPModel.ToString() + " WHERE Priority=5")){ ProcessModelsList.Remove(ProcessModels.IterativeWaterfall);}
           if(ReadHighPriority("SELECT * FROM " + TableName.RADTablePModel.ToString() + " WHERE Priority=5", "SELECT COUNT(*) FROM "+ TableName.RADTablePModel.ToString() + " WHERE Priority=5")){ ProcessModelsList.Remove(ProcessModels.RAD);}
           if(ReadHighPriority("SELECT* FROM " + TableName.COTSTablePModel.ToString() + " WHERE Priority = 5", "SELECT COUNT(*) FROM " + TableName.COTSTablePModel.ToString() + " WHERE Priority=5")){ ProcessModelsList.Remove(ProcessModels.COTS);}
        }
        int ComputeScore(string query, string queryCount)
        {
            int Score = 0;
            string[,] Size = ReadQuery(queryCount, 1, 1, 0);
            int Rows = Convert.ToInt32(Size[0, 0]);
            Debug.Print("ROWS ARE: " + Rows);
            string[,] PModel = new string[Rows, 3]; //hard coding the number of columns for now
            PModel = ReadQuery(query, PModel.GetLength(0), PModel.GetLength(1), 0);
            Debug.Print(PModel[0, 0]);
            for (int i = 0; i < PModel.GetLength(0); i++)
            {
                Score += Answers[i].Equals(PModel[i, 1].Trim()) ? Convert.ToInt32(PModel[i, 2].Trim()) : -1 * Convert.ToInt32(PModel[i, 2].Trim());
            }
            return Score;
        }
        public string ChooseProcessModels()
        {
            Debug.Print("In ChooseProcessModels");
            
            Dictionary<string, int> Points= new Dictionary<string, int>();
            Dictionary<string, int> Distance = new Dictionary<string, int>();
            for (int i = 0; i < ProcessModelsList.Count; i++)
            {
                Points.Add(ProcessModelsList[i].ToString() + "Points", 0);
                Distance.Add(ProcessModelsList[i].ToString() + "Distance", 0);  
            }
            for(int i=0; i<ProcessModelsList.Count; i++)
            {
                if(ProcessModelsList[i].ToString().Equals(ProcessModels.Waterfall.ToString())){ Points[ProcessModelsList[i].ToString()+"Points"]= ComputeScore("SELECT * FROM " +TableName.WaterFallPModel.ToString(), "SELECT COUNT(*) FROM " + TableName.WaterFallPModel.ToString());}
                else if(ProcessModelsList[i].ToString().Equals(ProcessModels.IterativeWaterfall.ToString())){ Points[ProcessModelsList[i].ToString() + "Points"] = ComputeScore("SELECT * FROM " + TableName.IterativeWaterfallPModel.ToString(), "SELECT COUNT(*) FROM " + TableName.IterativeWaterfallPModel.ToString());}
                else if(ProcessModelsList[i].ToString().Equals(ProcessModels.RAD.ToString())){ Points[ProcessModelsList[i].ToString() + "Points"] = ComputeScore("SELECT * FROM " + TableName.RADTablePModel.ToString(), "SELECT COUNT(*) FROM " + TableName.RADTablePModel.ToString());}
                else if(ProcessModelsList[i].ToString().Equals(ProcessModels.COTS.ToString())){ Points[ProcessModelsList[i].ToString() + "Points"] = ComputeScore("SELECT * FROM " + TableName.COTSTablePModel.ToString(), "SELECT COUNT(*) FROM " + TableName.COTSTablePModel.ToString());}
            }
            for (int i = 0; i < ProcessModelsList.Count; i++)
            {
                Debug.Print(ProcessModelsList[i].ToString() + "Scores");
                if(IsWithinRange(WinningScores[ProcessModelsList[i].ToString()+"Scores"], Points[ProcessModelsList[i].ToString()+"Points"]))
                {
                    Distance[ProcessModelsList[i].ToString()+"Distance"] = ComputeDistanceFromMedian(Points[ProcessModelsList[i].ToString() + "Points"], WinningScores[ProcessModelsList[i].ToString() + "Scores"]);
                }
            }
            //find min distance
            int winVal = Distance[ProcessModelsList[0].ToString()+"Distance"]; //starting with value in the dictionary to have the min distance
            string winProcess = ProcessModelsList[0].ToString();
            Debug.Print("Winner is: " + winProcess + "With value: " + winVal.ToString());
            for (int i=1; i<ProcessModelsList.Count; i++)
            {
                Debug.Write(ProcessModelsList[i].ToString() + "With distance: ");
                Debug.Print(Distance[ProcessModelsList[i].ToString() + "Distance"].ToString());
                if(winVal>Distance[ProcessModelsList[i].ToString()+"Distance"])
                {
                    winVal=Distance[ProcessModelsList[i].ToString()+"Distance"];
                    winProcess = ProcessModelsList[i].ToString();
                    Debug.Print("Winner is: " + winProcess + "With value: " + winVal.ToString());
                }
            }
            return winProcess;
        }
        public Boolean IsValid(string[] answers)
        {
            return answers.Length == Questions.GetLength(0) ? true : false;
        }
        public string[] RemoveQuestionIDS(string[] answers)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                Debug.Write("before: " + answers[i] + "---");
                Regex pattern = new Regex(@"^[\d-]*\s*");
                answers[i] = pattern.Replace(answers[i], "");  //removing the first character from the string
            }
            return answers;
        }
        private int[] InitializeScores(string processModel)
        {
            int[] ScoreArray;
            string[,] Size = ReadQuery("SELECT COUNT(*) FROM " + TableName.PModelScore.ToString() + " WHERE ProcessModel='"+processModel+"'", 1, 1, 0);
            int Rows = Convert.ToInt32(Size[0, 0]);
            string[,] TempTable = new string[Rows, 1];
            TempTable = ReadQuery("SELECT Score FROM " + TableName.PModelScore.ToString() + " WHERE ProcessModel='" + processModel + "'", TempTable.GetLength(0), TempTable.GetLength(1), 0);
            ScoreArray = new int[TempTable.GetLength(0)];
            Debug.Print("scores: ");
            for (int i = 0; i < TempTable.GetLength(0); i++)
            {
                Debug.Print("Score " + i + ": " + TempTable[i, 0]);
                ScoreArray[i] = Int32.Parse(TempTable[i, 0], NumberStyles.AllowLeadingSign);
            }
            return ScoreArray;
        }
        private void InitializeQuestionsAndAnswers()
        {
            string[,] Size = ReadQuery("SELECT COUNT(*) FROM " + TableName.PModelQuestions.ToString(), 1, 1, 0); //return the size of the questions table
            int Rows = Convert.ToInt32(Size[0, 0]);
            Debug.Print("Rows are: " + Rows);
            Questions = new string[Rows, 3]; //initializing the size of the questions
            Questions = ReadQuery("SELECT * FROM " + TableName.PModelQuestions.ToString(), Questions.GetLength(0), Questions.GetLength(1), 1);
            Size = ReadQuery("SELECT COUNT(*) FROM " + TableName.MultipleChoiceAnswers.ToString(), 1, 1, 0);
            Rows = Convert.ToInt32(Size[0, 0]);
            MultipleChoiceAnswers = new string[Rows, 6];
            MultipleChoiceAnswers = ReadQuery("SELECT * FROM " + TableName.MultipleChoiceAnswers.ToString(), MultipleChoiceAnswers.GetLength(0), MultipleChoiceAnswers.GetLength(1), 0);
        }
        private void InsertIntoDatabase(int idVal, int score, string winner)
        {
            string Query = "INSERT INTO " + TableName.PModelScore.ToString() + "(SId, Score, ProcessModel) VALUES('" + idVal + "','" + score + "','" + winner + "')";
            Debug.Print("training querty is: " + Query);
            Connection.Open();
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }
        private void CreateScoreDictionary()
        {
            WinningScores = new Dictionary<string, int[]>();
            WinningScores.Add(ProcessModels.Waterfall.ToString() + "Scores", WaterfallScores);
            WinningScores.Add(ProcessModels.IterativeWaterfall.ToString() + "Scores", IterativeWaterfallScores);
            WinningScores.Add(ProcessModels.RAD.ToString()+"Scores", RADScores);
            WinningScores.Add(ProcessModels.COTS.ToString()+"Scores", COTSScores);
        }
    }
}
