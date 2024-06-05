using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;

namespace OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl
{
    public class RequestService : IRequestService
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepo _requestRepo;
        private readonly MyDbContext _context;

        public static int PAGE_SIZE { get; set; } = 5;
        public RequestService(IMapper mapper, IRequestRepo requestRepo, MyDbContext context)
        {
            _mapper = mapper;
            _requestRepo = requestRepo;
            _context = context;
        }

        public async Task<ResponseDTO> CreateRequestAsync(RequestDTO userRequest)
        {
            /*
             service layer would validate the constraint request
             which mean validate relationship related to RequestDTO
             */

            // check requestCategory is exist or not then go ahead

            // check duplicate request 
            var existedRequest = await _requestRepo.GetRequestByRequestIDAsync(userRequest.Id);
            if (existedRequest != null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Request has already exist",
                };
            }
            // convert dto to entity
            // assume valid request
            var request = _mapper.Map<Request>(userRequest);
            // forward request to the last layer ( Persistant Layer: layer handle request between app and DB )
            Task<int> successRow = _requestRepo.AddRequestAsync(request);
            if (successRow.GetAwaiter().GetResult() > 0)
            {
                return new ResponseDTO
                {
                    Success = true,
                    Message = "Add request successfully."
                };
            }
            return new ResponseDTO
            {
                Success = false,
                Message = "Can not add request.",
            };

        }

        public async Task<ResponseDTO> DeleteRequestAsync(int id)
        {
            var deletedRequest = await _requestRepo.GetRequestByRequestIDAsync(id);
            if (deletedRequest == null)
            {
                return new ResponseDTO()
                {
                    Success = false,
                    Message = "Request not found."
                };
            }

            try
            {
                await _requestRepo.DeleteRequestAsync(deletedRequest);
                await _context.SaveChangesAsync();
                return new ResponseDTO()
                {
                    Success = true,
                    Message = "Delete request successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = $"Delete request failed. Please try again. Error: {ex.Message}"
                };
            }
        }
        public async Task<ResponseDTO<IEnumerable<RequestDTO>>> GetAllRequestAsync(string? search, string? sortBy, int pageIndex = 1)
        {
            var allRequests = await _requestRepo.GetAllRequestsAsync();

            // Search by description if search string is not null or empty
            if (!string.IsNullOrEmpty(search))
            {
                allRequests = allRequests.Where(u => u.Description.Contains(search));
            }

            // Default sort by CreatedDate

            // Sort by CreatedDate in descending order if sortBy is "cc"
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "cc":
                        allRequests = allRequests.OrderByDescending(u => u.CreatedDate);
                        break;
                    default:
                        // Add more sorting options as needed
                        break;
                }
            }

            // Paginate the results
            var result = PaginatedList<Request>.Create(allRequests, pageIndex, PAGE_SIZE);

            // Check if the result is empty
            if (!result.Any())
            {
                return new ResponseDTO<IEnumerable<RequestDTO>>
                {
                    Success = true,
                    Message = "Does not have any result match your request."
                };
            }

            // Map the users to UserResponseDTOs
            var requestResponseDTOs = new List<RequestDTO>();

            return new ResponseDTO<IEnumerable<RequestDTO>>
            {
                Success = true,
                Message = "Here is current users match your request.",
            };
        }

        public async Task<ResponseDTO<IEnumerable<RequestDTO>>> GetRequestByRequestIDAsync(int id, string? search, string? sortBy, int pageIndex = 1)
        {
            // Fetch the request by ID
            var request = await _requestRepo.GetRequestByRequestIDAsync(id);

            if (request == null)
            {
                return new ResponseDTO<IEnumerable<RequestDTO>>
                {
                    Success = false,
                    Message = "Request not found."
                };
            }

            // Convert the single request to a list to apply search and sort operations
            var allRequests = new List<Request> { request };

            // Apply search criteria if provided
            if (!string.IsNullOrEmpty(search))
            {
                allRequests = allRequests.Where(r => r.Description.Contains(search)).ToList();
            }

            // Apply sorting if provided
            switch (sortBy)
            {
                case "cc":
                    allRequests = allRequests.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
                default:
                    // Default sorting by CreatedDate
                    allRequests = allRequests.OrderBy(r => r.CreatedDate).ToList();
                    break;
            }

            // Paginate the results
            const int PAGE_SIZE = 10; // Define page size
            var paginatedRequests = allRequests.Skip((pageIndex - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();

            // Map the results to RequestDTO
            var requestDTOs = _mapper.Map<IEnumerable<RequestDTO>>(paginatedRequests);

            return new ResponseDTO<IEnumerable<RequestDTO>>
            {
                Success = true,
                Message = "Request retrieved successfully.",
            };
        }


        public async Task<ResponseDTO<IEnumerable<RequestDTO>>> GetRequestByUserIDAsync(string userId, string? search, string? sortBy, int pageIndex = 1)
        {
            // Fetch requests by user ID
            var request = await _requestRepo.GetRequestByUserIDAsync(userId);

            var allRequests = new List<Request> { request };

            // Apply search criteria if provided
            if (!string.IsNullOrEmpty(search))
            {
                allRequests = allRequests.Where(r => r.Description.Contains(search)).ToList();
            }

            // Apply sorting if provided
            switch (sortBy)
            {
                case "cc":
                    allRequests = allRequests.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
                default:
                    // Default sorting by CreatedDate
                    allRequests = allRequests.OrderBy(r => r.CreatedDate).ToList();
                    break;
            }

            // Paginate the results
            const int PAGE_SIZE = 10; // Define page size
            var paginatedRequests = allRequests.Skip((pageIndex - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();

            // Map the results to RequestDTO
            var requestDTOs = _mapper.Map<IEnumerable<RequestDTO>>(paginatedRequests);

            return new ResponseDTO<IEnumerable<RequestDTO>>
            {
                Success = true,
                Message = "Requests retrieved successfully."
            };
        }


        public async Task<ResponseDTO> UpdateRequestAsync(int id, RequestDTO requestDTO)
        {
            var existingRequest = await _context.Requests.FindAsync(id);
            if (existingRequest == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = "Request not found."
                };
            }

            // Update the existing request properties
            _context.Entry(existingRequest).CurrentValues.SetValues(requestDTO);

            try
            {
                await _context.SaveChangesAsync();
                return new ResponseDTO
                {
                    Success = true,
                    Message = "Update request successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Message = $"Update request failed: {ex.Message}"
                };
            }
        }
    }
}