using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using System.Drawing.Printing;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepo _responseRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IRequestService _requestService;

        public static int PAGE_SIZE { get; set; } = 10;

        public ResponseService(IResponseRepo responseRepo, IRequestRepo requestRepo, IMapper mapper, IEmailService emailService, IRequestService requestService)
        {
            _responseRepo = responseRepo;
            _requestRepo = requestRepo;
            _mapper = mapper;
            _emailService = emailService;
            _requestService = requestService;
        }

        public async Task<ResponseApiDTO<IEnumerable<ResponseDTOWithData>>> GetAllAsync(string? search, string? requestId, DateTime? from, DateTime? to, string? sortBy, int page = 1)
        {
            var responses = await _responseRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                int id;
                if(int.TryParse(search, out id))
                {
                    responses = responses.Where(x => x.Id == id);
                }
                
            }

            if (!string.IsNullOrEmpty(requestId))
            {
                int id;
                if(int.TryParse(requestId, out id))
                {
                    responses = responses.Where(x => x.RequestId == id);
                }
            }

            if (from.HasValue)
            {
                responses = responses.Where(x => x.ResponseDate >= from.Value);
            }

            if (to.HasValue)
            {
                responses = responses.Where(x => x.ResponseDate <= to.Value);
            }

            responses = responses.OrderBy(x => x.ResponseDate);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "des": responses = responses.OrderByDescending(x => x.ResponseDate); break;
                }
            }

            var result = PaginatedList<Response>.Create(responses, page, PAGE_SIZE);

            if (result.IsNullOrEmpty())
            {
                return new ResponseApiDTO<IEnumerable<ResponseDTOWithData>>
                {
                    Success = true,
                    Message = "Hiện tại chưa có phản hồi nào."
                };
            }

            return new ResponseApiDTO<IEnumerable<ResponseDTOWithData>>
            {
                Success = true,
                Message = "Đây là danh sách các phản hồi.",
                Data = result.Select(x => new ResponseDTOWithData
                {
                    Id = x.Id,
                    RequestId = x.RequestId,
                    CategoryName = x.Request.Category.CategoryName,
                    FullName = x.Request.User.FullName,
                    ResponseDate = x.ResponseDate,
                    Description = x.Description,
                    IsLocked = x.Request.User.IsLocked,
                }),
                Total = result.Count
            };
        }

        public async Task<ResponseApiDTO> SendResponseAsync(int requestId, ResponseContentDTO responseContent)
        {
            var request = await _requestRepo.GetByIdAsync(requestId);

            if(request == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Hệ thống không tìm thấy yêu cầu nào với Id: {requestId}"
                };
            }

            var userEmail = request.User.Email;

            var message = new EmailDTO
                (
                    new string[] {userEmail!},
                    responseContent.Title,
                    responseContent.Content!
                );
            var sendMail = _emailService.SendEmail(message);

            if(!sendMail.Success) 
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống lỗi khi gửi phản hồi cho người dùng."
                };
            }

            //add to db
            var responseDTO = new ResponseDTO { Description = responseContent.Title };
            var response = _mapper.Map<Response>(responseDTO);
            response.RequestId = requestId;
            var createResponse = await _responseRepo.CreateAsync(response);

            if(!createResponse)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi lưu lại phản hồi cho yêu cầu."
                };
            }

            // Cập nhật trạng thái của request thành "Đang xử lý"
            request.Status = "Đang xử lý";
            var updateRequest = await _requestRepo.UpdateAsync(request);

            if (!updateRequest)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hệ thống gặp lỗi khi cập nhật trạng thái của yêu cầu."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Phản hồi đã được gửi và lưu thành công."
            };
        }

        public async Task<ResponseApiDTO> DeleteAsync(int id)
        {
            var responses = await _responseRepo.GetAllAsync();
            var response = responses.FirstOrDefault(x => x.Id == id);
            if(response == null)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Không tìm thấy phản hồi trong hệ thống."
                };
            }

            var delete = await _responseRepo.DeleteAsync(response);
            if(!delete)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Lỗi xảy ra khi xóa phản hồi."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Xóa phản hồi thành công."
            };
        }
    }
}
