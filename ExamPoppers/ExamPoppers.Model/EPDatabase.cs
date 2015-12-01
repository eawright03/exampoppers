using ExamPoppers.Model;
using NMemory;
using NMemory.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPoppers.Model
{
    public class EPDatabase : Database
    {
        public EPDatabase()
        {
            var questionTable = base.Tables.Create<Question, int>(q => q.Id, null);
            var historyTable = base.Tables.Create<History, int>(h => h.Id, null);
            //looks like the commented out portion is to make the two tables created, related
            //Found @ https://github.com/tamasflamich/nmemory
            //var peopleGroupIdIndex = peopleTable.CreateIndex(
            //    new RedBlackTreeIndexFactory<Person>(), 
            //    p => p.GroupId);

            //this.Tables.CreateRelation(
            //    historyTable.PrimaryKeyIndex, 
            //    peopleGroupIdIndex, 
            //    x => x, 
            //    x => x);

            this.Question = questionTable;
            this.History = historyTable;
        }
        public ITable<Question> Question { get; private set; }

        public ITable<History> History { get; private set; }

    }
}
