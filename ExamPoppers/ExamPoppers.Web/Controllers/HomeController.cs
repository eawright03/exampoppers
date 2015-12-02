using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamPoppers.Model;

namespace ExamPoppers.Web.Controllers
{
    public class HomeController : Controller
    {
        public EPDatabase db;
        //
        // GET: /Home/
        public HomeController()
        {
            InitializeDatabase();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddQuestionAndQuestionList()
        {
            var query =
                from q in db.Question
                select new {
					Content = q.Content,
					Answer1 = q.Answer1,
					Answer2 = q.Answer2,
					Answer3 = q.Answer3,
					Answer4 = q.Answer4,
                    CorrectAnswer = q.CorrectAnswer
				};
            query.ToList();

			List<Question> qList = new List<Question>();
			foreach (var x in query)
			{
				qList.Add(new Question
				{
					Content = x.Content,
					Answer1 = x.Answer1,
					Answer2 = x.Answer2,
					Answer3 = x.Answer3,
					Answer4 = x.Answer4,
                    CorrectAnswer = x.CorrectAnswer
				});
			}
			return View(qList);

		}
  //      [HttpPost]
  //      public ActionResult AddQuestionAndQuestionList(Question que)
  //      {
  //          var query =
  //              from q in db.Question
  //              select new { Content = q.Content, Answer = q.Answer };
		//	query.ToList();

		//	List<Question> qList = new List<Question>();
		//	foreach (var x in query)
		//	{
		//		qList.Add(new Question
		//		{
		//			Content = x.Content,
		//			Answer = x.Answer
		//		});
		//	}
		//	return View(qList);
		//}

        [HttpPost]
        public ActionResult AddQuestion(Question q)
        {
			//
			//db.Question.Insert(q);
			//add q to db
			var query =
                from question in db.Question
				select new
				{
					Content = question.Content,
					Answer1 = question.Answer1,
					Answer2 = question.Answer2,
					Answer3 = question.Answer3,
					Answer4 = question.Answer4,
                    CorrectAnswer = question.CorrectAnswer
				};
			query.ToList();

			List<Question> qList = new List<Question>();

			//db.Question.Insert(new Question
			//{
			//	Id = query.Count() + 1,
			//	Content = q.Content,
			//	Answer = q.Answer
			//});

			q.Id = query.Count() + 1;
			db.Question.Insert(q);

			foreach (var x in query) //How is the new question making it into this query??
			{
				qList.Add(new Question
				{
					//    Id = x.Id,
					Content = x.Content,
					Answer1 = x.Answer1,
					Answer2 = x.Answer2,
					Answer3 = x.Answer3,
					Answer4 = x.Answer4,
                    CorrectAnswer = x.CorrectAnswer
				});
			}

			return View("AddQuestionAndQuestionList", qList);
        }
        public ActionResult NavBar()
        {
            return PartialView("_NavBar");
        }
        public void InitializeDatabase()
        {
            db = new EPDatabase();
            db.Question.Insert(new Question {
                Id = 1,
                Content = "What color is the sky?",
                Answer1 = "Blue",
                Answer2 = "Elephant",
                Answer3 = "Water",
                Answer4 = "Carrot",
                CorrectAnswer = "Blue"
			});
			db.Question.Insert(new Question
			{
				Id = 2,
				Content = "How many fingers does a human have?",
				Answer1 = "10",
				Answer2 = "Elephant",
				Answer3 = "Water",
                Answer4 = "Carrot",
                CorrectAnswer = "10"
			});
			db.Question.Insert(new Question
			{
				Id = 3,
				Content = "Where in the World is Carmen Sandiego?",
				Answer1 = "Blue",
                Answer2 = "Utah",
                Answer3 = "Water",
                Answer4 = "Carrot",
                CorrectAnswer = "Utah"
			});
			db.Question.Insert(new Question
			{
				Id = 4,
				Content = "How much wood did the woodchuck chuck?/",
				Answer1 = "Blue",
                Answer2 = "Elephant",
                Answer3 = "7",
                Answer4 = "Carrot",
                CorrectAnswer = "7"
			});
			db.Question.Insert(new Question
			{
				Id = 5,
				Content = "Who let the dogs out?",
				Answer1 = "Blue",
                Answer2 = "Elephant",
                Answer3 = "Dr. Doyle",
                Answer4 = "Carrot",
                CorrectAnswer = "Dr. Doyle"
			});
		}

		[HttpGet]
		public ActionResult Game()
		{
			var query =
				from question in db.Question
				select new
				{
					Content = question.Content,
					Answer1 = question.Answer1,
					Answer2 = question.Answer2,
					Answer3 = question.Answer3,
					Answer4 = question.Answer4,
                    CorrectAnswer = question.CorrectAnswer
				};
			query.ToList();

			List<Question> qList = new List<Question>();
			foreach (var x in query)
			{
				qList.Add(new Question
				{
					Content = x.Content,
					Answer1 = x.Answer1,
					Answer2 = x.Answer2,
					Answer3 = x.Answer3,
                    Answer4 = x.Answer4,
                    CorrectAnswer = x.CorrectAnswer
				});
			}

			var p = new Player();
			p.PlayerId = 1;
			p.Question = qList[0];

			return View(p);
		}

		[HttpPost]
		public ActionResult Game(string questionId, string playerId)
		{
			

			var query =
				from question in db.Question
				select new
				{
					Content = question.Content,
					Answer1 = question.Answer1,
					Answer2 = question.Answer2,
					Answer3 = question.Answer3,
					Answer4 = question.Answer4
				};
			query.ToList();

			List<Question> qList = new List<Question>();
			foreach (var x in query)
			{
				qList.Add(new Question
				{
					Content = x.Content,
					Answer1 = x.Answer1,
					Answer2 = x.Answer2,
					Answer3 = x.Answer3,
					Answer4 = x.Answer4
				});
			}

			
			var p = new Player();

			int pId = 0;
			int qId = 0;

			try
			{
				pId = Int32.Parse(playerId);
				qId = Int32.Parse(questionId);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}

			p.PlayerId = pId;
			p.Question = (qId == qList.Count()) ? qList[0] : qList[qId];

			return View(p);
		}
	}
}
}