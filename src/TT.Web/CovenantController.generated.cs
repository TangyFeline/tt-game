// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using TT.Web.Controllers.Generated;
namespace TT.Web.Controllers
{
    public partial class CovenantController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CovenantController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CovenantController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ApplyToCovenant()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ApplyToCovenant);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ApplicationResponse()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ApplicationResponse);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ChangeNoticeboardMessageSubmit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeNoticeboardMessageSubmit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ChangeCovenantDescriptionSubmit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeCovenantDescriptionSubmit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult StartNewCovenantSubmit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.StartNewCovenantSubmit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult LookAtCovenant()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LookAtCovenant);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult KickMember()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.KickMember);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddToCovenantChest()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToCovenantChest);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GiveMoneyFromCovenantChest()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GiveMoneyFromCovenantChest);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PurchaseFurniture()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PurchaseFurniture);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UseFurniture()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UseFurniture);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InviteCaptainSend()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InviteCaptainSend);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InviteLeaderSend()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InviteLeaderSend);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CovenantController Actions { get { return MVC.Covenant; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "covenant";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "covenant";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string MyCovenant = ("MyCovenant").ToLowerInvariant();
            public readonly string CovenantList = ("CovenantList").ToLowerInvariant();
            public readonly string ApplyToCovenant = ("ApplyToCovenant").ToLowerInvariant();
            public readonly string ReviewMyCovenantApplications = ("ReviewMyCovenantApplications").ToLowerInvariant();
            public readonly string ApplicationResponse = ("ApplicationResponse").ToLowerInvariant();
            public readonly string LeaveCovenant = ("LeaveCovenant").ToLowerInvariant();
            public readonly string ChangeNoticeboardMessage = ("ChangeNoticeboardMessage").ToLowerInvariant();
            public readonly string ChangeNoticeboardMessageSubmit = ("ChangeNoticeboardMessageSubmit").ToLowerInvariant();
            public readonly string ChangeCovenantDescription = ("ChangeCovenantDescription").ToLowerInvariant();
            public readonly string ChangeCovenantDescriptionSubmit = ("ChangeCovenantDescriptionSubmit").ToLowerInvariant();
            public readonly string StartNewCovenant = ("StartNewCovenant").ToLowerInvariant();
            public readonly string StartNewCovenantSubmit = ("StartNewCovenantSubmit").ToLowerInvariant();
            public readonly string LookAtCovenant = ("LookAtCovenant").ToLowerInvariant();
            public readonly string WithdrawApplication = ("WithdrawApplication").ToLowerInvariant();
            public readonly string CovenantLeaderAdmin = ("CovenantLeaderAdmin").ToLowerInvariant();
            public readonly string KickList = ("KickList").ToLowerInvariant();
            public readonly string KickMember = ("KickMember").ToLowerInvariant();
            public readonly string AddToCovenantChest = ("AddToCovenantChest").ToLowerInvariant();
            public readonly string GiveMoneyFromCovenantChest = ("GiveMoneyFromCovenantChest").ToLowerInvariant();
            public readonly string ClaimLocation = ("ClaimLocation").ToLowerInvariant();
            public readonly string ClaimLocationSend = ("ClaimLocationSend").ToLowerInvariant();
            public readonly string UpgradeSafeground = ("UpgradeSafeground").ToLowerInvariant();
            public readonly string ViewAvailableFurniture = ("ViewAvailableFurniture").ToLowerInvariant();
            public readonly string PurchaseFurniture = ("PurchaseFurniture").ToLowerInvariant();
            public readonly string MyCovenantFurniture = ("MyCovenantFurniture").ToLowerInvariant();
            public readonly string UseFurniture = ("UseFurniture").ToLowerInvariant();
            public readonly string MyCovenantLog = ("MyCovenantLog").ToLowerInvariant();
            public readonly string InviteCaptainList = ("InviteCaptainList").ToLowerInvariant();
            public readonly string InviteCaptainSend = ("InviteCaptainSend").ToLowerInvariant();
            public readonly string InviteLeaderList = ("InviteLeaderList").ToLowerInvariant();
            public readonly string InviteLeaderSend = ("InviteLeaderSend").ToLowerInvariant();
        }


        static readonly ActionParamsClass_ApplyToCovenant s_params_ApplyToCovenant = new ActionParamsClass_ApplyToCovenant();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ApplyToCovenant ApplyToCovenantParams { get { return s_params_ApplyToCovenant; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ApplyToCovenant
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ActionParamsClass_ApplicationResponse s_params_ApplicationResponse = new ActionParamsClass_ApplicationResponse();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ApplicationResponse ApplicationResponseParams { get { return s_params_ApplicationResponse; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ApplicationResponse
        {
            public readonly string id = ("id").ToLowerInvariant();
            public readonly string response = ("response").ToLowerInvariant();
        }
        static readonly ActionParamsClass_ChangeNoticeboardMessageSubmit s_params_ChangeNoticeboardMessageSubmit = new ActionParamsClass_ChangeNoticeboardMessageSubmit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangeNoticeboardMessageSubmit ChangeNoticeboardMessageSubmitParams { get { return s_params_ChangeNoticeboardMessageSubmit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangeNoticeboardMessageSubmit
        {
            public readonly string input = ("input").ToLowerInvariant();
        }
        static readonly ActionParamsClass_ChangeCovenantDescriptionSubmit s_params_ChangeCovenantDescriptionSubmit = new ActionParamsClass_ChangeCovenantDescriptionSubmit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangeCovenantDescriptionSubmit ChangeCovenantDescriptionSubmitParams { get { return s_params_ChangeCovenantDescriptionSubmit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangeCovenantDescriptionSubmit
        {
            public readonly string input = ("input").ToLowerInvariant();
        }
        static readonly ActionParamsClass_StartNewCovenantSubmit s_params_StartNewCovenantSubmit = new ActionParamsClass_StartNewCovenantSubmit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_StartNewCovenantSubmit StartNewCovenantSubmitParams { get { return s_params_StartNewCovenantSubmit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_StartNewCovenantSubmit
        {
            public readonly string input = ("input").ToLowerInvariant();
        }
        static readonly ActionParamsClass_LookAtCovenant s_params_LookAtCovenant = new ActionParamsClass_LookAtCovenant();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_LookAtCovenant LookAtCovenantParams { get { return s_params_LookAtCovenant; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_LookAtCovenant
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ActionParamsClass_KickMember s_params_KickMember = new ActionParamsClass_KickMember();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_KickMember KickMemberParams { get { return s_params_KickMember; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_KickMember
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ActionParamsClass_AddToCovenantChest s_params_AddToCovenantChest = new ActionParamsClass_AddToCovenantChest();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToCovenantChest AddToCovenantChestParams { get { return s_params_AddToCovenantChest; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToCovenantChest
        {
            public readonly string amount = ("amount").ToLowerInvariant();
        }
        static readonly ActionParamsClass_GiveMoneyFromCovenantChest s_params_GiveMoneyFromCovenantChest = new ActionParamsClass_GiveMoneyFromCovenantChest();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GiveMoneyFromCovenantChest GiveMoneyFromCovenantChestParams { get { return s_params_GiveMoneyFromCovenantChest; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GiveMoneyFromCovenantChest
        {
            public readonly string id = ("id").ToLowerInvariant();
            public readonly string amount = ("amount").ToLowerInvariant();
        }
        static readonly ActionParamsClass_PurchaseFurniture s_params_PurchaseFurniture = new ActionParamsClass_PurchaseFurniture();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PurchaseFurniture PurchaseFurnitureParams { get { return s_params_PurchaseFurniture; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PurchaseFurniture
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ActionParamsClass_UseFurniture s_params_UseFurniture = new ActionParamsClass_UseFurniture();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UseFurniture UseFurnitureParams { get { return s_params_UseFurniture; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UseFurniture
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ActionParamsClass_InviteCaptainSend s_params_InviteCaptainSend = new ActionParamsClass_InviteCaptainSend();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InviteCaptainSend InviteCaptainSendParams { get { return s_params_InviteCaptainSend; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InviteCaptainSend
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ActionParamsClass_InviteLeaderSend s_params_InviteLeaderSend = new ActionParamsClass_InviteLeaderSend();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InviteLeaderSend InviteLeaderSendParams { get { return s_params_InviteLeaderSend; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InviteLeaderSend
        {
            public readonly string id = ("id").ToLowerInvariant();
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string ChangeCovenantDescription = "ChangeCovenantDescription";
                public readonly string ChangeNoticeboardMessage = "ChangeNoticeboardMessage";
                public readonly string ClaimLocation = "ClaimLocation";
                public readonly string CovenantLeaderAdmin = "CovenantLeaderAdmin";
                public readonly string CovenantList = "CovenantList";
                public readonly string InviteCaptainList = "InviteCaptainList";
                public readonly string InviteLeaderList = "InviteLeaderList";
                public readonly string KickList = "KickList";
                public readonly string LookAtCovenant = "LookAtCovenant";
                public readonly string MyCovenant = "MyCovenant";
                public readonly string MyCovenantFurniture = "MyCovenantFurniture";
                public readonly string MyCovenantLog = "MyCovenantLog";
                public readonly string ReviewMyCovenantApplications = "ReviewMyCovenantApplications";
                public readonly string StartNewCovenant = "StartNewCovenant";
                public readonly string ViewAvailableFurniture = "ViewAvailableFurniture";
            }
            public readonly string ChangeCovenantDescription = "~/Views/Covenant/ChangeCovenantDescription.cshtml";
            public readonly string ChangeNoticeboardMessage = "~/Views/Covenant/ChangeNoticeboardMessage.cshtml";
            public readonly string ClaimLocation = "~/Views/Covenant/ClaimLocation.cshtml";
            public readonly string CovenantLeaderAdmin = "~/Views/Covenant/CovenantLeaderAdmin.cshtml";
            public readonly string CovenantList = "~/Views/Covenant/CovenantList.cshtml";
            public readonly string InviteCaptainList = "~/Views/Covenant/InviteCaptainList.cshtml";
            public readonly string InviteLeaderList = "~/Views/Covenant/InviteLeaderList.cshtml";
            public readonly string KickList = "~/Views/Covenant/KickList.cshtml";
            public readonly string LookAtCovenant = "~/Views/Covenant/LookAtCovenant.cshtml";
            public readonly string MyCovenant = "~/Views/Covenant/MyCovenant.cshtml";
            public readonly string MyCovenantFurniture = "~/Views/Covenant/MyCovenantFurniture.cshtml";
            public readonly string MyCovenantLog = "~/Views/Covenant/MyCovenantLog.cshtml";
            public readonly string ReviewMyCovenantApplications = "~/Views/Covenant/ReviewMyCovenantApplications.cshtml";
            public readonly string StartNewCovenant = "~/Views/Covenant/StartNewCovenant.cshtml";
            public readonly string ViewAvailableFurniture = "~/Views/Covenant/ViewAvailableFurniture.cshtml";
            static readonly _partialClass s_partial = new _partialClass();
            public _partialClass partial { get { return s_partial; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _partialClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string CovenantFurnitureViewModel = "CovenantFurnitureViewModel";
                    public readonly string FurnitureViewModel = "FurnitureViewModel";
                }
                public readonly string CovenantFurnitureViewModel = "~/Views/Covenant/partial/CovenantFurnitureViewModel.cshtml";
                public readonly string FurnitureViewModel = "~/Views/Covenant/partial/FurnitureViewModel.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CovenantController : TT.Web.Controllers.CovenantController
    {
        public T4MVC_CovenantController() : base(Dummy.Instance) { }

        [NonAction]
        partial void MyCovenantOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MyCovenant()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MyCovenant);
            MyCovenantOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CovenantListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult CovenantList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CovenantList);
            CovenantListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ApplyToCovenantOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult ApplyToCovenant(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ApplyToCovenant);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ApplyToCovenantOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ReviewMyCovenantApplicationsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ReviewMyCovenantApplications()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ReviewMyCovenantApplications);
            ReviewMyCovenantApplicationsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ApplicationResponseOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, string response);

        [NonAction]
        public override System.Web.Mvc.ActionResult ApplicationResponse(int id, string response)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ApplicationResponse);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "response", response);
            ApplicationResponseOverride(callInfo, id, response);
            return callInfo;
        }

        [NonAction]
        partial void LeaveCovenantOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult LeaveCovenant()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LeaveCovenant);
            LeaveCovenantOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChangeNoticeboardMessageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeNoticeboardMessage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeNoticeboardMessage);
            ChangeNoticeboardMessageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChangeNoticeboardMessageSubmitOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, TT.Domain.Models.Covenant input);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeNoticeboardMessageSubmit(TT.Domain.Models.Covenant input)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeNoticeboardMessageSubmit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "input", input);
            ChangeNoticeboardMessageSubmitOverride(callInfo, input);
            return callInfo;
        }

        [NonAction]
        partial void ChangeCovenantDescriptionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeCovenantDescription()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeCovenantDescription);
            ChangeCovenantDescriptionOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChangeCovenantDescriptionSubmitOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, TT.Domain.Models.Covenant input);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeCovenantDescriptionSubmit(TT.Domain.Models.Covenant input)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeCovenantDescriptionSubmit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "input", input);
            ChangeCovenantDescriptionSubmitOverride(callInfo, input);
            return callInfo;
        }

        [NonAction]
        partial void StartNewCovenantOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult StartNewCovenant()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.StartNewCovenant);
            StartNewCovenantOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void StartNewCovenantSubmitOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, TT.Domain.Models.Covenant input);

        [NonAction]
        public override System.Web.Mvc.ActionResult StartNewCovenantSubmit(TT.Domain.Models.Covenant input)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.StartNewCovenantSubmit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "input", input);
            StartNewCovenantSubmitOverride(callInfo, input);
            return callInfo;
        }

        [NonAction]
        partial void LookAtCovenantOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult LookAtCovenant(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LookAtCovenant);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            LookAtCovenantOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void WithdrawApplicationOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult WithdrawApplication()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WithdrawApplication);
            WithdrawApplicationOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CovenantLeaderAdminOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult CovenantLeaderAdmin()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CovenantLeaderAdmin);
            CovenantLeaderAdminOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void KickListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult KickList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.KickList);
            KickListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void KickMemberOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult KickMember(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.KickMember);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            KickMemberOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AddToCovenantChestOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int amount);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddToCovenantChest(int amount)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToCovenantChest);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "amount", amount);
            AddToCovenantChestOverride(callInfo, amount);
            return callInfo;
        }

        [NonAction]
        partial void GiveMoneyFromCovenantChestOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, decimal amount);

        [NonAction]
        public override System.Web.Mvc.ActionResult GiveMoneyFromCovenantChest(int id, decimal amount)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GiveMoneyFromCovenantChest);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "amount", amount);
            GiveMoneyFromCovenantChestOverride(callInfo, id, amount);
            return callInfo;
        }

        [NonAction]
        partial void ClaimLocationOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ClaimLocation()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ClaimLocation);
            ClaimLocationOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ClaimLocationSendOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ClaimLocationSend()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ClaimLocationSend);
            ClaimLocationSendOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UpgradeSafegroundOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpgradeSafeground()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpgradeSafeground);
            UpgradeSafegroundOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ViewAvailableFurnitureOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ViewAvailableFurniture()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ViewAvailableFurniture);
            ViewAvailableFurnitureOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PurchaseFurnitureOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult PurchaseFurniture(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PurchaseFurniture);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PurchaseFurnitureOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void MyCovenantFurnitureOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MyCovenantFurniture()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MyCovenantFurniture);
            MyCovenantFurnitureOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UseFurnitureOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult UseFurniture(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UseFurniture);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            UseFurnitureOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void MyCovenantLogOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MyCovenantLog()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MyCovenantLog);
            MyCovenantLogOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InviteCaptainListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult InviteCaptainList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InviteCaptainList);
            InviteCaptainListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InviteCaptainSendOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InviteCaptainSend(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InviteCaptainSend);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InviteCaptainSendOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InviteLeaderListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult InviteLeaderList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InviteLeaderList);
            InviteLeaderListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InviteLeaderSendOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InviteLeaderSend(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InviteLeaderSend);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InviteLeaderSendOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
