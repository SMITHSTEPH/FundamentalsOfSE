using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ProcessModel
    {
        //fields
        enum ProcessModels{Waterfall, IterativeWaterfall, RAD, Agile, COTS}; //holds all of the possible process models that we are using
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
        /**
        These 6 Dictionaries give us insight on what a 'winning' process model looks like
        **/
        Dictionary<int, Array> WaterfallData = new Dictionary<int, Array>();
        Dictionary<int, Array> IterativeWaterfallData = new Dictionary<int, Array>();
        Dictionary<int, Array> RADData = new Dictionary<int, Array>();
        Dictionary<int, Array> AgileData = new Dictionary<int, Array>();
        Dictionary<int, Array> COTSDAta= new Dictionary<int, Array>();
        //mystery
        /**
        Constructor populates an ArrayList with all of the ProcessModel tables in the database
        **/
        public ProcessModel()
        {
           foreach (ProcessModels processModel in Enum.GetValues(typeof(ProcessModels)))
           {
                ProcessModelsList.Add(processModel.ToString()); //adding all of the process models to the arraylist
           }
           Connection = new SqlConnection(ConnectionStr); //establishing a connection

           string[,] Size = ReadQuery("SELECT COUNT(*) FROM Questions2Table", 1, 1, 0); //return the size of the questions table
           int Rows = Convert.ToInt32(Size[0, 0]);
           Debug.Print("Rows are: " + Rows);
           Questions = new string[Rows, 3]; //initializing the size of the questions
           Questions = ReadQuery("SELECT * FROM Questions2Table", Questions.GetLength(0), Questions.GetLength(1), 1);
           Size = ReadQuery("SELECT COUNT(*) FROM MultipleChoiceTable", 1, 1, 0);
           Rows = Convert.ToInt32(Size[0, 0]);
           MultipleChoiceAnswers = new string[Rows, 6];
           MultipleChoiceAnswers = ReadQuery("SELECT * FROM MultipleChoiceTable", MultipleChoiceAnswers.GetLength(0), MultipleChoiceAnswers.GetLength(1), 0);
        }
        /**
        performs the query and stores the results in a 2D array
        **/
        private string[,] ReadQuery(string query, int rows, int columns, int offset)
        {
            string[,] TableData=new string[rows, columns]; //where the data extracted from the table will be stored
            Connection.Open();
            SqlCommand Command = new SqlCommand(query, Connection);
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
        /**
        Load the entire Questions table into A 2D array so that is can be passed
            off to the Questions View
        **/
        public void TrainData(string winner)
        {

            EliminateProcessModels();
            if(!ProcessModelsList.Contains(winner))
            {
                Debug.Print("You did selected incorrectly!!");
            }
            else
            {
                for (int i = 0; i < ProcessModelsList.Count; i++)
                {
                    //join answer table and winner table
                    //accumulate process model points
                    //add it to the appropriate arrayList
                }
            }
        }
        public Boolean ReadHighPriority(string query, string queryCount)
        {
            string[,] Size=ReadQuery(queryCount, 1, 1, 0);
            int Rows = Convert.ToInt32(Size[0, 0]);

            string[,] PModel = new string[Rows, 3]; //hard coding the number of columns for now
            PModel = ReadQuery(query, PModel.GetLength(0), PModel.GetLength(1), 0);
            
            for(int i=0; i<PModel.GetLength(0); i++)
            {
                if(Answers[i + 1].Equals(PModel[i, 1])) {
                    return true; }
            }
            return false;
        }
       public void EliminateProcessModels()
       {
           if(!ReadHighPriority("SELECT * FROM WaterfallTable2 WHERE Priority=5", "SELECT COUNT(*) FROM WaterfallTable2 WHERE Priority=5")) {
                Debug.Print("Removed waterfall");
                ProcessModelsList.Remove(ProcessModels.Waterfall);}
           if(!ReadHighPriority("SELECT * FROM WaterfallIterationTable WHERE Priority=5", "SELECT * FROM WaterfallIterationTable WHERE Priority=5")){
                Debug.Print("Removed Iterative waterfall");
                ProcessModelsList.Remove(ProcessModels.IterativeWaterfall);}
           if(!ReadHighPriority("SELECT * FROM RADTable WHERE Priority=5", "SELECT COUNT(*) FROM WaterfallTable2 WHERE Priority=5")){
                Debug.Print("Removed RAD");
                ProcessModelsList.Remove(ProcessModels.RAD);}
           if(!ReadHighPriority("SELECT* FROM COTSTable WHERE Priority = 5", "SELECT COUNT(*) FROM WaterfallTable2 WHERE Priority=5")){
                Debug.Print("Removed");
                ProcessModelsList.Remove(ProcessModels.COTS);}
        }
        public void ChooseProcessModels()
        {
            int[] ProcessPoints = new int[ProcessModelsList.Count];
            
            for (int i=0; i<ProcessModelsList.Count; i++)
            {
                //join ProcessModel Table With Answer Table
                //join ProcessModelTable With Answer Table
                //if when selecting where answer!=desired answer
                    //Priorty=Priorty*-1
                //else
                    //add normal priority
                //ProcessPoints[i] += 0;
            }
            for(int i=0; i<ProcessModelsList.Count; i++)
            {
                //if mean of processModel is closer to processVal than the previous process model
                //select this process model
            }
            //add the selected process to the array list
            //if the arraylist is over a certain capactiy
            //remove largest outlier from the list
        }
        public Boolean IsValid(string[] answers)
        { 
            return answers.Length == Questions.GetLength(0) ? true : false;
        }

    }
}
