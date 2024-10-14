using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Dtos.Documents;

namespace KoiDeliveryOrdering.Service.Interfaces
{
    public interface IDocumentService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> CreateDocument(DocumentMutationDto dto);
        Task<ServiceResult> UpdateDocument(Guid id, DocumentMutationDto dto);
    }
}