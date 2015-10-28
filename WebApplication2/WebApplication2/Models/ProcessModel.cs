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
        public int QuestionSize { get; set; }
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
            QuestionSize = QNum;
            Debug.Print("Question Size: " + QuestionSize);
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
            
            int Columns = 3;
            Connection.Open();
            SqlDataReader MyDataSet = ReadQuery(queryCount);
            MyDataSet.Read();
            //int Count = 5;
            //string val = MyDataSet.GetValue(0).ToString();
            //Debug.Print("val is: " + val);
            int Count = Convert.ToInt32( MyDataSet.GetValue(0).ToString());
            string[,] PModel = new string[Count, Columns];
            int Rows = 0;
            Debug.Print("Count: " + Count);
            MyDataSet = ReadQuery(query);
            MyDataSet.Read();
            while (MyDataSet.Read())
            {
                for (int i = 0; i < Columns; i++)
                {
                    PModel[Rows, i] = MyDataSet.GetValue(i).ToString();
                    Debug.Print(PModel[Rows, i].ToString());
                }
                if (Rows < Count) { Rows++; } //error prevention
            }
            //now determine if any of the answers violate the priority
            /*for(int i=0; i<PModel.GetLength(0); i++)
            {
                if(answers[i+1].Equals(PModel[i,1]))
                {
                    return true;
                }
            }*/
            Connection.Close();
            return false;
        }
       public void EliminateProcessModels()
        {
                /*for(int i=0; i< Answers.Length; i++)
                {
                    Debug.Print(i + ": " + Answers[i]);
                }*/
                int Columns = 3;

                string QueryCount = "SELECT COUNT(*) FROM WaterfallTable2 WHERE Priority=5";
           

                string[,] Waterfall;
                string[,] WaterfallIt = new string[QuestionSize, 3];
                string[,] RAD = new string[QuestionSize, 3];
                string[,] COTS = new string[QuestionSize, 3];

                string QueryW = "SELECT * FROM WaterfallTable2 WHERE Priority=5";
                string QueryWI = "SELECT * FROM WaterfallIterationTable WHERE Priority=5";
                string QueryRAD = "SELECT * FROM RADTable WHERE Priority=5";
                string QueryCOTS = "SELECT* FROM COTSTable WHERE Priority = 5";

                ReadHighPriority(QueryW, QueryCount);
               /* WaterfallIt = ReadHighPriority(WaterfallIt, QueryWI, Columns);
                RAD = ReadHighPriority(RAD, QueryRAD, Columns);
                COTS = ReadHighPriority(COTS, QueryCOTS, Columns);*/

                
                



                
                //join ProcessModelTable with Answer Table
                //if selecting from this table where answer!=desired answer && priority==5 != NULL
                //remove ProcessModel from the list
            
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
            Debug.Print("answers: " + answers.Length);
            if (answers.Length >= 92)
                return true;
            else
                return false;
            //make sure all of the fields are filled out
        }

    }
}
