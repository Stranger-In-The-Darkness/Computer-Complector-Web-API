using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Exceptions;

namespace ComputerComplectorWebAPI.Models
{
    /// <summary>
    /// Base for adding component request
    /// </summary>
    public abstract class AddRequestBase
    {
        protected virtual bool Validate<T>(T data, string paramName)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int32:
                {
                    if (Convert.ToInt32(data) < 0)
                    {
                        throw new ValidationException("Negative values are not allowed", paramName);
                    }
                    return true;
                }
                case TypeCode.Double:
                {
                    if (Convert.ToDouble(data) < 0)
                    {
                        throw new ValidationException("Negative values are not allowed", paramName);
                    }
                    return true;
                }
                case TypeCode.String:
                {
                    if (data == null)
                    {
                        throw new ValidationException("String parameter validation failed", new ArgumentNullException("Null strings are not allowed"), paramName);
                    }
                    if (Convert.ToString(data).Length == 0 || Convert.ToString(data).Trim().Length == 0)
                    {
                        throw new ValidationException("Empty strings are not allowed", paramName);
                    }
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        protected virtual bool TryValidate<T>(T data, out string error)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int32:
                {
                    if (Convert.ToInt32(data) < 0)
                    {
                        error = "Negative values are not allowed";
                        return false;
                    }
                    error = null;
                    return true;
                }
                case TypeCode.Double:
                {
                    if (Convert.ToDouble(data) < 0)
                    {
                        error = "Negative values are not allowed";
                        return false;
                    }
                    error = null;
                    return true;
                }
                case TypeCode.String:
                {
                    if (data == null)
                    {
                        error = "Null strings are not allowed";
                        return false;
                    }
                    if (Convert.ToString(data).Length == 0 || Convert.ToString(data).Trim().Length == 0)
                    {
                        error = "Empty strings are not allowed";
                        return false;
                    }
                    error = null;
                    return true;
                }
                default:
                {
                    error = "Not supported type";
                    return false;
                }
            }
        }
    }
}
