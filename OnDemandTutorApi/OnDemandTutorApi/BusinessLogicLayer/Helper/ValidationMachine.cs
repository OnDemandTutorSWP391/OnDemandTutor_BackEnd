using Microsoft.IdentityModel.Tokens;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnDemandTutorApi.BusinessLogicLayer.Helper
{
    public static class ValidationMachine
    {
        public static ResponseApiDTO CheckValidRegister(UserRequestDTO userRequest)
        {
            var fullName = userRequest.FullName;
            var email = userRequest.Email;
            var password = userRequest.Password;
            var confirmPass = userRequest.ConfirmedPassword;
            var identityCard = userRequest.IdentityCard;
            var dob = userRequest.Dob;
            var phone = userRequest.PhoneNumber;
            var gender = userRequest.Gender;
            var avatar = userRequest.Avatar;
            var role = userRequest.Role;


            //====check null or empty
            if(fullName.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Họ và tên không được để trống."
                };
            }

            if (email.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Email không được để trống"
                };
            }

            if (password.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Mật khẩu không được để trống"
                };
            }

            if (confirmPass.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Xác nhận mật khẩu không được để trống."
                };
            }

            if (identityCard.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "CCCD/CMND không được để trống."
                };
            }

            if (phone.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Số điện thoại không được để trống."
                };
            }

            if (role.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hãy chọn vai trò của bạn."
                };
            }
            
            //===check valid email
            if(!new EmailAddressAttribute().IsValid(email))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Địa chỉ Email: {email} không hợp lệ."
                };
            }

            //===check valid password
            if (password.Length < 6)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Mật khẩu phải có ít nhất 6 ký tự."
                };
            }

            if (!password.Any(char.IsDigit))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Mật khẩu phải chứa ít nhất một ký tự số."
                };
            }

            if (!password.Any(char.IsUpper))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Mật khẩu phải chứa ít nhất một ký tự in hoa."
                };
            }

            if (!password.Any(char.IsLower))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Mật khẩu phải chứa ít nhất một ký tự in thường."
                };
            }

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Mật khẩu phải chứa ít nhất một ký tự đặc biệt."
                };
            }

            //===check confirmpass match pass
            if(!confirmPass.Equals(password))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Xác nhận mật khẩu không trùng với mật khẩu của bạn."
                };
            }

            //===check valid IdentityCard
            if (IsValidIdentityCardNumber(identityCard))
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Định dạng CCCD không hợp lệ."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Hợp lệ."
            };
        }

        public static ResponseApiDTO CheckValidAuthen(UserAuthenDTO userAuthen)
        {
            var email = userAuthen.Email;
            var password = userAuthen.Password;

            if(email.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = $"Email và mật khẩu không được để trống."
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
                Message = "Hợp lệ."
            };
        }

        public static ResponseApiDTO CheckValidCreateSubjectLevel(SubjectLevelRequestDTO subjectLevelRequest)
        {
            int? level = subjectLevelRequest.LevelId;
            int? subject = subjectLevelRequest.SubjectId;
            string? name = subjectLevelRequest.Name;
            string? description = subjectLevelRequest.Description;
            string? url = subjectLevelRequest.Url;
            float? coin = subjectLevelRequest.Coin;
            int? limitMember = subjectLevelRequest.LevelId;

            if(!level.HasValue)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Cấp bậc của khóa học không được để trống."
                };
            }

            if (!subject.HasValue)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Môn học của khóa học không được để trống."
                };
            }

            if(name.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Tên khóa học không được để trống."
                };
            }

            if (description.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Phần mô tả/giáo trình khóa học không được để trống."
                };
            }

            if (url.IsNullOrEmpty())
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Hãy thêm đường dẫn của khóa học."
                };
            }

            if(!coin.HasValue)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Số coin của khóa học không được để trống."
                };
            }

            if (!limitMember.HasValue)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Giới hạn thành viên của khóa học không được để trống."
                };
            }

            if(limitMember <= 0 || limitMember >= 5)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Để đảm bảo chất lượng giảng dạy, giới hạn thành viên phải lớn hơn 0 và nhỏ hơn hoặc bằng 5"
                };
            }

            return new ResponseApiDTO
            {
                Success = true,
            };
        }

        public static ResponseApiDTO CheckValidCoin(float? coin)
        {
            if(!coin.HasValue)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Số coin không được để trống."
                };
            }

            if(coin <= 0 || coin >= 10000)
            {
                return new ResponseApiDTO
                {
                    Success = false,
                    Message = "Số coin hợp lệ nằm trong khoản lớn hơn 0 và nhỏ hơn 10000."
                };
            }

            return new ResponseApiDTO
            {
                Success = true
            };
        }

        static bool IsValidIdentityCardNumber(string identityCardNumber)
        {
            string pattern = @"^0(0[1-9]|[1-8][0-9]|9[0-6])[0-3]([0-9][0-9])[0-9]{6}$";
            return Regex.IsMatch(identityCardNumber, pattern);
        }
    }
}
