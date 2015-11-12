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
        enum ProcessModels{Waterfall, IterativeWaterfall, RAD, Agile, COTS}; //holds all of the possible process models that we are using
        //enum ProcessTableNames: string { Waterfall="WaterfallTable2", IterativeWaterfall("WaterfallIterationTable") };
        private RegistrationEntities1 DB = new RegistrationEntities1(); //instance of process model Database

        //STEPHS STRING
        private string ConnectionStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Stephanie\\Source\\Repos\\FundamentalsOfSE\\WebApplication2\\WebApplication2\\App_Data\\Registration.mdf; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework";

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
           InitializeScores(TableName.WaterFallPModel.ToString(), WaterfallScores);
           InitializeScores(TableName.IterativeWaterfallPModel.ToString(), IterativeWaterfallScores);
           InitializeScores(TableName.RADTablePModel.ToString(), RADScores);
           InitializeScores(TableName.COTSTablePModel.ToString(), COTSScores);
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
            return pModel.Length % 2 == 0 ? (Convert.ToInt32(pModel[pModel.Length / 2 - 1]) + Convert.ToInt32(pModel[pModel.Length / 2])) / 2 : Convert.ToInt32(pModel[pModel.Length / 2]);
        }
        private int ComputeDistanceFromMedian(int myScore, int[] pModel)
        {
            return Math.Abs(myScore - ComputeMedian(pModel));
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
        public void ChooseProcessModels()
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
           

            //because we have no training data we will do this for now
            //determine max out of all of these
            /*int Max=Math.Max(WaterfallPoints, IterativeWaterfallPoints);
            Max = Math.Max(Max, COTSPoints);
            Max = Math.Max(Max, RADPoints);

            if (Max == WaterfallPoints) {
                Result = "Waterfall";
                Debug.Print("Waterfall is max with: " + WaterfallPoints); }
            else if (Max == IterativeWaterfallPoints) {
                Result = "Iterative Waterfall";
                Debug.Print("IterativeWaterfall is max with: " + IterativeWaterfallPoints); }
            else if (Max == COTSPoints) { 
                Result = "COTS";
                Debug.Print("COTS is max with: " + COTSPoints); }
            else { 
                Result="RAD";
                 Debug.Print("RAD is max with: " + RADPoints); }*/

            /*if (IsWithinRange(WaterfallScores, WaterfallPoints) && ProcessModelsList.Contains(ProcessModels.Waterfall.ToString()))
            {
                WaterfallDistance=ComputeDistanceFromMedian(WaterfallPoints, WaterfallScores);
            }
            if (IsWithinRange(IterativeWaterfallScores, IterativeWaterfallPoints) && ProcessModelsList.Contains(ProcessModels.IterativeWaterfall.ToString()))
            {
                IterativeWaterfallDistance=ComputeDistanceFromMedian(IterativeWaterfallPoints, IterativeWaterfallScores);
            }
            if (IsWithinRange(RADScores, RADPoints) && ProcessModelsList.Contains(ProcessModels.RAD.ToString()))
            {
                RADDistance=ComputeDistanceFromMedian(RADPoints, RADScores);
            }
            if (IsWithinRange(COTSScores, COTSPoints) && ProcessModelsList.Contains(ProcessModels.COTS.ToString()))
            {
                COTSDistance=ComputeDistanceFromMedian(COTSPoints, COTSScores);
            }*/

            //WHAT WE NEED TO ACTUALLY DO:
            //see if waterfall score lies within range of waterfall points
            //if yes, calcuate range waterfall score and median point
            //store median value in dictionary
            //do the same for the rest of the PModels

            //iterating over the dictionary values
            //entry with min value is the winner




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
        private void InitializeScores(string processModel, int[] scoreArray)
        {
            string[,] Size = ReadQuery("SELECT COUNT(*) FROM " + TableName.PModelScore.ToString() + " WHERE ProcessModel='"+processModel+"'", 1, 1, 0);
            int Rows = Convert.ToInt32(Size[0, 0]);
            string[,] TempTable = new string[Rows, 1];
            TempTable = ReadQuery("SELECT Score FROM " + TableName.PModelScore.ToString() + " WHERE ProcessModel='" + processModel + "'", TempTable.GetLength(0), TempTable.GetLength(1), 0);
            scoreArray = new int[TempTable.GetLength(0)];
            Debug.Print("scores: ");
            for (int i = 0; i < TempTable.GetLength(0); i++)
            {
                Debug.Print("Score " + i + ": " + TempTable[i, 0]);
                scoreArray[i] = Int32.Parse(TempTable[i, 0], NumberStyles.AllowLeadingSign);
            }
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
    }
}
