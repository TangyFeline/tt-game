﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tfgame.dbModels.Abstract;
using tfgame.dbModels.Concrete;
using tfgame.dbModels.Models;
using tfgame.Procedures;
using tfgame.Statics;
using tfgame.ViewModels.Quest;

namespace tfgame.Controllers
{
    [Authorize(Roles = PvPStatics.Permissions_QuestWriter)]
    public class QuestWriterController : Controller
    {
        // GET: QuestWriter
        public ActionResult Index()
        {
            List<QuestStart> output = QuestProcedures.GetAllQuestStarts().ToList();

            ViewBag.ErrorMessage = TempData["Error"];
            ViewBag.SubErrorMessage = TempData["SubError"];
            ViewBag.Result = TempData["Result"];

            return View(output);
        }

        public ActionResult QuestStart(int Id)
        {

            IQuestRepository repo = new EFQuestRepository();

            QuestStart questStart = repo.QuestStarts.FirstOrDefault(q => q.Id == Id);

            if (questStart == null)
            {
                questStart = new QuestStart
                {
                    IsLive = false,
                    Name = "[ UNNAMED QUEST ]",
                    MinStartTurn = 0,
                    MinStartLevel = 0,
                    MaxStartTurn = 99999,
                    MaxStartLevel = 99999,
                };
            }

            IEnumerable<QuestStart> allStarts = repo.QuestStarts;
            ViewBag.OtherQuests = allStarts;

            QuestState firstState = repo.QuestStates.FirstOrDefault(f => f.Id == questStart.StartState);
            ViewBag.firstState = firstState;

            return PartialView(questStart);
        }

        public ActionResult QuestStartSend(QuestStart input)
        {

            QuestWriterProcedures.SaveQuestStart(input);

            return RedirectToAction("QuestStart", "QuestWriter", new { input.Id });
        }

        public ActionResult MarkQuestAsLive (int Id, bool live)
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return RedirectToAction("QuestStart", "QuestWriter", new { Id = Id });
            }

            QuestWriterProcedures.MarkQuestAsLive(Id, live);
            return RedirectToAction("QuestStart", "QuestWriter", new { Id = Id });

        }

        public ActionResult QuestState(int Id, int QuestId, int ParentStateId)
        {
            IQuestRepository repo = new EFQuestRepository();

            QuestState questState = repo.QuestStates.FirstOrDefault(q => q.Id == Id);

            if (questState == null)
            {
                questState = new QuestState
                {
                    ChoiceText = "[ CHOICE TEXT ]",
                    QuestEndId = -1,
                    ParentQuestStateId = ParentStateId,
                    Text = "",
                    QuestId = QuestId,
                    QuestStateName = "[ state name ]",

                    QuestEnds = new List<QuestEnd>(),
                    QuestStateRequirements = new List<QuestStateRequirement>()
                };
            } else
            {

            }

            QuestStateFormViewModel output = new QuestStateFormViewModel();
            output.QuestState = questState;
            output.ParentQuestState = repo.QuestStates.FirstOrDefault(q => q.Id == questState.ParentQuestStateId);
            output.ChildQuestStates = repo.QuestStates.Where(q => q.ParentQuestStateId == questState.Id);

            return PartialView(output);
        }

        public ActionResult QuestStateSend(QuestStateFormViewModel input)
        {

            int id = QuestWriterProcedures.SaveQuestState(input.QuestState);

            return RedirectToAction("QuestState", "QuestWriter", new { Id = id, QuestId = input.QuestState.QuestId, ParentStateId = input.QuestState.ParentQuestStateId });
        }

        public ActionResult QuestStateRequirement(int Id, int QuestStateId, int QuestId)
        {
            IQuestRepository repo = new EFQuestRepository();

            QuestStateRequirement questStateRequirement = repo.QuestStateRequirements.FirstOrDefault(q => q.Id == Id);
            QuestState state = repo.QuestStates.FirstOrDefault(q => q.Id == QuestStateId);

            if (questStateRequirement == null)
            {
                questStateRequirement = new QuestStateRequirement
                {
                   QuestId = QuestId,
                   QuestStateRequirementName = "",
                   
                };

                questStateRequirement.QuestStateId = state;
            }
            else
            {

            }

            QuestStateRequirementFormViewModel output = new QuestStateRequirementFormViewModel();
            output.QuestStateRequirement = questStateRequirement;
            output.ParentQuestState = state;

            return PartialView(output);
        }

        public ActionResult QuestStateRequirementSend(QuestStateRequirementFormViewModel input)
        {

            IQuestRepository repo = new EFQuestRepository();
            QuestState state = repo.QuestStates.FirstOrDefault(q => q.Id == input.ParentQuestState.Id);

            int savedId = QuestWriterProcedures.SaveQuestStateRequirement(input.QuestStateRequirement, state);

            return RedirectToAction("QuestStateRequirement", "QuestWriter", new { Id = savedId, QuestStateId = state.Id, QuestId = input.QuestStateRequirement.QuestId });
        }

        public ActionResult QuestStateRequirementDelete(int Id) //, int QuestStateId, int QuestId, int ParentStateId)
        {

            IQuestRepository repo = new EFQuestRepository();

            QuestStateRequirement questStateRequirement = repo.QuestStateRequirements.FirstOrDefault(q => q.Id == Id);
            QuestState state = repo.QuestStates.FirstOrDefault(q => q.Id == questStateRequirement.QuestStateId.Id);

            QuestWriterProcedures.DeleteQuestStateRequirement(Id);

            return RedirectToAction("QuestState", "QuestWriter", new { Id = questStateRequirement.QuestStateId.Id, QuestId = questStateRequirement.QuestId, ParentStateId = state.ParentQuestStateId });
        }

        public ActionResult QuestEnd(int Id, int QuestStateId, int QuestId)
        {
            IQuestRepository repo = new EFQuestRepository();

            QuestEnd questEnd = repo.QuestEnds.FirstOrDefault(q => q.Id == Id);
            QuestState state = repo.QuestStates.FirstOrDefault(q => q.Id == QuestStateId);

            if (questEnd == null)
            {
                questEnd = new QuestEnd
                {
                    QuestEndName = "[NAME NOT SET]",
                    QuestId = QuestId,
                    QuestStateId = state,
                };


            }
            else
            {

            }

            QuestEndFormViewModel output = new QuestEndFormViewModel();
            output.QuestEnd = questEnd;
            output.ParentQuestState = repo.QuestStates.FirstOrDefault(q => q.Id == QuestStateId);

            return PartialView(output);
        }

        public ActionResult QuestEndSend(QuestEndFormViewModel input)
        {

            IQuestRepository repo = new EFQuestRepository();
            QuestState state = repo.QuestStates.FirstOrDefault(q => q.Id == input.ParentQuestState.Id);

            int savedId = QuestWriterProcedures.SaveQuestEnd(input.QuestEnd, state);

            return RedirectToAction("QuestEnd", "QuestWriter", new { Id = savedId, QuestStateId = state.ParentQuestStateId, QuestId = input.QuestEnd.QuestId });
        }

        public ActionResult QuestEndDelete(int Id)
        {
            IQuestRepository repo = new EFQuestRepository();

            QuestEnd questEnd = repo.QuestEnds.FirstOrDefault(q => q.Id == Id);
            QuestState state = repo.QuestStates.FirstOrDefault(s => s.Id == questEnd.QuestStateId.Id);

            QuestWriterProcedures.DeleteQuestEnd(Id);

            return RedirectToAction("QuestState", "QuestWriter", new { Id = questEnd.QuestStateId.Id, QuestId = state.Id, ParentStateId = state.ParentQuestStateId  });
        }


    }
}