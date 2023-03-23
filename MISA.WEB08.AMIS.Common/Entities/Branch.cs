using MISA.WEB08.AMIS.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Đơn vị ứng với bảng unit trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Branch : BaseEntity
    {
        /// <summary>
        /// id đơn vị
        /// </summary>
        [Validate(PrimaryKey = true)]
        public Guid? BranchID { get; set; }

        /// <summary>
        /// mã đơn vị
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Mã đơn vị không được để trống")]
        public string BranchCode { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        public string BranchName { get; set; }
    }
}