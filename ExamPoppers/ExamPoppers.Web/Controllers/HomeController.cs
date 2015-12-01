﻿using System;
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
        public ActionResult AddQuestion()
        {
            var query =
                from q in db.Question
                select new { Content = q.Content, Answer = q.Answer };
            query.ToList();

            List<Question> qList= new List<Question>();
            foreach(var x in query)
            {
                qList.Add(new Question
                {
                    Content = x.Content,
                    Answer = x.Answer
                });
            }
            return View(qList);
        }
        //[HttpPost]
        //public ActionResult AddQuestion()
        //{
        //    return View();
        //}
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
                Answer = "Blue"

            });
        }

	}
}