using System;

namespace UniSharp.Common
{
    public class Result
    {
        #region Properties

        public bool IsSuccess { get; }

        public string ErrorMessage { get; } = null;

        public string StackTrace { get; } = null;

        public string ErrorCode { get; set; } = null;

        #endregion

        #region Ctors

        protected Result(bool isSuccess, string errorCode = null, string errorMessage = null, string stackTrace = null)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
        }

        #endregion

        #region Public Methods

        public static Result Success => new(true);

        public static Result Fail(string errorCode, string errorMessage)
        {
            return new Result(false, errorCode, errorMessage);
        }

        public static implicit operator Result(Exception exception)
        {
            return new Result(false, "Exception", exception.Message, exception.StackTrace);
        }

        public override string ToString()
        {
            if (IsSuccess)
                return "Success";

            if (string.IsNullOrEmpty(StackTrace))
                return $"Failure -> ErrorCode:{ErrorCode}, ErrorMessage:{ErrorMessage}";

            return $"Failure -> ErrorCode:{ErrorCode}, ErrorMessage:{ErrorMessage}, StackTrace:{StackTrace}";
        }

        #endregion
    }

    public class Result<T> : Result
    {
        #region Properties

        public T Value { get; }

        #endregion

        #region Ctors

        private Result(T value) : base(true)
        {
            Value = value;
        }

        private Result(string errorCode = null, string errorMessage = null, string stackTrace = null) 
            : base(false, errorCode, errorMessage, stackTrace)
        {
            Value = default;
        }

        #endregion

        #region Public Methods

        public static new Result<T> Success(T value) 
        {
            return new Result<T>(value);
        }

        public static Result<T> Fail(string errorCode, string errorMessage, string stackTrace = null)
        {
            return new Result<T>(errorCode, errorMessage, stackTrace);
        }

        public static Result<T> Fail(Result result)
        {
            return new Result<T>(result.ErrorCode, result.ErrorMessage, result.StackTrace);
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator Result<T>(Exception exception)
        {
            return new Result<T>("Exception", exception.Message, exception.StackTrace);
        }

        public override string ToString()
        {
            if (IsSuccess)
                return $"Success: {Value}";

            return base.ToString();
        }

        #endregion
    }
}