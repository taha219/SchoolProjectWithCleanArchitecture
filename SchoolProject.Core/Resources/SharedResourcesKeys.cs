﻿namespace SchoolProject.Core.Resources
{
    public class SharedResourcesKeys
    {
        #region General
        public const string NotFound = "NotFound";
        public const string Success = "Success";
        public const string NotEmpty = "NotEmpty";
        public const string Required = "Required";
        #endregion

        #region validation messages
        public const string NameRequired = "NameRequired";
        public const string Namelength = "Namelength";
        public const string AddressRequired = "AddressRequired";
        public const string Addresslength = "Addresslength";
        public const string PhoneRequired = "PhoneRequired";
        public const string PhoneNotVaild = "PhoneNotVaild";
        public const string EmailRequired = "EmailRequired";
        public const string EmailNotVaild = "EmailNotVaild";
        public const string PasswordNotValid = "PasswordNotValid";
        public const string PasswordRequired = "PasswordRequired";
        #endregion 

        #region Student
        public const string NotFoundStudent = "NotFoundStudent";
        public const string DeletedStudent = "DeletedStudent";
        public const string DeletedFailedStudent = "DeletedFailedStudent";
        public const string GetStudent = "GetStudent";
        public const string GetStudentList = "GetStudentList";
        public const string EditStudent = "EditStudent";
        public const string EditStudentDepartment = "EditStudentDepartment";
        #endregion

        #region Department
        public const string NotFoundDepartment = "NotFoundDepartment";
        public const string GetDepartmentByID = "GetDepartmentByID";
        public const string DepartmentIsNotExist = "DepartmentIsNotExist";
        #endregion

        #region User
        public const string UserWithExistUserNameFound = "UserWithExistUserNameFound";
        public const string UserWithExistEmailFound = "UserWithExistEmailFound";
        public const string AddUserSuccessfully = "AddUserSuccessfully";
        public const string AddUserFailed = "AddUserFailed";
        public const string UserNotFound = "UserNotFound";
        public const string GetSingleUserByUserName = "GetSingleUserByUserName";
        public const string UserWithExistUserNameOrEmailFound = "UserWithExistUserNameOrEmailFound";
        public const string EditUserSuccessfully = "EditUserSuccessfully";
        public const string EditUserFailed = "EditUserFailed";
        public const string DeletedUser = "DeletedUser";
        public const string DeleteUserFailed = "DeleteUserFailed";
        public const string PasswordChangedSuccessfully = "PasswordChangedSuccessfully";
        public const string NewPassNotEqualConfirmPass = "NewPassNotEqualConfirmPass";
        public const string OldPasswordIncorrect = "OldPasswordIncorrect";
        public const string PasswordNotCorrect = "PasswordNotCorrect";
        #endregion

        #region Authenticaion
        public const string SuccessSignIn = "SuccessSignIn";
        public const string RefreshTokenIsExpired = "RefreshTokenIsExpired";
        public const string RefreshTokenIsNotFound = "RefreshTokenIsNotFound";
        public const string TokenIsNotExpired = "TokenIsNotExpired";
        public const string AlgorithmIsWrong = "AlgorithmIsWrong";
        public const string TokenIsExpired = "TokenIsExpired";
        #endregion
    }
}
