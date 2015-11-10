using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public sealed class TableName
    {

        private readonly string name;

        public static readonly TableName WaterFallPModel = new TableName("WaterfallTable2");
        public static readonly TableName IterativeWaterfallPModel = new TableName("WaterfallIterationTable");
        public static readonly TableName RADTablePModel = new TableName("RADTable");
        public static readonly TableName COTSTablePModel = new TableName("COTSTable");
        public static readonly TableName MultipleChoiceAnswers = new TableName("MultipleChoiceTable");
        public static readonly TableName PModelQuestions = new TableName("Questions2Table");
        public static readonly TableName PModelScore = new TableName("ScoreTable");

        private TableName(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }

    }
}