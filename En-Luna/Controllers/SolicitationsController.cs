using AutoMapper;
using En_Luna.Data.Models;
using En_Luna.Data.Services;
using En_Luna.Extensions;
using En_Luna.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace En_Luna.Controllers
{
    [Route("Solicitations")]
    [Authorize(Roles = "Solicitor")]
    public class SolicitationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDeadlineTypeService _deadlineTypeService;
        private readonly IProjectDeliverableService _projectDeliverableService;
        private readonly ISolicitationService _solicitationService;
        private readonly IStateService _stateService;
        private readonly UserManager<User> _userManager;

        public SolicitationsController(IMapper mapper, IDeadlineTypeService deadlineTypeService, IProjectDeliverableService projectDeliverableService,
            ISolicitationService solicitationService, IStateService stateService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _deadlineTypeService = deadlineTypeService;
            _projectDeliverableService = projectDeliverableService;
            _solicitationService = solicitationService;
            _stateService = stateService;
            _userManager = userManager;
        }

        [HttpGet("{id:int}/{page:int?}")]
        public IActionResult Index(int id, int? page)
        {
            IEnumerable<Solicitation> solicitations = _solicitationService.List(x => x.SolicitorId == id);

            IPagedList<SolicitationViewModel> solicitationViewModels = solicitations
                .ToPagedList(page ?? 1, Constants.Constants.PageSize)
                .Map<Solicitation, SolicitationViewModel>(_mapper);

            SolicitationIndexViewModel model = new SolicitationIndexViewModel
            {
                Solicitations = solicitationViewModels
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var isSolicitor = await _userManager.IsInRoleAsync(user, "Solicitor");
            
            if (!isSolicitor || user.SolicitorId == null)
            {
                return RedirectToAction("Index");
            }

            Solicitation? solicitation = id.HasValue
                ? _solicitationService.Get(x => x.Id == id.Value)
                : new Solicitation();

            if (solicitation == null)
            {
                return RedirectToAction("Index");
            }

            solicitation.SolicitorId = user.SolicitorId.Value;

            SolicitationEditViewModel model = _mapper.Map<SolicitationEditViewModel>(solicitation);
            InstantiateSelectLists(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SolicitationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                InstantiateSelectLists(model);
                return View(model);
            }

            if (model.Id != 0)
            {
                Solicitation? solicitation = _solicitationService.Get(x => x.Id == model.Id);
                _mapper.Map(model, solicitation);
                _solicitationService.Update(solicitation);
            }
            else
            {
                Solicitation solicitation = _mapper.Map<Solicitation>(model);
                _solicitationService.Create(solicitation);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Activate(int id)
        {
            //todo add logic that informs admin of activity

            Solicitation? solicitation = _solicitationService.Get(x => x.Id == id);

            if (solicitation == null)
            {
                return Json(false);
            }

            _solicitationService.Activate(solicitation);

            return Json(true);
        }

        public JsonResult Deactivate(int id)
        {
            //todo add logic that informs admin of activity

            Solicitation? solicitation = _solicitationService.Get(x => x.Id == id);

            if (solicitation == null)
            {
                return Json(false);
            }

            _solicitationService.Deactivate(solicitation);

            return Json(true);
        }

        public JsonResult Complete(int id, bool isComplete)
        {
            //todo add logic that informs admin of activity

            Solicitation? solicitation = _solicitationService.Get(x => x.Id == id);

            if (solicitation == null)
            {
                return Json(false);
            }

            _solicitationService.Complete(solicitation, isComplete);

            return Json(true);
        }

        public JsonResult Cancel(int id, bool isCancelled)
        {
            //todo add logic that informs admin of activity

            Solicitation? solicitation = _solicitationService.Get(x => x.Id == id);

            if (solicitation == null)
            {
                return Json(false);
            }

            _solicitationService.Cancel(solicitation, isCancelled);

            return Json(true);
        }

        private void InstantiateSelectLists(SolicitationEditViewModel model)
        {
            model.SolicitationDeadline.DeadlineTypes = new SelectList(
                _deadlineTypeService.List(),
                "Id",
                "Name",
                model.SolicitationDeadline.DeadlineTypeId
            );

            model.SolicitationRole.ProjectDeliverables = new SelectList(
                _projectDeliverableService.List(), 
                "Id", 
                "Name", 
                model.SolicitationRole.ProjectDeliverableId
            );

            model.States = new SelectList(_stateService.List(), "Id", "Name", model.StateId);
        }

    }
}
