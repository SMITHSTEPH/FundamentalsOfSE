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
           }
        }
        public string[,] GetMultipleChoiceResponses()
        {
            return null;
        }
        /**
        Load the entire Questions table into A 2D array so that is can be passed
            off to the Questions View
        **/
        public string[,] GetProcessModelQuestions()
        {
            string[,] Test = new string[2, 4];
            string QueryCount = "SELECT COUNT(*) FROM Questions2Table";
            string QueryQ = "SELECT * FROM Questions2Table";
            Debug.Print("Print Connection String");
            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Stephanie\\Source\\Repos\\FundamentalsOfSE\\WebApplication2\\WebApplication2\\App_Data\\Registration.mdf; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework";
            SqlCommand Command = new SqlCommand(QueryCount, Connection);
            Connection.Open();
            SqlDataReader MyDataSet = Command.ExecuteReader();
            MyDataSet.Read();
            Debug.Print(MyDataSet.GetValue(0).ToString());
            
            //Debug.Print(MyDataSet.GetValue(0).ToString());
            int QNum = Convert.ToInt32(MyDataSet.GetValue(0));
            string[,] Questions = new string[QNum, 4];

            Command = new SqlCommand(QueryQ, Connection);
            MyDataSet = Command.ExecuteReader();
            int Rows = 0;
            //int Columns = 0;
            while (MyDataSet.Read())
            {
                //Debug.Print(Rows.ToString());
                //Debug.Print(Rows.ToString() +  ": "+ MyDataSet.GetValue(1).ToString());
                Questions[Rows, 0]= MyDataSet.GetValue(0).ToString();
                Questions[Rows, 1]=MyDataSet.GetValue(1).ToString();
                Questions[Rows, 2] = MyDataSet.GetValue(2).ToString();
                Questions[Rows, 3] = MyDataSet.GetValue(2).ToString();
                if (Rows<92){ Rows++; }
           
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