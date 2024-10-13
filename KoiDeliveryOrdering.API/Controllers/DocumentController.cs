using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Data.Dtos.Documents;
using KoiDeliveryOrdering.Service.Interfaces;
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
        public async Task<IServiceResult> GetAll()
        {
            return await _documentService.GetAll();
        }

        [HttpPost(ApiRoute.Document.CreateDocument)]
        public async Task<IServiceResult> CreateDocument([FromBody] DocumentMutationDto dto)
        {
            return await _documentService.CreateDocument(dto);
        }
    }
}