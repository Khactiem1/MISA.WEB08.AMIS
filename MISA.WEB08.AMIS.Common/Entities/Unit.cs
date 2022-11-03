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
    public class Unit : BaseEntity
    {
        /// <summary>
        /// id đơn vị
        /// </summary>
        public Guid? UnitID { get; set; }

        /// <summary>
        /// mã đơn vị
        /// </summary>
        [Unique("Mã đơn vị <{0}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.")]
        [IsNotNullOrEmpty("Mã đơn vị không được để trống")]
        public string UnitCode { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        public string UnitName { get; set; }
    }
}