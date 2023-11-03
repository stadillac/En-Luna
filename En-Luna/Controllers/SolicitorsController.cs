using AutoMapper;
using En_Luna.Data.Models;
using En_Luna.Data.Services;
using En_Luna.Extensions;
using En_Luna.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace En_Luna.Controllers
{
    public class SolicitorsController : Controller
    {
        // create / edit / delete
        // view my solicitations (filter on page)

        private readonly IMapper _mapper;
        private readonly ISolicitationService _solicitationService;


        public SolicitorsController(IMapper mapper, ISolicitationService solicitationService)
        {
            _mapper = mapper;
            _solicitationService = solicitationService;
        }

        [HttpGet("{id:int}")]
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

        public IActionResult Edit(int? id)
        {
            Solicitation? solicitation = id.HasValue
                ? _solicitationService.Get(x => x.Id == id.Value)
                : new Solicitation();

            if (solicitation == null)
            {
                return RedirectToAction("Index");
            }

            SolicitationEditViewModel model = _mapper.Map<SolicitationEditViewModel>(solicitation);
            //InstantiateSelectLists(model);

            return View(model);
        }

        public IActionResult Edit(SolicitationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //InstantiateSelectLists(model);
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

    }
}
