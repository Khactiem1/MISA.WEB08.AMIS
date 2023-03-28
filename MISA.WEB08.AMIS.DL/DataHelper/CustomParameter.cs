using Dapper;

namespace MISA.WEB08.AMIS.DL
{
    public class CustomParameter<T>
    {
        /// <summary>
        /// Hàm xử lý custom các tham số parameter truyền vào proc create ngoài những tham số mặc định
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="record"></param>
        /// create by: nguyễn khắc tiềm (21/10/2022)
        public virtual void CustomParameterForCreate(ref DynamicParameters? parameters, T record)
        {

        }

        /// <summary>
        /// Hàm xử lý custom các tham số parameter truyền vào proc update ngoài những tham số mặc định
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="record"></param>
        /// create by: nguyễn khắc tiềm (21/10/2022)
        public virtual void CustomParameterForUpdate(ref DynamicParameters? parameters, T record)
        {

        }
    }
}
