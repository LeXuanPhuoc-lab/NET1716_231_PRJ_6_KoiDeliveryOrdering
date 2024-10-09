using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Data.Repositories;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Business
{
    public class DocumentService : IDocumentService
    {
        private readonly UnitOfWork _unitOfWork;

        public DocumentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<ServiceResult> GetAllAsync()
		{
            try
            {
                var documents = await _unitOfWork.DocumentRepository.FindAllAsync();

                if (documents.Any())
                {
                    return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, documents.ToList());
                }
                else
                {
					return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, new List<Document>());
				}
			}
			catch(Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
		}
	}
}
