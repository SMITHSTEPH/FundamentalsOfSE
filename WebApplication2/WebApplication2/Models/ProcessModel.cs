using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
           foreach (ProcessModels processModel in Enum.GetValues(typeof(ProcessModels)))
           {
                ProcessModelsList.Add(processModel.ToString());
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

    }
}