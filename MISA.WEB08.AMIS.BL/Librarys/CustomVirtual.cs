using MISA.WEB08.AMIS.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class CustomVirtual<T>
    {
        #region Method

        /// <summary>
        /// Hàm xử lý custom validate những model riêng biệt
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual ServiceResponse CustomValidate(T record)
        {
            return new ServiceResponse
            {
                Success = true
            };
        }

        /// <summary>
        /// Hàm xử lý kiểm tra phát sinh
        /// </summary>
        /// <param name="record">Record cần custom validate</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual ServiceResponse CheckIncurred(Guid record)
        {
            return new ServiceResponse
            {
                Success = true
            };
        }

        /// <summary>
        /// Hàm xử lý lưu hình ảnh
        /// <param name="record">Record cần custom </param>
        /// </summary>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual void SaveImage(ref T record)
        {
        }

        /// <summary>
        /// Hàm xử lý lưu mã để tự sinh
        /// </summary>
        /// <param name="record">Bản ghi</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (5/10/2022)
        public virtual void SaveCode(T record)
        {
        }
        #endregion
    }
}
