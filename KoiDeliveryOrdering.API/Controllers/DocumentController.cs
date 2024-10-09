using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers
{
    [ApiController]
    public class DocumentController : ControllerBase
    {
		private readonly IDocumentService _documentService;

		public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet(ApiRoute.Document.GetAll)]
        public async Task<IServiceResult> GetAllAsync()
        {
            return await _documentService.GetAllAsync();
        }
    }
}
