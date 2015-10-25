using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ProcessModel
    {
        enum ProcessModels{Waterfall, IterativeWaterfall, RAD, Agile, COTS};
        ArrayList ProcessModelsList = new ArrayList();
        private ProcessModels DB = new ProcessModels(); //instance of process model Database
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
           foreach (ProcessModels processModel in Enum.GetValues(typeof(ProcessModel)))
           {
                ProcessModelsList.Add(processModel);
           }
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
        public void TrainData()
        {
          
        }
       public ArrayList EliminateProcessModels()
        {
            //for a given question
            //for a given process model
            //if answer in DB !=equal answer for priority 5 questions
            //remove process model from arraylist of models
            //
            
            return null;
        }
        public void ChooseProcessModels()
        {

        }

    }
}