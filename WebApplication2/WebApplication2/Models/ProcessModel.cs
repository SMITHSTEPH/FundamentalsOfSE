using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ProcessModel
    {
        enum ProcessModels{Waterfall, IterativeWaterfall, RAD, Agile, COTS};
        ArrayList ProcessModelsList = new ArrayList();
        private RegistrationEntities1 DB = new RegistrationEntities1(); //instance of process model Database
        private string ConnectionStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Stephanie\\Source\\Repos\\FundamentalsOfSE\\WebApplication2\\WebApplication2\\App_Data\\Registration.mdf; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework";
        private SqlConnection Connection;
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
                ProcessModelsList.Add(processModel.ToString());
                Connection= new SqlConnection(ConnectionStr);
            }
        }
        public string[,] GetMultipleChoiceResponses()
        {
            int Rows = 0;
            int Columns = 6;
            string QueryCount = "SELECT COUNT(*) FROM MultipleChoiceTable";
            Connection.Open();
            SqlDataReader MyDataSet = ReadQuery(QueryCount);
            MyDataSet.Read();
            //Debug.Print(MyDataSet.GetValue(0).ToString()); //test
            int QNum = Convert.ToInt32(MyDataSet.GetValue(0));

            string QueryQ = "SELECT * FROM MultipleChoiceTable";
            string[,] MultAnswers = new string[QNum, Columns];
            MyDataSet = ReadQuery(QueryQ);
            while (MyDataSet.Read())
            {
                for (int i = 0; i < Columns; i++)
                {
                    MultAnswers[Rows, i] = MyDataSet.GetValue(i).ToString(); //ID
                }
                if (Rows < QNum) { Rows++; }
                
            }
            Connection.Close();
            return MultAnswers;
        }
        private SqlDataReader ReadQuery(string query)
        {
            SqlCommand Command = new SqlCommand(query, Connection);
            return Command.ExecuteReader();
        }
        /**
        Load the entire Questions table into A 2D array so that is can be passed
            off to the Questions View
        **/
        public string[,] GetProcessModelQuestions()
        {
            int Rows = 0;
            int Columns = 3;
            string QueryCount = "SELECT COUNT(*) FROM Questions2Table";
            Connection.Open();
            SqlDataReader MyDataSet=ReadQuery(QueryCount);
            MyDataSet.Read();
            //Debug.Print(MyDataSet.GetValue(0).ToString()); //test
            int QNum = Convert.ToInt32(MyDataSet.GetValue(0));

            string QueryQ = "SELECT * FROM Questions2Table";
            string[,] Questions = new string[QNum, Columns];
            MyDataSet=ReadQuery(QueryQ);
            while (MyDataSet.Read())
            {
                for (int i = 0; i < Columns; i++)
                {
                    //don't need to store QuestionIDs b/c it's just "Question's index-1"
                    Questions[Rows, i] = MyDataSet.GetValue(i+1).ToString();
                }
                if (Rows<QNum){ Rows++; } //error prevention
            }
            Connection.Close();
            return Questions;
        }
        /**
        Adds the submitted form to the answers databases
        **/
        public void ProcessAnswers(Array answers)
        {
            for(int i=0; i<answers.Length; i++)
            {
                //DB.Account.Add(answers);
            }
        }
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
       public void EliminateProcessModels()
        {
            foreach (ProcessModels processModel in Enum.GetValues(typeof(ProcessModels)))
            {
                //join ProcessModelTable with Answer Table
                //if selecting from this table where answer!=desired answer && priority==5 != NULL
                //remove ProcessModel from the list
            }
        }
        public void ChooseProcessModels()
        {
            int[] ProcessPoints = new int[ProcessModelsList.Count];
            int ProcessVal = 0;
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
        public Boolean IsValid()
        {
            return true;
            //make sure all of the fields are filled out
        }

    }
}
